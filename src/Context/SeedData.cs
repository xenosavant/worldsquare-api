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
            // context.Database.Migrate();

            //  Look for any users.
            if (context.Users.Any())
                {
                    return; // DB has been seeded
                }

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var geolocation = new GeoLocation()
                    {
                        Latitude = 51.5073509,
                        Longitude = -0.12775829999998223,
                        IsDeleted = false,
                        UniqueId = Guid.NewGuid()
                    };

                    context.GeoLocations.Add(geolocation);

                    var location = new Location()
                    {
                        GeoLocation = geolocation,
                        Address = "11 Victoria Street",
                        IsDeleted = false,
                        LocationComponents = string.Empty,
                        UniqueId = Guid.NewGuid(),
                        Verified = true
                    };

                    context.Locations.Add(location);

                    var rewardsLevel = new RewardsLevel()
                    {
                        Active = true,
                        Description = "Level 1",
                        DisplayOrder = 1
                    };

                    context.RewardsLevels.Add(rewardsLevel);

                    var conditions = new ReadonlyViewModel[]
                        {
                            new ReadonlyViewModel()
                            {
                                Active = true,
                                Description = "New",
                                DisplayOrder = 1
                            },
                            new ReadonlyViewModel()
                            {
                                Active = true,
                                Description = "Like New",
                                DisplayOrder = 2
                            },
                            new ReadonlyViewModel()
                            {
                                Active = true,
                                Description = "Very Good",
                                DisplayOrder = 3
                            },
                            new ReadonlyViewModel()
                            {
                                Active = true,
                                Description = "Good",
                                DisplayOrder = 4
                            },
                            new ReadonlyViewModel()
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

                    var currency = new Currency()
                    {
                        Active = true,
                        Description = "USD",
                        DisplayOrder = 1
                    };

                    context.Currencies.Add(currency);

                    var twoFactor = new TwoFactorAuthenticationType()
                    {
                        Active = true,
                        Description = "Google",
                        DisplayOrder = 1
                    };

                    context.TwoFactorAuthenticationTypes.Add(twoFactor);

                    var verificationLevel = new VerificationLevel()
                    {
                        Active = true,
                        Description = "Level 1",
                        DisplayOrder = 1
                    };

                    // Add Some dummy categories

                    context.VerificationLevels.Add(verificationLevel);

                    var user = new ApplicationUser
                    {
                        Email = configuration["SeedData:InitialAdminUser"],
                        UserName = configuration["SeedData:InitialAdminUser"],
                        NativeCurrency = currency,
                        PrimaryShippingLocation = location,
                        RewardsLevel = rewardsLevel,
                        TwoFactorAuthenticationType = twoFactor,
                        VerificationLevel = verificationLevel,
                        CreatedBy = 1,
                        EmailConfirmed = true,
                        FirstName = "Elton",
                        LastName = "John",
                        IsActive = true,
                        IsDeleted = false,
                        LockoutEnabled = false,
                        PhoneNumber = "00443239923023",
                        PhoneNumberConfirmed = true,
                        UniqueId = Guid.NewGuid()
                    };

                    var password = configuration["SeedData:InitialAdminPassword"];

                    // Result used to avoid "The connection does not support MultipleActiveResultSets" since usermanager doesn't have non asynch method
                    var obj = userManager.CreateAsync(user, password).Result;
                    var obj2 = userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "admin")).Result;

                    var internalStore = new OnlineStore()
                    {
                        Internal = true,
                        Global = true,
                        Name = "WorldSquare Store",
                        Description = "A place to find odds and ends",
                        TagLine = "The entire world, squared",
                        Verified = true,
                        UserId = 1,
                        NativeCurrencyId = 1
                    };

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
