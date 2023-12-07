using ConsoleTools;
using NT7PTO_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Xml.Linq;
using static NT7PTO_HFT_2023241.Logic.SpaceshipLogic;
using static NT7PTO_HFT_2023241.Logic.SpaceTravelLogic;
using static NT7PTO_HFT_2023241.Models.Spaceship;

namespace NT7PTO_HFT_2023241.Client
{
    internal class Program
    {
        static RestService rest;

        static void Create(string entity)
        {
            SpaceshipType[] types = {
            SpaceshipType.Fighter,
            SpaceshipType.Cruiser,
            SpaceshipType.Destroyer,
            SpaceshipType.Battleship,
            SpaceshipType.Transport
            };


            if (entity == "Captain")
            {
                try
                {
                    //captainId
                    Console.Write("Enter captainId");
                    string captainId = Console.ReadLine();
                    //name
                    Console.Write("Enter name of captain");
                    string name = Console.ReadLine();
                    //age
                    Console.Write("Enter age of captain");
                    int age = int.Parse(Console.ReadLine());
                    //birthPlace
                    Console.Write("Enter captains birthplace");
                    string birthPlace= Console.ReadLine();
                    rest.Post(new Captain() {captainId = captainId, name = name, age = age,birthPlace=birthPlace },"captain");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }


            }
            else if(entity == "Spaceship")
            {
                try
                {
                    //spaceshipId
                    Console.Write("Enter spaceshipId");
                    string spaceshipId = Console.ReadLine();
                    //shipName
                    Console.Write("Enter ship name");
                    string shipName = Console.ReadLine();
                    //type
                    Console.Write("Enter the number of the type (fighter,cruiser,destroyer,battleship,transport)");
                    SpaceshipType type = types[int.Parse(Console.ReadLine())];
                    //size
                    Console.Write("Enter size of ship");
                    int size = int.Parse(Console.ReadLine());
                    //captainId
                    Console.Write("Enter captainId");
                    string captainId = Console.ReadLine();
                    rest.Post(new Spaceship() {spaceshipId= spaceshipId, shipName = shipName, type=type, size = size,captainId=captainId },"spaceship");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
            else if(entity == "SpaceTravel")
            {
                try
                {
                    //travelId
                    Console.Write("Enter travelId");
                    string travelId = Console.ReadLine();
                    //captainId
                    Console.Write("Enter captainId");
                    string captainId = Console.ReadLine();
                    //spaceshipId
                    Console.Write("Enter spaceshipId");
                    string spaceshipId = Console.ReadLine();
                    //travelFrom
                    Console.Write("Enter start location");
                    string travelFrom = Console.ReadLine();
                    //travelTo
                    Console.Write("Enter finish location");
                    string travelTo = Console.ReadLine();
                    //travelStartYear
                    Console.Write("Enter year of the travels start");
                    int travelStartYear = int.Parse(Console.ReadLine());
                    rest.Post(new SpaceTravel() 
                    {travelId= travelId,captainId= captainId,spaceshipId = spaceshipId,
                        travelFrom=travelFrom,travelTo=travelTo,travelStartYear=travelStartYear },"spacetravel");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
        }

        static void List(string entity)
        {
            if (entity == "Captain")
            {
                List<Captain> caps = rest.Get<Captain>("captain");
                foreach (var item in caps)
                {
                    Console.WriteLine($"Name:{item.name}, age:{item.age} birthplace:{item.birthPlace}");
                }
            }
            else if (entity == "Spaceship")
            {
                List<Spaceship> ships = rest.Get<Spaceship>("spaceship");
                foreach (var item in ships)
                {
                    Console.WriteLine($"Name: {item.shipName} type:{item.type.ToString()}, size:{item.size}");
                }
            }
            else if (entity == "SpaceTravel")
            {
                List<SpaceTravel> travs = rest.Get<SpaceTravel>("spacetravel");
                foreach (var item in travs)
                {
                    Console.WriteLine($"Captain id:{item.captainId}, ship id:{item.spaceshipId},Traveled from {item.travelFrom} to {item.travelTo} in {item.travelStartYear}");
                }
            }
            Console.ReadLine();
        }

        static void Update(string entity)
        {
            if (entity == "Captain")
            {
                try
                {
                    Console.Write("Enter captain id to update: ");
                    string id = Console.ReadLine();
                    Captain one = rest.Get<Captain>(id, "captain");
                    Console.Write($"New name [old: {one.name}]: ");
                    string name = Console.ReadLine();
                    one.name = name;
                    rest.Put(one, "captain");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
            else if (entity == "Spaceship")
            {
                try
                {
                    Console.Write("Enter Spaceship id to update: ");
                    string id = Console.ReadLine();
                    Spaceship one = rest.Get<Spaceship>(id, "spaceship");
                    Console.Write($"New name [old: {one.shipName}]: ");
                    string name = Console.ReadLine();
                    one.shipName = name;
                    rest.Put(one, "spaceship");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
            else if (entity == "SpaceTravel")
            {
                try
                {
                    Console.Write("Enter spacetravel id to update: ");
                    string id = Console.ReadLine();
                    SpaceTravel one = rest.Get<SpaceTravel>(id, "spacetravel");
                    Console.Write($"New location [old: {one.travelTo}]: ");
                    string name = Console.ReadLine();
                    one.travelTo = name;
                    rest.Put(one, "spacetravel");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
        }

        static void Delete(string entity)
        {
            if (entity == "Captain")
            {
                try
                {
                    Console.Write("Enter captain id to delete: ");
                    string id = Console.ReadLine();
                    rest.Delete(id, "captain");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
            else if (entity == "Spaceship")
            {
                try
                {
                    Console.Write("Enter spaceship id to delete: ");
                    string id = Console.ReadLine();
                    rest.Delete(id, "spaceship");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
            else if (entity == "SpaceTravel")
            {
                try
                {
                    Console.Write("Enter spacetravel id to delete: ");
                    string id = Console.ReadLine();
                    rest.Delete(id, "spacetravel");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
        }

        static void GetOne(string entity)
        {
            if (entity == "Captain")
            {
                Console.WriteLine("Enter Captain id: ");
                try
                {
                    string id = Console.ReadLine();
                    Captain item = rest.Get<Captain>(id, "captain");
                    Console.WriteLine($"Name:{item.name}, age:{item.age} birthplace:{item.birthPlace}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                    return;
                }
            }
            else if (entity == "Spaceship")
            {
                Console.WriteLine("Enter spaceship id: ");
                try
                {
                    string id = Console.ReadLine();
                    Spaceship item = rest.Get<Spaceship>(id, "spaceship");
                    Console.WriteLine($"Name: {item.shipName} type:{item.type.ToString()}, size:{item.size}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                    return;
                }
            }
            else if (entity == "SpaceTravel")
            {
                Console.WriteLine("Enter spacetravel id: ");
                try
                {
                    string id = Console.ReadLine();
                    SpaceTravel item = rest.Get<SpaceTravel>(id, "captain");
                    Console.WriteLine($"Captain id:{item.captainId}, ship id:{item.spaceshipId},Traveled from {item.travelFrom} to {item.travelTo} in {item.travelStartYear}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                    return;
                }
            }
        }

        static void NonCrud(string entity)
        {
            if (entity == "MostTravels")
            {
                try
                {
                    var l = rest.Get<CaptainsTravels>("noncrud/MostTravels");
                    foreach (var item in l)
                    {
                        Console.WriteLine($"Captain: {item.Captain.name} number of travels: {item.NumberOfTravels.ToString()}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
            else if (entity == "MostDangerousCaptains")
            {
                try
                {
                    var l = rest.Get<Captain>("noncrud/MostDangerousCaptains");
                    foreach (var item in l)
                    {
                        Console.WriteLine($"{item.name}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
            else if (entity == "BiggestShip")
            {
                try
                {
                    var l = rest.Get<Captain>("noncrud/BiggestShip");
                    foreach (var item in l)
                    {
                        Console.WriteLine($"{item.name}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
            else if (entity == "UnusedShips")
            {
                try
                {
                    var l = rest.Get<Spaceship>("noncrud/UnusedShips");
                    foreach (var item in l)
                    {
                        Console.WriteLine($"{item.shipName}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
            else if (entity == "CaptainsAndShips")
            {
                try
                {
                    var l = rest.Get<CapAndShips>("noncrud/CaptainsAndShips");
                    foreach (var item in l)
                    {
                        Console.WriteLine($"Captain: {item.Captain.name}");
                        foreach (var ship in item.Spaceships)
                        {
                            Console.WriteLine($"\nShip name: {ship.shipName}");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
            Console.ReadKey();
        }


        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:5000/", "captain");

            var captainSubmenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Captain"))
                .Add("GetOne", () => GetOne("Captain"))
                .Add("Create", () => Create("Captain"))
                .Add("Delete", () => Delete("Captain"))
                .Add("Update", () => Update("Captain"))
                .Add("Exit", ConsoleMenu.Close);

            var spaceshipSubmenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Spaceship"))
                .Add("GetOne", () => GetOne("Spaceship"))
                .Add("Create", () => Create("Spaceship"))
                .Add("Delete", () => Delete("Spaceship"))
                .Add("Update", () => Update("Spaceship"))
                .Add("Exit", ConsoleMenu.Close);

            var spacetravelSubmenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("SpaceTravel"))
                .Add("GetOne", () => GetOne("SpaceTravel"))
                .Add("Create", () => Create("SpaceTravel"))
                .Add("Delete", () => Delete("SpaceTravel"))
                .Add("Update", () => Update("SpaceTravel"))
                .Add("Exit", ConsoleMenu.Close);

            var noncrudSubMenu = new ConsoleMenu(args, level: 1)
                .Add("MostTravels", () => NonCrud("MostTravels"))
                .Add("MostDangerousCaptains", () => NonCrud("MostDangerousCaptains"))
                .Add("BiggestShip", () => NonCrud("BiggestShip"))
                .Add("UnusedShips", () => NonCrud("UnusedShips"))
                .Add("CaptainsAndShips", () => NonCrud("CaptainsAndShips"))
                .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Captains", () => captainSubmenu.Show())
                .Add("Spaceships", () => spaceshipSubmenu.Show())
                .Add("SpaceTravels", () => spacetravelSubmenu.Show())
                .Add("Non-crud methods", () => noncrudSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    }
}
