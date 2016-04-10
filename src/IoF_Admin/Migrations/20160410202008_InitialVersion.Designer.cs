using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using IoF_Admin.Models;

namespace IoF_Admin.Migrations
{
    [DbContext(typeof(IoFContext))]
    [Migration("20160410202008_InitialVersion")]
    partial class InitialVersion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("IoF_Admin.Models.Aquarium", b =>
                {
                    b.Property<int>("AquariumID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HardwareID")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<int>("OfficeID");

                    b.HasKey("AquariumID");
                });

            modelBuilder.Entity("IoF_Admin.Models.Fish", b =>
                {
                    b.Property<int>("FishID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AquariumID");

                    b.Property<int>("Channel");

                    b.Property<int>("OfficeID");

                    b.Property<int>("SecondsActive");

                    b.HasKey("FishID");
                });

            modelBuilder.Entity("IoF_Admin.Models.Office", b =>
                {
                    b.Property<int>("OfficeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("ContactPerson");

                    b.Property<string>("ContactPhone");

                    b.Property<string>("CountryCode")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.HasKey("OfficeID");
                });

            modelBuilder.Entity("IoF_Admin.Models.Aquarium", b =>
                {
                    b.HasOne("IoF_Admin.Models.Office")
                        .WithMany()
                        .HasForeignKey("OfficeID");
                });

            modelBuilder.Entity("IoF_Admin.Models.Fish", b =>
                {
                    b.HasOne("IoF_Admin.Models.Aquarium")
                        .WithMany()
                        .HasForeignKey("AquariumID");

                    b.HasOne("IoF_Admin.Models.Office")
                        .WithMany()
                        .HasForeignKey("OfficeID");
                });
        }
    }
}