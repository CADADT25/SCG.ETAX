using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY.Controllers;
using System.Net;
using System.Net.Mail;

namespace SCG.CAD.ETAX.UTILITY.AdminTool
{
    public class ResendEmail
    {
        AdminToolHelper adminToolHelper = new AdminToolHelper();
        UtilityProfileCustomerController profileCustomerController = new UtilityProfileCustomerController();
        UtilityProfileEmailTemplateController profileEmailTemplateController = new UtilityProfileEmailTemplateController();
        UtilityProfileCompanyController profileCompanyController = new UtilityProfileCompanyController();
        UtilityConfigMftsEmailSettingController configMftsEmailSettingController = new UtilityConfigMftsEmailSettingController();
        UtilityRdDocumentController rdDocumentController = new UtilityRdDocumentController();


        List<ProfileCustomer> profileCustomers = new List<ProfileCustomer>();
        List<TransactionDescription> transactionDescription = new List<TransactionDescription>();
        List<ProfileEmailTemplate> profileEmailTemplates = new List<ProfileEmailTemplate>();
        List<ProfileCompany> profileCompanies = new List<ProfileCompany>();
        List<ConfigMftsEmailSetting> configMftsEmailSettings = new List<ConfigMftsEmailSetting>();
        List<RdDocument> rdDocuments = new List<RdDocument>();

        public Response ResendEmailAgain(string billno, string updateby)
        {
            Response res = new Response();
            bool result = false;
            string subjectemail = "";

            ProfileCustomer profileCustomer = new ProfileCustomer();
            ProfileEmailTemplate profileEmailTemplate = new ProfileEmailTemplate();
            ProfileCompany profileCompany = new ProfileCompany();
            ConfigMftsEmailSetting configEmail = new ConfigMftsEmailSetting();
            try
            {
                GetDataFromDataBase(billno);
                if (transactionDescription != null && transactionDescription.Count > 0)
                {
                    profileCustomer = profileCustomers.FirstOrDefault(x => x.CompanyCode == transactionDescription[0].CompanyCode
                                                                        && x.CustomerId == transactionDescription[0].CustomerId);

                    profileEmailTemplate = profileEmailTemplates.FirstOrDefault(x => x.EmailTemplateNo.ToString() == profileCustomer.EmailTemplateNo) ?? new ProfileEmailTemplate();

                    profileCompany = profileCompanies.FirstOrDefault(x => x.CompanyCode == transactionDescription[0].CompanyCode) ?? new ProfileCompany();
                    subjectemail = profileEmailTemplate.EmailSubject.Replace("[COMPANY-NAME]", profileCompany.CompanyNameTh);
                    configEmail = configMftsEmailSettings.FirstOrDefault(x => x.ConfigMftsEmailSettingCompanyCode == transactionDescription[0].CompanyCode);

                    result = SendEmailbyCompany(transactionDescription[0], configEmail, profileCustomer, subjectemail, profileCompany, updateby);
                }
                res.STATUS = result;

            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.Message;
            }
            return res;
        }

        public void GetDataFromDataBase(string billno)
        {
            try
            {
                transactionDescription = adminToolHelper.GetBillingTransaction(billno);
                profileCustomers = profileCustomerController.List().Result;
                profileEmailTemplates = profileEmailTemplateController.List().Result;
                profileCompanies = profileCompanyController.List().Result;
                configMftsEmailSettings = configMftsEmailSettingController.List().Result;
                rdDocuments = rdDocumentController.List().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool SendEmailbyCompany(TransactionDescription dataTran, ConfigMftsEmailSetting config, ProfileCustomer profileemailCustomer, string subjectemail, ProfileCompany profileCompany, string sendby)
        {
            bool result = false;
            var document = "";
            string customerid = "";
            string customername = "";
            try
            {
                customerid = dataTran.CustomerId;
                customername = dataTran.CustomerName;

                var templateemail = profileEmailTemplates.FirstOrDefault(x => x.EmailTemplateNo.ToString() == profileemailCustomer.EmailTemplateNo);
                if (templateemail != null)
                {
                    //var fromEmailAddress = config.ConfigMftsEmailSettingEmail;
                    //var fromEmailPassword = config.ConfigMftsEmailSettingPassword;
                    //var smtpHost = config.ConfigMftsEmailSettingHost;
                    //var smtpPort = config.ConfigMftsEmailSettingPort;
                    //var toEmailAddress = config.ConfigMftsEmailSettingEmail;
                    //var fromEmailAddress = sendby;
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

                    document += "<tr>";
                    document += "<td width='10%' style='width:10.0%;padding:.75pt .75pt .75pt .75pt'></td>";
                    document += "<td width='20%' style='width:20.0%;padding:.75pt .75pt .75pt .75pt'>";
                    document += "<p class='MsoNormal'><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif'>" + GetDocType(dataTran.DocType ?? "") + "</span><o:p></o:p></p>";
                    document += "</td>";
                    document += "<td width='5%' style='width:5.0%;padding:.75pt .75pt .75pt .75pt'>";
                    document += "<p class='MsoNormal'><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif'>เลขที่:</span><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif;mso-ascii-font-family:&quot;Times New Roman&quot;;";
                    document += "mso-hansi-font-family:&quot;Times New Roman&quot;'> </span><o:p></o:p></p>";
                    document += "</td>";
                    document += "<td style='padding:.75pt .75pt .75pt .75pt'>";
                    document += "<p class='MsoNormal'>" + dataTran.BillingNumber + "<o:p></o:p></p>";
                    document += "</td>";
                    document += "</tr>";

                    document += "</tbody>";
                    document += "</table>";

                    body = body.Replace("[FILE-NAME]", document);

                    MailMessage message = new MailMessage();


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

                    message.Attachments.Add(new Attachment(dataTran.PdfSignLocation)
                    {
                        Name = RenameFileName(dataTran.CompanyCode, dataTran.BillingNumber)
                    });

                    try
                    {
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
                        throw ex;
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
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

        public string RenameFileName(string comcode, string billno)
        {
            string filename = "";
            try
            {
                filename = comcode + "_" + DateTime.Now.ToString("yyyy") + "_" + billno + ".pdf";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return filename;
        }
    }
}
