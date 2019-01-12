using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Context.Entities.ReadOnly;
using Stellmart.Context;
using System;
using System.Linq;
using System.Security.Claims;

namespace Stellmart.Api.Context
{
    public class SeedData
    {
        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            // ensure database exists and migration applied
            context.Database.Migrate();

            //  Look for any users.
            if (context.Users.Any())
            {
                return; // DB has been seeded
            }

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var securityQuestions = new SecurityQuestion[]
                    {
                        new SecurityQuestion
                        {
                            DisplayOrder = 1,
                            Description = "Who was your childhood hero?",
                            Active = true
                        },
                        new SecurityQuestion
                        {
                            DisplayOrder = 2,
                            Description = "What is your oldest cousin's first and last name?",
                            Active = true
                        }
                    };

                    context.SecurityQuestions.AddRange(securityQuestions);

                    var rewardsLevel = new RewardsLevel()
                    {
                        Active = true,
                        Description = "Level 1",
                        DisplayOrder = 1
                    };

                    context.RewardsLevels.Add(rewardsLevel);

                    var conditions = new ItemCondition[]
                        {
                            new ItemCondition()
                            {
                                Active = true,
                                Description = "New",
                                DisplayOrder = 1
                            },
                            new ItemCondition()
                            {
                                Active = true,
                                Description = "Like New",
                                DisplayOrder = 2
                            },
                            new ItemCondition()
                            {
                                Active = true,
                                Description = "Very Good",
                                DisplayOrder = 3
                            },
                            new ItemCondition()
                            {
                                Active = true,
                                Description = "Good",
                                DisplayOrder = 4
                            },
                            new ItemCondition()
                            {
                                Active = true,
                                Description = "Fair",
                                DisplayOrder = 5
                            }
                    };

                    context.ItemConditions.AddRange(conditions);

                    var quantityUnits = new QuantityUnit[]
                    {
                        new QuantityUnit
                        {
                            Active = true,
                            Description = "Ordinal",
                            DisplayOrder = 1
                        }
                    };

                    context.QuantityUnits.AddRange(quantityUnits);

                    var currencies  = 
                        new Currency[] {
                            new Currency()
                            {
                                Active = true,
                                Description = "USD",
                                DisplayOrder = 1,
                                Precision = 100
                            },
                            new Currency()
                            {
                                Active = true,
                                Description = "XLM",
                                DisplayOrder = 1,
                                Precision = 10000000
                            }
                        };

                    context.Currencies.AddRange(currencies);

                    var twoFactors = new TwoFactorAuthenticationType[]
                    {
                        new TwoFactorAuthenticationType
                        {
                            Active = true,
                            Description = "Email",
                            DisplayOrder = 1
                        },

                        new TwoFactorAuthenticationType()
                        {
                            Active = true,
                            Description = "SMS",
                            DisplayOrder = 2
                        },

                        new TwoFactorAuthenticationType()
                        {
                            Active = true,
                            Description = "Google Authenticator",
                            DisplayOrder = 3
                        }
                    };

                    context.TwoFactorAuthenticationTypes.AddRange(twoFactors);

                    var verificationLevels = new VerificationLevel[]
                    {
                        new VerificationLevel()
                        {
                            Active = true,
                            Description = "Non verified",
                            DisplayOrder = 1
                        },
                        new VerificationLevel()
                        {
                            Active = true,
                            Description = "Level 1",
                            DisplayOrder = 2
                        },
                        new VerificationLevel()
                        {
                            Active = true,
                            Description = "Level 2",
                            DisplayOrder = 3
                        }
                    };

                    context.VerificationLevels.AddRange(verificationLevels);

                    var categories = new Category[]
                    {
                        new Category()
                        {
                            Active = true,
                            Description = "Apple iPhone",
                            DisplayOrder = 1,
                            ParentCategory =
                            new Category()
                            {
                                  Active = true,
                                  Description = "Cell Phones",
                                  DisplayOrder = 1,
                                  ParentCategory =
                                  new Category()
                                  {
                                      Active = true,
                                      Description = "Electronics",
                                      DisplayOrder = 1
                                  }
                            }
                        }
                    };

                    context.Categories.AddRange(categories);

                    var user = new ApplicationUser
                    {
                        Email = configuration["SeedData:InitialAdminUser"],
                        UserName = configuration["SeedData:InitialAdminUser"],
                        NativeCurrency = currencies[0],
                        RewardsLevel = rewardsLevel,
                        TwoFactorAuthenticationType = twoFactors[2],
                        VerificationLevel = verificationLevels[0],
                        CreatedBy = 1,
                        EmailConfirmed = true,
                        FirstName = "Elton",
                        LastName = "John",
                        IsActive = true,
                        IsDeleted = false,
                        LockoutEnabled = false,
                        PhoneNumber = "+14349899872",
                        PhoneNumberConfirmed = true,
                        UniqueId = Guid.NewGuid(),
                        SecurityQuestions = "[{\"order\":1,\"question\":\"Who was your childhood hero?\"},{\"order\":2,\"question\":\"What is your oldest cousin's first and last name?\"}]"
                    };

                    var password = configuration["SeedData:InitialAdminPassword"];

                    // Result used to avoid "The connection does not support MultipleActiveResultSets" since usermanager doesn't have synchronous method
                    var obj = userManager.CreateAsync(user, password).Result;
                    var obj2 = userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "admin")).Result;

                    context.SaveChanges();

                    var internalStore = new OnlineStore()
                    {
                        UniqueId = Guid.NewGuid(),
                        Internal = true,
                        Global = true,
                        Name = "WorldSquare Store",
                        Description = "A place to find odds and ends",
                        TagLine = "The entire world, squared",
                        Verified = true,
                        User = user,
                        NativeCurrency = currencies[0]
                    };

                    context.OnlineStores.Add(internalStore);

                    var geolocation = new GeoLocation()
                    {
                        Latitude = 51.5073509,
                        Longitude = -0.12775829999998223,
                        IsDeleted = false
                    };

                    context.GeoLocations.Add(geolocation);

                    var location = new Location()
                    {
                        GeoLocation = geolocation,
                        Address = "422 Massachusetts Ave, Arlington, MA 02474, USA",
                        IsDeleted = false,
                        LocationComponentsFromApp = "{\"search\":\"4\",\"route\":\"9th Avenue\",\"street_number\":\"432\",\"locality\":\"New York\",\"administrative_area_level_1\":\"NY\",\"administrative_area_level_2\":\"New York County\",\"sublocality_level_1\":\"Manhattan\",\"neighborhood\":\"\",\"postal_code\":\"10001\",\"country\":\"United States\"}",
                        LocationComponentsFromGoogleApi = "[ { \"long_name\": \"422\", \"short_name\": \"422\", \"types\": [ \"street_number\" ] }, { \"long_name\": \"Massachusetts Avenue\", \"short_name\": \"Massachusetts Ave\", \"types\": [ \"route\" ] }, { \"long_name\": \"Arlington\", \"short_name\": \"Arlington\", \"types\": [ \"locality\", \"political\" ] }, { \"long_name\": \"Middlesex County\", \"short_name\": \"Middlesex County\", \"types\": [ \"administrative_area_level_2\", \"political\" ] }, { \"long_name\": \"Massachusetts\", \"short_name\": \"MA\", \"types\": [ \"administrative_area_level_1\", \"political\" ] }, { \"long_name\": \"United States\", \"short_name\": \"US\", \"types\": [ \"country\", \"political\" ] }, { \"long_name\": \"02474\", \"short_name\": \"02474\", \"types\": [ \"postal_code\" ] }, { \"long_name\": \"6725\", \"short_name\": \"6725\", \"types\": [ \"postal_code_suffix\" ] } ]",
                        Verified = true,
                        IsActive = true,
                        IsDefault = true,
                        User = user
                    };

                    context.Locations.Add(location);

                    context.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
        }
    }
}
