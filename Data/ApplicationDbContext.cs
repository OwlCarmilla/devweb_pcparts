using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PCParts.Models;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace PCParts.Data;

public partial class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
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
    public virtual DbSet<EmpregadosArmazem> EmpregadosArmazems { get; set; }
    public virtual DbSet<Encomendum> Encomenda { get; set; }
    public virtual DbSet<Inventario> Inventarios { get; set; }
    public virtual DbSet<ItemEncomendum> ItemEncomenda { get; set; }
    public virtual DbSet<Peca> Pecas { get; set; }
    public virtual DbSet<Utilizador> Utilizadors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Call the base method first to configure Identity tables
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Armazem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Armazem");

            entity.HasIndex(e => e.Codigo, "codigo").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cidade)
                .HasMaxLength(60)
                .HasColumnName("cidade");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .HasColumnName("codigo");
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
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao)
                .HasMaxLength(150)
                .HasColumnName("descricao");
            entity.Property(e => e.Nome)
                .HasMaxLength(30)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<EmpregadosArmazem>(entity =>
        {
            entity.HasKey(e => new { e.IdEmpregado, e.IdArmazem })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("Empregados_Armazem");

            entity.HasIndex(e => e.IdArmazem, "id_armazem");

            entity.Property(e => e.IdEmpregado).HasColumnName("id_empregado");
            entity.Property(e => e.IdArmazem).HasColumnName("id_armazem");
            entity.Property(e => e.Cargo)
                .HasMaxLength(50)
                .HasColumnName("cargo");
            entity.Property(e => e.DataInicio).HasColumnName("data_inicio");

            entity.HasOne(d => d.IdArmazemNavigation).WithMany(p => p.EmpregadosArmazems)
                .HasForeignKey(d => d.IdArmazem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Empregados_Armazem_ibfk_2");

            entity.HasOne(d => d.IdEmpregadoNavigation).WithMany(p => p.EmpregadosArmazems)
                .HasForeignKey(d => d.IdEmpregado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Empregados_Armazem_ibfk_1");
        });

        modelBuilder.Entity<Encomendum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.IdCliente, "id_cliente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataEncomenda)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("data_encomenda");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'Pendente'")
                .HasColumnType("enum('Pendente','Processando','Enviada','Entregue','Cancelada')")
                .HasColumnName("estado");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Total)
                .HasPrecision(12, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Encomenda)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Encomenda_ibfk_1");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => new { e.IdPeca, e.IdArmazem })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("Inventario");

            entity.HasIndex(e => e.IdArmazem, "id_armazem");

            entity.Property(e => e.IdPeca).HasColumnName("id_peca");
            entity.Property(e => e.IdArmazem).HasColumnName("id_armazem");
            entity.Property(e => e.Quantidade).HasColumnName("quantidade");
            entity.Property(e => e.UltimaAtualizacao)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("ultima_atualizacao");

            entity.HasOne(d => d.IdArmazemNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdArmazem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Inventario_ibfk_2");

            entity.HasOne(d => d.IdPecaNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdPeca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Inventario_ibfk_1");
        });

        modelBuilder.Entity<ItemEncomendum>(entity =>
        {
            entity.HasKey(e => new { e.IdEncomenda, e.IdPeca })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("Item_Encomenda");

            entity.HasIndex(e => e.IdPeca, "id_peca");

            entity.Property(e => e.IdEncomenda).HasColumnName("id_encomenda");
            entity.Property(e => e.IdPeca).HasColumnName("id_peca");
            entity.Property(e => e.PrecoUnitario)
                .HasPrecision(10, 2)
                .HasColumnName("preco_unitario");
            entity.Property(e => e.Quantidade).HasColumnName("quantidade");

            entity.HasOne(d => d.IdEncomendaNavigation).WithMany(p => p.ItemEncomenda)
                .HasForeignKey(d => d.IdEncomenda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Item_Encomenda_ibfk_1");

            entity.HasOne(d => d.IdPecaNavigation).WithMany(p => p.ItemEncomenda)
                .HasForeignKey(d => d.IdPeca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Item_Encomenda_ibfk_2");
        });

        modelBuilder.Entity<Peca>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.Categoria, "categoria");

            entity.HasIndex(e => e.NumSerie, "num_serie").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categoria).HasColumnName("categoria");
            entity.Property(e => e.Descricao)
                .HasMaxLength(1500)
                .HasColumnName("descricao");
            entity.Property(e => e.Nome)
                .HasMaxLength(30)
                .HasColumnName("nome");
            entity.Property(e => e.NumSerie)
                .HasMaxLength(50)
                .HasColumnName("num_serie");
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
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Utilizador");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.Property(e => e.Nif)
                .HasMaxLength(20)
                .HasColumnName("NIF");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.Pais)
                .HasMaxLength(25)
                .HasColumnName("pais");
            entity.Property(e => e.Pass)
                .HasMaxLength(64)
                .IsFixedLength()
                .HasColumnName("pass");
            entity.Property(e => e.Role)
                .HasDefaultValueSql("'Cliente'")
                .HasColumnType("enum('Cliente','Empregado','Admin')")
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}