using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppServiceLayer.Models
{
    public class EmpInfoModel
    {
        public int EmpInfoId { get; set; }
        public string EmailId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string PassCode { get; set; }
    }
}