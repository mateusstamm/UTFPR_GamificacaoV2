﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230509181412_Database")]
    partial class Database
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("API.Models.AtendimentoModel", b =>
                {
                    b.Property<int?>("AtendimentoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GarconID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("HorarioAtendimento")
                        .HasColumnType("TEXT");

                    b.Property<int?>("MesaID")
                        .HasColumnType("INTEGER");

                    b.Property<float?>("PrecoTotal")
                        .HasColumnType("REAL");

                    b.HasKey("AtendimentoID");

                    b.HasIndex("GarconID");

                    b.HasIndex("MesaID");

                    b.ToTable("Atendimentos", (string)null);
                });

            modelBuilder.Entity("API.Models.AtendimentoProdutoModel", b =>
                {
                    b.Property<int?>("AtendimentoID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProdutoID")
                        .HasColumnType("INTEGER");

                    b.HasKey("AtendimentoID", "ProdutoID");

                    b.HasIndex("ProdutoID");

                    b.ToTable("AtendimentoProduto");
                });

            modelBuilder.Entity("API.Models.CategoriaModel", b =>
                {
                    b.Property<int?>("CategoriaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descricao")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("CategoriaID");

                    b.ToTable("Categorias", (string)null);
                });

            modelBuilder.Entity("API.Models.GarconModel", b =>
                {
                    b.Property<int?>("GarconID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<int?>("NumIdentificao")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Sobrenome")
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefone")
                        .HasColumnType("TEXT");

                    b.HasKey("GarconID");

                    b.ToTable("Garcons", (string)null);
                });

            modelBuilder.Entity("API.Models.MesaModel", b =>
                {
                    b.Property<int?>("MesaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("HoraAbertura")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Numero")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ocupada")
                        .HasColumnType("TEXT");

                    b.HasKey("MesaID");

                    b.ToTable("Mesas", (string)null);
                });

            modelBuilder.Entity("API.Models.ProdutoModel", b =>
                {
                    b.Property<int?>("ProdutoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CategoriaID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descricao")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<float?>("Preco")
                        .HasColumnType("REAL");

                    b.HasKey("ProdutoID");

                    b.HasIndex("CategoriaID");

                    b.ToTable("Produtos", (string)null);
                });

            modelBuilder.Entity("API.Models.AtendimentoModel", b =>
                {
                    b.HasOne("API.Models.GarconModel", "GarconResponsavel")
                        .WithMany()
                        .HasForeignKey("GarconID");

                    b.HasOne("API.Models.MesaModel", "MesaAtendida")
                        .WithMany()
                        .HasForeignKey("MesaID");

                    b.Navigation("GarconResponsavel");

                    b.Navigation("MesaAtendida");
                });

            modelBuilder.Entity("API.Models.AtendimentoProdutoModel", b =>
                {
                    b.HasOne("API.Models.AtendimentoModel", "Atendimento")
                        .WithMany()
                        .HasForeignKey("AtendimentoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.ProdutoModel", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atendimento");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("API.Models.ProdutoModel", b =>
                {
                    b.HasOne("API.Models.CategoriaModel", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaID");

                    b.Navigation("Categoria");
                });
#pragma warning restore 612, 618
        }
    }
}