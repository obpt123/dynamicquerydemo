using Microsoft.EntityFrameworkCore;
using System;

namespace DynamicQuery
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateDatabase();

            QueryStudents(10, 1, "zhang", "2", null, null);
        }
        static void CreateDatabase()
        {
            using (var context = new EFCoreDataContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Students.AddRange(new Student[] {
                    new Student{ Name="zhang", Tel="123", City="xi'an", Birthday= new DateTime(2022,3,4) },
                    new Student{ Name="li", Tel="456", City="xi'an", Birthday= new DateTime(2021,3,4) },
                    new Student{ Name="zhang", Tel="111", City="xi'an", Birthday= new DateTime(2021,3,4) },
                    new Student{ Name="wang", Tel="8888", City="xi'an", Birthday= new DateTime(2022,3,4) },
                });
                context.SaveChanges();
            }
        }
        static void QueryStudents(int pageSize, int pageIndex, string name, string tel, DateTime? startTime, DateTime? entTime)
        {
            var queryInfo = new StudentQueryInfo
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Name = name,
                EndTime = entTime,
                StartTime = startTime,
                Tel = tel
            };

            var studentService = new StudentService();
            var allStudents = studentService.QueryByCondition(queryInfo);
            allStudents.ForEach(Console.WriteLine);

        }

    }
}
