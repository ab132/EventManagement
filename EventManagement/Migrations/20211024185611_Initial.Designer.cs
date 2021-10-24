﻿// <auto-generated />
using System;
using EventManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EventManagement.Migrations
{
    [DbContext(typeof(EventModelContext))]
    [Migration("20211024185611_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EventManagement.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalInfo")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime?>("Date")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NumberOfGuests")
                        .HasColumnType("int");

                    b.Property<string>("Venue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AdditionalInfo = "Additional info 1",
                            Date = new DateTime(2021, 10, 26, 21, 56, 10, 625, DateTimeKind.Local).AddTicks(3930),
                            EventName = "Event1",
                            NumberOfGuests = 0,
                            Venue = "Venue1"
                        },
                        new
                        {
                            Id = 2,
                            AdditionalInfo = "Additional info 2",
                            Date = new DateTime(2021, 10, 29, 21, 56, 10, 632, DateTimeKind.Local).AddTicks(4362),
                            EventName = "Event2",
                            NumberOfGuests = 0,
                            Venue = "Venue2"
                        },
                        new
                        {
                            Id = 3,
                            AdditionalInfo = "Additional info 3",
                            Date = new DateTime(2021, 10, 31, 21, 56, 10, 632, DateTimeKind.Local).AddTicks(4436),
                            EventName = "Event3",
                            NumberOfGuests = 0,
                            Venue = "Venue3"
                        },
                        new
                        {
                            Id = 4,
                            AdditionalInfo = "Additional info 4",
                            Date = new DateTime(2021, 11, 3, 21, 56, 10, 632, DateTimeKind.Local).AddTicks(4444),
                            EventName = "Event4",
                            NumberOfGuests = 0,
                            Venue = "Venue4"
                        });
                });

            modelBuilder.Entity("EventManagement.Models.LegalPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalInfo")
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NumberOfGuests")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("PaymentOption")
                        .HasColumnType("int");

                    b.Property<string>("RegistryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("LegalPersons");
                });

            modelBuilder.Entity("EventManagement.Models.PrivateGuest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalInfo")
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentOption")
                        .HasColumnType("int");

                    b.Property<string>("PersonalIdentificationCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("PrivateGuests");
                });

            modelBuilder.Entity("EventManagement.Models.LegalPerson", b =>
                {
                    b.HasOne("EventManagement.Models.Event", "Event")
                        .WithMany("LegalPersons")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Event");
                });

            modelBuilder.Entity("EventManagement.Models.PrivateGuest", b =>
                {
                    b.HasOne("EventManagement.Models.Event", "Event")
                        .WithMany("PrivateGuests")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Event");
                });

            modelBuilder.Entity("EventManagement.Models.Event", b =>
                {
                    b.Navigation("LegalPersons");

                    b.Navigation("PrivateGuests");
                });
#pragma warning restore 612, 618
        }
    }
}