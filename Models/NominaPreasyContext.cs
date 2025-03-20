using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Crud.Models;

public partial class NominaPreasyContext : DbContext
{
    public NominaPreasyContext()
    {
    }

    public NominaPreasyContext(DbContextOptions<NominaPreasyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Direccion> Direccions { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Jornadum> Jornada { get; set; }

    public virtual DbSet<Perfil> Perfils { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    public virtual DbSet<TurnoEmpleado> TurnoEmpleados { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=nomina-preasy.mssql.somee.com;user id=rarias12_SQLLogin_1;pwd=v2yj8yiwst; persist security info=False;initial catalog=nomina-preasy;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<Area>(entity =>
        {
            entity.ToTable("area");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Direccion).HasColumnName("direccion");
            entity.Property(e => e.Jefe).HasColumnName("jefe");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.DireccionNavigation).WithMany(p => p.Areas)
                .HasForeignKey(d => d.Direccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_area_direccion");
        });

        modelBuilder.Entity<Direccion>(entity =>
        {
            entity.ToTable("direccion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Jefe).HasColumnName("jefe");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.ToTable("empleado");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Area).HasColumnName("area");
            entity.Property(e => e.Documento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("documento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.SalarioBasico).HasColumnName("salario_basico");
            entity.Property(e => e.SalarioHora).HasColumnName("salario_hora");
            entity.Property(e => e.TipoDoc).HasColumnName("tipo_doc");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("titulo");

            entity.HasOne(d => d.AreaNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.Area)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_empleado_area");

            entity.HasOne(d => d.TipoDocNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.TipoDoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_empleado_tipo_documento");
        });

        modelBuilder.Entity<Jornadum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Table_1");

            entity.ToTable("jornada");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DesCorta)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("des_corta");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Perfil>(entity =>
        {
            entity.ToTable("perfil");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.ToTable("tipo_documento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DesCorta)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("des_corta");
            entity.Property(e => e.Documento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("documento");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.ToTable("turno");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Costo).HasColumnName("costo");
            entity.Property(e => e.DesCorta)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("des_corta");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Extra).HasColumnName("extra");
            entity.Property(e => e.FechaFinal)
                .HasColumnType("datetime")
                .HasColumnName("fecha_final");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.Jornada).HasColumnName("jornada");

            entity.HasOne(d => d.JornadaNavigation).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.Jornada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_turno_jornada");
        });

        modelBuilder.Entity<TurnoEmpleado>(entity =>
        {
            entity.ToTable("turno_empleado");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Empleado).HasColumnName("empleado");
            entity.Property(e => e.FechaFinal)
                .HasColumnType("datetime")
                .HasColumnName("fecha_final");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.Turno).HasColumnName("turno");

            entity.HasOne(d => d.EmpleadoNavigation).WithMany(p => p.TurnoEmpleados)
                .HasForeignKey(d => d.Empleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_turno_empleado_empleado");

            entity.HasOne(d => d.TurnoNavigation).WithMany(p => p.TurnoEmpleados)
                .HasForeignKey(d => d.Turno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_turno_empleado_turno");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("usuarios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Empleado).HasColumnName("empleado");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Perfil).HasColumnName("perfil");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");

            entity.HasOne(d => d.EmpleadoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Empleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_usuarios_empleado");

            entity.HasOne(d => d.PerfilNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Perfil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_usuarios_perfil");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
