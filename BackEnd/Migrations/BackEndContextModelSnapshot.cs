﻿// <auto-generated />
using System;
using BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackEnd.Migrations
{
    [DbContext(typeof(BackEndContext))]
    partial class BackEndContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CommonModels.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActivityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ActivityPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ActivityProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ResortId")
                        .HasColumnType("int");

                    b.HasKey("ActivityId");

                    b.HasIndex("PostalCode");

                    b.HasIndex("ResortId");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("CommonModels.ActivityReservation", b =>
                {
                    b.Property<int>("ActivityReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ActivityReservationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("CabinReservationId")
                        .HasColumnType("int");

                    b.HasKey("ActivityReservationId");

                    b.HasIndex("ActivityId");

                    b.HasIndex("CabinReservationId");

                    b.ToTable("ActivityReservation");
                });

            modelBuilder.Entity("CommonModels.Cabin", b =>
                {
                    b.Property<int>("CabinId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Area")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CabinName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CabinPricePerDay")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ResortId")
                        .HasColumnType("int");

                    b.Property<int>("Rooms")
                        .HasColumnType("int");

                    b.HasKey("CabinId");

                    b.HasIndex("PersonId");

                    b.HasIndex("PostalCode");

                    b.HasIndex("ResortId");

                    b.ToTable("Cabin");
                });

            modelBuilder.Entity("CommonModels.CabinImage", b =>
                {
                    b.Property<int>("CabinImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CabinId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CabinImageId");

                    b.HasIndex("CabinId");

                    b.HasIndex("ImageUrl")
                        .IsUnique()
                        .HasFilter("[ImageUrl] IS NOT NULL");

                    b.ToTable("CabinImage");
                });

            modelBuilder.Entity("CommonModels.CabinReservation", b =>
                {
                    b.Property<int>("CabinReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CabinId")
                        .HasColumnType("int");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReservationBookingTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReservationEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReservationStartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CabinReservationId");

                    b.HasIndex("CabinId");

                    b.HasIndex("PersonId");

                    b.ToTable("CabinReservation");
                });

            modelBuilder.Entity("CommonModels.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CabinReservationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfExpiry")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("InvoiceTotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("PaidYesNo")
                        .HasColumnType("bit");

                    b.Property<decimal>("Vat")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("InvoiceId");

                    b.HasIndex("CabinReservationId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("CommonModels.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SocialSecurityNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonId");

                    b.HasIndex("PostalCode");

                    b.ToTable("Person");

                    b.HasData(
                        new
                        {
                            PersonId = 1,
                            Address = "admin@localhost.org",
                            Email = "admin@localhost.org",
                            FirstName = "admin@localhost.org",
                            LastName = "admin@localhost.org",
                            PhoneNumber = "admin@localhost.org",
                            PostalCode = "00100",
                            SocialSecurityNumber = "admin@localhost.org"
                        });
                });

            modelBuilder.Entity("CommonModels.Post", b =>
                {
                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostalCode");

                    b.ToTable("Post");

                    b.HasData(
                        new
                        {
                            PostalCode = "00100",
                            City = "Helsinki"
                        });
                });

            modelBuilder.Entity("CommonModels.Resort", b =>
                {
                    b.Property<int>("ResortId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ResortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ResortId");

                    b.HasIndex("ResortName")
                        .IsUnique();

                    b.ToTable("Resort");
                });

            modelBuilder.Entity("CommonModels.Activity", b =>
                {
                    b.HasOne("CommonModels.Post", "Post")
                        .WithMany("Activities")
                        .HasForeignKey("PostalCode");

                    b.HasOne("CommonModels.Resort", "Resort")
                        .WithMany("Activities")
                        .HasForeignKey("ResortId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("CommonModels.ActivityReservation", b =>
                {
                    b.HasOne("CommonModels.Activity", "Activity")
                        .WithMany("ActivityReservations")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CommonModels.CabinReservation", "CabinReservation")
                        .WithMany("ActivityReservations")
                        .HasForeignKey("CabinReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CommonModels.Cabin", b =>
                {
                    b.HasOne("CommonModels.Person", "Person")
                        .WithMany("Cabins")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CommonModels.Post", "Post")
                        .WithMany("Cabins")
                        .HasForeignKey("PostalCode")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CommonModels.Resort", "Resort")
                        .WithMany("Cabins")
                        .HasForeignKey("ResortId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("CommonModels.CabinImage", b =>
                {
                    b.HasOne("CommonModels.Cabin", "Cabin")
                        .WithMany("CabinImages")
                        .HasForeignKey("CabinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CommonModels.CabinReservation", b =>
                {
                    b.HasOne("CommonModels.Cabin", "Cabin")
                        .WithMany("CabinReservations")
                        .HasForeignKey("CabinId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CommonModels.Person", "Person")
                        .WithMany("CabinReservations")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("CommonModels.Invoice", b =>
                {
                    b.HasOne("CommonModels.CabinReservation", "CabinReservation")
                        .WithMany("Invoices")
                        .HasForeignKey("CabinReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CommonModels.Person", b =>
                {
                    b.HasOne("CommonModels.Post", "Post")
                        .WithMany("Persons")
                        .HasForeignKey("PostalCode")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}