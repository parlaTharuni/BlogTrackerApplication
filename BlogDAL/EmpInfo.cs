using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDAL
{
    public class EmpInfo
    {
        public int EmpInfoId { get; set; }
        [Index("UQ_EmailId", IsUnique = true)]
        [MaxLength(255)]
        public string EmailId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string PassCode { get; set; }
    }
}
