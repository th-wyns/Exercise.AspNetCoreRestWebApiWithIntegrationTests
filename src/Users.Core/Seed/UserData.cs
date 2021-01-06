using System.Collections.Generic;
using Users.Models.Entities;

namespace Users.Seed
{
    public static class UserData
    {
        public static List<User> Get()
        {
            return new List<User>
            {
                User01,
                User02,
                User03,
                User04,
                User05,
                User06,
                User07,
                User08,
                User09,
                User10
            };
        }

        public static User User01
        {
            get
            {
                return new User
                {
                    Id = "000000000000000000000001",
                    Name = "Leanne Graham",
                    UserName = "Bret",
                    Email = "Sincere@april.biz",
                    Address = new Address
                    {
                        Street = "Kulas Light",
                        Suite = "Apt. 556",
                        City = "Gwenborough",
                        Zipcode = "92998-3874",
                        Geo = new GeoCoordinate
                        {
                            Lat = -37.3159,
                            Lng = 81.1496
                        }
                    },
                    Phone = "1-770-736-8031 x56442",
                    Website = "hildegard.org",
                    Company = new Company
                    {
                        Name = "Romaguera-Crona",
                        CatchPhrase = "Multi-layered client-server neural-net",
                        Bs = "harness real-time e-markets"
                    }
                };
            }
        }

        public static User User02
        {
            get
            {
                return new User
                {
                    Id = "000000000000000000000002",
                    Name = "Ervin Howell",
                    UserName = "Antonette",
                    Email = "Shanna@melissa.tv",
                    Address = new Address
                    {
                        Street = "Victor Plains",
                        Suite = "Suite 879",
                        City = "Wisokyburgh",
                        Zipcode = "90566-7771",
                        Geo = new GeoCoordinate
                        {
                            Lat = -43.9509,
                            Lng = -34.4618
                        }
                    },
                    Phone = "010-692-6593 x09125",
                    Website = "anastasia.net",
                    Company = new Company
                    {
                        Name = "Deckow-Crist",
                        CatchPhrase = "Proactive didactic contingency",
                        Bs = "synergize scalable supply-chains"
                    }
                };
            }
        }

        public static User User03
        {
            get
            {
                return new User
                {
                    Id = "000000000000000000000003",
                    Name = "Clementine Bauch",
                    UserName = "Samantha",
                    Email = "Nathan@yesenia.net",
                    Address = new Address
                    {
                        Street = "Douglas Extension",
                        Suite = "Suite 847",
                        City = "McKenziehaven",
                        Zipcode = "59590-4157",
                        Geo = new GeoCoordinate
                        {
                            Lat = -68.6102,
                            Lng = -47.0653
                        }
                    },
                    Phone = "1-463-123-4447",
                    Website = "ramiro.info",
                    Company = new Company
                    {
                        Name = "Romaguera-Jacobson",
                        CatchPhrase = "Face to face bifurcated interface",
                        Bs = "e-enable strategic applications"
                    }
                };
            }
        }

        public static User User04
        {
            get
            {
                return new User
                {
                    Id = "000000000000000000000004",
                    Name = "Patricia Lebsack",
                    UserName = "Karianne",
                    Email = "Julianne.OConner@kory.org",
                    Address = new Address
                    {
                        Street = "Hoeger Mall",
                        Suite = "Apt. 692",
                        City = "South Elvis",
                        Zipcode = "53919-4257",
                        Geo = new GeoCoordinate
                        {
                            Lat = 29.4572,
                            Lng = -164.2990
                        }
                    },
                    Phone = "493-170-9623 x156",
                    Website = "kale.biz",
                    Company = new Company
                    {
                        Name = "Robel-Corkery",
                        CatchPhrase = "Multi-tiered zero tolerance productivity",
                        Bs = "transition cutting-edge web services"
                    }
                };
            }
        }

        public static User User05
        {
            get
            {
                return new User
                {
                    Id = "000000000000000000000005",
                    Name = "Chelsey Dietrich",
                    UserName = "Kamren",
                    Email = "Lucio_Hettinger@annie.ca",
                    Address = new Address
                    {
                        Street = "Skiles Walks",
                        Suite = "Suite 351",
                        City = "Roscoeview",
                        Zipcode = "33263",
                        Geo = new GeoCoordinate
                        {
                            Lat = -31.8129,
                            Lng = 62.5342
                        }
                    },
                    Phone = "(254)954-1289",
                    Website = "demarco.info",
                    Company = new Company
                    {
                        Name = "Keebler LLC",
                        CatchPhrase = "User-centric fault-tolerant solution",
                        Bs = "revolutionize end-to-end systems"
                    }
                };
            }
        }

        public static User User06
        {
            get
            {
                return new User
                {
                    Id = "000000000000000000000006",
                    Name = "Mrs. Dennis Schulist",
                    UserName = "Leopoldo_Corkery",
                    Email = "Karley_Dach@jasper.info",
                    Address = new Address
                    {
                        Street = "Norberto Crossing",
                        Suite = "Apt. 950",
                        City = "South Christy",
                        Zipcode = "23505-1337",
                        Geo = new GeoCoordinate
                        {
                            Lat = -71.4197,
                            Lng = 71.7478
                        }
                    },
                    Phone = "1-477-935-8478 x6430",
                    Website = "ola.org",
                    Company = new Company
                    {
                        Name = "Considine-Lockman",
                        CatchPhrase = "Synchronised bottom-line interface",
                        Bs = "e-enable innovative applications"
                    }
                };
            }
        }

        public static User User07
        {
            get
            {
                return new User
                {
                    Id = "000000000000000000000007",
                    Name = "Kurtis Weissnat",
                    UserName = "Elwyn.Skiles",
                    Email = "Telly.Hoeger@billy.biz",
                    Address = new Address
                    {
                        Street = "Rex Trail",
                        Suite = "Suite 280",
                        City = "Howemouth",
                        Zipcode = "58804-1099",
                        Geo = new GeoCoordinate
                        {
                            Lat = 24.8918,
                            Lng = 21.8984
                        }
                    },
                    Phone = "210.067.6132",
                    Website = "elvis.io",
                    Company = new Company
                    {
                        Name = "Johns Group",
                        CatchPhrase = "Configurable multimedia task-force",
                        Bs = "generate enterprise e-tailers"
                    }
                };
            }
        }

        public static User User08
        {
            get
            {
                return new User
                {
                    Id = "000000000000000000000008",
                    Name = "Nicholas Runolfsdottir V",
                    UserName = "Maxime_Nienow",
                    Email = "Sherwood@rosamond.me",
                    Address = new Address
                    {
                        Street = "Ellsworth Summit",
                        Suite = "Suite 729",
                        City = "Aliyaview",
                        Zipcode = "45169",
                        Geo = new GeoCoordinate
                        {
                            Lat = -14.3990,
                            Lng = -120.7677
                        }
                    },
                    Phone = "586.493.6943 x140",
                    Website = "jacynthe.com",
                    Company = new Company
                    {
                        Name = "Abernathy Group",
                        CatchPhrase = "Implemented secondary concept",
                        Bs = "e-enable extensible e-tailers"
                    }
                };
            }
        }

        public static User User09
        {
            get
            {
                return new User
                {
                    Id = "000000000000000000000009",
                    Name = "Glenna Reichert",
                    UserName = "Delphine",
                    Email = "Chaim_McDermott@dana.io",
                    Address = new Address
                    {
                        Street = "Dayna Park",
                        Suite = "Suite 449",
                        City = "Bartholomebury",
                        Zipcode = "76495-3109",
                        Geo = new GeoCoordinate
                        {
                            Lat = 24.6463,
                            Lng = -168.8889
                        }
                    },
                    Phone = "(775)976-6794 x41206",
                    Website = "conrad.com",
                    Company = new Company
                    {
                        Name = "Yost and Sons",
                        CatchPhrase = "Switchable contextually-based project",
                        Bs = "aggregate real-time technologies"
                    }
                };
            }
        }

        public static User User10
        {
            get
            {
                return new User
                {
                    Id = "0000000000000000000000010",
                    Name = "Clementina DuBuque",
                    UserName = "Moriah.Stanton",
                    Email = "Rey.Padberg@karina.biz",
                    Address = new Address
                    {
                        Street = "Kattie Turnpike",
                        Suite = "Suite 198",
                        City = "Lebsackbury",
                        Zipcode = "31428-2261",
                        Geo = new GeoCoordinate
                        {
                            Lat = -38.2386,
                            Lng = 57.2232
                        }
                    },
                    Phone = "024-648-3804",
                    Website = "ambrose.net",
                    Company = new Company
                    {
                        Name = "Hoeger LLC",
                        CatchPhrase = "Centralized empowering task-force",
                        Bs = "target end-to-end models"
                    }
                };
            }
        }
    }
}