using SCG.CAD.ETAX.EMAIL.Controller;
using SCG.CAD.ETAX.EMAIL.Model;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace SCG.CAD.ETAX.EMAIL.BussinessLayer
{
    public class Email
    {
        ConfigMftsEmailSettingController configMftsEmailSettingController = new ConfigMftsEmailSettingController();
        TransactionDescriptionController transactionDescriptionController = new TransactionDescriptionController();
        ConfigGlobalController configGlobalController = new ConfigGlobalController();
        ProfileCustomerController profileCustomerController = new ProfileCustomerController();
        ProfileEmailTemplateController profileEmailTemplateController = new ProfileEmailTemplateController();
        OutputSearchEmailSendController outputSearchEmailSendController = new OutputSearchEmailSendController();
        ProfileCompanyController profileCompanyController = new ProfileCompanyController();
        RdDocumentController rdDocumentController = new RdDocumentController();

        List<ConfigMftsEmailSetting> configMftsEmailSettings = new List<ConfigMftsEmailSetting>();
        List<TransactionDescription> transactionDescriptions = new List<TransactionDescription>();
        List<ConfigGlobal> configGlobals = new List<ConfigGlobal>();
        List<ProfileCustomer> profileCustomers = new List<ProfileCustomer>();
        List<ProfileEmailTemplate> profileEmailTemplates = new List<ProfileEmailTemplate>();
        List<ProfileCompany> profileCompanies = new List<ProfileCompany>();
        List<RdDocument> rdDocuments = new List<RdDocument>();
        ConfigGlobal configGlobal = new ConfigGlobal();

        int maxsize = 2;
        string pathoutputpdfsendemail;
        string pathoutputcontentemail;
        string outputsearchemailsendno = "";

        public void ProcessSendEmail()
        {
            try
            {
                Console.WriteLine("Start SendEmail");
                GetDataFromDataBase();
                GetConfig();
                LoopbyCompany();
                Console.WriteLine("End SendEmail");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetDataFromDataBase()
        {
            try
            {
                configMftsEmailSettings = configMftsEmailSettingController.List().Result;
                transactionDescriptions = transactionDescriptionController.List().Result;
                configGlobals = configGlobalController.List().Result;
                profileCustomers = profileCustomerController.List().Result;
                profileEmailTemplates = profileEmailTemplateController.List().Result;
                profileCompanies = profileCompanyController.List().Result;
                rdDocuments = rdDocumentController.List().Result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetDataTransactionDescription()
        {
            transactionDescriptions = transactionDescriptionController.List().Result;
        }

        public void GetConfig()
        {
            try
            {
                configGlobal = configGlobals.FirstOrDefault(x => x.ConfigGlobalName == "MAXSIZEATTACHMENTEMAIL");
                if (configGlobal != null)
                {
                    maxsize = Convert.ToInt32(configGlobal.ConfigGlobalValue);
                }
                pathoutputpdfsendemail = configGlobals.FirstOrDefault(x => x.ConfigGlobalName == "PATHBACKUPPDFSENDEMAIL").ConfigGlobalValue;
                pathoutputcontentemail = configGlobals.FirstOrDefault(x => x.ConfigGlobalName == "PATHBACKUPEMAILCONTENT").ConfigGlobalValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool LoopbyCompany()
        {
            bool result = false;
            try
            {
                double sumfilesize;
                double filesize;
                string customerid = "";
                string emailto = "";
                string emailcc = "";
                string subjectemail = "";
                string filename = "";
                bool isactive = false;
                string[] anytime = null;

                List<TransactionDescription> dataPDFsend = new List<TransactionDescription>();
                List<PDFFileDetailModel> filePDFsend = new List<PDFFileDetailModel>();
                PDFFileDetailModel pDFFileDetails = new PDFFileDetailModel();
                List<ProfileCustomer> profileemailCustomer = new List<ProfileCustomer>();
                ProfileEmailTemplate profileEmailTemplate = new ProfileEmailTemplate();
                ProfileCompany profileCompany = new ProfileCompany();
                FileInfo fileemail;
                foreach (var config in configMftsEmailSettings)
                {
                    if (CheckRunningTime(config))
                    {
                        Console.WriteLine("Start CompanyCode :" + config.ConfigMftsEmailSettingCompanyCode);
                        customerid = "";
                        profileemailCustomer = profileCustomers.Where(x => x.CompanyCode == config.ConfigMftsEmailSettingCompanyCode && x.StatusEmail == 1).ToList();
                        if (profileemailCustomer != null && profileemailCustomer.Count > 0)
                        {
                            profileCompany = profileCompanies.FirstOrDefault(x => x.CompanyCode == config.ConfigMftsEmailSettingCompanyCode) ?? new ProfileCompany();
                            foreach (var customer in profileemailCustomer)
                            {
                                dataPDFsend = transactionDescriptions.Where(x => x.CompanyCode == config.ConfigMftsEmailSettingCompanyCode
                                                                 && x.CustomerId == customer.CustomerId
                                                                 && x.EmailSendStatus == "Waiting"
                                                                 //&& !String.IsNullOrEmpty(x.PdfSignLocation)
                                                                 && x.Isactive == 1).ToList();
                                if (dataPDFsend != null && dataPDFsend.Count > 0)
                                {
                                    profileEmailTemplate = profileEmailTemplates.FirstOrDefault(x => x.EmailTemplateNo.ToString() == customer.EmailTemplateNo) ?? new ProfileEmailTemplate();
                                    //GetEmail(profileemailCustomer, out emailto, out emailcc, out customerid);
                                    subjectemail = profileEmailTemplate.EmailSubject.Replace("[COMPANY-NAME]", profileCompany.CompanyNameTh);
                                    sumfilesize = 0;
                                    filePDFsend = new List<PDFFileDetailModel>();
                                    foreach (var item in dataPDFsend)
                                    {
                                        pDFFileDetails = new PDFFileDetailModel();
                                        using (FileStream zipFileToOpen = new FileStream(item.PdfSignLocation, FileMode.Open))
                                        {
                                            filesize = CalculateMBbyByte(zipFileToOpen.Length);
                                            sumfilesize += filesize;
                                            if (sumfilesize > maxsize)
                                            {
                                                SendEmailbyCompany(filePDFsend, config, customer, subjectemail, profileCompany);
                                                fileemail = ReadEMLFile();
                                                filename = config.ConfigMftsEmailSettingCompanyCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".eml";
                                                //MoveFile(fileemail.FullName, pathoutputcontentemail, filename, DateTime.Now);
                                                InsertOutputSearchEmailSend(config, customer, subjectemail, pathoutputcontentemail + "\\" + filename);
                                                UpdateStatusTransactionDescription(filePDFsend, config.ConfigMftsEmailSettingCompanyCode, customerid);
                                                sumfilesize = filesize;
                                                filePDFsend = new List<PDFFileDetailModel>();
                                                pDFFileDetails.BillingNo = item.BillingNumber;
                                                pDFFileDetails.FullPath = item.PdfSignLocation;
                                                pDFFileDetails.BillingDate = item.BillingDate ?? DateTime.Now;
                                                pDFFileDetails.RenameFileName = RenameFileName(config.ConfigMftsEmailSettingCompanyCode, item.PdfSignLocation);
                                                pDFFileDetails.CustomerId = item.CustomerId;
                                                pDFFileDetails.CustomerName = item.CustomerName;
                                                pDFFileDetails.Doctype = GetDocType(item.DocType ?? "");
                                                filePDFsend.Add(pDFFileDetails);
                                            }
                                            else if (sumfilesize == maxsize)
                                            {
                                                pDFFileDetails.BillingNo = item.BillingNumber;
                                                pDFFileDetails.FullPath = item.PdfSignLocation;
                                                pDFFileDetails.BillingDate = item.BillingDate ?? DateTime.Now;
                                                pDFFileDetails.RenameFileName = RenameFileName(config.ConfigMftsEmailSettingCompanyCode, item.PdfSignLocation);
                                                pDFFileDetails.CustomerId = item.CustomerId;
                                                pDFFileDetails.CustomerName = item.CustomerName;
                                                pDFFileDetails.Doctype = GetDocType(item.DocType ?? "");
                                                filePDFsend.Add(pDFFileDetails);
                                                SendEmailbyCompany(filePDFsend, config, customer, subjectemail, profileCompany);
                                                fileemail = ReadEMLFile();
                                                filename = config.ConfigMftsEmailSettingCompanyCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".eml";
                                                //MoveFile(fileemail.FullName, pathoutputcontentemail, filename, DateTime.Now);
                                                InsertOutputSearchEmailSend(config, customer, subjectemail, pathoutputcontentemail + "\\" + filename);
                                                UpdateStatusTransactionDescription(filePDFsend, config.ConfigMftsEmailSettingCompanyCode, customerid);
                                                sumfilesize = 0;
                                                filePDFsend = new List<PDFFileDetailModel>();
                                            }
                                            else
                                            {
                                                pDFFileDetails.BillingNo = item.BillingNumber;
                                                pDFFileDetails.FullPath = item.PdfSignLocation;
                                                pDFFileDetails.BillingDate = item.BillingDate ?? DateTime.Now;
                                                pDFFileDetails.RenameFileName = RenameFileName(config.ConfigMftsEmailSettingCompanyCode, item.PdfSignLocation);
                                                pDFFileDetails.CustomerId = item.CustomerId;
                                                pDFFileDetails.CustomerName = item.CustomerName;
                                                pDFFileDetails.Doctype = GetDocType(item.DocType ?? "");
                                                filePDFsend.Add(pDFFileDetails);
                                            }
                                        }
                                    }
                                    if (filePDFsend.Count > 0)
                                    {
                                        SendEmailbyCompany(filePDFsend, config, customer, subjectemail, profileCompany);
                                        fileemail = ReadEMLFile();
                                        filename = config.ConfigMftsEmailSettingCompanyCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".eml";
                                        //MoveFile(fileemail.FullName, pathoutputcontentemail, filename, DateTime.Now);
                                        InsertOutputSearchEmailSend(config, customer, subjectemail, pathoutputcontentemail + "\\" + filename);
                                        UpdateStatusTransactionDescription(filePDFsend, config.ConfigMftsEmailSettingCompanyCode, customerid);
                                        sumfilesize = 0;
                                        filePDFsend = new List<PDFFileDetailModel>();
                                    }
                                }
                            }
                        }
                    }
                    Console.WriteLine("End CompanyCode :" + config.ConfigMftsEmailSettingCompanyCode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool CheckRunningTime(ConfigMftsEmailSetting config)
        {
            bool result = false;
            try
            {
                if (config.ConfigMftsEmailSettingOneTime != null &&
                        !String.IsNullOrEmpty(config.ConfigMftsEmailSettingOneTime) &&
                        Convert.ToDateTime(config.ConfigMftsEmailSettingOneTime) <= DateTime.Now)
                {
                    result = true;
                }
                if (config.ConfigMftsEmailSettingAnyTime != null &&
                    !String.IsNullOrEmpty(config.ConfigMftsEmailSettingAnyTime))
                {
                    if (config.ConfigMftsEmailSettingAnyTime.IndexOf(DateTime.Now.ToString("HH:mm")) >= 0)
                    {
                        result = true;
                    }
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool SendEmailbyCompany(List<PDFFileDetailModel> filePDFsend, ConfigMftsEmailSetting config, ProfileCustomer profileemailCustomer, string subjectemail, ProfileCompany profileCompany)
        {
            bool result = false;
            var document = "";
            string customerid = "";
            string customername = "";
            string foldertemp = AppDomain.CurrentDomain.BaseDirectory + "\\TempEmail\\";
            try
            {
                if (!Directory.Exists(foldertemp))
                {
                    Directory.CreateDirectory(foldertemp);
                }
                var customer = filePDFsend.OrderByDescending(x => x.BillingDate).FirstOrDefault();
                customerid = customer.CustomerId;
                customername = customer.CustomerName;

                var templateemail = profileEmailTemplates.FirstOrDefault(x => x.EmailTemplateNo.ToString() == profileemailCustomer.EmailTemplateNo);
                if (templateemail != null)
                {
                    //var fromEmailAddress = config.ConfigMftsEmailSettingEmail;
                    //var fromEmailPassword = config.ConfigMftsEmailSettingPassword;
                    //var smtpHost = config.ConfigMftsEmailSettingHost;
                    //var smtpPort = config.ConfigMftsEmailSettingPort;
                    //var toEmailAddress = config.ConfigMftsEmailSettingEmail;
                    var fromEmailAddress = "etaxadm@scg.com";
                    var fromEmailPassword = "Thai2020";
                    var smtpHost = "outmail.scg.co.th";
                    var smtpPort = "25";
                    var toEmailAddress = "cadadt25@scg.com";

                    string body = templateemail.EmailBody;
                    body = body.Replace("[COMPANY-NAME]", profileCompany.CompanyNameTh);
                    body = body.Replace("[CUSTOMER-COMPANY-NAME]", customername);
                    body = body.Replace("[CUSTOMER-COMPANY-CODE]", customerid);
                    body = body.Replace("[DATE]", DateTime.Now.ToString("dd-MM-yyyy"));

                    document = "<table class='MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100.0%;mso-cellspacing:0in;mso-yfti-tbllook:1184;mso-padding-alt:0in 0in 0in 0in'>";
                    document += "<tbody>";
                    foreach (var item in filePDFsend)
                    {
                        document += "<tr>";
                        document += "<td width='10%' style='width:10.0%;padding:.75pt .75pt .75pt .75pt'></td>";
                        document += "<td width='20%' style='width:20.0%;padding:.75pt .75pt .75pt .75pt'>";
                        document += "<p class='MsoNormal'><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif'>" + item.Doctype + "</span><o:p></o:p></p>";
                        document += "</td>";
                        document += "<td width='5%' style='width:5.0%;padding:.75pt .75pt .75pt .75pt'>";
                        document += "<p class='MsoNormal'><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif'>เลขที่:</span><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif;mso-ascii-font-family:&quot;Times New Roman&quot;;";
                        document += "mso-hansi-font-family:&quot;Times New Roman&quot;'> </span><o:p></o:p></p>";
                        document += "</td>";
                        document += "<td style='padding:.75pt .75pt .75pt .75pt'>";
                        document += "<p class='MsoNormal'>" + item.BillingNo + "<o:p></o:p></p>";
                        document += "</td>";
                        document += "</tr>";
                    }
                    document += "</tbody>";
                    document += "</table>";

                    body = body.Replace("[FILE-NAME]", document);

                    /*
                     [FILE-NAME]
                     <tr>
                        <td width='10%' style='width:10.0%;padding:.75pt .75pt .75pt .75pt'></td>
                        <td width='20%' style='width:20.0%;padding:.75pt .75pt .75pt .75pt'>
                        <p class='MsoNormal'><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif'>[FILE-NAME]</span><o:p></o:p></p>
                        </td>
                        <td width='5%' style='width:5.0%;padding:.75pt .75pt .75pt .75pt'>
                        <p class='MsoNormal'><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif'>เลขที่:</span><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif;mso-ascii-font-family:&quot;Times New Roman&quot;;
                        mso-hansi-font-family:&quot;Times New Roman&quot;'> </span><o:p></o:p></p>
                        </td>
                        <td style='padding:.75pt .75pt .75pt .75pt'>
                        <p class='MsoNormal'>[BILLING-NUMBER]<o:p></o:p></p>
                        </td>
                       </tr>
                     */
                    MailMessage message = new MailMessage();


                    //Setting From , To and CC
                    message.From = new MailAddress(fromEmailAddress);
                    if (!string.IsNullOrEmpty(profileemailCustomer.CustomerEmail))
                    {
                        message.To.Add(new MailAddress(toEmailAddress));
                    }
                    if (!string.IsNullOrEmpty(profileemailCustomer.CustomerCcemail))
                    {
                        message.CC.Add(new MailAddress(toEmailAddress));
                    }
                    message.Subject = subjectemail;
                    message.IsBodyHtml = true;
                    message.Body = body;
                    foreach (var file in filePDFsend)
                    {
                        message.Attachments.Add(new Attachment(file.FullPath)
                        {
                            Name = file.RenameFileName
                        });
                    }
                    //var client = new SmtpClient();
                    try
                    {
                        using (var client = new SmtpClient())
                        {
                            client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                            //client.PickupDirectoryLocation = @"G:\";
                            client.PickupDirectoryLocation = foldertemp;
                            client.Host = smtpHost;
                            client.EnableSsl = false;
                            client.UseDefaultCredentials = false;
                            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
                            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
                            client.Send(message);
                        }

                        using (var client = new SmtpClient())
                        {
                            client.Host = smtpHost;
                            client.EnableSsl = false;
                            client.UseDefaultCredentials = false;
                            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
                            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
                            client.Send(message);
                        }
                    }
                    catch (Exception ex)
                    {
                        result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool UpdateStatusTransactionDescription(List<PDFFileDetailModel> pDFFileDetails, string comcode, string customercode)
        {
            bool result = false;
            try
            {
                Task<Response> res;
                TransactionDescription updatetransaction = new TransactionDescription();
                List<TransactionDescription> listupdatetransaction = new List<TransactionDescription>();
                foreach (var pdfdata in pDFFileDetails)
                {
                    updatetransaction = transactionDescriptions.FirstOrDefault(x => x.BillingNumber == pdfdata.BillingNo);
                    if (updatetransaction != null)
                    {
                        Console.WriteLine("Update Status TransactionDescription BillingNo : " + pdfdata.BillingNo);
                        updatetransaction.EmailSendStatus = "Successful";
                        updatetransaction.EmailSendDetail = "Batch email was sent to customer code " + customercode + " is completely";
                        updatetransaction.EmailSendDateTime = DateTime.Now;
                        updatetransaction.OutputMailTransactionNo = outputsearchemailsendno;
                        listupdatetransaction.Add(updatetransaction);

                        Console.WriteLine("Start MoveFile Company : " + comcode);
                        //MoveFile(pdfdata.FullPath, pathoutputpdfsendemail, Path.GetFileName(pdfdata.FullPath), updatetransaction.BillingDate ?? DateTime.Now);
                        Console.WriteLine("End MoveFile Company : " + comcode);
                    }
                }
                if (listupdatetransaction.Count > 0)
                {
                    var json = JsonSerializer.Serialize(listupdatetransaction);
                    res = transactionDescriptionController.UpdateList(json);
                    if (res.Result.MESSAGE == "Updated Success.")
                    {
                        result = true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool InsertOutputSearchEmailSend(ConfigMftsEmailSetting config, ProfileCustomer customer, string subjectemail, string filepath)
        {
            bool result = false;
            try
            {
                Task<Response> res;
                OutputSearchEmailSend dataInsert = new OutputSearchEmailSend();

                dataInsert.OutputSearchEmailSendCompanyCode = config.ConfigMftsEmailSettingCompanyCode;
                dataInsert.OutputSearchEmailSendSubject = subjectemail;
                dataInsert.OutputSearchEmailSendFrom = config.ConfigMftsEmailSettingEmail;
                dataInsert.OutputSearchEmailSendTo = customer.CustomerEmail;
                dataInsert.OutputSearchEmailSendCc = customer.CustomerCcemail;
                dataInsert.OutputSearchEmailSendFileName = filepath;
                dataInsert.OutputSearchEmailSendStatus = 1;
                dataInsert.OutputSearchEmailSendLastBy = config.ConfigMftsEmailSettingEmail;
                dataInsert.OutputSearchEmailSendLastTime = DateTime.Now;
                dataInsert.CreateBy = config.ConfigMftsEmailSettingEmail;
                dataInsert.CreateDate = DateTime.Now;
                dataInsert.UpdateBy = config.ConfigMftsEmailSettingEmail;
                dataInsert.UpdateDate = DateTime.Now;
                dataInsert.Isactive = 1;



                var json = JsonSerializer.Serialize(dataInsert);
                res = outputSearchEmailSendController.Insert(json);
                if (res.Result.MESSAGE == "Insert success.")
                {
                    outputsearchemailsendno = res.Result.OUTPUT_DATA.ToString();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public double CalculateMBbyByte(double bytes)
        {
            double result = 0;
            try
            {
                double temp = bytes % (1024 * 1024 * 1024);

                double MBs = temp / (1024 * 1024);
                result = MBs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool MoveFile(string pathinput, string pathout, string filename, DateTime billingdate)
        {
            bool result = false;
            string output = "";
            try
            {
                output = pathout + "\\" + billingdate.ToString("yyyy") + "\\" + billingdate.ToString("MM") + "\\";
                if (!File.Exists(pathinput))
                {
                    // This statement ensures that the file is created,  
                    // but the handle is not kept.  
                    using (FileStream fs = File.Create(pathinput)) { }
                }
                // Ensure that the target does not exist.  
                if (!Directory.Exists(output))
                {
                    Directory.CreateDirectory(output);
                }
                // Move the file.  
                File.Move(pathinput, output + filename);
                Console.WriteLine("{0} was moved to {1}.", pathinput, output);

                // See if the original exists now.  
                if (File.Exists(pathinput))
                {
                    Console.WriteLine("The original file still exists, which is unexpected.");
                }
                else
                {
                    Console.WriteLine("The original file no longer exists, which is expected.");
                }
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            return result;
        }

        public void GetEmail(List<ProfileCustomer> profileemailCustomer, out string emailto, out string emailcc, out string customerid)
        {
            emailto = "";
            emailcc = "";
            customerid = "";
            try
            {
                foreach (var data in profileemailCustomer)
                {
                }
                customerid = customerid.Substring(0, customerid.Length - 2);

                foreach (var item in profileemailCustomer)
                {
                    if (!string.IsNullOrEmpty(item.CustomerEmail))
                    {
                        emailto += item.CustomerEmail + "; ";
                    }
                    if (!string.IsNullOrEmpty(item.CustomerCcemail))
                    {
                        emailcc += item.CustomerCcemail + "; ";
                    }
                    if (!string.IsNullOrEmpty(item.CustomerId))
                    {
                        customerid += item.CustomerId + ", ";
                    }
                }

                if (emailto.Length > 0)
                {
                    emailto = emailto.Substring(0, emailto.Length - 2);
                }
                if (emailcc.Length > 0)
                {
                    emailcc = emailcc.Substring(0, emailcc.Length - 2);
                }
                if (customerid.Length > 0)
                {
                    customerid = customerid.Substring(0, customerid.Length - 2);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GetDocType(string doccode)
        {
            string result = "";
            try
            {
                var rdcode = rdDocuments.FirstOrDefault(x => x.RdDocumentCode == doccode);
                if (rdcode != null)
                {
                    result = rdcode.RdDocumentNameTh ?? "";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        public string RenameFileName(string comcode, string filepath)
        {
            string filename = "";
            string billno = "";
            try
            {
                billno = Path.GetFileName(filepath).Replace(".pdf", "");
                if (billno.IndexOf('_') > -1)
                {
                    billno = billno.Substring(8, (billno.IndexOf('_')) - 8);
                }
                else
                {
                    billno = billno.Substring(8);
                }
                filename = comcode + "_" + DateTime.Now.ToString("yyyy") + "_" + billno + ".pdf";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return filename;
        }

        public FileInfo ReadEMLFile()
        {
            FileInfo result;
            string[] fullpath = new string[0];
            string pathFolder = "";
            string fileType = "*.eml";
            try
            {
                pathFolder = AppDomain.CurrentDomain.BaseDirectory + "\\TempEmail\\";
                if (!Directory.Exists(pathFolder))
                {
                    Directory.CreateDirectory(pathFolder);
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(pathFolder);
                var myFile = directoryInfo.GetFiles(fileType)
                 .OrderByDescending(f => f.LastWriteTime)
                 .First();
                result = myFile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

    }
}
