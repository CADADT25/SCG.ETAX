﻿using DocumentFormat.OpenXml.Vml.Office;
using SCG.CAD.ETAX.API.Services;
using SCG.CAD.ETAX.MODEL;
using System.Xml.Linq;
using System.Xml.Serialization;
using SCG.CAD.ETAX.MODEL.Revenue.ETDA.CodeList.Provice;
using SCG.CAD.ETAX.MODEL.Revenue.ETDA.CodeList.City;
using SCG.CAD.ETAX.MODEL.Revenue.ETDA.CodeList.SubDivision;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using static SCG.CAD.ETAX.MODEL.Revenue.ETDA.CodeList.Provice.ThaiISOCountrySubdivisionCodeModel;

namespace SCG.CAD.ETAX.API.Repositories
{
    public class ETDARepository : IETDARepository
    {
        ETDAService eTDAService = new ETDAService();

        public Task<Response> GetThaiDocumentNameCode()
        {
            throw new NotImplementedException();
        }

        public async Task<Response> GetThaiISOCountrySubdivisionCode()
        {
            List<ETDAProvice> eTDAProvice = new List<ETDAProvice>();

            Response resp = new Response();

            try
            {
                var getXml = eTDAService.ReadXmlThaiISOCountrySubdivisionCode();

                if (!string.IsNullOrEmpty(getXml))
                {
                    eTDAProvice = eTDAService.MappingToObject(getXml);

                    if (eTDAProvice.Count() > 0)
                    {
                        resp.STATUS = true;
                        resp.OUTPUT_DATA = eTDAProvice;
                    }
                }
            }

            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.ToString();
            }

            return resp;
        }

        public Task<Response> GetThaiMessageFunctionCode()
        {
            throw new NotImplementedException();
        }

        public Task<Response> GetThaiPurposeCode_Invoice()
        {
            throw new NotImplementedException();
        }

        public Task<Response> GetThai_MessageFunctionCode()
        {
            throw new NotImplementedException();
        }

        public Task<Response> GetTISICityName()
        {
            throw new NotImplementedException();
        }

        public Task<Response> GetTISICitySubDivisionName()
        {
            throw new NotImplementedException();
        }
    }
}
