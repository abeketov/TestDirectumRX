using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestDirectumRX.Db;
using TestDirectumRX.Services;
using TestDirectumRX.Services.Interfacaces;

namespace TestDirectumRX
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IMainService, MainService>()
                .AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TestDb"))
                .BuildServiceProvider();

            serviceProvider.GetService<IMainService>().Execute();
        }
    }
}
