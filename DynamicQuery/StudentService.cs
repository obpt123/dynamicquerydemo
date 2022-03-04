using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DynamicQuery
{
    public class StudentService
    {
        public List<Student> QueryByCondition(StudentQueryInfo queryInfo)
        {
            using (var dataContext = new EFCoreDataContext())
            {
                var offset = queryInfo.PageSize * (queryInfo.PageIndex - 1);
                var limit = queryInfo.PageSize;
                IQueryable<Student> students = dataContext.Students;
                if (!string.IsNullOrEmpty(queryInfo.Name))
                {
                    students = students.Where(p => p.Name == queryInfo.Name);
                }
                if (!string.IsNullOrEmpty(queryInfo.Tel))
                {
                    students = students.Where(p => p.Tel.Contains(queryInfo.Tel));
                }
                if (queryInfo.StartTime.HasValue)
                {
                    students = students.Where(p => p.Birthday > queryInfo.StartTime);
                }
                if (queryInfo.EndTime.HasValue)
                {
                    students = students.Where(p => p.Birthday > queryInfo.StartTime);
                }
                return students.Skip(offset).Take(limit).ToList();
            }
        }


    }
    public class PageInfo
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
    public class StudentQueryInfo : PageInfo
    {
        public string Name { get; set; }
        public string Tel { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

    }
}
