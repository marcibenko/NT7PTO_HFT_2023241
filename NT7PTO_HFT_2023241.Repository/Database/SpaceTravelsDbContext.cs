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

                //connection string vegere: MultipleActiveResultSet = true -> lazy loading miatt

                //                string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;
                //AttachDbFilename=C:\Users\Márton\Desktop\hft\feleves\NT7PTO_HFT_2023241\NT7PTO_HFT_2023241.Repository\SpaceTraffic3.mdf;
                //Integrated Security=True;
                //                MultipleActiveResultSets = true";
                string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\Márton\Desktop\hft\feleves\NT7PTO_HFT_2023241\NT7PTO_HFT_2023241.Repository\SpaceTraffic.mdf;
Integrated Security=True;
MultipleActiveResultSets = true";

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

            //spaceship relations
            modelBuilder.Entity<Spaceship>()
                .HasKey(s => s.spaceshipId);

            modelBuilder.Entity<Spaceship>(ship => ship
                .HasOne(ship => ship.captain)
                .WithMany(cap => cap.Spaceships)
                .HasForeignKey(ship => ship.captainId)
                .OnDelete(DeleteBehavior.SetNull));
            ;

            //spacetravel relations
            modelBuilder.Entity<SpaceTravel>()
                .HasKey(st => st.travelId);


            modelBuilder.Entity<SpaceTravel>(st => st
                .HasOne(trav => trav.captain)
                .WithMany(cap => cap.SpaceTravels)
                .HasForeignKey("captainId")
                .OnDelete(DeleteBehavior.Cascade)
                );

            modelBuilder.Entity<SpaceTravel>(st => st
                .HasOne(trav => trav.spaceship)
                .WithMany(ship => ship.SpaceTravels)
                .HasForeignKey("spaceshipId")
                .OnDelete(DeleteBehavior.Cascade)
                );


            modelBuilder.Entity<Captain>().HasData(new Captain[]
            {
                new Captain("123456","Captain Hook",30,"Earth"),
                new Captain("334652","Ferenc",99,"Moon"),
                new Captain("664206","XAEA-12",30,"Mars"),
                new Captain("212223", "Spock", 60, "Vulcan"),
                new Captain("161718", "Ezri Dax", 28, "Trill"),
                new Captain("456789", "James T. Kirk", 35, "Earth"),
                new Captain("282930", "Thane Krios", 40, "Dekuuna"),
                new Captain("353637", "Lando Calrissian", 45, "Socorro")
            });

            modelBuilder.Entity<Spaceship>().HasData(new Spaceship[]
            {
                new Spaceship("ADFBKS", "Traveler",Spaceship.SpaceshipType.Cruiser,6,"334652"),
                new Spaceship("LKGICO","SuperDeathStar",Spaceship.SpaceshipType.Destroyer,1500,"664206"),
                new Spaceship("GGGAST","ThePirateShip",Spaceship.SpaceshipType.Transport,300,"123456"),
                new Spaceship("AAAAAA","Star",Spaceship.SpaceshipType.Fighter,1,"664206"),

                new Spaceship("IJK123", "Heart of Gold", Spaceship.SpaceshipType.Transport, 170, "212223"),
                new Spaceship("CDE345", "Executor", Spaceship.SpaceshipType.Battleship, 900, "212223"),
                new Spaceship("YZA345", "Xindi-Insectoid Warship", Spaceship.SpaceshipType.Destroyer, 340, "161718"),
                new Spaceship("MNO123", "Jupiter 2", Spaceship.SpaceshipType.Transport, 120, "161718"),
                new Spaceship("ZAB012", "Yamato", Spaceship.SpaceshipType.Battleship, 980, "456789"),
                new Spaceship("MNO345", "TIE Fighter", Spaceship.SpaceshipType.Fighter, 200, "282930"),
                new Spaceship("BCD890", "Imperial Star Destroyer", Spaceship.SpaceshipType.Battleship, 800, "282930"),
                new Spaceship("PQR456", "Battlestar Pegasus", Spaceship.SpaceshipType.Battleship, 850, "353637")
            });

            modelBuilder.Entity<SpaceTravel>().HasData(new SpaceTravel[]
            {
                new SpaceTravel("ABDS1D536J","334652","ADFBKS","Earth","Mars",2020),
                new SpaceTravel("JDFG72627A","664206","LKGICO","Mars","Venus",2033),
                new SpaceTravel("GT63FL9Y1G","123456","GGGAST","Earth","Jupyter",2025),

                new SpaceTravel("YZA383BCD9", "212223", "IJK123", "Mars", "Jupiter", 2280),
                new SpaceTravel("EFG393HIJ0", "212223", "IJK123", "Earth", "Mars", 2300),
                new SpaceTravel("KLM404NOP1", "212223", "CDE345", "Alderaan", "Tatooine", 2330),
                new SpaceTravel("QRS414TUV2", "161718", "YZA345", "Tatooine", "Coruscant", 2350),
                new SpaceTravel("WXYZ424ABC", "161718", "MNO123", "Endor", "Tatooine", 2380),
                new SpaceTravel("YZA121BCD3", "456789", "ZAB012", "Earth", "Hoth", 3250),
                new SpaceTravel("EFG131HIJ4", "456789", "ZAB012", "Shadow", "Persephone", 3300),
                new SpaceTravel("KLM141NOP5", "282930", "MNO345", "Mars", "Jupiter", 3350),
                new SpaceTravel("QRS151TUV6", "282930", "BCD890", "Coruscant", "Endor", 3400),
                new SpaceTravel("WXYZ161ABC", "282930", "BCD890", "Alderaan", "Tatooine", 3450),
                new SpaceTravel("CDE171FGH8", "353637", "PQR456", "Kamino", "Geonosis", 3500),
                new SpaceTravel("IJK181LMN9", "353637", "PQR456", "Zebes", "SR388", 3550)
            });


            base.OnModelCreating(modelBuilder);
        }


    }
}
