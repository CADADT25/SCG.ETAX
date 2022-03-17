using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PDFSign.Class
{
    public class XMLSign
    {
        public bool XMLSignSignature()
        {
            //Sha 512
            bool result = true;
            try
            {
                // Create a new CspParameters object to specify
                // a key container.
                CspParameters cspParams = new CspParameters();
                var XML_DSIG_RSA_KEY = "test";
                cspParams.KeyContainerName = XML_DSIG_RSA_KEY;

                // Create a new RSA signing key and save it in the container.
                RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider(cspParams);

                // Create a new XML document.
                XmlDocument xmlDoc = new XmlDocument();

                // Load an XML file into the XmlDocument object.
                xmlDoc.PreserveWhitespace = true;
                xmlDoc.Load(@"D:/testXML.xml");

                // Sign the XML document.
                SignXml(xmlDoc, rsaKey);

                //Console.WriteLine("XML file signed.");

                // Save the document.
                xmlDoc.Save(@"D:/test_sign.xml");
            }
            catch (Exception e)
            {
                throw e;
            }
            return result; 
        }

        public static void SignXml(XmlDocument xmlDoc, RSA rsaKey)
        {
            // Check arguments.
            if (xmlDoc == null)
                throw new ArgumentException(nameof(xmlDoc));
            if (rsaKey == null)
                throw new ArgumentException(nameof(rsaKey));

            // Create a SignedXml object.
            SignedXml signedXml = new SignedXml(xmlDoc);

            // Add the key to the SignedXml document.
            signedXml.SigningKey = rsaKey;

            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "";

            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            // Append the element to the XML document.
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
        }

        public void XMLSign2()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"D:/testXML.xml");
            GetSignedXMLDocument(xmlDoc);


            //To verify
            // Display the results of the signature verification to 
            // the console.
            var filename = @"D:/signedxml.xml";
            //bool result = VerifyXmlFile(filename);
            //if (result)
            //{
            //    Console.WriteLine("The XML signature is valid.");
            //}
            //else
            //{
            //    Console.WriteLine("The XML signature is not valid.");
            //}

            Console.ReadLine();
        }
        public static XmlDocument GetSignedXMLDocument(XmlDocument xmlDoc)
        {
            try
            {
                X509Certificate2 myCert = null;
                //var store = new X509Store(StoreLocation.CurrentUser); //StoreLocation.LocalMachine fails too
                //store.Open(OpenFlags.ReadOnly);
                //var certificates = store.Certificates;
                //foreach (var certificate in certificates)
                //{
                //    if (certificate.Subject.ToLower().Contains("vikash"))
                //    {
                //        myCert = certificate;
                //        break;
                //    }
                //}
                string certificateFile = @"D:/wild card.SCG.CO.TH.pfx";
                string password = "abcDEF99";
                myCert = new X509Certificate2(certificateFile, password, X509KeyStorageFlags.UserKeySet);

                if (myCert != null)
                {
                    Console.Write("Certificate Subject:" + myCert.Subject);

                    // Sign the XML document. 
                    string signedXmldata = SignXmlWithCertificate(xmlDoc, myCert);
                    Console.Write(signedXmldata);

                    //write signed xml in to file
                    System.IO.File.WriteAllText("D:\\signedxml.xml", signedXmldata);

                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return xmlDoc;
        }
        public static string SignXmlWithCertificate(XmlDocument Document, X509Certificate2 cert)
        {
            SignedXml signedXml = new SignedXml(Document);
            signedXml.SigningKey = cert.PrivateKey;

            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "";

            // Add an enveloped transformation to the reference.            
            XmlDsigEnvelopedSignatureTransform env =
               new XmlDsigEnvelopedSignatureTransform(true);
            reference.AddTransform(env);

            //canonicalize
            XmlDsigC14NTransform c14t = new XmlDsigC14NTransform();
            reference.AddTransform(c14t);

            KeyInfo keyInfo = new KeyInfo();
            KeyInfoX509Data keyInfoData = new KeyInfoX509Data(cert);
            KeyInfoName kin = new KeyInfoName();
            kin.Value = "Public key of certificate";
            RSACryptoServiceProvider rsaprovider = (RSACryptoServiceProvider)cert.PublicKey.Key;
            RSAKeyValue rkv = new RSAKeyValue(rsaprovider);
            keyInfo.AddClause(kin);
            keyInfo.AddClause(rkv);
            keyInfo.AddClause(keyInfoData);
            signedXml.KeyInfo = keyInfo;

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save 
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            Document.DocumentElement.AppendChild(
                Document.ImportNode(xmlDigitalSignature, true));

            return Document.OuterXml;
        }
        // Verify the signature of an XML file and return the result.
        public static Boolean VerifyXmlFile(String Name)
        {
            try
            {
                // Check the arguments.  
                if (Name == null)
                    throw new ArgumentNullException("Name");

                // Create a new XML document.
                XmlDocument xmlDocument = new XmlDocument();

                // Format using white spaces.
                xmlDocument.PreserveWhitespace = true;

                // Load the passed XML file into the document. 
                xmlDocument.Load(Name);

                // Create a new SignedXml object and pass it
                // the XML document class.
                SignedXml signedXml = new SignedXml(xmlDocument);

                // Find the "Signature" node and create a new
                // XmlNodeList object.
                XmlNodeList nodeList = xmlDocument.GetElementsByTagName("Signature");

                // Load the signature node.
                signedXml.LoadXml((XmlElement)nodeList[0]);

                // Check the signature and return the result.
                return signedXml.CheckSignature();
            }
            catch (Exception exc)
            {
                Console.Write("Error:" + exc);
                return false;
            }
        }

    }
}
