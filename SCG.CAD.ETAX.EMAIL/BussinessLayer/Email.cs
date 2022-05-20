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
        TransactionDescriptionController transactionDescriptionController = new TransactionDescriptionController();

        List<ConfigMftsEmailSetting> configMftsEmailSettings = new List<ConfigMftsEmailSetting>();
        List<OutputSearchXmlZip> outputSearchXmlZips = new List<OutputSearchXmlZip>();
        List<TransactionDescription> transactionDescriptions = new List<TransactionDescription>();

        public void ProcessSendEmail()
        {
            try
            {
                GetDataFromDataBase();
                List<TransactionDescription> dataPDFsend = new List<TransactionDescription>();
                foreach (var config in configMftsEmailSettings)
                {
                    dataPDFsend = transactionDescriptions.Where(x => x.CompanyCode == config.ConfigMftsEmailSettingCompanyCode 
                                                                    && x.EmailSendStatus == "Waiting"
                                                                    && !String.IsNullOrEmpty(x.PdfSignLocation)
                                                                    && x.Isactive == 1).ToList();
                    if(dataPDFsend != null && dataPDFsend.Count > 0)
                    {
                        SendEmailbyCompany(dataPDFsend, config);
                    }

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
                transactionDescriptions = transactionDescriptionController.List().Result;

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

        public bool SendEmailbyCompany(List<TransactionDescription> dataPDFsend, ConfigMftsEmailSetting config)
        {
            bool result = false;
            try
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
                var toEmailAddress = "cadcadt25@scg.com";

                string body = "Test";//config.ConfigMftsEmailSettingEmailTemplate;
                MailMessage message = new MailMessage();

                //Setting From , To and CC
                message.From = new MailAddress(fromEmailAddress);
                message.To.Add(new MailAddress(toEmailAddress));
                message.Subject = "Thank You For Your Registration";
                message.IsBodyHtml = true;
                message.Body = body;
                foreach (var file in dataPDFsend)
                {
                    message.Attachments.Add(new Attachment(file.PdfSignLocation));
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
    }
}
