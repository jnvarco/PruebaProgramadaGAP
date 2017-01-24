using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperZapatos.Models
{
    public class Article
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public Nullable<int> total_in_shelf { get; set; }
        public Nullable<int> total_in_vault { get; set; }
        public Nullable<int> store_id { get; set; }
    }
}