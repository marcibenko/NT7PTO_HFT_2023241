using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NT7PTO_HFT_2023241.Models;
using NT7PTO_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NT7PTO_HFT_2023241.Endpoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            SpaceTravelsDbContext db = new SpaceTravelsDbContext();
            ;

            
            foreach (var item in db.Captains.Include(t=>t.Spaceships))
	        {
                Console.WriteLine(item.name);
                foreach (var ship in item.Spaceships)
	            {
                    Console.WriteLine("\t"+ ship.shipName);
            	}

           	}

            ;

            CreateHostBuilder(args).Build().Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
