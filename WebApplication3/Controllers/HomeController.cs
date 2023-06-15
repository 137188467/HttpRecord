using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        public static object i = new object();
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