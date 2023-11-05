using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NT7PTO_HFT_2023241.Models;

namespace NT7PTO_HFT_2023241.Repository
{
    public class SpaceTravelsDbContext: DbContext
    {
        public DbSet<Spaceship> Spaceships { get; set; }
        public DbSet<Captain> Captains { get; set; }
        public DbSet<SpaceTravel> SpaceTravels { get; set; }

        public SpaceTravelsDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //LocalDb van -> InMemory kell majd
            if (!builder.IsConfigured)
            {
                string conn =
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Márton\Desktop\hft\feleves\NT7PTO_HFT_2023241\NT7PTO_HFT_2023241.Repository\SpaceTraffic2.mdf;Integrated Security=True";
                builder
                .UseLazyLoadingProxies()
                .UseSqlServer(conn);
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            //captain relations
            modelBuilder.Entity<Captain>(cap => cap
                .HasKey("captainId")
                );

            modelBuilder.Entity<Spaceship>()
                .HasKey(s => s.spaceshipId);

            modelBuilder.Entity<Spaceship>()
                .HasOne<Captain>()
                .WithMany()
                .HasForeignKey(ship => ship.captain)
                .OnDelete(DeleteBehavior.SetNull);
            ;

            //spacetravel relations
            modelBuilder.Entity<SpaceTravel>()
                .HasKey(st => st.travelId);


            modelBuilder.Entity<SpaceTravel>()
                .HasOne<Captain>()
                .WithMany()
                .HasForeignKey("captainId")
                .OnDelete(DeleteBehavior.Cascade)
                ;

            modelBuilder.Entity<SpaceTravel>()
                .HasOne<Spaceship>()
                .WithMany()
                .HasForeignKey("spaceshipId")
                .OnDelete(DeleteBehavior.Cascade)
                ;



            modelBuilder.Entity<Captain>().HasData(new Captain[]
            {
                new Captain("123456","Captain Hook",30,"Earth"),
                new Captain("334652","Ferenc",99,"Moon"),
                new Captain("664206","XAEA-12",30,"Mars")
            });

            modelBuilder.Entity<Spaceship>().HasData(new Spaceship[]
            {
                new Spaceship("ADFBKS", "Traveler",Spaceship.SpaceshipType.Cruiser,6,"334652"),
                new Spaceship("LKGICO","SuperDeathStar",Spaceship.SpaceshipType.Destroyer,1500,"664206"),
                new Spaceship("GGGAST","ThePirateShip",Spaceship.SpaceshipType.Transport,300,"123456"),
                new Spaceship("AAAAAA","Star",Spaceship.SpaceshipType.Fighter,1,"664206")
            });

            modelBuilder.Entity<SpaceTravel>().HasData(new SpaceTravel[]
            {
                new SpaceTravel("ABDS1D536J","334652","ADFBKS","Earth","Mars",2020),
                new SpaceTravel("JDFG72627A","664206","LKGICO","Mars","Venus",2033),
                new SpaceTravel("GT63FL9Y1G","123456","GGGAST","Earth","Jupyter",2025),
            });

            

            base.OnModelCreating(modelBuilder);
        }


    }
}
