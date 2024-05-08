using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.db
{
    public class WriteData
    {
        public static QimenLog Write(QimenLog log)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (MyDbContext dbContext = new MyDbContext(connectionString))
            {
                // 查询所有在London的客户
                var customersInLondon = dbContext.QimenLogs.Add(log); 
                dbContext.SaveChanges();
                return customersInLondon;
            }
        }
    }
}