using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.DAL
{
    public class sqlRequest
    {
        public string GET_REQUEST_RUNNING()
        {
            string strSql = @"SELECT NEXT VALUE FOR [dbo].RequestRunning AS number";

            return strSql;
        }

        public string GET_REQUEST_ITEM_TRANSACTION(string requestNo)
        {
            string strSql = @"SELECT
                            t.transactionNo AS TransactionNo
                            ,t.billingNumber AS BillingNumber
                            ,t.billingDate AS BillingDate
                            ,t.billingYear AS BillingYear
                            ,t.processingDate AS ProcessingDate
                            ,t.companyCode AS CompanyCode
                            ,t.companyName AS CompanyName
                            ,t.customerId AS CustomerId
                            ,t.customerName AS CustomerName
                            ,t.soldTo                    AS SoldTo                                      
                            ,t.shipTo                    AS ShipTo                                      
                            ,t.billTo                    AS BillTo                                      
                            ,t.payer                     AS Payer                                       
                            ,t.sourceName                AS SourceName                                  
                            ,t.foc                       AS Foc                                         
                            ,t.ic                        AS Ic                                          
                            ,t.postingYear               AS PostingYear                                 
                            ,t.fiDoc                     AS FiDoc                                       
                            ,t.imageDocType              AS PmageDocType                                
                            ,t.docType                   AS DocType                                     
                            ,t.sellOrg                   AS SellOrg                                     
                            ,t.poNumber                  AS PoNumber                                    
                            ,t.typeInput                 AS TypeInput                                   
                            ,t.generateStatus            AS GenerateStatus                              
                            ,t.generateDetail            AS GenerateDetail                              
                            ,t.generateDateTime          AS GenerateDateTime                            
                            ,t.xmlSignStatus             AS XmlSignStatus                               
                            ,t.xmlSignDetail             AS XmlSignDetail                               
                            ,t.xmlSignDateTime           AS XmlSignDateTime                             
                            ,t.pdfSignStatus             AS PdfSignStatus                               
                            ,t.pdfSignDetail             AS PdfSignDetail                               
                            ,t.pdfSignDateTime           AS PdfSignDateTime                             
                            ,t.printStatus               AS PrintStatus                                 
                            ,t.printDetail               AS PrintDetail                                 
                            ,t.printDateTime             AS PrintDateTime                               
                            ,t.emailSendStatus           AS EmailSendStatus                             
                            ,t.emailSendDetail           AS EmailSendDetail                             
                            ,t.emailSendDateTime         AS EmailSendDateTime                           
                            ,t.xmlCompressStatus         AS XmlCompressStatus                           
                            ,t.xmlCompressDetail         AS XmlCompressDetail                           
                            ,t.xmlCompressDateTime       AS XmlCompressDateTime                         
                            ,t.pdfIndexingStatus         AS PdfIndexingStatus                           
                            ,t.pdfIndexingDetail         AS PdfIndexingDetail                           
                            ,t.pdfIndexingDateTime       AS PdfIndexingDateTime                         
                            ,t.pdfSignLocation           AS PdfSignLocation                             
                            ,t.xmlSignLocation           AS XmlSignLocation                             
                            ,t.outputXmlTransactionNo    AS OutputXmlTransactionNo                      
                            ,t.outputPdfTransactionNo    AS OutputPdfTransactionNo                      
                            ,t.outputMailTransactionNo   AS OutputMailTransactionNo                     
                            ,t.dmsAttachmentFileName     AS DmsAttachmentFileName                       
                            ,t.dmsAttachmentFilePath     AS DmsAttachmentFilePath                       
                            ,t.createBy                  AS CreateBy                                    
                            ,t.createDate                AS CreateDate                                  
                            ,t.updateBy                  AS UpdateBy                                    
                            ,t.updateDate                AS UpdateDate                                  
                            ,t.isactive                  AS Isactive                                    
                            ,t.oneTimeEmail              AS OneTimeEmail                                
                            ,t.sentRevenueDepartment     AS SentRevenueDepartment                       
                            ,t.cancelBilling             AS CancelBilling      
                            FROM [dbo].[request] AS r
                            INNER JOIN [dbo].[requestItem] AS rt ON r.id = rt.requestId
                            INNER JOIN [dbo].[transactionDescription] AS t ON rt.transactionNo = t.transactionNo
                            WHERE r.requestNo = '" + requestNo + "'";

            return strSql;
        }

    }
}
