using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.HighLevelAPI.MechanismParams;
using Net.Pkcs11Interop.HighLevelAPI41;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Drawing;
using System.IO;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Crypto.Parameters;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Security;
using System.Security.Cryptography.Pkcs;
using System.Security.AccessControl;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Microsoft.Win32.SafeHandles;
using Org.BouncyCastle.X509;
using Net.Pkcs11Interop.X509Store;

namespace PDFSign.Class
{
    public class ConnectHSM : IPinProvider
    {
        //static string pkcs11LibraryPath = @"C:\Program Files\SafeNet\Protect Toolkit 5\Protect Toolkit C SDK\bin\hsm\cryptoki.dll";
        static string pkcs11LibraryPath = @"C:\Program Files\SafeNet\Protect Toolkit 5\Protect Toolkit C SDK\bin\hsm\cryptoki.dll";
        static string pinCode = "P@ssw0rd1";
        String alias = "NEW06391012205001173_200916150834";
        public byte[] _pin = null;

        public IPinProvider pinProvider;
        public void SimplePinProvider(string pin)
        {
            _pin = ConvertUtils.Utf8StringToBytes(pin);
        }

        public GetPinResult GetKeyPin(Pkcs11X509StoreInfo storeInfo, Pkcs11SlotInfo slotInfo, Pkcs11TokenInfo tokenInfo, Pkcs11X509CertificateInfo certificateInfo)
        {
            return new GetPinResult(false, false, _pin);
        }

        public GetPinResult GetTokenPin(Pkcs11X509StoreInfo storeInfo, Pkcs11SlotInfo slotInfo, Pkcs11TokenInfo tokenInfo)
        {
            return new GetPinResult(false, false, _pin);
        }


        public void ConnectHSMFile()
        {
            try
            {
                //var certStore = new X509Store(StoreLocation.CurrentUser);
                //certStore.Open(OpenFlags.ReadOnly);
                //var certCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbPrint, true);
                //var myCert = certCollection[0];

                // I can get the correct certificate but the following line throws "Keyset does not exist" error
                //var SigningKey = myCert.PrivateKey;

                string slotName = "eTax-TEST"; //we are using the user slot
                //string pin = string.Empty; //the pin that we plan to use to login // P@ssw0rd1 //

                if (Net.Pkcs11Interop.Common.Platform.Uses32BitRuntime)
                {
                    pkcs11LibraryPath = @"C:\Program Files (x86)\SafeNet\Protect Toolkit 5\Protect Toolkit C SDK\bin\hsm\cryptoki.dll";
                }

                byte[] iv = new byte[16];
                //Environment.SetEnvironmentVariable("PKCS11_LOGGER_ORIG_LIB", "/home/honnguyen/libOpenPGP11_64.so");

                Pkcs11InteropFactories factories = new Pkcs11InteropFactories();
                using (IPkcs11Library pkcs11Library = factories.Pkcs11LibraryFactory.LoadPkcs11Library(factories, pkcs11LibraryPath, AppType.SingleThreaded))
                {
                    var slot = pkcs11Library.GetSlotList(SlotsType.WithOrWithoutTokenPresent);
                    var info = pkcs11Library.GetInfo();
                    ISlot s = slot[1];
                    using (var session = s.OpenSession(SessionType.ReadWrite))
                    {
                        iv = session.GenerateRandom(8);
                        session.Login(CKU.CKU_USER, pinCode);
                        var ss = session.GetSessionInfo();
                        var sss = session.GetOperationState();

                        List<IObjectAttribute> publicKeyAttributes = new List<IObjectAttribute>();
                        //publicKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_KEY_TYPE, CKK.CKK_DES3));
                        //publicKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_ENCRYPT, true));
                        //publicKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_DECRYPT, true));
                        //publicKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_UNWRAP, true));
                        //publicKeyAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE));

                        IMechanism mechanism = session.Factories.MechanismFactory.Create(CKM.CKM_RSA_AES_KEY_WRAP);
                        IObjectHandle privateKey = null;
                        //IObjectHandle generatedKey = session.GenerateKey(mechanism, publicKeyAttributes);

                        var foundPublicKeys = session.FindAllObjects(publicKeyAttributes);
                        List<CKA> ckas = CreateListCKA();
                        //List<CKA> ckas = new List<CKA>();
                        //ckas.Add(CKA.CKA_VALUE);
                        List<HSMDataAttributesClass> dataAttributeslist = new List<HSMDataAttributesClass>();
                        List<IObjectAttribute> objectAttribute = new List<IObjectAttribute>();
                        foreach (var foundObject in foundPublicKeys)
                        {
                            List<IObjectAttribute> objectAttributes = session.GetAttributeValue(foundObject, ckas);
                            //foreach(var item in objectAttributes)
                            //{
                            //    certificatetemp = new X509Certificate2(certAttributes[0].GetValueAsByteArray());
                            //}
                            //var test = objectAttributes[20].GetValueAsByteArray();
                            //var decodetest = session.UnwrapKey(mechanism, privateKey, test, objectAttribute);
                            //var dataReturned = Encoding.Default.GetString(decodetest);

                            dataAttributeslist.Add(GetHSMDataAttributes(objectAttributes));
                        }
                        var json = JsonConvert.SerializeObject(dataAttributeslist);
                        session.Logout();

                    }

                    //using (ISession session = s.OpenSession(SessionType.ReadWrite))
                    //{
                    //    // Login as normal user
                    //    session.Login(CKU.CKU_USER, pin);

                    //    // List certificates in smart card
                    //    List<IObjectAttribute> certificateTemplate = new List<IObjectAttribute>();
                    //    certificateTemplate.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE));
                    //    //certificateTemplate.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_TOKEN, true));
                    //    //certificateTemplate.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CERTIFICATE_CATEGORY, CKO.CKO_CERTIFICATE));
                    //    List<IObjectHandle> foundCertificatesResult = session.FindAllObjects(certificateTemplate);

                    //    // Attribute template of certificate
                    //    List<CKA> attributeTemplate = new List<CKA>();
                    //    attributeTemplate.Add(CKA.CKA_VALUE);

                    //    // Find the certificate to use (GE or NR)
                    //    int certIndex = -1; // Index of certificate to use

                    //    for (int i = 0; i < foundCertificatesResult.Count; i++)
                    //    {
                    //        // Get key usage of certificate
                    //        List<IObjectAttribute> certAttributes = session.GetAttributeValue(foundCertificatesResult[i], attributeTemplate);
                    //        X509Certificate2 x509Cert2 = new X509Certificate2(certAttributes[0].GetValueAsByteArray());
                    //        //var der_bytes = certAttributes[0].GetValueAsByteArray();
                    //        //var cert = X509Certificate2.Certificate.load(der_bytes);
                    //        var format = x509Cert2.GetFormat();
                    //        var effdate = x509Cert2.GetEffectiveDateString();
                    //        var pubkey = x509Cert2.GetPublicKeyString();
                    //        var pubkeybyte = x509Cert2.GetPublicKey();
                    //        var privatekey = x509Cert2.GetRSAPrivateKey();
                    //        var cerbyte = x509Cert2.GetRawCertData();
                    //        var cer = x509Cert2.GetRawCertDataString();
                    //        var ser = x509Cert2.GetSerialNumberString();
                    //        var serbyte = x509Cert2.GetSerialNumber();
                    //        foreach (X509Extension x509Ext in x509Cert2.Extensions)
                    //        {
                    //            if ("Key Usage" == x509Ext.Oid.FriendlyName)
                    //            {
                    //                X509KeyUsageExtension usageExt = (X509KeyUsageExtension)x509Ext;

                    //                if (usageExt.KeyUsages == (X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.NonRepudiation | X509KeyUsageFlags.DigitalSignature)) // GE cert
                    //                                                                                                                                                       //if (usageExt.KeyUsages == (X509KeyUsageFlags.NonRepudiation)) // NR cert
                    //                {
                    //                    Console.WriteLine("Found certificate at index: " + i);

                    //                    // Update index
                    //                    if (-1 == certIndex)
                    //                    {
                    //                        certIndex = i;
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }

                    //    // Check if a cert was found
                    //    if (-1 != certIndex)
                    //    {
                    //        List<ObjectHandle> foundCerts = foundCertificatesResult.ConvertAll(x => (ObjectHandle)x);
                    //        ObjectHandle geCert = foundCerts[certIndex];
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine("No valid certificate found in smart card.");
                    //    }
                    //    session.Logout();
                    //}
                }
                //Pkcs11InteropFactories factories = new Pkcs11InteropFactories();
                //using (IPkcs11Library pkcs11Library = factories.Pkcs11LibraryFactory.LoadPkcs11Library(factories, pkcs11LibraryPath, AppType.SingleThreaded))
                //{
                //    var slot = pkcs11Library.GetSlotList(SlotsType.WithOrWithoutTokenPresent);

                //    foreach (var s in slot)
                //    {
                //        using (var session = s.OpenSession(SessionType.ReadWrite))
                //        {
                //            // Login as normal user
                //            session.Login(CKU.CKU_USER, pin);
                //            // Prepare attribute template of new key
                //            List<IObjectAttribute> objectAttributes = new List<IObjectAttribute>();
                //            objectAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_SECRET_KEY));
                //            objectAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_KEY_TYPE, CKK.CKK_DES3));
                //            objectAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_ENCRYPT, true));
                //            objectAttributes.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_DECRYPT, true));
                //            // Specify key generation mechanism
                //            IMechanism mechanism = session.Factories.MechanismFactory.Create(CKM.CKM_DES3_KEY_GEN);
                //            // Generate key
                //            IObjectHandle objectHandle = session.GenerateKey(mechanism, objectAttributes);
                //            // Do something interesting with generated key
                //            // Destroy object
                //            session.DestroyObject(objectHandle);

                //            session.Logout();
                //        }
                //    }
                // Open RW session


                //}

                //using (Pkcs11 pkcs11 = new Pkcs11(pkcs11LibraryPath, AppType.MultiThreaded))
                //{
                //    var getslot = pkcs11.GetSlotList(SlotsType.WithTokenPresent);
                //    Slot s = getslot[1];
                //    var i = s.GetSlotInfo();
                //    var ii = i.SlotDescription;
                //    var iii = i.ManufacturerId;

                //    var t = s.GetTokenInfo();
                //    var tt = t.Label;
                //    var ttt = t.ManufacturerId;
                //    var tttt = t.Model;
                //    //test(s);
                //    using (var session = s.OpenSession(SessionType.ReadOnly))
                //    {
                //        if (!string.IsNullOrEmpty(pin))
                //            session.Login(CKU.CKU_USER, pin);
                //        var ss = session.GetSessionInfo();
                //        var sss = session.GetOperationState();

                //        //var objects = FindKey(session, "NEW06391012205001173_200916150834");
                //        List<ObjectAttribute> publicKeyAttributes = new List<ObjectAttribute>();
                //        //publicKeyAttributes.Add(new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE));
                //        publicKeyAttributes.Add(new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_SECRET_KEY));
                //        //objectAttributes.Add(new ObjectAttribute(CKA.CKA_LABEL, "Generated key"));
                //        //publicKeyAttributes.Add(new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_SECRET_KEY));
                //        //publicKeyAttributes.Add(new ObjectAttribute(CKA.CKA_KEY_TYPE, CKK.CKK_DES3));
                //        //publicKeyAttributes.Add(new ObjectAttribute(CKA.CKA_TOKEN, true));
                //        //publicKeyAttributes.Add(new ObjectAttribute(CKA.CKA_ENCRYPT, true));
                //        publicKeyAttributes.Add(new ObjectAttribute(CKA.CKA_DECRYPT, true));
                //        //publicKeyAttributes.Add(new ObjectAttribute(CKA.CKA_EXTRACTABLE, true));
                //        //publicKeyAttributes.Add(new ObjectAttribute(CKA.CKA_CERTIFICATE_TYPE, CKC.CKC_X_509));
                //        List<ObjectHandle> foundPublicKeys = session.FindAllObjects(publicKeyAttributes);
                //        List<CKA> ckas = CreateListCKA();

                //        session.FindObjectsInit(publicKeyAttributes);
                //        List<ObjectHandle> foundObjects = session.FindObjects(1);
                //        //ObjectHandle objectHandle = session.FindObjects(1);
                //        List<HSMDataAttributesClass> dataAttributeslist = new List<HSMDataAttributesClass>();
                //        ObjectHandle publicKey = null;
                //        ObjectHandle privateKey = null;
                //        var mechanism = new Mechanism(CKM.CKM_AES_CBC_PAD, iv);

                //        //session.GenerateKeyPair(mechanism,publicKeyAttributes, publicKeyAttributes,out publicKey, out privateKey);
                //        foreach (var foundObject in foundPublicKeys)
                //        {
                //            List<ObjectAttribute> objectAttributes = session.GetAttributeValue(foundObject, ckas);

                //            var test = objectAttributes[5].GetValueAsByteArray();
                //            //var test1 = Encoding.Default.GetString(objectAttributes[9].GetValueAsByteArray());
                //            //var test2 = Encoding.ASCII.GetString(objectAttributes[9].GetValueAsByteArray());
                //            //var test3 = Encoding.BigEndianUnicode.GetString(objectAttributes[9].GetValueAsByteArray());
                //            //var test4 = Encoding.Unicode.GetString(objectAttributes[9].GetValueAsByteArray());
                //            //var test5 = Encoding.UTF32.GetString(objectAttributes[9].GetValueAsByteArray());
                //            //var test6 = Encoding.UTF7.GetString(objectAttributes[9].GetValueAsByteArray());
                //            //var test7 = Encoding.UTF8.GetString(objectAttributes[9].GetValueAsByteArray());
                //            //var test8 = Convert.ToBase64String(objectAttributes[9].GetValueAsByteArray());

                //            //var testt = objectAttributes[22].GetValueAsByteArray();
                //            //var testt1 = Encoding.Default.GetString(objectAttributes[22].GetValueAsByteArray());
                //            //var testt2 = Encoding.ASCII.GetString(objectAttributes[22].GetValueAsByteArray());
                //            //var testt3 = Encoding.BigEndianUnicode.GetString(objectAttributes[22].GetValueAsByteArray());
                //            //var testt4 = Encoding.Unicode.GetString(objectAttributes[22].GetValueAsByteArray());
                //            //var testt5 = Encoding.UTF32.GetString(objectAttributes[22].GetValueAsByteArray());
                //            //var testt6 = Encoding.UTF7.GetString(objectAttributes[22].GetValueAsByteArray());
                //            //var testt7 = Encoding.UTF8.GetString(objectAttributes[22].GetValueAsByteArray());
                //            //var testt8 = Convert.ToBase64String(objectAttributes[22].GetValueAsByteArray());

                //            var decodetest = session.Decrypt(mechanism, foundObject, test);
                //            //var decodetestt = session.Decrypt(mechanism, foundObject, testt);
                //            var dataReturned = Encoding.UTF8.GetString(decodetest);
                //            //var dataReturnedt = Encoding.UTF8.GetString(decodetestt);
                //            dataAttributeslist.Add(GetHSMDataAttributes(objectAttributes));
                //        }
                //        //foreach (var item in objectAttributes)
                //        //{
                //        //    string data = string.Empty;
                //        //    var type = item.GetType();
                //        //    data = item.GetValueAsString();
                //        //    listdata.Add(data);
                //        //}
                //        //objects = FindKey(session, "5c6e65dc1b7b9da8");
                //        //objects = FindKey(session, "0105542000095");
                //        //objects = FindKey(session, "571271\\:28593");
                //        //if (objects != null)
                //        //{
                //        //for (int num = 1; num <= objects.Count; num++)
                //        //{
                //        //    //var obj = session.FindObjects(num);
                //        //    List<ObjectHandle> foundObjects = session.FindObjects(1);
                //        //}
                //        //foreach (var o in objects)
                //        //{
                //        //    var mechanism = new Mechanism(CKM.CKM_AES_CBC_PAD, iv);

                //        //    var data = Encoding.UTF8.GetBytes("Hello World");

                //        //    byte[] encryptedData = session.Encrypt(mechanism, o, data);

                //        //    byte[] decryptedData = session.Decrypt(mechanism, o, encryptedData);

                //        //    var dataReturned = Encoding.UTF8.GetString(decryptedData); //we should get back hello world here
                //        //}
                //        //}
                //        //Mechanism mechanism = session.
                //        if (!string.IsNullOrEmpty(pin))
                //            session.Logout();
                //    }
                //}

                //using (Pkcs11 pkcs11 = new Pkcs11(pkcs11LibraryPath, AppType.SingleThreaded))
                //{
                //    //find the slot
                //    var getslot = pkcs11.GetSlotList(SlotsType.WithTokenPresent);
                //    var slot = getslot.FirstOrDefault(s => s.GetTokenInfo().Label == slotName);
                //    using (var session = slot.OpenSession(SessionType.ReadOnly))
                //    {
                //        if (!string.IsNullOrEmpty(pin))
                //            session.Login(CKU.CKU_USER, pin);

                //        //find our demo key
                //        var generatedKey = FindKey(session, "TestKey");
                //        if (generatedKey != null)
                //        {
                //            var mechanism = new Mechanism(CKM.CKM_AES_CBC_PAD, iv);

                //            var data = Encoding.UTF8.GetBytes("Hello World");

                //            byte[] encryptedData = session.Encrypt(mechanism, generatedKey, data);

                //            byte[] decryptedData = session.Decrypt(mechanism, generatedKey, encryptedData);

                //            if (!string.IsNullOrEmpty(pin))
                //                session.Logout();

                //            var dataReturned = Encoding.UTF8.GetString(decryptedData); //we should get back hello world here

                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //private List<ObjectHandle> FindKey(Session session, string keyName)
        //{
        //    List<ObjectAttribute> objectAttributes = new List<ObjectAttribute>
        //    {
        //        //new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_SECRET_KEY),
        //        //,new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_PRIVATE_KEY)
        //        //,new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_PUBLIC_KEY)
        //        //,new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_MECHANISM)
        //        new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE)
        //        //,new ObjectAttribute(CKA.CKA_KEY_TYPE, CKK.CKK_RSA)
        //        //,new ObjectAttribute(CKA.CKA_LABEL, keyName)
        //    };
        //    session.FindObjectsInit(objectAttributes);
        //    //List<ObjectHandle> foundObjects = session.FindObjects(1);
        //    List<ObjectHandle> foundObjects = session.FindAllObjects(objectAttributes);
        //    //session.FindObjectsFinal();
        //    //return foundObjects.FirstOrDefault();
        //    return foundObjects;
        //}

        public List<CKA> CreateListCKA()
        {
            List<CKA> ckas = new List<CKA>();
            ckas.Add(CKA.CKA_CLASS);
            ckas.Add(CKA.CKA_TOKEN);
            ckas.Add(CKA.CKA_PRIVATE);
            ckas.Add(CKA.CKA_LABEL);
            ckas.Add(CKA.CKA_APPLICATION);
            ckas.Add(CKA.CKA_VALUE);
            ckas.Add(CKA.CKA_OBJECT_ID);
            ckas.Add(CKA.CKA_CERTIFICATE_TYPE);
            ckas.Add(CKA.CKA_ISSUER);
            ckas.Add(CKA.CKA_SERIAL_NUMBER);
            ckas.Add(CKA.CKA_AC_ISSUER);
            ckas.Add(CKA.CKA_OWNER);
            ckas.Add(CKA.CKA_ATTR_TYPES);
            ckas.Add(CKA.CKA_TRUSTED);
            ckas.Add(CKA.CKA_CERTIFICATE_CATEGORY);
            ckas.Add(CKA.CKA_JAVA_MIDP_SECURITY_DOMAIN);
            ckas.Add(CKA.CKA_URL);
            ckas.Add(CKA.CKA_HASH_OF_SUBJECT_PUBLIC_KEY);
            ckas.Add(CKA.CKA_HASH_OF_ISSUER_PUBLIC_KEY);
            ckas.Add(CKA.CKA_CHECK_VALUE);
            ckas.Add(CKA.CKA_KEY_TYPE);
            ckas.Add(CKA.CKA_SUBJECT);
            ckas.Add(CKA.CKA_ID);
            ckas.Add(CKA.CKA_SENSITIVE);
            ckas.Add(CKA.CKA_ENCRYPT);
            ckas.Add(CKA.CKA_DECRYPT);
            ckas.Add(CKA.CKA_WRAP);
            ckas.Add(CKA.CKA_UNWRAP);
            ckas.Add(CKA.CKA_SIGN);
            ckas.Add(CKA.CKA_SIGN_RECOVER);
            ckas.Add(CKA.CKA_VERIFY);
            ckas.Add(CKA.CKA_VERIFY_RECOVER);
            ckas.Add(CKA.CKA_DERIVE);
            ckas.Add(CKA.CKA_START_DATE);
            ckas.Add(CKA.CKA_END_DATE);
            ckas.Add(CKA.CKA_MODULUS);
            ckas.Add(CKA.CKA_MODULUS_BITS);
            ckas.Add(CKA.CKA_PUBLIC_EXPONENT);
            ckas.Add(CKA.CKA_PRIVATE_EXPONENT);
            ckas.Add(CKA.CKA_PRIME_1);
            ckas.Add(CKA.CKA_PRIME_2);
            ckas.Add(CKA.CKA_EXPONENT_1);
            ckas.Add(CKA.CKA_EXPONENT_2);
            ckas.Add(CKA.CKA_COEFFICIENT);
            ckas.Add(CKA.CKA_PUBLIC_KEY_INFO);
            ckas.Add(CKA.CKA_PRIME);
            ckas.Add(CKA.CKA_SUBPRIME);
            ckas.Add(CKA.CKA_BASE);
            ckas.Add(CKA.CKA_PRIME_BITS);
            ckas.Add(CKA.CKA_SUBPRIME_BITS);
            ckas.Add(CKA.CKA_VALUE_BITS);
            ckas.Add(CKA.CKA_VALUE_LEN);
            ckas.Add(CKA.CKA_EXTRACTABLE);
            ckas.Add(CKA.CKA_LOCAL);
            ckas.Add(CKA.CKA_NEVER_EXTRACTABLE);
            ckas.Add(CKA.CKA_ALWAYS_SENSITIVE);
            ckas.Add(CKA.CKA_KEY_GEN_MECHANISM);
            ckas.Add(CKA.CKA_MODIFIABLE);
            ckas.Add(CKA.CKA_COPYABLE);
            ckas.Add(CKA.CKA_DESTROYABLE);
            ckas.Add(CKA.CKA_ECDSA_PARAMS);
            ckas.Add(CKA.CKA_EC_PARAMS);
            ckas.Add(CKA.CKA_EC_POINT);
            ckas.Add(CKA.CKA_SECONDARY_AUTH);
            ckas.Add(CKA.CKA_AUTH_PIN_FLAGS);
            ckas.Add(CKA.CKA_ALWAYS_AUTHENTICATE);
            ckas.Add(CKA.CKA_WRAP_WITH_TRUSTED);
            ckas.Add(CKA.CKA_WRAP_TEMPLATE);
            ckas.Add(CKA.CKA_UNWRAP_TEMPLATE);
            ckas.Add(CKA.CKA_DERIVE_TEMPLATE);
            ckas.Add(CKA.CKA_OTP_FORMAT);
            ckas.Add(CKA.CKA_OTP_LENGTH);
            ckas.Add(CKA.CKA_OTP_TIME_INTERVAL);
            ckas.Add(CKA.CKA_OTP_USER_FRIENDLY_MODE);
            ckas.Add(CKA.CKA_OTP_CHALLENGE_REQUIREMENT);
            ckas.Add(CKA.CKA_OTP_TIME_REQUIREMENT);
            ckas.Add(CKA.CKA_OTP_COUNTER_REQUIREMENT);
            ckas.Add(CKA.CKA_OTP_PIN_REQUIREMENT);
            ckas.Add(CKA.CKA_OTP_COUNTER);
            ckas.Add(CKA.CKA_OTP_TIME);
            ckas.Add(CKA.CKA_OTP_USER_IDENTIFIER);
            ckas.Add(CKA.CKA_OTP_SERVICE_IDENTIFIER);
            ckas.Add(CKA.CKA_OTP_SERVICE_LOGO);
            ckas.Add(CKA.CKA_OTP_SERVICE_LOGO_TYPE);
            ckas.Add(CKA.CKA_GOSTR3410_PARAMS);
            ckas.Add(CKA.CKA_GOSTR3411_PARAMS);
            ckas.Add(CKA.CKA_GOST28147_PARAMS);
            ckas.Add(CKA.CKA_HW_FEATURE_TYPE);
            ckas.Add(CKA.CKA_RESET_ON_INIT);
            ckas.Add(CKA.CKA_HAS_RESET);
            ckas.Add(CKA.CKA_PIXEL_X);
            ckas.Add(CKA.CKA_PIXEL_Y);
            ckas.Add(CKA.CKA_RESOLUTION);
            ckas.Add(CKA.CKA_CHAR_ROWS);
            ckas.Add(CKA.CKA_CHAR_COLUMNS);
            ckas.Add(CKA.CKA_COLOR);
            ckas.Add(CKA.CKA_BITS_PER_PIXEL);
            ckas.Add(CKA.CKA_CHAR_SETS);
            ckas.Add(CKA.CKA_ENCODING_METHODS);
            ckas.Add(CKA.CKA_MIME_TYPES);
            ckas.Add(CKA.CKA_MECHANISM_TYPE);
            ckas.Add(CKA.CKA_REQUIRED_CMS_ATTRIBUTES);
            ckas.Add(CKA.CKA_DEFAULT_CMS_ATTRIBUTES);
            ckas.Add(CKA.CKA_SUPPORTED_CMS_ATTRIBUTES);
            ckas.Add(CKA.CKA_ALLOWED_MECHANISMS);
            ckas.Add(CKA.CKA_VENDOR_DEFINED);

            return ckas;
        }

        public HSMDataAttributesClass GetHSMDataAttributes(List<IObjectAttribute> objectAttributes)
        {
            HSMDataAttributesClass data = new HSMDataAttributesClass();

            try
            {
                data.CKA_CLASS = objectAttributes[0].GetValueAsString();
                data.CKA_TOKEN = objectAttributes[1].GetValueAsString();
                data.CKA_PRIVATE = objectAttributes[2].GetValueAsString();
                data.CKA_LABEL = objectAttributes[3].GetValueAsString();
                data.CKA_APPLICATION = objectAttributes[4].GetValueAsString();
                data.CKA_VALUE = objectAttributes[5].GetValueAsString();
                data.CKA_OBJECT_ID = objectAttributes[6].GetValueAsString();
                data.CKA_CERTIFICATE_TYPE = objectAttributes[7].GetValueAsString();
                data.CKA_ISSUER = objectAttributes[8].GetValueAsString();
                //data.CKA_SERIAL_NUMBER = Convert.ToBase64String((objectAttributes[9].GetValueAsByteArray()));
                data.CKA_SERIAL_NUMBER = objectAttributes[9].GetValueAsString();
                data.CKA_AC_ISSUER = objectAttributes[10].GetValueAsString();
                data.CKA_OWNER = objectAttributes[11].GetValueAsString();
                data.CKA_ATTR_TYPES = objectAttributes[12].GetValueAsString();
                data.CKA_TRUSTED = objectAttributes[13].GetValueAsString();
                data.CKA_CERTIFICATE_CATEGORY = objectAttributes[14].GetValueAsString();
                data.CKA_JAVA_MIDP_SECURITY_DOMAIN = objectAttributes[15].GetValueAsString();
                data.CKA_URL = objectAttributes[16].GetValueAsString();
                data.CKA_HASH_OF_SUBJECT_PUBLIC_KEY = objectAttributes[17].GetValueAsString();
                data.CKA_HASH_OF_ISSUER_PUBLIC_KEY = objectAttributes[18].GetValueAsString();
                data.CKA_CHECK_VALUE = objectAttributes[19].GetValueAsString();
                data.CKA_KEY_TYPE = objectAttributes[20].GetValueAsString();
                data.CKA_SUBJECT = objectAttributes[21].GetValueAsString();
                //data.CKA_ID = Convert.ToBase64String((objectAttributes[22].GetValueAsByteArray()));
                data.CKA_ID = objectAttributes[22].GetValueAsString();
                data.CKA_SENSITIVE = objectAttributes[23].GetValueAsString();
                data.CKA_ENCRYPT = objectAttributes[24].GetValueAsString();
                data.CKA_DECRYPT = objectAttributes[25].GetValueAsString();
                data.CKA_WRAP = objectAttributes[26].GetValueAsString();
                data.CKA_UNWRAP = objectAttributes[27].GetValueAsString();
                data.CKA_SIGN = objectAttributes[28].GetValueAsString();
                data.CKA_SIGN_RECOVER = objectAttributes[29].GetValueAsString();
                data.CKA_VERIFY = objectAttributes[30].GetValueAsString();
                data.CKA_VERIFY_RECOVER = objectAttributes[31].GetValueAsString();
                data.CKA_DERIVE = objectAttributes[32].GetValueAsString();
                data.CKA_START_DATE = objectAttributes[33].GetValueAsString();
                data.CKA_END_DATE = objectAttributes[34].GetValueAsString();
                data.CKA_MODULUS = objectAttributes[35].GetValueAsString();
                data.CKA_MODULUS_BITS = objectAttributes[36].GetValueAsString();
                data.CKA_PUBLIC_EXPONENT = objectAttributes[37].GetValueAsString();
                data.CKA_PRIVATE_EXPONENT = objectAttributes[38].GetValueAsString();
                data.CKA_PRIME_1 = objectAttributes[39].GetValueAsString();
                data.CKA_PRIME_2 = objectAttributes[40].GetValueAsString();
                data.CKA_EXPONENT_1 = objectAttributes[41].GetValueAsString();
                data.CKA_EXPONENT_2 = objectAttributes[42].GetValueAsString();
                data.CKA_COEFFICIENT = objectAttributes[43].GetValueAsString();
                data.CKA_PUBLIC_KEY_INFO = objectAttributes[44].GetValueAsString();
                data.CKA_PRIME = objectAttributes[45].GetValueAsString();
                data.CKA_SUBPRIME = objectAttributes[46].GetValueAsString();
                data.CKA_BASE = objectAttributes[47].GetValueAsString();
                data.CKA_PRIME_BITS = objectAttributes[48].GetValueAsString();
                data.CKA_SUBPRIME_BITS = objectAttributes[49].GetValueAsString();
                data.CKA_VALUE_BITS = objectAttributes[50].GetValueAsString();
                data.CKA_VALUE_LEN = objectAttributes[51].GetValueAsString();
                data.CKA_EXTRACTABLE = objectAttributes[52].GetValueAsString();
                data.CKA_LOCAL = objectAttributes[53].GetValueAsString();
                data.CKA_NEVER_EXTRACTABLE = objectAttributes[54].GetValueAsString();
                data.CKA_ALWAYS_SENSITIVE = objectAttributes[55].GetValueAsString();
                data.CKA_KEY_GEN_MECHANISM = objectAttributes[56].GetValueAsString();
                data.CKA_MODIFIABLE = objectAttributes[57].GetValueAsString();
                data.CKA_COPYABLE = objectAttributes[58].GetValueAsString();
                data.CKA_DESTROYABLE = objectAttributes[59].GetValueAsString();
                data.CKA_ECDSA_PARAMS = objectAttributes[60].GetValueAsString();
                data.CKA_EC_PARAMS = objectAttributes[61].GetValueAsString();
                data.CKA_EC_POINT = objectAttributes[62].GetValueAsString();
                data.CKA_SECONDARY_AUTH = objectAttributes[63].GetValueAsString();
                data.CKA_AUTH_PIN_FLAGS = objectAttributes[64].GetValueAsString();
                data.CKA_ALWAYS_AUTHENTICATE = objectAttributes[65].GetValueAsString();
                data.CKA_WRAP_WITH_TRUSTED = objectAttributes[66].GetValueAsString();
                data.CKA_WRAP_TEMPLATE = objectAttributes[67].GetValueAsString();
                data.CKA_UNWRAP_TEMPLATE = objectAttributes[68].GetValueAsString();
                data.CKA_DERIVE_TEMPLATE = objectAttributes[69].GetValueAsString();
                data.CKA_OTP_FORMAT = objectAttributes[70].GetValueAsString();
                data.CKA_OTP_LENGTH = objectAttributes[71].GetValueAsString();
                data.CKA_OTP_TIME_INTERVAL = objectAttributes[72].GetValueAsString();
                data.CKA_OTP_USER_FRIENDLY_MODE = objectAttributes[73].GetValueAsString();
                data.CKA_OTP_CHALLENGE_REQUIREMENT = objectAttributes[74].GetValueAsString();
                data.CKA_OTP_TIME_REQUIREMENT = objectAttributes[75].GetValueAsString();
                data.CKA_OTP_COUNTER_REQUIREMENT = objectAttributes[76].GetValueAsString();
                data.CKA_OTP_PIN_REQUIREMENT = objectAttributes[77].GetValueAsString();
                data.CKA_OTP_COUNTER = objectAttributes[78].GetValueAsString();
                data.CKA_OTP_TIME = objectAttributes[79].GetValueAsString();
                data.CKA_OTP_USER_IDENTIFIER = objectAttributes[80].GetValueAsString();
                data.CKA_OTP_SERVICE_IDENTIFIER = objectAttributes[81].GetValueAsString();
                data.CKA_OTP_SERVICE_LOGO = objectAttributes[82].GetValueAsString();
                data.CKA_OTP_SERVICE_LOGO_TYPE = objectAttributes[83].GetValueAsString();
                data.CKA_GOSTR3410_PARAMS = objectAttributes[84].GetValueAsString();
                data.CKA_GOSTR3411_PARAMS = objectAttributes[85].GetValueAsString();
                data.CKA_GOST28147_PARAMS = objectAttributes[86].GetValueAsString();
                data.CKA_HW_FEATURE_TYPE = objectAttributes[87].GetValueAsString();
                data.CKA_RESET_ON_INIT = objectAttributes[88].GetValueAsString();
                data.CKA_HAS_RESET = objectAttributes[89].GetValueAsString();
                data.CKA_PIXEL_X = objectAttributes[90].GetValueAsString();
                data.CKA_PIXEL_Y = objectAttributes[91].GetValueAsString();
                data.CKA_RESOLUTION = objectAttributes[92].GetValueAsString();
                data.CKA_CHAR_ROWS = objectAttributes[93].GetValueAsString();
                data.CKA_CHAR_COLUMNS = objectAttributes[94].GetValueAsString();
                data.CKA_COLOR = objectAttributes[95].GetValueAsString();
                data.CKA_BITS_PER_PIXEL = objectAttributes[96].GetValueAsString();
                data.CKA_CHAR_SETS = objectAttributes[97].GetValueAsString();
                data.CKA_ENCODING_METHODS = objectAttributes[98].GetValueAsString();
                data.CKA_MIME_TYPES = objectAttributes[99].GetValueAsString();
                data.CKA_MECHANISM_TYPE = objectAttributes[100].GetValueAsString();
                data.CKA_REQUIRED_CMS_ATTRIBUTES = objectAttributes[101].GetValueAsString();
                data.CKA_DEFAULT_CMS_ATTRIBUTES = objectAttributes[102].GetValueAsString();
                data.CKA_SUPPORTED_CMS_ATTRIBUTES = objectAttributes[103].GetValueAsString();
                data.CKA_ALLOWED_MECHANISMS = objectAttributes[104].GetValueAsString();
                data.CKA_VENDOR_DEFINED = objectAttributes[105].GetValueAsString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public void SignPDF()
        {
            string folderDest = @"D:\sign";
            string fileNameDest = "testFile_sign.pdf";
            string fileType = ".pdf";
            try
            {
                string[] pDFFile = GetAllPDFFile();
                if (pDFFile != null && pDFFile.Length > 0)
                {
                    var getcertificate = GetCertificate();
                    IExternalSignature pks = GetPriveteKey(getcertificate, "NEW06391012205001173_200916150834");
                    //getcertificate.PrivateKey = pks as AsymmetricKeyEntry;
                    var bouncyCert = DotNetUtilities.FromX509Certificate(getcertificate);
                    var certificateEntry = new X509CertificateEntry(bouncyCert);
                    Pkcs12Store store = new Pkcs12StoreBuilder().Build();
                    store.SetCertificateEntry("certificate", certificateEntry);
                    ICollection<Org.BouncyCastle.X509.X509Certificate> chain = new List<Org.BouncyCastle.X509.X509Certificate>();
                    //AsymmetricKeyEntry pk = store.GetKey(alias);
                    //chain.Add(certificateEntry.Certificate);
                    chain.Add(certificateEntry.Certificate);

                    //Org.BouncyCastle.X509.X509Certificate vert = Org.BouncyCastle.Security.DotNetUtilities.FromX509Certificate(getcertificate);

                    //X509CertificateParser objCP = new X509CertificateParser();
                    //Org.BouncyCastle.X509.X509Certificate[] objChain = new Org.BouncyCastle.X509.X509Certificate[] { objCP.ReadCertificate(getcertificate.RawData) };

                    //IList<ICrlClient> crlList = new List<ICrlClient>();
                    //crlList.Add(new CrlClientOnline(objChain));

                    //RsaPrivateCrtKeyParameters parameters = pk.Key as RsaPrivateCrtKeyParameters;

                    foreach (string src in pDFFile)
                    {
                        fileNameDest = Path.GetFileName(src).Replace(".pdf", "");
                        PdfReader reader = new PdfReader(src);
                        FileStream os = new FileStream(folderDest + fileNameDest + "_sign" + fileType, FileMode.Create);
                        PdfStamper stamper = PdfStamper.CreateSignature(reader, os, '\0');
                        // Creating the appearance
                        PdfSignatureAppearance appearance = stamper.SignatureAppearance;
                        //appearance.Reason = "My reason for signing";
                        //appearance.Location = "The middle of nowhere";
                        //appearance.SetVisibleSignature(new Rectangle(36, 748, 144, 780), 1, "sig");

                        // Creating the signature
                        //IExternalSignature pks = new PrivateKeySignature(parameters, DigestAlgorithms.SHA512);
                        ////IExternalSignature pks = GetPriveteKey(getcertificate, "NEW06391012180004545_181008132542");
                        //IExternalSignature pks = GetPriveteKey(getcertificate, "NEW06391012205001173_200916150834");
                        //IExternalSignature pks = new X509Certificate2Signature(getcertificate, DigestAlgorithms.SHA512);
                        MakeSignature.SignDetached(appearance, pks, chain, null, null, null, 0, CryptoStandard.CMS);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string[] GetAllPDFFile()
        {
            string[] result = new string[0];
            try
            {
                StringBuilder sb = new StringBuilder();
                string pathFolder = @"D:\sign";
                string fileType = "*.pdf";
                result = Directory.GetFiles(pathFolder, fileType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public X509Certificate2 GetCertificate()
        {
            X509Certificate2 certificate = null;
            try
            {
                string slotName = "eTax-TEST"; //we are using the user slot
                //string pin = string.Empty; //the pin that we plan to use to login // P@ssw0rd1 //

                if (Net.Pkcs11Interop.Common.Platform.Uses32BitRuntime)
                {
                    pkcs11LibraryPath = @"C:\Program Files (x86)\SafeNet\Protect Toolkit 5\Protect Toolkit C SDK\bin\hsm\cryptoki.dll";
                }

                byte[] iv = new byte[16];

                Pkcs11InteropFactories factories = new Pkcs11InteropFactories();
                using (IPkcs11Library pkcs11Library = factories.Pkcs11LibraryFactory.LoadPkcs11Library(factories, pkcs11LibraryPath, AppType.SingleThreaded))
                {
                    var slot = pkcs11Library.GetSlotList(SlotsType.WithOrWithoutTokenPresent);
                    var info = pkcs11Library.GetInfo();
                    ISlot s = slot[1];
                    string KeyContainerName = "";
                    using (ISession session = s.OpenSession(SessionType.ReadWrite))
                    {
                        var name = s.GetTokenInfo();
                        var namde = s.GetType();
                        var namess = s.ToString();
                        // Login as normal user
                        session.Login(CKU.CKU_USER, pinCode);

                        // List certificates in smart card
                        List<IObjectAttribute> certificateTemplate = new List<IObjectAttribute>();
                        //certificateTemplate.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE));
                        certificateTemplate.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE));
                        List<IObjectHandle> foundCertificatesResult = session.FindAllObjects(certificateTemplate);

                        // Attribute template of certificate
                        List<CKA> attributeTemplate = new List<CKA>();
                        attributeTemplate.Add(CKA.CKA_VALUE);
                        attributeTemplate.Add(CKA.CKA_LABEL);

                        List<IObjectAttribute> certAttributes;
                        //X509Certificate2 certificatetemp;
                        foreach (var item in foundCertificatesResult)
                        {
                            certAttributes = session.GetAttributeValue(item, attributeTemplate);
                            KeyContainerName = certAttributes[1].GetValueAsString();

                            //byte[] certData = certAttributes[0].GetValueAsByteArray();
                            //X509Certificate2 x509Certificate2 = new X509Certificate2(certData);
                            //RSA rsaPrivateKey = x509Certificate2.GetRSAPrivateKey();
                            //List<IObjectAttribute> searchTemplate = new List<IObjectAttribute>();
                            //searchTemplate.Add(session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_PRIVATE_KEY));

                            //var foundObjects = session.FindAllObjects(searchTemplate);
                            //var privateKey = foundObjects[0];
                            //certificate = new X509Certificate2(certAttributes[0].GetValueAsByteArray());
                            //var sads = certificate.GetDSAPrivateKey();
                            //var asdd = certificate.GetECDsaPrivateKey();
                            //var sadss = certificate.GetRSAPrivateKey();
                            //if (KeyContainerName == "NEW06391012180004545_181008132542")
                            if (KeyContainerName == "NEW06391012205001173_200916150834")
                            {
                                certificate = new X509Certificate2(certAttributes[0].GetValueAsByteArray());
                                //GetPriveteKey(certificate, KeyContainerName);
                                //certificate.PrivateKey = certificate.CopyWithPrivateKey(key);
                                break;
                            }
                            //certificate = new X509Certificate2(certAttributes[0].GetValueAsByteArray());
                            //GetPriveteKey(certificate, KeyContainerName);
                        }
                        //List<IObjectAttribute> certAttributes = session.GetAttributeValue(foundCertificatesResult[0], attributeTemplate);
                        //certificate = new X509Certificate2(certAttributes[0].GetValueAsByteArray());
                        session.Logout();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return certificate;
        }

        public IExternalSignature GetPriveteKey(string KeyContainerName)
        {
            IExternalSignature pk = null;
            bool checkPrivateKey = true;
            try
            {
                if (Net.Pkcs11Interop.Common.Platform.Uses32BitRuntime)
                {
                    pkcs11LibraryPath = @"C:\Program Files (x86)\SafeNet\Protect Toolkit 5\Protect Toolkit C SDK\bin\hsm\cryptoki.dll";
                }
                var certificate2 = new X509Certificate2(pkcs11LibraryPath);
                pk = GetPriveteKey(certificate2, KeyContainerName);
            }
            catch (Exception ex)
            {
                checkPrivateKey = false;
                throw ex;
            }
            //if (checkPrivateKey)
            //{
            //    //breakpoint
            //    var key = KeyContainerName;
            //}
            return pk;
        }

        public IExternalSignature GetPriveteKey(X509Certificate2 certificate, string KeyContainerName)
        {
            IExternalSignature pk = null;
            bool checkPrivateKey = true;
            try
            {
                if (Net.Pkcs11Interop.Common.Platform.Uses32BitRuntime)
                {
                    pkcs11LibraryPath = @"C:\Program Files (x86)\SafeNet\Protect Toolkit 5\Protect Toolkit C SDK\bin\hsm\cryptoki.dll";
                }
                SimplePinProvider(pinCode);
                //GetPinResult a = GetKeyPin();
                //GetPinResult b = GetTokenPin();
                X509Store store = new X509Store(StoreLocation.CurrentUser);
                //X509Store store = new X509Store("", StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);
                var certificateList = store.Certificates;

                var storepk = new Pkcs11X509Store(pkcs11LibraryPath, pinProvider);
                //var stores = new Pkcs11X509Store(pkcs11LibraryPath, new ConstPinProvider("Pin"));
                //Pkcs11X509Certificate cert = store.Slots[0].Token.Certificates[0];
                //RSA rsaPrivateKey = cert.GetRSAPrivateKey();

                //var name = "";
                //var certificateList = store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, name, false);

                //if (Net.Pkcs11Interop.Common.Platform.Uses32BitRuntime)
                //{
                //    pkcs11LibraryPath = @"C:\Program Files (x86)\SafeNet\Protect Toolkit 5\Protect Toolkit C SDK\bin\hsm\cryptoki.dll";
                //}
                //var certificate2 = new X509Certificate2(pkcs11LibraryPath);

                var pass = new SecureString();
                char[] array = (pinCode).ToCharArray();
                foreach (char ch in array)
                {
                    pass.AppendChar(ch);
                }
                var cspParameters = new CspParameters()
                {
                    ProviderType = 24,
                    ProviderName = "Microsoft Enhanced RSA and AES Cryptographic Provider",
                    KeyContainerName = KeyContainerName,
                    //Flags = CspProviderFlags.UseExistingKey | CspProviderFlags.UseMachineKeyStore
                    Flags = CspProviderFlags.UseExistingKey,
                    //KeyPassword = pass,
                    CryptoKeySecurity = new System.Security.AccessControl.CryptoKeySecurity()
                };

                //CspParameters cspParameters = new CspParameters(
                //    24,
                //    "Microsoft Enhanced RSA and AES Cryptographic Provider",
                //    KeyContainerName,
                //    new System.Security.AccessControl.CryptoKeySecurity(),
                //    pass);


                var privateKey = new RSACryptoServiceProvider(cspParameters);
                //RSACryptoServiceProvider intermediateProvider = privateKey;

                //RSACryptoServiceProvider csp = new RSACryptoServiceProvider(cspParameters);
                ////csp.ImportCspBlob(intermediateProvider.ExportCspBlob(true));

                //RSAParameters RSAKeyInfo = privateKey.ExportParameters(includePrivateParameters: true);
                //string publicPrivateXml = privateKey.ToXmlString(true);
                AsymmetricCipherKeyPair keypair = DotNetUtilities.GetRsaKeyPair(privateKey);
                pk = new PrivateKeySignature(keypair.Private, DigestAlgorithms.SHA256);
                //certificate.PrivateKey = privateKey;

                //Provider provider = Security.getProvider("SunPKCS11");

                var rsaCsp = new RSACryptoServiceProvider(cspParameters);

                certificate.PrivateKey = rsaCsp;

                Org.BouncyCastle.X509.X509Certificate bcCert = DotNetUtilities.FromX509Certificate(certificate);
                CertificateKeyPack certificateKeyPack = new CertificateKeyPack();
                certificateKeyPack.PrivateKey = null;
                certificateKeyPack.CertificateInstance = certificate;
                certificateKeyPack.CertificateChain = new List<Org.BouncyCastle.X509.X509Certificate> { bcCert };

                CertificateKeyPack ck = new CertificateKeyPack();
            }
            catch (Exception ex)
            {
                checkPrivateKey = false;
                //throw ex;
            }
            if (checkPrivateKey)
            {
                //breakpoint
                var key = KeyContainerName;
            }
            return pk;
        }

        internal class CertificateKeyPack
        {
            public ICipherParameters PrivateKey { get; set; }
            public List<Org.BouncyCastle.X509.X509Certificate> CertificateChain { get; set; }
            public X509Certificate2 CertificateInstance { get; set; }

        }

        public void GetKeyAliases()
        {
            try
            {
                Pkcs12Store store = new Pkcs12Store();
                char[] PASSWORD = (pinCode).ToCharArray();
                if (Net.Pkcs11Interop.Common.Platform.Uses32BitRuntime)
                {
                    pkcs11LibraryPath = @"C:\Program Files (x86)\SafeNet\Protect Toolkit 5\Protect Toolkit C SDK\bin\hsm\cryptoki.dll";
                }
                store.Load(new FileStream(pkcs11LibraryPath, FileMode.Open), PASSWORD);
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
        }


    }
}
