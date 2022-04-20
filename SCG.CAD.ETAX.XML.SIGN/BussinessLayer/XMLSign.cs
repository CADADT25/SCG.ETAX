using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCG.CAD.ETAX.XML.SIGN.Controller;
using SCG.CAD.ETAX.MODEL.etaxModel;

namespace SCG.CAD.ETAX.XML.SIGN.BussinessLayer
{
    public class XMLSign
    {
        public string[] ReadXmlFile()
        {
            string[] result = new string[0];
            try
            {
                StringBuilder sb = new StringBuilder();
                string pathFolder = @"D:\sign";
                string fileType = "*.xml";
                result = Directory.GetFiles(pathFolder, fileType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public void PdfSign()
        {
            string folderDest = @"D:/";
            string fileNameDest = "";
            string fileType = ".xml";
            bool resultPDFSign = false;
            try
            {
                var allfile = ReadPdfFile();
                if (allfile != null && allfile.Length > 0)
                {
                    var alldataTransactionDes = GetAllTransactionDescriptions();
                    foreach (string src in allfile)
                    {
                        fileNameDest = Path.GetFileName(src).Replace(".xml", "");
                        PdfReader reader = new PdfReader(src);
                        FileStream os = new FileStream(folderDest + fileNameDest + "_sign" + fileType, FileMode.Create);
                        PdfStamper stamper = PdfStamper.CreateSignature(reader, os, '\0');
                        resultPDFSign = SendFilePDFSign();
                        if (alldataTransactionDes.Count > 0)
                        {
                            UpdateStatusAfterSignXML(resultPDFSign);
                        }
                        else
                        {
                            InsertDataAfterSignXML(resultPDFSign);
                        }

                        ExportPDFAfterSign(resultPDFSign);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SendFilePDFSign()
        {
            bool result = false;
            try
            {
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        
        public bool UpdateStatusAfterSignXML(bool status)
        {
            bool result = false;
            string jsondata = "";
            try
            {

                TransactionDescriptionController transactiondescriptioncontroller = new TransactionDescriptionController();
                if (status)
                {

                }
                else
                {

                }
                var jsonresult = transactiondescriptioncontroller.Update(jsondata);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool InsertDataAfterSignXML(bool status)
        {
            bool result = false;
            string jsondata = "";
            try
            {

                TransactionDescriptionController transactiondescriptioncontroller = new TransactionDescriptionController();
                if (status)
                {

                }
                else
                {

                }
                var jsonresult = transactiondescriptioncontroller.Insert(jsondata);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<TransactionDescription> GetAllTransactionDescriptions()
        {
            List<TransactionDescription> result = new List<TransactionDescription>();
            try
            {
                TransactionDescriptionController transactiondescriptioncontroller = new TransactionDescriptionController();
                result = transactiondescriptioncontroller.List().Result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public void ExportPDFAfterSign(bool resultPDFSign)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
