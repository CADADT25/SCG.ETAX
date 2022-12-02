using OfficeOpenXml;
using System.Text;

namespace SCG.CAD.ETAX.API.Services
{
    public class ProfileCompanyService
    {

        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));

        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.profileCompany.ToList();

                if (getList.Count > 0)
                {
                    resp.STATUS = true;
                    resp.MESSAGE = "Get list count '" + getList.Count + "' records. ";
                    resp.OUTPUT_DATA = getList;
                }
                else
                {
                    resp.STATUS = false;
                    resp.MESSAGE = "Data not found";
                }

            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

        public Response GET_DETAIL(int id)
        {
            Response resp = new Response();

            try
            {
                var getList = _dbContext.profileCompany.Where(x => x.CompanyNo == id).ToList();

                if (getList.Count > 0)
                {
                    resp.STATUS = true;
                    resp.MESSAGE = "Get data from ID '" + id + "' success. ";
                    resp.OUTPUT_DATA = getList;
                }
                else
                {
                    resp.STATUS = false;
                    resp.MESSAGE = "Data not found";
                }

            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

        public Response INSERT(ProfileCompany param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var getDuplicate = _dbContext.profileCompany.Where(x => x.CompanyCode == param.CompanyCode || x.TaxNumber == param.TaxNumber).ToList();

                    if (getDuplicate.Count <= 0)
                    {
                        param.CreateDate = dtNow;
                        param.UpdateDate = dtNow;

                        _dbContext.profileCompany.Add(param);
                        _dbContext.SaveChanges();


                        resp.STATUS = true;
                        resp.MESSAGE = "Insert success.";
                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.ERROR_MESSAGE = "Can't insert new record becuase Company Code or TaxNumber is duplicate.";
                    }



                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Insert faild.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

        public Response UPDATE(ProfileCompany param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.profileCompany.Where(x => x.CompanyNo == param.CompanyNo).FirstOrDefault();

                    if (update != null)
                    {
                        update.CompanyCode = param.CompanyCode;
                        update.CompanyNameTh = param.CompanyNameTh;
                        update.CompanyNameEn = param.CompanyNameEn;
                        update.TaxNumber = param.TaxNumber;
                        //update.CertificateProfileNo = param.CertificateProfileNo;
                        update.IsEbill = param.IsEbill;
                        update.UpdateBy = param.UpdateBy;
                        update.UpdateDate = dtNow;
                        update.Isactive = param.Isactive;

                        _dbContext.SaveChanges();

                        resp.STATUS = true;
                        resp.MESSAGE = "Updated Success.";
                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.MESSAGE = "Can't update because data not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Update faild.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

        public Response DELETE(ProfileCompany param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.profileCompany.Find(param.CompanyNo);

                    if (delete != null)
                    {
                        _dbContext.profileCompany.Remove(delete);
                        _dbContext.SaveChanges();

                        resp.STATUS = true;
                        resp.MESSAGE = "Delete success.";
                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.MESSAGE = "Can't delete because data not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Delete faild.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

        public Response ExportDataProfileCompany()
        {
            Response resp = new Response();
            string path = "C:\\FileExport\\";
            string filename = "scg-etax-ProfileCompany.xlsx";
            try
            {
                using (_dbContext)
                {
                    var getList = _dbContext.profileCompany.ToList();

                    if (getList.Count > 0)
                    {
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        if (File.Exists(path + filename))
                        {
                            File.Delete(path + filename);
                        }

                        ExcelPackage ExcelPkg = new ExcelPackage();
                        ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                        wsSheet1.Cells["A1"].Value = "CompanyNo";
                        wsSheet1.Cells["B1"].Value = "CompanyCode";
                        wsSheet1.Cells["C1"].Value = "CompanyNameTh";
                        wsSheet1.Cells["D1"].Value = "CompanyNameEn";
                        wsSheet1.Cells["E1"].Value = "CertificateProfileNo";
                        wsSheet1.Cells["F1"].Value = "TaxNumber";
                        wsSheet1.Cells["G1"].Value = "IsEbill";
                        wsSheet1.Cells["H1"].Value = "CreateBy";
                        wsSheet1.Cells["I1"].Value = "CreateDate";
                        wsSheet1.Cells["J1"].Value = "UpdateBy";
                        wsSheet1.Cells["K1"].Value = "UpdateDate";
                        wsSheet1.Cells["L1"].Value = "Isactive";

                        for (int x = 1; x <= getList.Count; x++)
                        {
                            wsSheet1.Cells[x + 1, 1].Value = getList[x - 1].CompanyNo;
                            wsSheet1.Cells[x + 1, 2].Value = getList[x - 1].CompanyCode;
                            wsSheet1.Cells[x + 1, 3].Value = getList[x - 1].CompanyNameTh;
                            wsSheet1.Cells[x + 1, 4].Value = getList[x - 1].CompanyNameEn;
                            wsSheet1.Cells[x + 1, 5].Value = getList[x - 1].CertificateProfileNo;
                            wsSheet1.Cells[x + 1, 6].Value = getList[x - 1].TaxNumber;
                            wsSheet1.Cells[x + 1, 7].Value = getList[x - 1].IsEbill;
                            wsSheet1.Cells[x + 1, 8].Value = getList[x - 1].CreateBy;
                            wsSheet1.Cells[x + 1, 9].Value = getList[x - 1].CreateDate.ToString("yyyy-MM-dd hh:mm:ss");
                            wsSheet1.Cells[x + 1, 10].Value = getList[x - 1].UpdateBy;
                            wsSheet1.Cells[x + 1, 11].Value = getList[x - 1].UpdateDate.ToString("yyyy-MM-dd hh:mm:ss");
                            wsSheet1.Cells[x + 1, 12].Value = getList[x - 1].Isactive;
                        }

                        wsSheet1.Protection.IsProtected = false;
                        wsSheet1.Protection.AllowSelectLockedCells = false;
                        ExcelPkg.SaveAs(new FileInfo(path + filename));

                        byte[] bytes = File.ReadAllBytes(path + filename);
                        resp.OUTPUT_DATA = Convert.ToBase64String(bytes, 0, bytes.Length);
                        resp.STATUS = true;
                        resp.MESSAGE = filename;

                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.MESSAGE = "Can't update because data not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resp;
        }

    }
}
