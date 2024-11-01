using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static WebApplication3.Controllers.WMSController.result;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        public static object i = new object();

        //http://localhost:8081
        public ActionResult Index()
        {
            var userip = Request.UserHostAddress;
            if (Request.UserHostAddress != null)
            {
                Int64 macinfo = new Int64();
                string macSrc = macinfo.ToString("X");
                
            }

            lock (i)
            {
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
                    sw.WriteLine("time:"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sw.WriteLine("body:"+body);
                    sw.WriteLine("header:"+header);
                    sw.WriteLine("ip:"+userip);
                    sw.WriteLine("query:"+Request.QueryString.ToString());
                    sw.WriteLine("method:" + Request.HttpMethod);
                }
            }
          
            return View();
        }

        /// <summary>
        /// 天猫国际奇门推单测试例子。
        /// </summary>
        /// <returns></returns>
        public ActionResult QiMenConfirmTest()
        {
            var userip = Request.UserHostAddress;
            if (Request.UserHostAddress != null)
            {
                Int64 macinfo = new Int64();
                string macSrc = macinfo.ToString("X");

            }

            lock (i)
            {
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
            }

            return Content(@"<response>
                <success>false</success>
                <errorCode>89</errorCode>
                <errorMsg>订单解析失败</errorMsg>
                <retry>false</retry>
            </response>", "application/xml");


//            return Content(@"
//<response>
//    <success>true</success>
//    <errorCode>0</errorCode>
//    <errorMsg></errorMsg>
//    <retry>false</retry>
//</response>
//", "application/xml");

            //            return Content(@"
            //<response>
            //    <success>true</success>
            //    <errorCode>0</errorCode>
            //    <errorMsg></errorMsg>
            //    <retry>false</retry>
            //</response>
            //", "application/xml");
            //return Json(new 
            //{
            //    response =new{
            //    success = true,
            //    errorCode = 0,
            //    errorMsg = "",
            //    retry = false}


            //}, JsonRequestBehavior.AllowGet);
        }


        //http://localhost:8081/wxcd6ca419b834cecd/callback
        //[HttpPost]
        //[HttpGet]
        public ActionResult Callback(string appid)
        {
            // 在这里处理接口逻辑
            // 使用 appid 参数进行进一步的操作
            // 返回相应的结果
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
