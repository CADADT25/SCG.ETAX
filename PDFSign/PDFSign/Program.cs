using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace PDFSign
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new TestService()
            //};
            //ServiceBase.Run(ServicesToRun);
            Class.PDFSign testrunPDF = new Class.PDFSign();
            Class.XMLSign testrunXML = new Class.XMLSign();
            Class.ConnectHSM testrunHSM = new Class.ConnectHSM();
            Class.CspSample cspSample = new Class.CspSample();
            Class.XMLGenerate textrunXMLGen = new Class.XMLGenerate();
            Class.APIclass getapi = new Class.APIclass();
            //testrunPDF.SignSignature();
            //testrunXML.XMLSignSignature();
            //testrunXML.XMLSign2();
            //testrunPDF.SignSignature();
            //testrunHSM.ConnectHSMFile();
            //testrunHSM.SignPDF();
            //testrunHSM.GetCertificate();
            //testrunHSM.GetPriveteKey("NEW06391012205001173_200916150834");
            //testrunHSM.GetKeyAliases();
            //textrunXMLGen.GenXMLFile();
            //textrunXMLGen.ReadXMLFile();

            var fffff = "";
            //var list = cspSample.ListtempProvider();
        }
    }

}
