using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.XML.GENERATOR.Controller;
using SCG.CAD.ETAX.XML.GENERATOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.XML.GENERATOR.BussinessLayer
{
    public class TextFileValidate
    {

        public List<string> ValidateTextFile(TextFileSchematic textfile, List<ProfileBranch> profileBranches, List<ProductUnit> productUnit)
        {
            LogicTool tool = new LogicTool();
            List<string> result = new List<string>();
            bool checkproductname = false;
            int countlengthpo = 0;
            bool checksalesunit = false;
            bool checkprice = false;
            try
            {
                if (tool.CheckDataRule(textfile.BUYER_PSTLZ, "") || tool.CheckDataRule(textfile.SELLER_PSTLZ, ""))
                {
                    result.Add("Failed ไม่มีรหัสไปรษณีย์");
                }
                if (tool.CheckDataRule(textfile.FI_DOC_TYPE, "") || tool.CheckDataRule(textfile.BILLING_NO, ""))
                {
                    result.Add("Failed เลขทีอ้างอิง หรือประเภทเอกสารอ้างอิงไม่ครบถ้วน");
                }
                if (textfile.Item == null || textfile.Item.Count == 0)
                {
                    result.Add("Failed ไม่มีข้อมูล Item");
                }
                else
                {
                    foreach (var item in textfile.Item)
                    {
                        if (checkproductname == false && tool.CheckDataRule(item.PRODUCT_NAME, ""))
                        {
                            checkproductname = true;
                        }
                        if (checksalesunit == false && productUnit.FirstOrDefault(x=> x.ProductUnitErp == item.SALES_UNT)  == null)// map database
                        {
                            checksalesunit = true;
                        }
                        try
                        {
                            if (checkprice == false && 
                                (Convert.ToDecimal(item.UNIT_PRICT) < 0 || 
                                Convert.ToDecimal(item.NETUNIT_PRICE) < 0 ||
                                Convert.ToDecimal(item.TAX_AMOUNT) < 0 || 
                                Convert.ToDecimal(item.TAX_BASIS_AMOUNT) < 0 ||
                                Convert.ToDecimal(item.TAX_TOTAL_AMOUNT) < 0 || 
                                Convert.ToDecimal(item.GRAND_TOTAL_AMOUNT) < 0))
                            {
                                checkprice = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            checkprice = true;
                        }
                        countlengthpo += item.PO_NUMBER.Length;
                    }
                }
                if (checkproductname)
                {
                    result.Add("Failed ชื่อสินค้ามีค่าเป็น Null");
                }
                if(countlengthpo > 255)
                {
                    result.Add("Failed เลขที่ PO ที่อ้างอิงในแต่ละ item เมื่อมารวมกันยาวเกินกว่าที่กำหนด");
                }
                if(profileBranches.FirstOrDefault(x=> x.ProfileBranchCode == textfile.SELLER_BRANCH) == null)
                {
                    //result.Add("Failed Branch " + textfile.SELLER_BRANCH + " ไม่ได้ mapping กับ e - Tax");
                }
                if (checksalesunit)
                {
                    result.Add("Failled มีอักษรพิเศษใน Unit of Measure");
                }
                try
                {
                    if (checkprice ||(Convert.ToDecimal(textfile.SALES_AMOUNT) < 0 || 
                        Convert.ToDecimal(textfile.TAX_AMOUNT) < 0 || 
                        //Convert.ToDecimal(textfile.CORRECT_AMOUNT) < 0 || 
                        //Convert.ToDecimal(textfile.DIFFERENCE_AMOUNT) < 0 ||
                        Convert.ToDecimal(textfile.GRAND_TOTAL_AMOUNT) < 0 || 
                        //Convert.ToDecimal(textfile.ORIGINAL_AMOUNT) < 0 || 
                        Convert.ToDecimal(textfile.TAX_BASIS_AMOUNT) < 0 ||
                        Convert.ToDecimal(textfile.TAX_TOTAL_AMOUNT) < 0))
                    {
                        result.Add("Failed จำนวนเงินติดลบ");
                    }
                }
                catch (Exception ex)
                {
                    result.Add("Failed จำนวนเงินติดลบ");
                }
                string s = JsonConvert.SerializeObject(textfile);
                if (tool.CheckSpecialChar(s))
                {
                    result.Add("Failled มีอักษรพิเศษใน Text file");
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
