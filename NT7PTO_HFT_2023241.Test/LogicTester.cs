using System;
using NUnit.Framework;
using Moq;
using NT7PTO_HFT_2023241.Logic;
using NT7PTO_HFT_2023241.Repository;
using NT7PTO_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static NT7PTO_HFT_2023241.Logic.SpaceshipLogic;
using NUnit.Framework.Constraints;
using static NT7PTO_HFT_2023241.Logic.SpaceTravelLogic;

namespace NT7PTO_HFT_2023241.Test
{
    [TestFixture]
    public class LogicTester
    {
        CaptainLogic captainLogic;
        Mock<IRepository<Captain>> mockCaptainRepo;

        SpaceshipLogic spaceshipLogic;
        Mock<IRepository<Spaceship>> mockSpaceshipRepo;

        SpaceTravelLogic spaceTravelLogic;
        Mock<IRepository<SpaceTravel>> mockSpaceTravelRepo;


        [SetUp]
        public void Init()
        {
            //create mock captain repo
            mockCaptainRepo = new Mock<IRepository<Captain>>();
            mockCaptainRepo.Setup(m => m.ReadAll()).Returns(new List<Captain>()
            {
                new Captain(){
                    captainId="000001",
                    name = "cap1",
                    age=20,
                    birthPlace="Earth",
                },

                new Captain(){
                    captainId="000002",
                    name = "cap2",
                    age=30,
                    birthPlace="Mars",
                    },

                new Captain(){
                    captainId="000003",
                    name = "cap3",
                    age=40,
                    birthPlace="Venus",
                    },

                new Captain(){
                    captainId="000004",
                    name = "cap4",
                    age=50,
                    birthPlace="Earth",
                    },
            }.AsQueryable());


            //create mock spaceship repo
            mockSpaceshipRepo = new Mock<IRepository<Spaceship>>();
            mockSpaceshipRepo.Setup(t => t.ReadAll()).Returns(new List<Spaceship>()
            {
                new Spaceship("AAAAAA","ship1",Spaceship.SpaceshipType.Destroyer,10,"000001")
                ,//2000

                new Spaceship("BBBBBB","ship2",Spaceship.SpaceshipType.Battleship,30,"000002")
                ,//5100

                new Spaceship("CCCCCC","ship3",Spaceship.SpaceshipType.Cruiser,40,"000003")
                ,//3200

                new Spaceship("DDDDDD","ship4",Spaceship.SpaceshipType.Fighter,20,"000004")
                ,//3000

                new Spaceship("EEEEEE","ship5",Spaceship.SpaceshipType.Destroyer,100,"000003")
                //20000

            }.AsQueryable());


            //create mock spacetravel repo
            mockSpaceTravelRepo = new Mock<IRepository<SpaceTravel>>();
            mockSpaceTravelRepo.Setup(t => t.ReadAll()).Returns(new List<SpaceTravel>()
            {
                new SpaceTravel("AAAAA00000","000001","AAAAAA","Earth","Mars",2030),
                new SpaceTravel("AAAAA00001","000002","BBBBBB","Mars","Earth",2030),
                new SpaceTravel("AAAAA00002","000003","CCCCCC","Jupiter","Earth",2031),
                new SpaceTravel("AAAAA00003","000003","EEEEEE","Mars","Venus",2032),
                new SpaceTravel("AAAAA00004","000004","DDDDDD","Earth","Moon",2033),
                new SpaceTravel("AAAAA00005","000004","DDDDDD","Moon","Jupiter",2033)
            }.AsQueryable());

            //setup relations
            foreach (var captain in mockCaptainRepo.Object.ReadAll())
            {
                captain.Spaceships = mockSpaceshipRepo.Object.ReadAll().Where(s => s.captainId == captain.captainId).ToList();
                captain.SpaceTravels = mockSpaceTravelRepo.Object.ReadAll().Where(st => st.captainId == captain.captainId).ToList();
                foreach (var spaceship in captain.Spaceships)
                {
                    spaceship.captain = captain;
                    spaceship.SpaceTravels = mockSpaceTravelRepo.Object.ReadAll().Where(st => st.spaceshipId == spaceship.spaceshipId).ToList();
                }
                foreach (var spaceTravel in captain.SpaceTravels)
                {
                    spaceTravel.captain = captain;
                    spaceTravel.spaceship = mockSpaceshipRepo.Object.ReadAll().FirstOrDefault(s => s.spaceshipId == spaceTravel.spaceshipId);
                }
            }


            //create logic 
            captainLogic = new CaptainLogic(mockCaptainRepo.Object);
            spaceTravelLogic = new SpaceTravelLogic(mockSpaceTravelRepo.Object);
            spaceshipLogic = new SpaceshipLogic(mockSpaceshipRepo.Object);


        }

        //non crud methods tests
        //captain non cruds
        [Test]
        public void MostDangerousCaptainsTest()
        {
            var actual = captainLogic.MostDangerousCaptains().ToList();
            //3,1,2,4
            
            var expected = new List<Captain>()
            {
                new Captain(){
                    captainId="000003",
                    name = "cap3",
                    age=40,
                    birthPlace="Venus",
                    Spaceships = new List<Spaceship>
                    {
                        new Spaceship("CCCCCC","ship3",Spaceship.SpaceshipType.Cruiser,40,"000003"),
                        new Spaceship("EEEEEE","ship5",Spaceship.SpaceshipType.Destroyer,100,"000003")
                    },
                    SpaceTravels =
                    {
                        new SpaceTravel("AAAAA00002","000003","CCCCCC","Jupiter","Earth",2031),
                        new SpaceTravel("AAAAA00003","000003","EEEEEE","Mars","Venus",2032)
                    }}, //23200 - cap3
                new Captain(){
                    captainId="000002",
                    name = "cap2",
                    age=30,
                    birthPlace="Mars",
                    Spaceships =new List<Spaceship>
                    {
                        new Spaceship("BBBBBB","ship2",Spaceship.SpaceshipType.Battleship,30,"000002")
                    },
                    SpaceTravels =
                    {
                        new SpaceTravel("AAAAA00001","000002","BBBBBB","Mars","Earth",2030)
                    }}, //5100 - cap2
                new Captain(){
                    captainId="000004",
                    name = "cap4",
                    age=50,
                    birthPlace="Earth",
                    Spaceships =new List<Spaceship>
                    {
                        new Spaceship("DDDDDD","ship4",Spaceship.SpaceshipType.Fighter,20,"000004")
                    },
                    SpaceTravels =
                    {
                        new SpaceTravel("AAAAA00004","000004","DDDDDD","Earth","Moon",2033),
                        new SpaceTravel("AAAAA00005","000004","DDDDDD","Moon","Jupiter",2033)
                    }}, //3000 - cap4
                new Captain(){
                    captainId="000001",
                    name = "cap1",
                    age=20,
                    birthPlace="Earth",
                    Spaceships =new List<Spaceship>
                    {
                        new Spaceship("AAAAAA","ship1",Spaceship.SpaceshipType.Destroyer,10,"000001")
                    },
                    SpaceTravels =
                    {
                        new SpaceTravel("AAAAA00000","000001","AAAAAA","Earth","Mars",2030)
                    }
                } //2000 - cap1
            };

            CollectionAssert.AreEqual(actual, expected);
        }

        [Test]
        public void BiggestShipTest()
        {
            var actual = captainLogic.BiggestShip().ToList();

            var expected = new List<Captain>
            {
                new Captain(){
                    captainId="000003",
                    name = "cap3",
                    age=40,
                    birthPlace="Venus",
                    Spaceships = new List<Spaceship>
                    {
                        new Spaceship("CCCCCC","ship3",Spaceship.SpaceshipType.Cruiser,40,"000003"),
                        new Spaceship("EEEEEE","ship5",Spaceship.SpaceshipType.Destroyer,100,"000003")
                    },
                    SpaceTravels =
                    {
                        new SpaceTravel("AAAAA00002","000003","CCCCCC","Jupiter","Earth",2031),
                        new SpaceTravel("AAAAA00003","000003","EEEEEE","Mars","Venus",2032)
                    }},

                new Captain(){captainId="000004",
                    name = "cap4",
                    age=50,
                    birthPlace="Earth",
                    Spaceships =new List<Spaceship>
                    {
                        new Spaceship("DDDDDD","ship4",Spaceship.SpaceshipType.Fighter,20,"000004")
                    },
                    SpaceTravels = new List<SpaceTravel>
                    {
                        new SpaceTravel("AAAAA00004","000004","DDDDDD","Earth","Moon",2033),
                        new SpaceTravel("AAAAA00005","000004","DDDDDD","Moon","Jupiter",2033)
                    }},
                new Captain(){captainId="000002",
                    name = "cap2",
                    age=30,
                    birthPlace="Mars",
                    Spaceships =new List<Spaceship>
                    {
                        new Spaceship("BBBBBB","ship2",Spaceship.SpaceshipType.Battleship,30,"000002")
                    },
                    SpaceTravels =new List<SpaceTravel>
                    {
                        new SpaceTravel("AAAAA00001","000002","BBBBBB","Mars","Earth",2030)
                    }},
                new Captain(){captainId="000001",
                    name = "cap1",
                    age=20,
                    birthPlace="Earth",
                    Spaceships = new List<Spaceship>
                    {
                        new Spaceship("AAAAAA","ship1",Spaceship.SpaceshipType.Destroyer,10,"000001")
                    },
                    SpaceTravels =new List<SpaceTravel>
                    {
                        new SpaceTravel("AAAAA00000","000001","AAAAAA","Earth","Mars",2030)
                    }},


            };

            Assert.AreEqual(actual.First(),expected.First());
            CollectionAssert.AreEquivalent(actual,expected);
        }

        //spaceship non cruds
        [Test]
        public void UnusedShipsTest()
        {
            var actual = spaceshipLogic.UnusedShips().ToList();
            //var expected = new List<Spaceship>();
            
            CollectionAssert.IsEmpty(actual);
        }

        [Test]
        public void CaptainsAndShipsTest()
        {
            var actual = spaceshipLogic.CaptainsAndShips().ToList();
            var expected = new List<CapAndShips>
            {
                new CapAndShips(){
                    Captain = spaceshipLogic.ReadAll().Where(x => x.captainId=="000003").Select(x => x.captain).First(),
                    Spaceships = spaceshipLogic.ReadAll().Where(x => x.captainId=="000003").ToList(),
                },
                new CapAndShips(){
                    Captain = spaceshipLogic.ReadAll().Where(x => x.captainId=="000001").Select(x => x.captain).First(),
                    Spaceships = spaceshipLogic.ReadAll().Where(x => x.captainId=="000001").ToList(),
                },
                new CapAndShips(){
                    Captain = spaceshipLogic.ReadAll().Where(x => x.captainId=="000002").Select(x => x.captain).First(),
                    Spaceships = spaceshipLogic.ReadAll().Where(x => x.captainId=="000002").ToList(),
                },
                new CapAndShips(){
                    Captain = spaceshipLogic.ReadAll().Where(x => x.captainId=="000004").Select(x => x.captain).First(),
                    Spaceships = spaceshipLogic.ReadAll().Where(x => x.captainId=="000004").ToList(),
                },
            };


            Assert.AreEqual(actual.First().Captain.name, expected.First().Captain.name);
        }

        //spacetravel non cruds
        [Test]
        public void MostTravelsTest()
        {
            
            var actual = spaceTravelLogic.MostTravels().ToList();
            
            var expected = new List<CaptainsTravels> { 
                new CaptainsTravels() {
                    Captain = spaceTravelLogic.ReadAll().Where(x => x.captainId == "000003" ).Select(x => x.captain).First(),
                    NumberOfTravels = 2
                },
                new CaptainsTravels() {
                    Captain = spaceTravelLogic.ReadAll().Where(x => x.captainId == "000004").Select(x => x.captain).First(),
                    NumberOfTravels =2
                },
                new CaptainsTravels() {
                    Captain = spaceTravelLogic.ReadAll().Where(x => x.captainId == "000001").Select(x => x.captain).First(),
                    NumberOfTravels =1
                },
                new CaptainsTravels() {
                    Captain = spaceTravelLogic.ReadAll().Where(x => x.captainId == "000002").Select(x => x.captain).First(),
                    NumberOfTravels =1
                },

            };
            
            CollectionAssert.AreEquivalent(expected,actual);
        }

        //crud methods
        //captain
        [Test]
        public void CreateCaptainTestWithCorrectName()
        {
            var cap = new Captain() { captainId = "00005", name = "János" };

            captainLogic.Create(cap);

            mockCaptainRepo.Verify(r => r.Create(cap), Times.Once);
        }

        [Test]
        public void CreateCaptainWithIncorrectName()
        {
            var cap = new Captain() { captainId = "00006", name = "O" };

            try 
            {
                captainLogic.Create(cap);
            }catch
            {

            }
            
            mockCaptainRepo.Verify(r => r.Create(cap), Times.Never);
        }

        //spaceTravel
        [Test]
        public void ReadSpaceTravelWithCorrectId()
        {
            

            try
            {
                spaceTravelLogic.Read("AAAAA00000");
            }catch
            {

            }
            mockSpaceTravelRepo.Verify(r => r.Read("AAAAA00000"), Times.Once);
            
        }

        //spaceship
        [Test]
        public void ReadSpaceshipWithCorrectId()
        {
            try
            {
                spaceshipLogic.Read("AAAAAA");
            }
            catch
            {

            }
            mockSpaceshipRepo.Verify(r => r.Read("AAAAAA"), Times.Once);
        }

        [Test]
        public void CreateSpaceship()
        {
            var ship = new Spaceship() { spaceshipId = "ZZZZZZ"};

            spaceshipLogic.Create(ship);

            mockSpaceshipRepo.Verify(r => r.Create(ship), Times.Once);
        }


    }
}

