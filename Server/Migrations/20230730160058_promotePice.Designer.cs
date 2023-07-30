﻿// <auto-generated />
using System;
using BlazorChess.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BlazorChess.Migrations
{
    [DbContext(typeof(ChessContext))]
    [Migration("20230730160058_promotePice")]
    partial class promotePice
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BlazorChess.Shared.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsBlackTurn")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("BlazorChess.Shared.Models.HistoryMove", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FromX")
                        .HasColumnType("integer");

                    b.Property<int>("FromY")
                        .HasColumnType("integer");

                    b.Property<int?>("GameId")
                        .HasColumnType("integer");

                    b.Property<int>("PromotionChoise")
                        .HasColumnType("integer");

                    b.Property<int>("ToX")
                        .HasColumnType("integer");

                    b.Property<int>("ToY")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("HistoryMove");
                });

            modelBuilder.Entity("BlazorChess.Shared.Models.HistoryMove", b =>
                {
                    b.HasOne("BlazorChess.Shared.Models.Game", null)
                        .WithMany("MovesMade")
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("BlazorChess.Shared.Models.Game", b =>
                {
                    b.Navigation("MovesMade");
                });
#pragma warning restore 612, 618
        }
    }
}
