using SCG.CAD.ETAX.MODEL.etaxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SCG.CAD.ETAX.UTILITY.Controllers;

namespace SCG.CAD.ETAX.EMAIL.BussinessLayer
{
    public class TestEmail
    {
        ConfigGlobalController configGlobalController = new ConfigGlobalController();
        List<ConfigGlobal> configGlobals = new List<ConfigGlobal>();
        ConfigGlobal configGlobal = new ConfigGlobal();
        string pathoutputpdfsendemail;
        string pathoutputcontentemail;

        public void TestSendEmail()
        {
            try
            {
                GetDataFromDataBase();
                GetConfig();
                List<string> filePDFsend = new List<string>();
                filePDFsend.Add(@"F:\003020204000001696.pdf");
                filePDFsend.Add(@"F:\003020204000001697.pdf");
                Console.WriteLine("Start Test SendEmail");
                Console.WriteLine("pathoutputpdfsendemail : " + pathoutputpdfsendemail);
                Console.WriteLine("pathoutputcontentemail : " + pathoutputcontentemail);
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
                string billno = "";
                string body = "";
                body += "<p><span style='font-size: 11.998px; letter-spacing: 0.14px;'>[LOGO]</span></p><p>";
                body += "</p><p></p><p></p><br><p></p><table class='MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='80%' style='width:80.0%;mso-cellspacing:0in;mso-yfti-tbllook:1184;mso-padding-alt:";
                body += "0in 0in 0in 0in'><tbody><tr>";
                body += "<td style='padding:.75pt .75pt .75pt .75pt'>";
                body += "<p><b><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif'>เรียน</span></b><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif;mso-ascii-font-family:&quot;Times New Roman&quot;;";
                body += "mso-hansi-font-family:&quot;Times New Roman&quot;'> </span><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif'>[CUSTOMER-COMPANY-NAME]</span><u>([CUSTOMER-COMPANY-CODE])</u><o:p></o:p></p>";
                body += "</td>";
                body += "</tr>";
                body += "<tr>";
                body += "<td style='padding:.75pt .75pt .75pt .75pt'>";
                body += "<p><b><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif'>เรื่อง</span></b><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif;mso-ascii-font-family:&quot;Times New Roman&quot;;";
                body += "mso-hansi-font-family:&quot;Times New Roman&quot;'> </span><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif'>ขอนำส่ง ใบกำกับภาษีอิเล็กทรอนิกส์ /";
                body += "ใบลดหนี้ / ใบเพิ่มหนี้ ของวันที่ </span>[DATE]<o:p></o:p></p>";
                body += "</td>";
                body += "</tr>";
                body += "<tr>";
                body += "<td style='padding:.75pt .75pt .75pt .75pt'></td>";
                body += "</tr>";
                body += "<tr>";
                body += "<td style='padding:.75pt .75pt .75pt .75pt'>";
                body += "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span lang='TH' style='font-family:&quot;Angsana New&quot;,serif'>[COMPANY-NAME] ขอนำส่ง</span><span lang='TH' style='font-family:";
                body += "&quot;Angsana New&quot;,serif;mso-ascii-font-family:&quot;Times New Roman&quot;;mso-hansi-font-family:";
                body += "&quot;Times New Roman&quot;'> </span><o:p></o:p></p>";
                body += "</td>";
                body += "</tr>";
                body += "<tr style='mso-yfti-irow:4;height:24.75pt'>";
                body += "<td style='padding:.75pt .75pt .75pt .75pt;height:24.75pt'>";
                body += "<table class='MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100.0%;mso-cellspacing:0in;mso-yfti-tbllook:1184;mso-padding-alt:";
                body += "0in 0in 0in 0in'>";
                body += "<tbody><tr>";
                body += "<td width='10%' style='width:10.0%;padding:.75pt .75pt .75pt .75pt'></td>";
                body += "<td width='20%' style='width:20.0%;padding:.75pt .75pt .75pt .75pt'>";
                body += "<p class='MsoNormal'><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif'>[FILE-NAME]</span><o:p></o:p></p>";
                body += "</td>";
                body += "<td width='5%' style='width:5.0%;padding:.75pt .75pt .75pt .75pt'>";
                body += "<p class='MsoNormal'><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif'>เลขที่:</span><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif;mso-ascii-font-family:&quot;Times New Roman&quot;;";
                body += "mso-hansi-font-family:&quot;Times New Roman&quot;'> </span><o:p></o:p></p>";
                body += "</td>";
                body += "<td style='padding:.75pt .75pt .75pt .75pt'>";
                body += "<p class='MsoNormal'>[BILLING-NUMBER]<o:p></o:p></p>";
                body += "</td>";
                body += "</tr>";
                body += "</tbody></table>";
                body += "</td>";
                body += "</tr>";
                body += "<tr>";
                body += "<td style='padding:.75pt .75pt .75pt .75pt'>";
                body += "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span lang='TH' style='font-family:&quot;Angsana New&quot;,serif'>ตามเอกสารที่แนบมาด้วยใน </span>e-mail";
                body += "<span lang='TH' style='font-family:&quot;Angsana New&quot;,serif'>ฉบับนี้</span><o:p></o:p></p>";
                body += "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span lang='TH' style='font-family:&quot;Angsana New&quot;,serif'>กรณีต้องการสอบถามข้อมูลเพิ่มเติม";
                body += "โปรดติดต่อทีมขายของท่าน หรือ </span>e-mail : <a href='mailto:etax-cbmadm@scg.com'>etax-cbmadm@scg.com</a><o:p></o:p></p>";
                body += "</td>";
                body += "</tr>";
                body += "<tr style='mso-yfti-irow:6;mso-yfti-lastrow:yes;height:24.75pt'>";
                body += "<td style='padding:.75pt .75pt .75pt .75pt;height:24.75pt'>";
                body += "<table class='MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100.0%;mso-cellspacing:0in;mso-yfti-tbllook:1184;mso-padding-alt:";
                body += "0in 0in 0in 0in'>";
                body += "<tbody>";
                body += "<tr>";
                body += "<td style='padding:.75pt .75pt .75pt .75pt'>";
                body += "<p><b><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif'>ขอแสดงความนับถือ</span></b><o:p></o:p></p>";
                body += "</td>";
                body += "</tr>";
                body += "<tr>";
                body += "<td style='padding:.75pt .75pt .75pt .75pt'>";
                body += "[COMPANY-NAME]<br>";
                body += "</td>";
                body += "</tr>";
                body += "</tbody></table>";
                body += "<p><o:p>&nbsp;</o:p></p>";
                body += "<p><span style='color:blue'>*</span><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif;";
                body += "color:blue'>กรุณาอย่าตอบกลับ </span><span style='color:blue'>e-mail </span><span lang='TH' style='font-family:&quot;Angsana New&quot;,serif;color:blue'>ฉบับนี้ / </span><span style='color:blue'>Please do not reply to this e-mail*&nbsp;<o:p></o:p></span><span style='letter-spacing: 0.14px; background-color: rgb(255, 255, 255);'></span></p>";
                body += "<p>&nbsp;<o:p></o:p></p>";
                body += "</td>";
                body += "</tr>";
                body += "</tbody><tbody></tbody></table>";



                //config.ConfigMftsEmailSettingEmailTemplate;
                MailMessage message = new MailMessage();

                //Setting From , To and CC
                message.From = new MailAddress(fromEmailAddress);
                message.To.Add(new MailAddress(toEmailAddress));
                message.Subject = "นำส่ง [FILE-NAME] [COMPANY-NAME]";
                message.IsBodyHtml = true;
                message.Body = body;
                foreach (var file in filePDFsend)
                {
                    billno = Path.GetFileName(file).Replace(".pdf", "");
                    if (billno.IndexOf('_') > -1)
                    {
                        billno = billno.Substring(8, (billno.IndexOf('_')) - 8);
                    }
                    else
                    {
                        billno = billno.Substring(8);
                    }
                    message.Attachments.Add(new Attachment(file)
                    {
                        Name = "0090" + "_" + DateTime.Now.ToString("yyyy") + "_" + billno + ".pdf"
                    });
                }
                //var client = new SmtpClient();
                try
                {
                    using (var client = new SmtpClient())
                    {
                        client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        //client.PickupDirectoryLocation = @"G:\";
                        client.PickupDirectoryLocation = AppDomain.CurrentDomain.BaseDirectory;
                        client.Host = smtpHost;
                        client.EnableSsl = false;
                        client.UseDefaultCredentials = false;
                        client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
                        client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
                        client.Send(message);
                        //string root = AppDomain.CurrentDomain.BaseDirectory;

                        //string root = "F:\\";
                        //string pickupRoot = client.PickupDirectoryLocation.Replace("~/", root);
                        //pickupRoot = pickupRoot.Replace("/", @"\");
                        //client.PickupDirectoryLocation = pickupRoot;
                        Console.WriteLine("Save Email :" + client.PickupDirectoryLocation);
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
                    Console.WriteLine("Error : " + ex.Message);
                }
                Console.WriteLine("End Test SendEmail");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
            }
        }

        public void ToEmlStream()
        {
            FileInfo files = ReadEMLFile();
            string dummyEmail = "cadadt25@scg.com";
            var tempFolder = @"D:\Email";
            // tempFolder should contain 1 eml file

            //foreach (var file in files)
            //{
            //    // create new file and remove all lines that start with 'X-Sender:' or 'From:'
            //    string newFile = Path.Combine(tempFolder, "modified_" + DateTime.Now.ToString("yyyyMMddHHMMSSsss") + ".eml");
            //    using (var sr = new StreamReader(file))
            //    {
            //        using (var sw = new StreamWriter(newFile))
            //        {
            //            string line;
            //            while ((line = sr.ReadLine()) != null)
            //            {
            //                if (!line.StartsWith("X-Sender:") &&
            //                    !line.StartsWith("From:") &&
            //                    // dummy email which is used if receiver address is empty
            //                    !line.StartsWith("X-Receiver: " + dummyEmail) &&
            //                    // dummy email which is used if receiver address is empty
            //                    !line.StartsWith("To: " + dummyEmail))
            //                {
            //                    sw.WriteLine(line);
            //                }
            //            }
            //        }
            //    }
            //    var stream = new MemoryStream();
            //    // stream out the contents
            //    using (var fs = new FileStream(newFile, FileMode.Open))
            //    {
            //        fs.CopyTo(stream);
            //    }
            //}
        }

        public FileInfo ReadEMLFile()
        {
            FileInfo result;
            string[] fullpath = new string[0];
            string pathFolder = "";
            string fileType = "*.eml";
            try
            {
                pathFolder = @"D:\Email";
                //var fullpaths = DirectoryInfo.GetFiles(pathFolder);
                //result = fullpath.ToList();

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

        public bool MoveFile(string pathinput, string filename, DateTime billingdate)
        {
            bool result = false;
            //pathinpput = @"c:\temp\MySample.txt";
            //pathoutput = @"D:\sign\backupfile\";
            string output = "";
            try
            {
                output = @"D:\Email" + "\\" + billingdate.ToString("yyyy") + "\\" + billingdate.ToString("MM") + "\\";
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

        public void GetDataFromDataBase()
        {
            try
            {
                configGlobals = configGlobalController.List().Result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetConfig()
        {
            try
            {
                pathoutputpdfsendemail = configGlobals.FirstOrDefault(x => x.ConfigGlobalName == "PATHBACKUPPDFSENDEMAIL").ConfigGlobalValue;
                pathoutputcontentemail = configGlobals.FirstOrDefault(x => x.ConfigGlobalName == "PATHBACKUPEMAILCONTENT").ConfigGlobalValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
