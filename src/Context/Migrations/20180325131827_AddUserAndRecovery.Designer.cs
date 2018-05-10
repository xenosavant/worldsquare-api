﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Stellmart.Context;
using Stellmart.Data;
using System;

namespace Stellmart.Migrations
{
    [DbContext(typeof(StellmartContext))]
    [Migration("20180325131827_AddUserAndRecovery")]
    partial class AddUserAndRecovery
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Stellmart.Data.ObjectModels.KeyRecoveryStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrderNumber");

                    b.Property<int>("SecurityQuestionId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SecurityQuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("KeyRecoveryStep");
                });

            modelBuilder.Entity("Stellmart.Data.ObjectModels.SecurityQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Question")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("SecurityQuestion");
                });

            modelBuilder.Entity("Stellmart.Data.ObjectModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<bool>("MustRecoverKey");

                    b.Property<bool>("MustResetPassword");

                    b.Property<string>("StellarPrivateKey");

                    b.Property<string>("StellarPublicKey");

                    b.Property<string>("StellarRecoveryKey");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<bool>("Verified");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("Stellmart.Data.ObjectModels.KeyRecoveryStep", b =>
                {
                    b.HasOne("Stellmart.Data.ObjectModels.SecurityQuestion", "SecurityQuestion")
                        .WithMany()
                        .HasForeignKey("SecurityQuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Stellmart.Data.ObjectModels.User", "User")
                        .WithMany("KeyRecoverySteps")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}