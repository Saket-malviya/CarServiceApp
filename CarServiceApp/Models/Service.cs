using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarServiceApp.Models
{
    public class Service
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ServiceType { get; set; }
        public DateTime ServiceDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string Photos { get; set; }
    }
}