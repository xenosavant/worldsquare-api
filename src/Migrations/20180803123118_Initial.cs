using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stellmart.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Active = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    ParentCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractStates",
                columns: table => new
                {
                    Active = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractTypes",
                columns: table => new
                {
                    Active = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    PhonePrefix = table.Column<string>(nullable: true),
                    OrderNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Active = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistanceUnits",
                columns: table => new
                {
                    Active = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistanceUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FulfillmentStates",
                columns: table => new
                {
                    Active = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FulfillmentStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeoLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemConditions",
                columns: table => new
                {
                    Active = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuantityUnits",
                columns: table => new
                {
                    Active = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantityUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LocationComponents = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RewardsLevels",
                columns: table => new
                {
                    Active = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardsLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingCarriers",
                columns: table => new
                {
                    Active = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingCarriers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingManifests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingManifests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeUnits",
                columns: table => new
                {
                    Active = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeInStates",
                columns: table => new
                {
                    Active = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeInStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TwoFactorAuthenticationTypes",
                columns: table => new
                {
                    Active = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwoFactorAuthenticationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VerificationLevels",
                columns: table => new
                {
                    Active = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    EscrowAccountId = table.Column<string>(nullable: false),
                    CurrentSequenceNumber = table.Column<int>(nullable: false),
                    ContractStateId = table.Column<int>(nullable: false),
                    ContractTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_ContractStates_ContractStateId",
                        column: x => x.ContractStateId,
                        principalTable: "ContractStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_ContractTypes_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalTable: "ContractTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyAmounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    CurrencyTypeId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(14, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyAmounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyAmounts_Currencies_CurrencyTypeId",
                        column: x => x.CurrencyTypeId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    Radius = table.Column<double>(nullable: false),
                    GeoLocationId = table.Column<int>(nullable: false),
                    DistanceUnitTypeId = table.Column<int>(nullable: false),
                    DistanceUnitId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Areas_DistanceUnits_DistanceUnitId",
                        column: x => x.DistanceUnitId,
                        principalTable: "DistanceUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Areas_GeoLocations_GeoLocationId",
                        column: x => x.GeoLocationId,
                        principalTable: "GeoLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemMetaDatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    KeyWords = table.Column<string>(nullable: true),
                    ItemConditionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMetaDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemMetaDatas_ItemConditions_ItemConditionId",
                        column: x => x.ItemConditionId,
                        principalTable: "ItemConditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    GeoLocationId = table.Column<int>(nullable: false),
                    LocationComponents = table.Column<string>(nullable: true),
                    PlaceId = table.Column<string>(nullable: true),
                    Verified = table.Column<bool>(nullable: false),
                    RegionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_GeoLocations_GeoLocationId",
                        column: x => x.GeoLocationId,
                        principalTable: "GeoLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Locations_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractPhases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    ContractId = table.Column<int>(nullable: false),
                    SequenceNumber = table.Column<int>(nullable: false),
                    Completed = table.Column<bool>(nullable: false),
                    Contested = table.Column<bool>(nullable: false),
                    TimeDelay = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractPhases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractPhases_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PricePerDistances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CurrencyAmountId = table.Column<int>(nullable: false),
                    DistanceUnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricePerDistances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PricePerDistances_CurrencyAmounts_CurrencyAmountId",
                        column: x => x.CurrencyAmountId,
                        principalTable: "CurrencyAmounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PricePerDistances_DistanceUnits_DistanceUnitId",
                        column: x => x.DistanceUnitId,
                        principalTable: "DistanceUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PricePerTimes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CurrencyAmountId = table.Column<int>(nullable: false),
                    TimeUnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricePerTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PricePerTimes_CurrencyAmounts_CurrencyAmountId",
                        column: x => x.CurrencyAmountId,
                        principalTable: "CurrencyAmounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PricePerTimes_TimeUnits_TimeUnitId",
                        column: x => x.TimeUnitId,
                        principalTable: "TimeUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemMetaDataCategories",
                columns: table => new
                {
                    ItemMetaDataId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMetaDataCategories", x => new { x.ItemMetaDataId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ItemMetaDataCategories_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemMetaDataCategories_ItemMetaDatas_ItemMetaDataId",
                        column: x => x.ItemMetaDataId,
                        principalTable: "ItemMetaDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    MustRecoverKey = table.Column<bool>(nullable: false),
                    MustResetKey = table.Column<bool>(nullable: false),
                    StellarPublicKey = table.Column<string>(nullable: true),
                    StellarEncryptedSecretKey = table.Column<byte[]>(nullable: true),
                    StellarRecoveryKey = table.Column<byte[]>(nullable: true),
                    StellarSecretKeyIv = table.Column<byte[]>(nullable: true),
                    ManagedAccount = table.Column<bool>(nullable: false),
                    PrimaryShippingLocationId = table.Column<int>(nullable: false),
                    RewardsLevelId = table.Column<int>(nullable: false),
                    TwoFactorTypeId = table.Column<int>(nullable: false),
                    NativeCurrencyId = table.Column<int>(nullable: false),
                    VerificationLevelId = table.Column<int>(nullable: false),
                    Flagged = table.Column<bool>(nullable: false),
                    UseTwoFactorForLogin = table.Column<bool>(nullable: false),
                    SecurityQuestions = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Currencies_NativeCurrencyId",
                        column: x => x.NativeCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Locations_PrimaryShippingLocationId",
                        column: x => x.PrimaryShippingLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_RewardsLevels_RewardsLevelId",
                        column: x => x.RewardsLevelId,
                        principalTable: "RewardsLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_TwoFactorAuthenticationTypes_TwoFactorTypeId",
                        column: x => x.TwoFactorTypeId,
                        principalTable: "TwoFactorAuthenticationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_VerificationLevels_VerificationLevelId",
                        column: x => x.VerificationLevelId,
                        principalTable: "VerificationLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PreTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    PreTransactionTypeId = table.Column<int>(nullable: false),
                    XdrString = table.Column<string>(nullable: true),
                    Submitted = table.Column<bool>(nullable: false),
                    ContractPhaseId = table.Column<int>(nullable: false),
                    MinimumTime = table.Column<DateTime>(nullable: false),
                    MaximumTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreTransactions_ContractPhases_ContractPhaseId",
                        column: x => x.ContractPhaseId,
                        principalTable: "ContractPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KycDatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    AddressLine3 = table.Column<string>(nullable: true),
                    AddressLine4 = table.Column<string>(nullable: true),
                    AddressLine5 = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KycDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KycDatas_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KycDatas_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageThreads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    InitiatorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageThreads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageThreads_AspNetUsers_InitiatorId",
                        column: x => x.InitiatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ReviewerId = table.Column<int>(nullable: false),
                    Stars = table.Column<int>(nullable: false),
                    Body = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    DestinationId = table.Column<int>(nullable: true),
                    Fulfilled = table.Column<bool>(nullable: false),
                    LocationId = table.Column<int>(nullable: true),
                    RequestorId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_AspNetUsers_RequestorId",
                        column: x => x.RequestorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_AspNetUsers_RequestorId1",
                        column: x => x.RequestorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_Locations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TagLine = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    Verified = table.Column<bool>(nullable: false),
                    NativeCurrencyId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ServiceAreaId = table.Column<int>(nullable: true),
                    Internal = table.Column<bool>(nullable: true),
                    Global = table.Column<bool>(nullable: true),
                    OnlineStore_Internal = table.Column<bool>(nullable: true),
                    ServiceRegionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Areas_ServiceAreaId",
                        column: x => x.ServiceAreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_AspNetUsers_UserId1",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_Regions_ServiceRegionId",
                        column: x => x.ServiceRegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_AspNetUsers_UserId2",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_AspNetUsers_UserId3",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_Currencies_NativeCurrencyId",
                        column: x => x.NativeCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TradeItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    OwnerId = table.Column<int>(nullable: false),
                    TradeInValueId = table.Column<int>(nullable: true),
                    TradeInStateId = table.Column<int>(nullable: true),
                    ItemMetaDataId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeItems_ItemMetaDatas_ItemMetaDataId",
                        column: x => x.ItemMetaDataId,
                        principalTable: "ItemMetaDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeItems_TradeInStates_TradeInStateId",
                        column: x => x.TradeInStateId,
                        principalTable: "TradeInStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeItems_AspNetUsers_TradeInValueId",
                        column: x => x.TradeInValueId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeItems_CurrencyAmounts_TradeInValueId",
                        column: x => x.TradeInValueId,
                        principalTable: "CurrencyAmounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Signatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    PreTransactionId = table.Column<int>(nullable: false),
                    Signed = table.Column<bool>(nullable: false),
                    SignedOn = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    SignerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Signatures_PreTransactions_PreTransactionId",
                        column: x => x.PreTransactionId,
                        principalTable: "PreTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Signatures_AspNetUsers_SignerId",
                        column: x => x.SignerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MessageThreadId = table.Column<int>(nullable: false),
                    PosterId = table.Column<int>(nullable: false),
                    PostedOn = table.Column<DateTime>(nullable: false),
                    Body = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_MessageThreads_MessageThreadId",
                        column: x => x.MessageThreadId,
                        principalTable: "MessageThreads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_PosterId",
                        column: x => x.PosterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductShipments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    SenderId = table.Column<int>(nullable: true),
                    ReceiverId = table.Column<int>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    ShippedOn = table.Column<DateTime>(nullable: false),
                    DeliveredOn = table.Column<DateTime>(nullable: false),
                    FulfilledInternally = table.Column<bool>(nullable: false),
                    Internal = table.Column<bool>(nullable: false),
                    TradeIn = table.Column<bool>(nullable: false),
                    ShippingCarrierId = table.Column<int>(nullable: true),
                    DeliveryRequestId = table.Column<int>(nullable: true),
                    ShippingManifestId = table.Column<int>(nullable: false),
                    ContractId = table.Column<int>(nullable: false),
                    FulfillmentStateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductShipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductShipments_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductShipments_ServiceRequests_DeliveryRequestId",
                        column: x => x.DeliveryRequestId,
                        principalTable: "ServiceRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductShipments_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductShipments_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductShipments_ShippingCarriers_ShippingCarrierId",
                        column: x => x.ShippingCarrierId,
                        principalTable: "ShippingCarriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductShipments_ShippingManifests_ShippingManifestId",
                        column: x => x.ShippingManifestId,
                        principalTable: "ShippingManifests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Flagged = table.Column<bool>(nullable: false),
                    ThreadId = table.Column<int>(nullable: true),
                    Internal = table.Column<bool>(nullable: false),
                    ItemMetaDataId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listings_ItemMetaDatas_ItemMetaDataId",
                        column: x => x.ItemMetaDataId,
                        principalTable: "ItemMetaDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Listings_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Listings_MessageThreads_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "MessageThreads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OnlineStoreReviews",
                columns: table => new
                {
                    OnlineStoreId = table.Column<int>(nullable: false),
                    ReviewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineStoreReviews", x => new { x.OnlineStoreId, x.ReviewId });
                    table.ForeignKey(
                        name: "FK_OnlineStoreReviews_Services_OnlineStoreId",
                        column: x => x.OnlineStoreId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OnlineStoreReviews_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequestFulfillments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false),
                    ServiceRequestId = table.Column<int>(nullable: false),
                    ContractId = table.Column<int>(nullable: false),
                    FulfillmentStateId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequestFulfillments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequestFulfillments_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRequestFulfillments_FulfillmentStates_FulfillmentStateId",
                        column: x => x.FulfillmentStateId,
                        principalTable: "FulfillmentStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRequestFulfillments_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRequestFulfillments_ServiceRequests_ServiceRequestId",
                        column: x => x.ServiceRequestId,
                        principalTable: "ServiceRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    ListingId = table.Column<int>(nullable: true),
                    UnitPriceId = table.Column<int>(nullable: false),
                    UnitTypeId = table.Column<int>(nullable: false),
                    Descriptors = table.Column<string>(nullable: true),
                    UPC = table.Column<string>(nullable: true),
                    SKU = table.Column<string>(nullable: true),
                    UnitsAvailable = table.Column<int>(nullable: false),
                    UnitsSold = table.Column<int>(nullable: false),
                    UnitsReturned = table.Column<int>(nullable: false),
                    PriceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryItems_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryItems_CurrencyAmounts_PriceId",
                        column: x => x.PriceId,
                        principalTable: "CurrencyAmounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryItems_QuantityUnits_UnitTypeId",
                        column: x => x.UnitTypeId,
                        principalTable: "QuantityUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LineItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    InventoryItemId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ShippingManifestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineItems_InventoryItems_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "InventoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShippingManifestLineItems",
                columns: table => new
                {
                    ShippingManifestId = table.Column<int>(nullable: false),
                    LineItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingManifestLineItems", x => new { x.ShippingManifestId, x.LineItemId });
                    table.ForeignKey(
                        name: "FK_ShippingManifestLineItems_LineItems_LineItemId",
                        column: x => x.LineItemId,
                        principalTable: "LineItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShippingManifestLineItems_ShippingManifests_ShippingManifestId",
                        column: x => x.ShippingManifestId,
                        principalTable: "ShippingManifests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Areas_DistanceUnitId",
                table: "Areas",
                column: "DistanceUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_GeoLocationId",
                table: "Areas",
                column: "GeoLocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CountryId",
                table: "AspNetUsers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NativeCurrencyId",
                table: "AspNetUsers",
                column: "NativeCurrencyId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PrimaryShippingLocationId",
                table: "AspNetUsers",
                column: "PrimaryShippingLocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RewardsLevelId",
                table: "AspNetUsers",
                column: "RewardsLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TwoFactorTypeId",
                table: "AspNetUsers",
                column: "TwoFactorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_VerificationLevelId",
                table: "AspNetUsers",
                column: "VerificationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId",
                unique: true,
                filter: "[ParentCategoryId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContractPhases_ContractId",
                table: "ContractPhases",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractStateId",
                table: "Contracts",
                column: "ContractStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractTypeId",
                table: "Contracts",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyAmounts_CurrencyTypeId",
                table: "CurrencyAmounts",
                column: "CurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ListingId",
                table: "InventoryItems",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_PriceId",
                table: "InventoryItems",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_UnitTypeId",
                table: "InventoryItems",
                column: "UnitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMetaDataCategories_CategoryId",
                table: "ItemMetaDataCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMetaDatas_ItemConditionId",
                table: "ItemMetaDatas",
                column: "ItemConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_KycDatas_CountryId",
                table: "KycDatas",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_KycDatas_UserId",
                table: "KycDatas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_InventoryItemId",
                table: "LineItems",
                column: "InventoryItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Listings_ItemMetaDataId",
                table: "Listings",
                column: "ItemMetaDataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Listings_ServiceId",
                table: "Listings",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_ThreadId",
                table: "Listings",
                column: "ThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_GeoLocationId",
                table: "Locations",
                column: "GeoLocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_RegionId",
                table: "Locations",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageThreadId",
                table: "Messages",
                column: "MessageThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_PosterId",
                table: "Messages",
                column: "PosterId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageThreads_InitiatorId",
                table: "MessageThreads",
                column: "InitiatorId");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineStoreReviews_ReviewId",
                table: "OnlineStoreReviews",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_PreTransactions_ContractPhaseId",
                table: "PreTransactions",
                column: "ContractPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PricePerDistances_CurrencyAmountId",
                table: "PricePerDistances",
                column: "CurrencyAmountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PricePerDistances_DistanceUnitId",
                table: "PricePerDistances",
                column: "DistanceUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PricePerTimes_CurrencyAmountId",
                table: "PricePerTimes",
                column: "CurrencyAmountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PricePerTimes_TimeUnitId",
                table: "PricePerTimes",
                column: "TimeUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductShipments_ContractId",
                table: "ProductShipments",
                column: "ContractId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductShipments_DeliveryRequestId",
                table: "ProductShipments",
                column: "DeliveryRequestId",
                unique: true,
                filter: "[DeliveryRequestId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductShipments_ReceiverId",
                table: "ProductShipments",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductShipments_SenderId",
                table: "ProductShipments",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductShipments_ShippingCarrierId",
                table: "ProductShipments",
                column: "ShippingCarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductShipments_ShippingManifestId",
                table: "ProductShipments",
                column: "ShippingManifestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewerId",
                table: "Reviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestFulfillments_ContractId",
                table: "ServiceRequestFulfillments",
                column: "ContractId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestFulfillments_FulfillmentStateId",
                table: "ServiceRequestFulfillments",
                column: "FulfillmentStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestFulfillments_ServiceId",
                table: "ServiceRequestFulfillments",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestFulfillments_ServiceRequestId",
                table: "ServiceRequestFulfillments",
                column: "ServiceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_RequestorId",
                table: "ServiceRequests",
                column: "RequestorId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_RequestorId1",
                table: "ServiceRequests",
                column: "RequestorId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_DestinationId",
                table: "ServiceRequests",
                column: "DestinationId",
                unique: true,
                filter: "[DestinationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_LocationId",
                table: "ServiceRequests",
                column: "LocationId",
                unique: true,
                filter: "[LocationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceAreaId",
                table: "Services",
                column: "ServiceAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UserId",
                table: "Services",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UserId1",
                table: "Services",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceRegionId",
                table: "Services",
                column: "ServiceRegionId",
                unique: true,
                filter: "[ServiceRegionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UserId2",
                table: "Services",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UserId3",
                table: "Services",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_NativeCurrencyId",
                table: "Services",
                column: "NativeCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingManifestLineItems_LineItemId",
                table: "ShippingManifestLineItems",
                column: "LineItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Signatures_PreTransactionId",
                table: "Signatures",
                column: "PreTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Signatures_SignerId",
                table: "Signatures",
                column: "SignerId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeItems_ItemMetaDataId",
                table: "TradeItems",
                column: "ItemMetaDataId",
                unique: true,
                filter: "[ItemMetaDataId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TradeItems_TradeInStateId",
                table: "TradeItems",
                column: "TradeInStateId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeItems_TradeInValueId",
                table: "TradeItems",
                column: "TradeInValueId",
                unique: true,
                filter: "[TradeInValueId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ItemMetaDataCategories");

            migrationBuilder.DropTable(
                name: "KycDatas");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "OnlineStoreReviews");

            migrationBuilder.DropTable(
                name: "PricePerDistances");

            migrationBuilder.DropTable(
                name: "PricePerTimes");

            migrationBuilder.DropTable(
                name: "ProductShipments");

            migrationBuilder.DropTable(
                name: "ServiceRequestFulfillments");

            migrationBuilder.DropTable(
                name: "ShippingManifestLineItems");

            migrationBuilder.DropTable(
                name: "Signatures");

            migrationBuilder.DropTable(
                name: "TradeItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "TimeUnits");

            migrationBuilder.DropTable(
                name: "ShippingCarriers");

            migrationBuilder.DropTable(
                name: "FulfillmentStates");

            migrationBuilder.DropTable(
                name: "ServiceRequests");

            migrationBuilder.DropTable(
                name: "LineItems");

            migrationBuilder.DropTable(
                name: "ShippingManifests");

            migrationBuilder.DropTable(
                name: "PreTransactions");

            migrationBuilder.DropTable(
                name: "TradeInStates");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropTable(
                name: "ContractPhases");

            migrationBuilder.DropTable(
                name: "Listings");

            migrationBuilder.DropTable(
                name: "CurrencyAmounts");

            migrationBuilder.DropTable(
                name: "QuantityUnits");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "ItemMetaDatas");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "MessageThreads");

            migrationBuilder.DropTable(
                name: "ContractStates");

            migrationBuilder.DropTable(
                name: "ContractTypes");

            migrationBuilder.DropTable(
                name: "ItemConditions");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DistanceUnits");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "RewardsLevels");

            migrationBuilder.DropTable(
                name: "TwoFactorAuthenticationTypes");

            migrationBuilder.DropTable(
                name: "VerificationLevels");

            migrationBuilder.DropTable(
                name: "GeoLocations");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
