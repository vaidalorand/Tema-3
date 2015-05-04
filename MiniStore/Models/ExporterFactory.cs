using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniStore.Models
{
    public class ExporterFactory
    {

        public Exporter createExporter(String type)
        {
            if (type == null)
            {
                return null;
            }

            if ("json".Equals(type))
            {
                return new JsonExporter();
            }
            else if("csv".Equals(type))
            {
                return new CsvExporter();
            }

            return null;
        }

    }
}