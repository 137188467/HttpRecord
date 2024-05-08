using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using WebApplication3.db;

namespace WebApplication3.Controllers
{
    public class WMSController : Controller
    {
        // GET: WMS
        public ActionResult Index()
        {
            return View();
        }

        public ContentResult Qimen()
        {
            XmlDocument xmlDoc = new XmlDocument();
            using (MyDbContext dbContext = new MyDbContext())
            {
                var userip = Request.UserHostAddress;
                var method = Request.HttpMethod;
                var body = string.Empty;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream))
                {
                    body = sr.ReadToEnd();
                }
                var log = new QimenLog
                {
                    AddDTM = DateTime.Now,
                    ModifyDTM = DateTime.Now,
                    BillNo = "",
                    Data = body,
                    Response = "",
                    QueryString = Request.QueryString.ToString(),
                    HttpMethod = method
                };


                log = dbContext.QimenLogs.Add(log);
                dbContext.SaveChanges();
                string billno = "";
                int billtype = 0;
                var methodname = Request.QueryString["method"].ToString();
                log.MethodName = methodname;
                if (methodname.Contains("qimen.deliveryorder.create"))
                {
                    billno = XmlReaderExample.GetDeliveryOrderCode(body, "/request/deliveryOrder/deliveryOrderCode");
                    billtype = 1;
                    log.BillNo = billno;
                }
                else if (methodname.Contains("qimen.returnorder.create"))
                {
                     billno = XmlReaderExample.GetDeliveryOrderCode(body, "/request/returnOrder/returnOrderCode");
                    billtype = 2;
                    log.BillNo = billno;
                }
                else if(methodname.Contains("order.cancel"))
                {
                    billno = XmlReaderExample.GetDeliveryOrderCode(body, "/request/orderCode");
                    log.BillNo = billno;
                    //更新订单的状态为取消。
                    var query = from c in dbContext.QimenLogs
                                where c.BillNo == billno
                                select c;
                    if(query.Count()==0)
                    {
                        //如果找不到，需要返回错误。
                        xmlDoc = getCalcelResult(new result
                        {
                            flag = result.Flag.failure,
                            code = -88,
                            message = $"订单{billno}找不到",
                            success = result.Success.@false,

                        });

                        log.Response = xmlDoc.OuterXml;
                        dbContext.SaveChanges();

                        return Content(xmlDoc.OuterXml, "application/xml");
                    }
                    foreach(var one in query)
                    {
                        one.status = 1;
                    }
                    dbContext.SaveChanges();
                }

                log.BillType = billtype;

            


             
                if (methodname.Contains("qimen.deliveryorder.create"))
                    xmlDoc = getDeliveryResult();
                else if (methodname.Contains("qimen.returnorder.create"))
                    xmlDoc = getReturnResult();
                else if (methodname.Contains("order.cancel"))
                    xmlDoc = getCalcelResult(new result 
                    {
                     flag= result.Flag.success,
                     code=0,
                     message= "成功",
                     success= result.Success.@true,
                      
                    });

                log.Response = xmlDoc.OuterXml;
                dbContext.SaveChanges();

                return Content(xmlDoc.OuterXml, "application/xml");
            }
        }

        private static XmlDocument getDeliveryResult()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("response");
            xmlDoc.AppendChild(root);

            XmlElement flag = xmlDoc.CreateElement("flag");
            flag.InnerText = "success";
            root.AppendChild(flag);

            XmlElement code = xmlDoc.CreateElement("code");
            code.InnerText = "0";
            root.AppendChild(code);

            XmlElement message = xmlDoc.CreateElement("message");
            message.InnerText = "成功";
            root.AppendChild(message);

            XmlElement entryOrderId = xmlDoc.CreateElement("entryOrderId");
            entryOrderId.InnerText = "I0251EO231105002772";
            root.AppendChild(entryOrderId);

            XmlElement deliveryOrderId = xmlDoc.CreateElement("deliveryOrderId");
            deliveryOrderId.InnerText = "I0251EO231105002772";
            root.AppendChild(deliveryOrderId);

            XmlElement success = xmlDoc.CreateElement("success");
            success.InnerText = "true";
            root.AppendChild(success);
            return xmlDoc;
        }

        private static XmlDocument getReturnResult()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode responseNode = xmlDoc.CreateElement("response");
            xmlDoc.AppendChild(responseNode);

            XmlNode flagNode = xmlDoc.CreateElement("flag");
            flagNode.InnerText = "success";
            responseNode.AppendChild(flagNode);

            XmlNode codeNode = xmlDoc.CreateElement("code");
            codeNode.InnerText = "0";
            responseNode.AppendChild(codeNode);

            XmlNode messageNode = xmlDoc.CreateElement("message");
            messageNode.InnerText = "成功";
            responseNode.AppendChild(messageNode);

            XmlNode returnOrderIdNode = xmlDoc.CreateElement("returnOrderId");
            returnOrderIdNode.InnerText = "21SR231105001532";
            responseNode.AppendChild(returnOrderIdNode);

            XmlNode successNode = xmlDoc.CreateElement("success");
            successNode.InnerText = "true";
            responseNode.AppendChild(successNode);
            return xmlDoc;
        }

        private static XmlDocument getCalcelResult(result r)
        {
            // 创建XML文档对象
            XmlDocument xmlDoc = new XmlDocument();

            // 创建根节点
            XmlElement rootElement = xmlDoc.CreateElement("response");
            xmlDoc.AppendChild(rootElement);

            // 创建子节点
            XmlElement flagElement = xmlDoc.CreateElement("flag");
            flagElement.InnerText = r.flag.ToString();
            rootElement.AppendChild(flagElement);

            XmlElement codeElement = xmlDoc.CreateElement("code");
            codeElement.InnerText = r.code.ToString();
            rootElement.AppendChild(codeElement);

            XmlElement messageElement = xmlDoc.CreateElement("message");
            messageElement.InnerText = r.message;
            rootElement.AppendChild(messageElement);

            XmlElement successElement = xmlDoc.CreateElement("success");
            successElement.InnerText = r.success.ToString();
            rootElement.AppendChild(successElement);
            return xmlDoc;
        }

        public class result
        {
            public enum Flag { success , failure }

            public enum Success { @false, @true }

            public Flag flag { get; set; }

            public int code { get; set; }

            public string message { get; set; }
            public Success success { get; set; }

        }
    }
}