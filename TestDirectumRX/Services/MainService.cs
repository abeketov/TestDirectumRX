using System;
using System.Linq;
using TestDirectumRX.Db;
using TestDirectumRX.Models;
using TestDirectumRX.Services.Interfacaces;

namespace TestDirectumRX.Services
{
    public class MainService : IMainService
    {
        private readonly AppDbContext _context;

        public MainService(AppDbContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            SeedData();

            ShowData();

            ExecRequests();
        }

        private void SeedData()
        {
            _context.Departaments.AddRange(
                new Departament { Name = "D1" },
                new Departament { Name = "D2" },
                new Departament { Name = "D3" }
            );

            _context.SaveChanges();

            _context.Empoyees.AddRange(
                new Empoyee { DepartamentId = 1, ChiefId = 5, Name = "John", Salary = 100 },
                new Empoyee { DepartamentId = 1, ChiefId = 5, Name = "Misha", Salary = 600 },
                new Empoyee { DepartamentId = 2, ChiefId = 6, Name = "Eugen", Salary = 300 },
                new Empoyee { DepartamentId = 2, ChiefId = 6, Name = "Tolya", Salary = 400 },
                new Empoyee { DepartamentId = 3, ChiefId = 7, Name = "Stepan", Salary = 500 },
                new Empoyee { DepartamentId = 3, ChiefId = 7, Name = "Alex", Salary = 1000 },
                new Empoyee { DepartamentId = 3, ChiefId = 0, Name = "Ivan", Salary = 1100 }
            );

            _context.SaveChanges();
        }
        private void ShowData()
        {
            Console.WriteLine("Departaments:");
            foreach (var dep in _context.Departaments)
            {
                Console.WriteLine(dep.ToString());
            }

            Console.WriteLine("Empoyees:");
            foreach (var emp in _context.Empoyees)
            {
                Console.WriteLine(emp.ToString());
            }
        }

        private void ExecRequests()
        {
            Request1();
            Request2();
            Request3();
        }

        private void Request1()
        {
            // 1.Суммарную зарплату в разрезе департаментов(без руководителей и с руководителями)

            Console.WriteLine("1.Суммарную зарплату в разрезе департаментов(без руководителей и с руководителями)");

            Console.WriteLine("1. с руководителями:");

            var salaries1 = _context.Empoyees
                .GroupBy(x => x.Departament.Name)
                .Select(c => new
                {
                    c.Key,
                    sum = c.Sum(e => e.Salary)
                })
                .OrderBy(x => x.Key)
                .ToList();

            foreach (var item in salaries1)
            {
                Console.WriteLine($"Id: {item.Key} Salary: {item.sum}");
            }

            Console.WriteLine("2. без руководителей:");

            var chiefIds = _context.Empoyees
                .Where(x => x.ChiefId != 0)
                .Select(c => c.ChiefId)
                .Distinct()
                .ToList();

            var salaries2 = _context.Departaments.SelectMany(x => x.Empoyees)
                .GroupBy(x => x.Departament.Name)
                .Select(c => new
                {
                    c.Key,
                    sum = c.Sum(e => chiefIds.Contains(e.Id) ? 0 : e.Salary)
                })
                .OrderBy(x => x.Key)

                .ToList();

            foreach (var item in salaries2)
            {
                Console.WriteLine($"Id: {item.Key} Salary: {item.sum}");
            }
        }

        private void Request2()
        {
            // 2.Департамент, в котором у сотрудника зарплата максимальна

            Console.WriteLine("2.Департамент, в котором у сотрудника зарплата максимальна");
            var dep = _context.Empoyees.OrderByDescending(x => x.Salary).FirstOrDefault()?.Departament;
            Console.WriteLine($"Id: {dep.Id} Name:{dep.Name}\n");
        }

        private void Request3()
        {
            // 3.Зарплаты руководителей департаментов(по убыванию)

            Console.WriteLine("3.Зарплаты руководителей департаментов(по убыванию)");

            var chiefIds = _context.Empoyees
                .Where(x => x.ChiefId != 0)
                .Select(c => c.ChiefId)
                .Distinct()
                .ToList();

            var emps = _context.Empoyees
                .Where(x => chiefIds.Contains(x.Id))
                .OrderByDescending(c => c.Salary);

            foreach (var emp in emps)
            {
                Console.WriteLine(emp.ToString());
            }
        }
    }
}
