using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PCParts.Models;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace PCParts.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Armazem> Armazems { get; set; }

    public virtual DbSet<CategoriaPeca> CategoriaPecas { get; set; }

    public virtual DbSet<Empregado> Empregados { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Peca> Pecas { get; set; }

    public virtual DbSet<Utilizador> Utilizadors { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Armazem>(entity =>
        {
            entity.HasKey(e => e.IdA).HasName("PRIMARY");

            entity.ToTable("Armazem");

            entity.Property(e => e.IdA)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("id_A");
            entity.Property(e => e.Cidade)
                .HasMaxLength(60)
                .HasColumnName("cidade");
            entity.Property(e => e.Freguesia)
                .HasMaxLength(60)
                .HasColumnName("freguesia");
            entity.Property(e => e.Nome)
                .HasMaxLength(20)
                .HasColumnName("nome");
            entity.Property(e => e.Pais)
                .HasMaxLength(25)
                .HasColumnName("pais");
        });

        modelBuilder.Entity<CategoriaPeca>(entity =>
        {
            entity.HasKey(e => e.IdT).HasName("PRIMARY");

            entity.Property(e => e.IdT)
                .ValueGeneratedNever()
                .HasColumnName("id_T");
            entity.Property(e => e.Descricao)
                .HasMaxLength(150)
                .HasColumnName("descricao");
            entity.Property(e => e.NomeC)
                .HasMaxLength(30)
                .HasColumnName("nome_c");
        });

        modelBuilder.Entity<Empregado>(entity =>
        {
            entity.HasKey(e => e.IdE).HasName("PRIMARY");

            entity.ToTable("Empregado");

            entity.HasIndex(e => e.ArmazemL, "armazem_l");

            entity.Property(e => e.IdE)
                .ValueGeneratedNever()
                .HasColumnName("id_E");
            entity.Property(e => e.ArmazemL)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("armazem_l");

            entity.HasOne(d => d.ArmazemLNavigation).WithMany(p => p.Empregados)
                .HasForeignKey(d => d.ArmazemL)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Empregado_ibfk_2");

            entity.HasOne(d => d.IdENavigation).WithOne(p => p.Empregado)
                .HasForeignKey<Empregado>(d => d.IdE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Empregado_ibfk_1");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => new { e.Peca, e.Armazem })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("Inventario");

            entity.HasIndex(e => e.Armazem, "Armazem");

            entity.Property(e => e.Armazem)
                .HasMaxLength(2)
                .IsFixedLength();
            entity.Property(e => e.Qtd).HasColumnName("qtd");

            entity.HasOne(d => d.ArmazemNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.Armazem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Inventario_ibfk_2");

            entity.HasOne(d => d.PecaNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.Peca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Inventario_ibfk_1");
        });

        modelBuilder.Entity<Peca>(entity =>
        {
            entity.HasKey(e => e.IdP).HasName("PRIMARY");

            entity.HasIndex(e => e.Categoria, "categoria");

            entity.Property(e => e.IdP).HasColumnName("id_P");
            entity.Property(e => e.Categoria).HasColumnName("categoria");
            entity.Property(e => e.DescricaoP)
                .HasMaxLength(1500)
                .HasColumnName("descricao_p");
            entity.Property(e => e.NomeP)
                .HasMaxLength(30)
                .HasColumnName("nome_p");
            entity.Property(e => e.NumSerie).HasColumnName("num_serie");
            entity.Property(e => e.Preco)
                .HasPrecision(10, 2)
                .HasColumnName("preco");

            entity.HasOne(d => d.CategoriaNavigation).WithMany(p => p.Pecas)
                .HasForeignKey(d => d.Categoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pecas_ibfk_1");
        });

        modelBuilder.Entity<Utilizador>(entity =>
        {
            entity.HasKey(e => e.IdU).HasName("PRIMARY");

            entity.ToTable("Utilizador");

            entity.Property(e => e.IdU).HasColumnName("id_U");
            entity.Property(e => e.Cidade)
                .HasMaxLength(60)
                .HasColumnName("cidade");
            entity.Property(e => e.CodPostal)
                .HasMaxLength(20)
                .HasColumnName("cod_postal");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Freguesia)
                .HasMaxLength(60)
                .HasColumnName("freguesia");
            entity.Property(e => e.Nif).HasColumnName("NIF");
            entity.Property(e => e.NomeU)
                .HasMaxLength(100)
                .HasColumnName("nome_u");
            entity.Property(e => e.Pais)
                .HasMaxLength(25)
                .HasColumnName("pais");
            entity.Property(e => e.Pass)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("pass");

            entity.HasMany(d => d.Armazems).WithMany(p => p.Trabalhadors)
                .UsingEntity<Dictionary<string, object>>(
                    "UtilizadorArmazem",
                    r => r.HasOne<Armazem>().WithMany()
                        .HasForeignKey("Armazem")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Utilizador_Armazem_ibfk_2"),
                    l => l.HasOne<Utilizador>().WithMany()
                        .HasForeignKey("Trabalhador")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Utilizador_Armazem_ibfk_1"),
                    j =>
                    {
                        j.HasKey("Trabalhador", "Armazem")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("Utilizador_Armazem");
                        j.HasIndex(new[] { "Armazem" }, "armazem");
                        j.IndexerProperty<int>("Trabalhador").HasColumnName("trabalhador");
                        j.IndexerProperty<string>("Armazem")
                            .HasMaxLength(2)
                            .IsFixedLength()
                            .HasColumnName("armazem");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
