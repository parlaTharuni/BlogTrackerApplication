using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDAL
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<BlogDbContext>
    {
        public DbInitializer()
        {
            Seed(new BlogDbContext());
        }
        protected override void Seed(BlogDbContext context)
        {
            Console.WriteLine("DbInitializer Seed method is executing.");
            // Seed default data for AdminInfo table
            var admin = new AdminInfo
            {
                EmailId = "tharuni@admin.com",
                Password = "tha12345"
            };
            context.AdminInfos.Add(admin);
            // Add additional seeding for other tables if needed

            base.Seed(context);
        }

    }


}

