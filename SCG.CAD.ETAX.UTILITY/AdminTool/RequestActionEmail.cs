using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY.Controllers;
using System.Net;
using System.Net.Mail;

namespace SCG.CAD.ETAX.UTILITY.AdminTool
{
    public class RequestActionEmail
    {
        AdminToolHelper adminToolHelper = new AdminToolHelper();
        UtilityProfileEmailTypeController profileEmailTypeController = new UtilityProfileEmailTypeController();
        UtilityProfileEmailTemplateController profileEmailTemplateController = new UtilityProfileEmailTemplateController();
        UtilityConfigMftsEmailSettingController configMftsEmailSettingController = new UtilityConfigMftsEmailSettingController();
        UtilityProfileController profileController = new UtilityProfileController();


        List<ProfileEmailType> profileEmailType = new List<ProfileEmailType>();
        List<ProfileEmailTemplate> profileEmailTemplates = new List<ProfileEmailTemplate>();
        List<ConfigMftsEmailSetting> configMftsEmailSettings = new List<ConfigMftsEmailSetting>();

        public Response SendEmail(string requestNo, string action)
        {
            Response res = new Response();
            ProfileEmailTemplate profileEmailTemplate = new ProfileEmailTemplate();
            ConfigMftsEmailSetting configEmail = new ConfigMftsEmailSetting();
            var dataTables = new List<TransactionDescription>();
            res.STATUS = false;
            string subjectemail = "";
            string toName = "";
            string toEmail = "";
            string ccEmail = "";
            int emailType = 0;
            try
            {
                GetDataFromDataBase();
                var request = adminToolHelper.GetRequest(requestNo);

                if (request != null)
                {
                    if (request.RequestAction != Variable.RequestActionCode_ReSignNewTrans)
                    {
                        dataTables = adminToolHelper.GetRequestItemTransaction(requestNo);
                        dataTables = dataTables.OrderBy(t => t.BillingNumber).ToList();
                    }


                    emailType = profileEmailType.FirstOrDefault(x => x.EmailTypeCode == "request").EmailTypeNo;

                    if (action == "submit")
                    {
                        if (request.RequestAction == Variable.RequestActionCode_ReSignNewTrans || request.RequestAction == Variable.RequestActionCode_ReSignNewCert)
                        {
                            // กรณีที่ต้องให้ admin select path
                            profileEmailTemplate = profileEmailTemplates.FirstOrDefault(x => x.EmailTypeNo == emailType && x.EmailTemplateCode == "admin_check");
                            subjectemail = profileEmailTemplate.EmailSubject.Replace("#$Action$#", UtilityHelper.GetActionName(request.RequestAction));
                            subjectemail = subjectemail.Replace("#$RequestNo$#", request.RequestNo);
                            var userAdminList = profileController.ProfileUserManagementAllAdmin().Result;
                            foreach (var userAdmin in userAdminList)
                            {
                                toEmail += userAdmin.UserEmail + ";";
                                toName += userAdmin.FirstName + " " + userAdmin.LastName + ",";
                            }
                            if (toName != "")
                            {
                                toName = toName.Remove(toName.Length - 1);
                            }
                        }
                        else
                        {
                            profileEmailTemplate = profileEmailTemplates.FirstOrDefault(x => x.EmailTypeNo == emailType && x.EmailTemplateCode == "submit");
                            subjectemail = profileEmailTemplate.EmailSubject.Replace("#$Action$#", UtilityHelper.GetActionName(request.RequestAction));
                            subjectemail = subjectemail.Replace("#$RequestNo$#", request.RequestNo);

                            toEmail = request.ManagerEmail;
                            toName = request.ManagerName;
                        }

                    }
                    else if (action == "reject" && string.IsNullOrEmpty(request.OfficerEmail))
                    {
                        //manager reject
                        profileEmailTemplate = profileEmailTemplates.FirstOrDefault(x => x.EmailTypeNo == emailType && x.EmailTemplateCode == "reject");
                        subjectemail = profileEmailTemplate.EmailSubject.Replace("#$RequestNo$#", request.RequestNo);
                        toEmail = request.RequesterEmail;
                        toName = request.RequesterName;
                    }
                    else if (action == "reject" && !string.IsNullOrEmpty(request.OfficerEmail))
                    {
                        //officer reject
                        profileEmailTemplate = profileEmailTemplates.FirstOrDefault(x => x.EmailTypeNo == emailType && x.EmailTemplateCode == "reject");
                        subjectemail = profileEmailTemplate.EmailSubject.Replace("#$RequestNo$#", request.RequestNo);
                        toEmail = request.RequesterEmail;
                        ccEmail = request.ManagerEmail;
                        toName = request.RequesterName;
                    }

                    configEmail = configMftsEmailSettings.FirstOrDefault(x => x.ConfigMftsEmailSettingCompanyCode == request.CompanyCode);
                    //toEmail = "cadadt02@scg.com";
                    //if (!string.IsNullOrEmpty(ccEmail))
                    //    ccEmail = "cadadt02@scg.com";
                    res.STATUS = SendEmailbyCompany(configEmail, profileEmailTemplate, request, subjectemail, dataTables, toName, toEmail, ccEmail);
                    if (res.STATUS)
                    {
                        var history = request.RequestHistorys.OrderByDescending(t => t.CreateDate).FirstOrDefault();
                        history.SendEmail = true;
                        var jsonString = JsonConvert.SerializeObject(history);
                        adminToolHelper.UpdateRequestHistory(jsonString);
                    }
                }


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


        public bool SendEmailbyCompany(ConfigMftsEmailSetting config, ProfileEmailTemplate templateemail, RequestRelateDataModel data, string subjectemail, List<TransactionDescription> dataList, string toName, string toEmail, string ccEmail)
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
                    body = body.Replace("#$Action$#", UtilityHelper.GetActionName(data.RequestAction));
                    body = body.Replace("#$RequestDate$#", data.RequestDate.ToString("dd-MM-yyyy"));
                    body = body.Replace("#$RequestBy$#", data.RequesterName);
                    var urlWeb = new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("WebConfig")["WebBaseAddress"] ?? "";
                    var urlApprove = urlWeb + "RequestAction/ByEmail?token=" + UtilityHelper.EncodeSpecialChar(UtilityHelper.EncryptString(data.RequestNo + ";approve;" + data.ManagerEmail + ";" + data.ManagerName));
                    var urlReject = urlWeb + "RequestAction/ByEmail?token=" + UtilityHelper.EncodeSpecialChar(UtilityHelper.EncryptString(data.RequestNo + ";reject;" + data.ManagerEmail + ";" + data.ManagerName));

                    body = body.Replace("#$UrlApprove$#", urlApprove);
                    body = body.Replace("#$UrlReject$#", urlReject);
                    body = body.Replace("#$UrlEtaxApp$#", new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("WebConfig")["UrlEtaxAppforEmail"] ?? "");

                    if (data.RequestAction != Variable.RequestActionCode_ReSignNewTrans)
                    {
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
                    }
                    else
                    {
                        document = "<table class=\"table\">";
                        document += "<thead>";
                        document += "<tr>";
                        document += "<th>#</th>";
                        document += "<th>XML File</th>";
                        document += "<th>PDF File</th>";
                        document += "</tr>";
                        document += "</thead>";

                        document += "<tbody>";
                        int running = 1;
                        foreach (var item in data.RequestPaths)
                        {
                            document += "<tr>";
                            document += "<td>" + running.ToString() + "</td>";
                            document += "<td>" + item.PathXml + "</td>";
                            document += "<td>" + item.PathPdf + "</td>";
                            document += "</tr>";
                            running += 1;
                        }

                        document += "</tbody>";
                        document += "</table>";
                    }


                    body = body.Replace("#$DataTable$#", document);

                    MailMessage message = new MailMessage();


                    message.From = new MailAddress(fromEmailAddress);
                    foreach (var address in toEmail.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        message.To.Add(new MailAddress(address));
                    }
                    //message.To.Add(new MailAddress(toEmail));
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
