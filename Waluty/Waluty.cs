﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waluty
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Rate
    {
        public string currency { get; set; }
        public string code { get; set; }
        public double mid { get; set; }
    }

    public class Root
    {
        public string table { get; set; }
        public string no { get; set; }
        public string effectiveDate { get; set; }
        public List<Rate> rates { get; set; }
    }


}
