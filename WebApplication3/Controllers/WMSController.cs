using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

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
            var userip = Request.UserHostAddress;
            var method = Request.HttpMethod;
            var body = string.Empty;
            using (System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream))
            {
                body = sr.ReadToEnd();
            }
            var header = Request.Headers.ToString();
            var path = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());
            var file = System.IO.Path.Combine(path, "data.txt");
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(file, true))
            {
                sw.WriteLine("time:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sw.WriteLine("body:" + body);
                sw.WriteLine("header:" + header);
                sw.WriteLine("ip:" + userip);
                sw.WriteLine("query:" + Request.QueryString.ToString());
                sw.WriteLine("method:" + Request.HttpMethod);
            }


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

            return Content(xmlDoc.OuterXml, "application/xml");
        }

    }
}