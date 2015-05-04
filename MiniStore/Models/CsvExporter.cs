using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace MiniStore.Models
{
    public class CsvExporter :  Exporter
    {
        ProductDBContext db = new ProductDBContext();

        public void export()
        {
            System.Diagnostics.Debug.Write("Export csv");

            ProductDBContext db = new ProductDBContext();

            var all = db.Products.SqlQuery("select * from Products").ToList();

            string filePath = "E:\\mycsv.csv";

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            string delimiter = " , ";

            foreach (var item in all)
            {
                string id = item.ProductID.ToString();
                string name = item.Name;
                string price = item.Price.ToString();

                string[][]  output = new string[][] { new string[] { id , name , price } };

                int length = output.GetLength(0);

                StringBuilder sb = new StringBuilder();

                for (int index = 0; index < length; index++)
                    sb.AppendLine(string.Join(delimiter, output[index]));

                File.AppendAllText(filePath, sb.ToString());

            }


        }
    }
}