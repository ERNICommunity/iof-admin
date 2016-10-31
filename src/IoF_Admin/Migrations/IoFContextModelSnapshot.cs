using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using IoF_Admin.Models;

namespace IoF_Admin.Migrations
{
    [DbContext(typeof(IoFContext))]
    partial class IoFContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("IoF_Admin.Models.Aquarium", b =>
                {
                    b.Property<int>("AquariumID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HardwareID")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<int?>("OfficeID");

                    b.HasKey("AquariumID");

                    b.HasIndex("OfficeID");

                    b.ToTable("Aquariums");
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

                    b.HasIndex("AquariumID");

                    b.HasIndex("OfficeID");

                    b.ToTable("Fishes");
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
                        .IsRequired()
                        .HasAnnotation("MaxLength", 2);

                    b.Property<string>("Name");

                    b.HasKey("OfficeID");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("IoF_Admin.Models.Aquarium", b =>
                {
                    b.HasOne("IoF_Admin.Models.Office", "Office")
                        .WithMany()
                        .HasForeignKey("OfficeID");
                });

            modelBuilder.Entity("IoF_Admin.Models.Fish", b =>
                {
                    b.HasOne("IoF_Admin.Models.Aquarium", "Aquarium")
                        .WithMany("Fishes")
                        .HasForeignKey("AquariumID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("IoF_Admin.Models.Office", "Office")
                        .WithMany()
                        .HasForeignKey("OfficeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
