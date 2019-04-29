﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Reviso.Data;

namespace Reviso.Data.Migrations
{
    [DbContext(typeof(RevisoContext))]
    [Migration("20190429155214_field_change")]
    partial class field_change
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Reviso.Domain.Entities.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("BaseRate");

                    b.Property<int>("ProjectId");

                    b.Property<decimal>("VatRate");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId")
                        .IsUnique();

                    b.ToTable("Contract");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BaseRate = 100m,
                            ProjectId = 1,
                            VatRate = 0.22m
                        },
                        new
                        {
                            Id = 2,
                            BaseRate = 110m,
                            ProjectId = 2,
                            VatRate = 0.18m
                        });
                });

            modelBuilder.Entity("Reviso.Domain.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContractId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ContractId")
                        .IsUnique();

                    b.ToTable("Customer");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContractId = 1,
                            Name = "Acme A/S"
                        },
                        new
                        {
                            Id = 2,
                            ContractId = 2,
                            Name = "Insurance Inc."
                        });
                });

            modelBuilder.Entity("Reviso.Domain.Entities.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Customer");

                    b.Property<DateTime>("DueDate");

                    b.Property<DateTime>("InvoiceDate");

                    b.Property<decimal>("Net");

                    b.Property<string>("Project");

                    b.Property<int>("ProjectId");

                    b.Property<decimal>("Vat");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId")
                        .IsUnique();

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("Reviso.Domain.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("End");

                    b.Property<string>("Name");

                    b.Property<DateTime>("Start");

                    b.HasKey("Id");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            End = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Portal Solution",
                            Start = new DateTime(2019, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            End = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "New Version 2.0",
                            Start = new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Reviso.Domain.Entities.RateInterval", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContractId");

                    b.Property<decimal>("DiscountFactor");

                    b.Property<int>("FromHours");

                    b.Property<int>("ToHours");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.ToTable("RateInterval");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContractId = 1,
                            DiscountFactor = 0m,
                            FromHours = 0,
                            ToHours = 2147483647
                        },
                        new
                        {
                            Id = 2,
                            ContractId = 2,
                            DiscountFactor = 0m,
                            FromHours = 0,
                            ToHours = 2147483647
                        });
                });

            modelBuilder.Entity("Reviso.Domain.Entities.TimeRegistration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int>("Hours");

                    b.Property<int>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("TimeRegistration");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2019, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Hours = 6,
                            ProjectId = 1
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Hours = 8,
                            ProjectId = 1
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTime(2019, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Hours = 7,
                            ProjectId = 1
                        },
                        new
                        {
                            Id = 4,
                            Date = new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Hours = 4,
                            ProjectId = 2
                        },
                        new
                        {
                            Id = 5,
                            Date = new DateTime(2019, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Hours = 9,
                            ProjectId = 2
                        });
                });

            modelBuilder.Entity("Reviso.Domain.Entities.Contract", b =>
                {
                    b.HasOne("Reviso.Domain.Entities.Project")
                        .WithOne("Contract")
                        .HasForeignKey("Reviso.Domain.Entities.Contract", "ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Reviso.Domain.Entities.Customer", b =>
                {
                    b.HasOne("Reviso.Domain.Entities.Contract")
                        .WithOne("Customer")
                        .HasForeignKey("Reviso.Domain.Entities.Customer", "ContractId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Reviso.Domain.Entities.Invoice", b =>
                {
                    b.HasOne("Reviso.Domain.Entities.Project")
                        .WithOne("Invoice")
                        .HasForeignKey("Reviso.Domain.Entities.Invoice", "ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Reviso.Domain.Entities.RateInterval", b =>
                {
                    b.HasOne("Reviso.Domain.Entities.Contract")
                        .WithMany("RateIntervals")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Reviso.Domain.Entities.TimeRegistration", b =>
                {
                    b.HasOne("Reviso.Domain.Entities.Project")
                        .WithMany("TimeRegistrations")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
