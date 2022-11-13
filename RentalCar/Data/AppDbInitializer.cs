using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RentalCar.Data.Static;
using RentalCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Clients
                if (!context.Contacts.Any())
                {
                    context.Contacts.AddRange(new List<Contact>()
                    {
                        new Contact()
                        {
                              //Id
                              FullName = "Test",
                              Email  = "test@ubt-uni.net",
                              Subject = "2222",
                              Message = "Some Place",
                        },
                        new Contact()
                        {
                              //Id
                              FullName = "Test",
                              Email  = "test@ubt-uni.net",
                              Subject = "2222",
                              Message = "Some Place",
                        }

                    });
                    context.SaveChanges();
                }
                //Rentals
                if (!context.Rentals.Any())
                {
                    context.Rentals.AddRange(new List<Rental>()
                    {
                        new Rental()
                        {
                            Logo = "https://c7.alamy.com/comp/T8P429/vector-logo-for-car-rental-and-sales-T8P429.jpg",
                            Name = "Rent Kosova",
                            Address = "Prishtinë, Kosovë",
                            Mobile = 49111222,
                            Description = "RentKosova is the best Rent in town that you can drive best cars in Kosovo"

                        },
                         new Rental()
                        {
                            Logo = "https://cdn2.vectorstock.com/i/1000x1000/64/91/template-of-car-rental-logo-vector-9956491.jpg",
                            Name = "Besiana Rent",
                            Address = "Podujevë, Kosovë",
                            Mobile = 49222333,
                            Description = "Besiana Rent is the best rent in Podujevë with new cars"

                        },
                          new Rental()
                        {
                            Logo = "https://image.shutterstock.com/image-vector/rent-car-logo-design-vector-260nw-1072548182.jpg",
                            Name = "Krasniqi Rent",
                            Address = "Podujevë, Kosovë",
                            Mobile = 49222333,
                            Description = "Krasniqi Rent is the best rent in Prishtina with new cars"

                        },
                           new Rental()
                        {
                            Logo = "https://thumbs.dreamstime.com/z/rental-car-icon-template-deal-creative-vector-logo-design-illustration-element-177461744.jpg",
                            Name = "Ismajli Rent",
                            Address = "Shtime, Kosovë",
                            Mobile = 49222333,
                            Description = "Ismajli Rent is the best rent in Shtime with almost all models founded"

                        }, new Rental()
                        {
                            Logo = "https://www.sxm-cars.com/leisure/logo_main.jpg",
                            Name = "Llapi Rent",
                            Address = "Podujevë, Kosovë",
                            Mobile = 49222333,
                            Description = "Llapi Rent is the most popular rent in Podujevë with new cars"

                        },
                            new Rental()
                        {
                            Logo = "https://st2.depositphotos.com/2172301/6557/v/950/depositphotos_65575193-stock-illustration-vector-template-of-car-rental.jpg",
                            Name = "Auto Rent Beka",
                            Address = "Podujevë, Kosovë",
                            Mobile = 49222333,
                            Description = "Auto Rent Beka is the best rent in Podujevë with new cars"

                        }

                    });
                    context.SaveChanges();
                }

                //Brand
                if (!context.Brands.Any())
                {
                    context.Brands.AddRange(new List<Brand>()
                    {
                        new Brand()
                        {
                            FullName = "Mercedes Benz",
                            Bio = "The best company fort strong cars and Luxury",
                            ProfilePictureURL = "https://logodownload.org/wp-content/uploads/2014/04/mercedes-benz-logo-0.png"

                        },
                        new Brand()
                        {
                            FullName = "BMW",
                            Bio = "Great Company are known for fast and shifty Cars",
                            ProfilePictureURL = "https://logowik.com/content/uploads/images/849_bmw.jpg"
                        },
                        new Brand()
                        {
                            FullName = "Audi",
                            Bio = "German Company durability is in their Cars",
                            ProfilePictureURL = "https://logowik.com/content/uploads/images/t_562_audi.jpg"
                        },
                        new Brand()
                        {
                            FullName = "Volkswagen",
                            Bio = "Biggest company in europe long living cars",
                            ProfilePictureURL = "https://logowik.com/content/uploads/images/449_volkswagen.jpg"
                        },
                        new Brand()
                        {
                            FullName = "Opel",
                            Bio = "Economic cars",
                            ProfilePictureURL = "https://brandlogos.net/wp-content/uploads/2013/04/opel-2002-eps-vector-logo.png"
                        }
                    });
                    context.SaveChanges();
                }

                //Cars

                if (!context.Cars.Any())
                {
                    context.Cars.AddRange(new List<Car>()
                    {
                        new Car()
                        {

                              Name = "Rolls Royce",
                              Description  = "Description Rolls Royce",
                              Price = 25,
                              ImageURL = "https://i.pinimg.com/originals/5a/25/8b/5a258b432bd34cbb59f49b2375c4bb79.jpg",
                              StartDate= DateTime.Now.AddDays(-10),
                              EndDate = DateTime.Now.AddDays(-5),
                              RentalId=1,
                              BrandId=1,
                              CarCategory=CarCategory.Sport
                        },
                       new Car()
                        {

                              Name = "Golf 5 GTD",
                              Description  = "Description Golf 5 GTD",
                              Price = 30,
                              ImageURL = "https://images.wallpaperscraft.com/image/single/volkswagen_golf_mk5_volkswagen_car_187467_800x1420.jpg",
                              StartDate= DateTime.Now.AddDays(-10),
                              EndDate = DateTime.Now.AddDays(-5),
                              RentalId=2,
                              BrandId=2,
                              CarCategory=CarCategory.Hatchback
                        },
                        new Car()
                        {

                              Name = "Audi A7",
                              Description  = "Description Audi A7",
                              Price = 20,
                              ImageURL = "https://wallpaperaccess.com/full/108640.jpg",
                              StartDate= DateTime.Now.AddDays(-10),
                              EndDate = DateTime.Now.AddDays(-5),
                              RentalId=2,
                              BrandId=3,
                              CarCategory=CarCategory.Convertible
                        },
                         new Car()
                        {

                              Name = "Golf 5 GTI",
                              Description  = "Description Golf 5 GTI",
                              Price = 30,
                              ImageURL = "https://images.wallpaperscraft.com/image/single/volkswagen_golf_5_volkswagen_car_149767_938x1668.jpg",
                              StartDate= DateTime.Now.AddDays(-10),
                              EndDate = DateTime.Now.AddDays(-5),
                              RentalId=4,
                              BrandId=4,
                              CarCategory=CarCategory.Convertible
                        },
                         new Car()
                        {

                              Name = "Mercedes G Class",
                              Description  = "Description Mercedes G Class",
                              Price = 30,
                              ImageURL = "https://www.wsupercars.com/thumbnails-phone/Tuners/2021-Brabus-900-Rocket-Edition-002.jpg",
                              StartDate= DateTime.Now.AddDays(-10),
                              EndDate = DateTime.Now.AddDays(-5),
                              RentalId=4,
                              BrandId=4,
                              CarCategory=CarCategory.Convertible
                              
                        },
                          new Car()
                        {

                              Name = "Golf 7 GTD",
                              Description  = "Description Golf 7 GTD",
                              Price = 30,
                              ImageURL = "https://i.pinimg.com/236x/7a/0c/06/7a0c066cc5dc2be608445d330cc8b507.jpg",
                              StartDate= DateTime.Now.AddDays(-10),
                              EndDate = DateTime.Now.AddDays(-5),
                              RentalId=4,
                              BrandId=4,
                              CarCategory=CarCategory.Convertible
                        },
                          new Car()
                        {

                              Name = "Tiguan",
                              Description  = "Description Tiguan V6",
                              Price = 30,
                              ImageURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSQKBvYMDV5pS9bdAfuBHbUwxe7UpBb0lie8A&usqp=CAU",
                              StartDate= DateTime.Now.AddDays(-10),
                              EndDate = DateTime.Now.AddDays(-5),
                              RentalId=4,
                              BrandId=4,
                              CarCategory=CarCategory.Convertible
                        },
                          new Car()
                        {

                              Name = "BMW M5",
                              Description  = "Description BMW M5",
                              Price = 30,
                              ImageURL = "https://wallpaperbat.com/img/16678-bmw-iphone-wallpaper-top-free-bmw-iphone-background.jpg",
                              StartDate= DateTime.Now.AddDays(-10),
                              EndDate = DateTime.Now.AddDays(-5),
                              RentalId=4,
                              BrandId=4,
                              CarCategory=CarCategory.Convertible
                        },
                          new Car()
                        {

                              Name = "Range Rover",
                              Description  = "Description Range Rover",
                              Price = 30,
                              ImageURL = "https://i.pinimg.com/originals/86/21/c1/8621c157c34ff6edda40c51019403d09.jpg",
                              StartDate= DateTime.Now.AddDays(-10),
                              EndDate = DateTime.Now.AddDays(-5),
                              RentalId=4,
                              BrandId=4,
                              CarCategory=CarCategory.Convertible
                        },
                    });
                    context.SaveChanges();




                }

            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles Section
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //User section
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var adminUser = await userManager.FindByEmailAsync("admin@ubt-uni.net");
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Application Admin",
                        UserName = "app-admin",
                        Email = "admin@ubt-uni.net",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);

                }
                var appUser = await userManager.FindByEmailAsync("user@ubt-uni.net");
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = "user@ubt-uni.net",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
