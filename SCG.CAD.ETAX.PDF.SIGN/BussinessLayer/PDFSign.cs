using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.PDF.SIGN.BussinessLayer
{
    public class PDFSign
    {
        public void PdfSign()
        {
            var allfile = ReadPdfFile();
            SignSignature(allfile);
        }

        public string[] ReadPdfFile()
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

        public void SignSignature(string[] allfile)
        {
            string folderDest = @"D:/";
            string fileNameDest = "";
            string fileType = ".pdf";
            try
            {
                if (allfile != null && allfile.Length > 0)
                {
                    foreach (string src in allfile)
                    {
                        fileNameDest = Path.GetFileName(src).Replace(".pdf", "");
                        PdfReader reader = new PdfReader(src);
                        FileStream os = new FileStream(folderDest + fileNameDest + "_sign" + fileType, FileMode.Create);
                        PdfStamper stamper = PdfStamper.CreateSignature(reader, os, '\0');

                        if (SendFilePDFSign())
                        {
                            UpdateStatusAfterSignPDF(true);
                        }
                        else
                        {
                            UpdateStatusAfterSignPDF(false);
                        }


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
        
        public bool UpdateStatusAfterSignPDF(bool status)
        {
            bool result = false;
            try
            {
                if (status)
                {

                }
                else
                {

                }
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
