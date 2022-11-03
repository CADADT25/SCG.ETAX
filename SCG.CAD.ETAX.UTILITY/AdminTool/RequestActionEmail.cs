﻿using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY.Controllers;
using System.Net;
using System.Net.Mail;

namespace SCG.CAD.ETAX.UTILITY.AdminTool
{
    public class RequestActionEmail
    {
        AdminToolHelper adminToolHelper = new AdminToolHelper();
        UtilityProfileCustomerController profileCustomerController = new UtilityProfileCustomerController();
        UtilityProfileEmailTypeController profileEmailTypeController = new UtilityProfileEmailTypeController();
        UtilityProfileEmailTemplateController profileEmailTemplateController = new UtilityProfileEmailTemplateController();
        UtilityProfileCompanyController profileCompanyController = new UtilityProfileCompanyController();
        UtilityConfigMftsEmailSettingController configMftsEmailSettingController = new UtilityConfigMftsEmailSettingController();


        List<ProfileCustomer> profileCustomers = new List<ProfileCustomer>();

        List<ProfileEmailType> profileEmailType = new List<ProfileEmailType>();
        List<ProfileEmailTemplate> profileEmailTemplates = new List<ProfileEmailTemplate>();
        List<ProfileCompany> profileCompanies = new List<ProfileCompany>();
        List<ConfigMftsEmailSetting> configMftsEmailSettings = new List<ConfigMftsEmailSetting>();

        public Response SendEmail(string requestNo, string action)
        {
            Response res = new Response();
            ProfileEmailTemplate profileEmailTemplate = new ProfileEmailTemplate();
            ConfigMftsEmailSetting configEmail = new ConfigMftsEmailSetting();
            bool result = false;
            string subjectemail = "";
            string toName = "";
            string toEmail = "cadadt02@scg.com";
            string ccEmail = "";
            int emailType = 0;
            try
            {
                GetDataFromDataBase();
                var request = adminToolHelper.GetRequest(requestNo);
                var dataTables = adminToolHelper.GetRequestItemTransaction(requestNo);
                if (request != null && request.Count > 0)
                {

                    emailType = profileEmailType.FirstOrDefault(x => x.EmailTypeCode == "request").EmailTypeNo;

                    if (action == "submit")
                    {
                        profileEmailTemplate = profileEmailTemplates.FirstOrDefault(x => x.EmailTypeNo == emailType && x.EmailTemplateCode == "submit");
                        subjectemail = profileEmailTemplate.EmailSubject.Replace("#$Action$#", request[0].RequestAction);
                        subjectemail = subjectemail.Replace("#$RequestNo$#", request[0].RequestNo);
                    }
                    else if (action == "reject" && string.IsNullOrEmpty(request[0].OfficerBy))
                    {
                        //manager reject
                        profileEmailTemplate = profileEmailTemplates.FirstOrDefault(x => x.EmailTypeNo == emailType && x.EmailTemplateCode == "reject");
                        subjectemail = profileEmailTemplate.EmailSubject.Replace("#$RequestNo$#", request[0].RequestNo);
                    }
                    else if (action == "reject" && !string.IsNullOrEmpty(request[0].OfficerBy))
                    {
                        //officer reject
                        profileEmailTemplate = profileEmailTemplates.FirstOrDefault(x => x.EmailTypeNo == emailType && x.EmailTemplateCode == "reject");
                        subjectemail = profileEmailTemplate.EmailSubject.Replace("#$RequestNo$#", request[0].RequestNo);
                    }

                    configEmail = configMftsEmailSettings.FirstOrDefault(x => x.ConfigMftsEmailSettingCompanyCode == request[0].CompanyCode);

                    result = SendEmailbyCompany(configEmail, profileEmailTemplate, request[0], subjectemail, dataTables, toName, toEmail, ccEmail);
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

        public void GetDataFromDataBase()
        {
            try
            {
                profileEmailType = profileEmailTypeController.List().Result;
                profileEmailTemplates = profileEmailTemplateController.List().Result;
                configMftsEmailSettings = configMftsEmailSettingController.List().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool SendEmailbyCompany(ConfigMftsEmailSetting config, ProfileEmailTemplate templateemail, Request data, string subjectemail, List<TransactionDescription> dataList, string toName, string toEmail, string ccEmail)
        {
            bool result = false;
            var document = "";
            try
            {
                if (templateemail != null)
                {
                    var fromEmailAddress = "etaxadm@scg.com";
                    var fromEmailPassword = "Thai2020";
                    var smtpHost = "outmail.scg.co.th";
                    var smtpPort = "25";
                    //var toEmailAddress = "cadadt02@scg.com";

                    string body = templateemail.EmailBody;
                    body = body.Replace("#$DearName$#", toName);
                    body = body.Replace("#$RequestNo$#", data.RequestNo);
                    body = body.Replace("#$Action$#", data.RequestAction);
                    body = body.Replace("#$RequestDate$#", data.CreateDate.ToString("dd-MM-yyyy"));
                    body = body.Replace("#$RequestBy$#", toName);
                    body = body.Replace("#$UrlApprove$#", "https://stackoverflow.com/questions/23789745/create-outlook-email-in-c-sharp-with-anchor-tag-in-body-that-has-a-target");
                    body = body.Replace("#$UrlReject$#", toName);
                    body = body.Replace("#$UrlEtaxApp$#", toName);

                    document = "<table class=\"table\">";
                    document += "<thead>";
                    document += "<tr>";
                    document += "<th>#</th>";
                    document += "<th>Billing No</th>";
                    document += "<th>Posting year</th>";
                    document += "<th>Company</th>";
                    document += "<th>Customer</th>";
                    document += "<th>IC/O2C</th>";
                    document += "<th>Document Type</th>";
                    document += "<th>Data Source</th>";
                    document += "</tr>";
                    document += "</thead>";

                    document += "<tbody>";
                    int running = 1;
                    foreach (var item in dataList)
                    {
                        var ic = item.Ic == 1 ? "IC" : "O2C";
                        document += "<tr>";
                        document += "<td>" + running.ToString() + "</td>";
                        document += "<td>" + item.BillingNumber + "</td>";
                        document += "<td>" + item.PostingYear + "</td>";
                        document += "<td>" + item.CompanyCode + "</td>";
                        document += "<td>" + item.CustomerName + "</td>";
                        document += "<td>" + ic + "</td>";
                        document += "<td>" + item.DocType + "</td>";
                        document += "<td>" + item.SourceName + "</td>";
                        document += "</tr>";
                        running += 1;
                    }

                    document += "</tbody>";
                    document += "</table>";

                    body = body.Replace("#$DataTable$#", document);

                    MailMessage message = new MailMessage();


                    message.From = new MailAddress(fromEmailAddress);
                    message.To.Add(new MailAddress(toEmail));
                    if (!string.IsNullOrEmpty(ccEmail))
                    {
                        message.CC.Add(new MailAddress(ccEmail));
                    }
                    message.Subject = subjectemail;
                    message.IsBodyHtml = true;
                    message.Body = body;

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

    }
}