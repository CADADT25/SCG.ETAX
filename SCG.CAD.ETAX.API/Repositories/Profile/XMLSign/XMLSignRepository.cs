﻿using SCG.CAD.ETAX.MODEL.CustomModel;

namespace SCG.CAD.ETAX.API.Repositories
{
    public class XMLSignRepository : IXMLSignRepository
    {
        XMLSignService service = new XMLSignService();

        public async Task<Response> ProcessXMLSign(ConfigXmlSign configXmlSign, FileXML fileXML)
        {
            Response resp = new Response();

            try
            {
                resp = service.ProcessXMLSign(configXmlSign, fileXML);
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return await Task.FromResult(resp);
        }
    }
}
