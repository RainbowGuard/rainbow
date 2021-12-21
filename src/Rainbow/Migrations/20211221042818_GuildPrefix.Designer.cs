﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rainbow.Contexts;

#nullable disable

namespace Rainbow.Migrations
{
    [DbContext(typeof(GuildConfigurationContext))]
    [Migration("20211221042818_GuildPrefix")]
    partial class GuildPrefix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("Rainbow.Entities.GuildConfiguration", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Prefix")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("GuildConfigurations");
                });
#pragma warning restore 612, 618
        }
    }
}
