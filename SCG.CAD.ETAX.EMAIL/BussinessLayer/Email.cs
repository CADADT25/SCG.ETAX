using SCG.CAD.ETAX.EMAIL.Controller;
using SCG.CAD.ETAX.MODEL.etaxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.EMAIL.BussinessLayer
{
    public class Email
    {
        ConfigMftsEmailSettingController configMftsEmailSettingController = new ConfigMftsEmailSettingController();
        OutputSearchXmlZipController outputSearchXmlZip = new OutputSearchXmlZipController();

        List<ConfigMftsEmailSetting> configMftsEmailSettings = new List<ConfigMftsEmailSetting>();
        List<OutputSearchXmlZip> outputSearchXmlZips = new List<OutputSearchXmlZip>();

        public void ProcessSendEmail()
        {
            try
            {
                GetDataFromDataBase();
                List<OutputSearchXmlZip> fileXmlZip = new List<OutputSearchXmlZip>();
                foreach (var config in configMftsEmailSettings)
                {
                    fileXmlZip = outputSearchXmlZips.Where(x => x.OutputSearchXmlZipCompanyCode == config.ConfigMftsEmailSettingCompanyCode && x.Isactive == 1).ToList();


                }
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
                outputSearchXmlZips = outputSearchXmlZip.List().Result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SendEmailbyCompany(List<OutputSearchXmlZip> fileXmlZips, ConfigMftsEmailSetting config)
        {
            bool result = false;
            try
            {
                var fromEmailAddress = config.ConfigMftsEmailSettingEmail;
                var fromEmailPassword = config.ConfigMftsEmailSettingPassword;
                var smtpHost = config.ConfigMftsEmailSettingHost;
                var smtpPort = config.ConfigMftsEmailSettingPort;
                var toEmailAddress = config.ConfigMftsEmailSettingEmail;

                string body = config.ConfigMftsEmailSettingEmailTemplate;
                MailMessage message = new MailMessage();

                //Setting From , To and CC
                message.From = new MailAddress(fromEmailAddress);
                message.To.Add(new MailAddress(toEmailAddress));
                message.Subject = "Thank You For Your Registration";
                message.IsBodyHtml = true;
                message.Body = body;
                foreach (var file in fileXmlZips)
                {
                    message.Attachments.Add(new Attachment(file.OutputSearchXmlZipFullPath));
                }
                //var client = new SmtpClient();
                try
                {
                    using (var client = new SmtpClient())
                    {
                        client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
                        client.Host = smtpHost;
                        client.EnableSsl = true;
                        client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
                        client.Send(message);
                    }
                }
                catch (Exception ex)
                {
                    result = false;
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
