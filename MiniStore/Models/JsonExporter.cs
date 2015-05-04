using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Script.Serialization;

namespace MiniStore.Models
{
    public class JsonExporter : Exporter
    {
        

        public void export()
        {
            System.Diagnostics.Debug.Write("Export json");

            ProductDBContext db = new ProductDBContext();

            List<Product> productlist = new List<Product>();

            var all = db.Products.SqlQuery("select * from Products").ToList();

            foreach (var item in all)
            {
                int id = item.ProductID;
                string name = item.Name;
                int price = item.Price;

                Product p = new Product();
                p.ProductID = id;
                p.Name = name;
                p.Price = price;

                productlist.Add(p);


            }


            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(productlist);

            System.IO.File.WriteAllText(@"E:\myjson.json", json);

        }
    }
}