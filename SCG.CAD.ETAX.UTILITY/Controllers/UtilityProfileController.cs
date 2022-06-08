using SCG.CAD.ETAX.MODEL.etaxModel;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityProfileController
    {
        public async Task<List<ProfileCompany>> ProfileCompanyList()
        {
            Response resp = new Response();

            List<ProfileCompany> tran = new List<ProfileCompany>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileCompany/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileCompany>>(task.OUTPUT_DATA.ToString());
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }


            return tran;
        }
        public async Task<List<ProfileSeller>> ProfileSellerList()
        {
            Response resp = new Response();

            List<ProfileSeller> tran = new List<ProfileSeller>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileSeller/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileSeller>>(task.OUTPUT_DATA.ToString());
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            return tran;
        }

        public ProfileSeller ProfileAddress(List<ProfileSeller> profile, string comcode, string branchcode)
        {
            ProfileSeller profileSeller = new ProfileSeller();
            try
            {
                profileSeller = profile.Where(x => x.CompanyCode == comcode && x.BranchCode == branchcode).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }


            return profileSeller;
        }
    }
}
