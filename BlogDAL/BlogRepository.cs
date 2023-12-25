using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDAL
{
    public class BlogRepository : IBlogReposotiry
    {
        private readonly BlogDbContext _dbContext;
        private readonly EmpRepository empRepository;

        public BlogRepository(BlogDbContext blogDbContext, EmpRepository empRepository)
        {
            this._dbContext = blogDbContext;
            this.empRepository = empRepository;
        }
        public void AddBlogInfo(BlogInfo blogInfo)
        {
            _dbContext.BlogInfos.Add(blogInfo);
        }

        public void DeleteBlogInfo(int blogInfoId)
        {
            var blogInfo = _dbContext.BlogInfos.Find(blogInfoId);
            if (blogInfo != null)
                _dbContext.BlogInfos.Remove(blogInfo);
            _dbContext.SaveChanges();
        }

        public IEnumerable<BlogInfo> GetAllBlogInfos()
        {
            return _dbContext.BlogInfos.Include(blog => blog.Employee).ToList();
        }
        public IEnumerable<BlogInfo> GetBlogInfoByEmployeeId(string EmailId)
        {
            return _dbContext.BlogInfos
        .Include(b => b.Employee)  // Include the Employee navigation property
        .Where(b => b.Employee.EmailId == EmailId)  // Filter based on the Employee's EmailId
        .ToList();
        }
        public BlogInfo GetBlogInfoById(int blogInfoId)
        {
            return _dbContext.BlogInfos.Include(blog => blog.Employee).FirstOrDefault(blog => blog.BlogInfoId == blogInfoId);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void AddBlogWithForeignKey(string loggedInEmployeeEmail, BlogInfo blogInfo)
        {
            EmpInfo loggedInEmployeeEntity = empRepository.GetEmpInfoByEmialId(loggedInEmployeeEmail);

            if (_dbContext.Entry(loggedInEmployeeEntity).State == EntityState.Detached)
            {
                _dbContext.EmpInfos.Attach(loggedInEmployeeEntity);
            }

            blogInfo.Employee = loggedInEmployeeEntity;
            _dbContext.BlogInfos.Add(blogInfo);
            _dbContext.SaveChanges();
        }
        public void UpdateBlogInfo(BlogInfo blogInfo)
        {
            _dbContext.Entry(blogInfo).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}