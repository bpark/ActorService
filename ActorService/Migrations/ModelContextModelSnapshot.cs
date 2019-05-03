﻿// <auto-generated />

using ActorService.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ActorService.Migrations
{
    [DbContext(typeof(ModelContext))]
    partial class ModelContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("ActorService.Model.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abilities")
                        .HasColumnType("VARCHAR(255)");

                    b.Property<string>("Balance");

                    b.Property<int>("BaseHealth");

                    b.Property<int>("BasePower");

                    b.Property<int>("BaseSpeed");

                    b.Property<int>("CurrentHealth");

                    b.Property<int>("Experience");

                    b.Property<int>("Level");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)");

                    b.Property<string>("Quality")
                        .HasColumnType("VARCHAR(10)");

                    b.Property<int?>("ZoneId");

                    b.HasKey("Id");

                    b.HasIndex("ZoneId");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("ActorService.Model.Zone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Level");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("ZoneType")
                        .IsRequired()
                        .HasColumnType("VARCHAR(20)");

                    b.HasKey("Id");

                    b.ToTable("Zones");
                });

            modelBuilder.Entity("ActorService.Model.Actor", b =>
                {
                    b.HasOne("ActorService.Model.Zone")
                        .WithMany("Actors")
                        .HasForeignKey("ZoneId");
                });
#pragma warning restore 612, 618
        }
    }
}
