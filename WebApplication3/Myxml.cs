 

namespace WebApplication3
{
    using System;
    using System.Xml;

    public class XmlReaderExample
    {
        public static string GetDeliveryOrderCode(string xmlString,string xmlnode)
        {
            string deliveryOrderCode = null;

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlString.Trim());

                XmlNode deliveryOrderCodeNode = xmlDoc.SelectSingleNode(xmlnode);

                if (deliveryOrderCodeNode != null)
                {
                    deliveryOrderCode = deliveryOrderCodeNode.InnerText;
                }
            }
            catch (Exception ex)
            {
                // 处理XML解析过程中可能发生的任何异常
                Console.WriteLine("错误: " + ex.Message);
            }

            return deliveryOrderCode;
        }


    }

}