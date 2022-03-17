using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.XML.GENERATOR
{
    public class PDFSign
    {
        String alias = "";

        public string[] GetAllPDFFile()
        {
            string[] result = new string[0];
            try
            {
                StringBuilder sb = new StringBuilder();
                string pathFolder = @"D:\";
                string fileType = "*.pdf";
                result = Directory.GetFiles(pathFolder, fileType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public void SignSignature()
        {
            string folderDest = @"D:/";
            string fileNameDest = "testFile_sign.pdf";
            string fileType = ".pdf";
            try
            {
                string[] pDFFile = GetAllPDFFile();
                if (pDFFile != null && pDFFile.Length > 0)
                {
                    ICollection<System.Security.Cryptography.X509Certificates.X509Certificate> chain = new List<System.Security.Cryptography.X509Certificates.X509Certificate>();
                    Pkcs12Store store = GetCertificate();
                    AsymmetricKeyEntry pk = store.GetKey(alias);



                    foreach (X509CertificateEntry c in store.GetCertificateChain(alias))
                        chain.Add(c.Certificate);


                    RsaPrivateCrtKeyParameters parameters = pk.Key as RsaPrivateCrtKeyParameters;

                    foreach (string src in pDFFile)
                    {
                        fileNameDest = Path.GetFileName(src).Replace(".pdf", "");
                        PdfReader reader = new PdfReader(src);
                        FileStream os = new FileStream(folderDest + fileNameDest + "_sign" + fileType, FileMode.Create);
                        PdfStamper stamper = PdfStamper.CreateSignature(reader, os, '\0');
                        //Add image appearance
                        SignPictureSignature(stamper);

                        // Creating the appearance
                        PdfSignatureAppearance appearance = stamper.SignatureAppearance;
                        //appearance.Reason = "My reason for signing";
                        //appearance.Location = "The middle of nowhere";
                        //appearance.SetVisibleSignature(new Rectangle(36, 748, 144, 780), 1, "sig");

                        // Creating the signature
                        IExternalSignature pks = new PrivateKeySignature(parameters, DigestAlgorithms.SHA512);
                        MakeSignature.SignDetached(appearance, pks, (ICollection<Org.BouncyCastle.X509.X509Certificate>)chain, null, null, null, 0, CryptoStandard.CMS);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Pkcs12Store GetCertificate()
        {
            Pkcs12Store store;
            try
            {
                var KEYSTORE = @"D:/wild card.SCG.CO.TH.pfx";
                char[] PASSWORD = ("abcDEF99").ToCharArray();
                store = new Pkcs12Store(new FileStream(KEYSTORE, FileMode.Open), PASSWORD);

                // searching for private key
                foreach (string al in store.Aliases)
                    if (store.IsKeyEntry(al) && store.GetKey(al).Key.IsPrivate)
                    {
                        alias = al;
                        break;
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return store;
        }

        public void SignPictureSignature(PdfStamper pdf)
        {
            try
            {
                string imageURL = @"D:/signature.png";
                PdfContentByte pdfContentByte = pdf.GetOverContent(1);
                Image image = Image.GetInstance(iTextSharp.text.Image.GetInstance(imageURL));
                image.SetAbsolutePosition(100, 100);
                pdfContentByte.AddImage(image);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
