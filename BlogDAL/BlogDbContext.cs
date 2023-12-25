using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDAL
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext() : base("name=BlogTrackerConnection")
        {
            Database.SetInitializer<BlogDbContext>(null);


        }
        public DbSet<AdminInfo> AdminInfos { get; set; }
        public DbSet<EmpInfo> EmpInfos { get; set; }


        public DbSet<BlogInfo> BlogInfos { get; set; }

       
    }

}