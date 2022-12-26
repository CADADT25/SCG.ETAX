using SCG.CAD.ETAX.API.Repositories.Profile.ConnectHSM;
using SCG.CAD.ETAX.UTILITY;
using SCG.CAD.ETAX.UTILITY.Controllers;

namespace SCG.CAD.ETAX.API.Services
{
    public class APISignService
    {
        public async Task<Response> SyncCertificate()
        {
            var repo = new ConnectHSMRepository();
            Response res = new Response();
            List<CertificateMaster> listcertificateMaster = new List<CertificateMaster>();
            UtilityAPISignController utilityAPISignController = new UtilityAPISignController();
            CertificateMaster certificateMaster = new CertificateMaster();
            EncodeHelper encodeHelper = new EncodeHelper();

            res.STATUS = false;
            DateTime dateTime = DateTime.Now;
            string hsmname = "pse";
            string slotpassword = encodeHelper.Base64Encode("P@ssw0rd1");
            try
            {
                //var hsmSerial = repo.GetHSMSerial(hsmname).Result;
                var hsmSerial = utilityAPISignController.GetHSMSerialwithAPI(hsmname).Result;
                if(hsmSerial.resultCode != null)
                {
                    if (hsmSerial.resultCode.Equals("000"))
                    {
                        foreach(var itemhsmSerial in hsmSerial.hsmSerialList)
                        {
                            //var keyAlias = repo.GetKeyAlias(hsmname, itemhsmSerial.hsmSerial).Result;
                            var keyAlias = utilityAPISignController.GetKeyAliaswithAPI(hsmname, itemhsmSerial.hsmSerial).Result;
                            if (keyAlias.resultCode != null)
                            {
                                if (keyAlias.resultCode.Equals("000"))
                                {
                                    foreach(var itemkeyAlias in keyAlias.keyAliasList)
                                    {
                                        certificateMaster = new CertificateMaster();
                                        certificateMaster.CertificateName = itemkeyAlias.commonName;
                                        certificateMaster.CertificateHsmname = hsmname;
                                        certificateMaster.CertificateHsmserial = itemhsmSerial.hsmSerial;
                                        certificateMaster.CertificateCertSerial = itemkeyAlias.certSerial;
                                        certificateMaster.CertificateKeyAlias = itemkeyAlias.keyAlias;
                                        certificateMaster.CertificateSlotPassword = slotpassword;
                                        certificateMaster.CertificateStartDate = itemkeyAlias.startDate;
                                        certificateMaster.CertificateEndDate = itemkeyAlias.endDate;
                                        certificateMaster.CreateBy = "AutoSync";
                                        certificateMaster.CreateDate = dateTime;
                                        certificateMaster.UpdateBy = "AutoSync";
                                        certificateMaster.UpdateDate = dateTime;
                                        certificateMaster.Isactive = 1;
                                        listcertificateMaster.Add(certificateMaster);
                                    }
                                }
                            }
                        }
                    }
                }

                if(listcertificateMaster.Count > 0)
                {
                    res = UpdateCertificateMaster(listcertificateMaster);
                }
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.Message.ToString();
            }
            return res;
        }

        public Response UpdateCertificateMaster(List<CertificateMaster> listcertificateMaster)
        {
            Response res = new Response();
            res.STATUS = false;
            try
            {
                using (var _dbContext = new DatabaseContext())
                {
                    _dbContext.Database.ExecuteSqlRaw("Truncate table CertificateMaster");
                    _dbContext.AddRange(listcertificateMaster);
                    res.STATUS = true;
                }
            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.ERROR_MESSAGE = ex.Message.ToString();
            }
            return res;
        }
    }
}
