using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Models
{
    public class HospitalContext: DbContext
    {
        public HospitalContext()
        {
        }

        public HospitalContext(DbContextOptions<HospitalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Pacient> Pacients { get; set; }
        public virtual DbSet<ProvisionOfPaidService> ProvisionOfPaidServices { get; set; }
        public virtual DbSet<Servise> Servises { get; set; }
        public virtual DbSet<Treatment> Treatment { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var builder = new ConfigurationBuilder();
        //    builder.SetBasePath(Directory.GetCurrentDirectory());
        //    builder.AddJsonFile("appsettings.json");
        //    var config = builder.Build();
        //    string connectionString = config.GetConnectionString("DefaultConnection");
        //    var options = optionsBuilder
        //        .UseSqlServer(connectionString)
        //        .Options;
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Department>(entity =>
        //    {
        //        entity.HasKey(e => e.DepartmentId);

        //        entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

        //        entity.Property(e => e.NameDepartments).HasMaxLength(20);
        //    });

        //    modelBuilder.Entity<Doctor>(entity =>
        //    {
        //        entity.Property(e => e.DoctorsId).HasColumnName("DoctorsID");

        //        entity.Property(e => e.Categories).HasMaxLength(20);

        //        entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

        //        entity.Property(e => e.DoctorNames).HasMaxLength(20);

        //        entity.Property(e => e.DoctorSurnames).HasMaxLength(20);

        //        entity.Property(e => e.Specialties).HasMaxLength(20);

        //        entity.HasOne(d => d.Department)
        //            .WithMany(p => p.Doctors)
        //            .HasForeignKey(d => d.DepartmentId)
        //            .OnDelete(DeleteBehavior.Cascade)
        //            .HasConstraintName("FK_Name_Departments");
        //    });

        //    modelBuilder.Entity<Pacient>(entity =>
        //    {
        //        entity.Property(e => e.PacientsId).HasColumnName("PacientsID");

        //        entity.Property(e => e.Adres).HasMaxLength(20);

        //        entity.Property(e => e.PatientNames).HasMaxLength(20);

        //        entity.Property(e => e.PatientSurnames).HasMaxLength(20);
        //    });

        //    modelBuilder.Entity<ProvisionOfPaidService>(entity =>
        //    {
        //        entity.HasKey(e => e.ProvisionId);

        //        entity.Property(e => e.ProvisionId).HasColumnName("ProvisionID");

        //        entity.Property(e => e.DateOfServiceProvision).HasColumnType("date");

        //        entity.Property(e => e.DoctorsId).HasColumnName("DoctorsID");

        //        entity.Property(e => e.PacientsId).HasColumnName("PacientsID");

        //        entity.Property(e => e.ServisesId).HasColumnName("ServisesID");

        //        entity.HasOne(d => d.Doctors)
        //            .WithMany(p => p.ProvisionOfPaidServices)
        //            .HasForeignKey(d => d.DoctorsId)
        //            .OnDelete(DeleteBehavior.Cascade)
        //            .HasConstraintName("FK_Name_Doctors2");

        //        entity.HasOne(d => d.Pacients)
        //            .WithMany(p => p.ProvisionOfPaidServices)
        //            .HasForeignKey(d => d.PacientsId)
        //            .OnDelete(DeleteBehavior.Cascade)
        //            .HasConstraintName("FK_Name_Pacients2");
        //    });

        //    modelBuilder.Entity<Servise>(entity =>
        //    {
        //        entity.Property(e => e.ServisesId).HasColumnName("ServisesID");

        //        entity.Property(e => e.DataPrice).HasColumnType("date");

        //        entity.Property(e => e.NameType).HasMaxLength(20);

        //        entity.Property(e => e.ProvisionId).HasColumnName("ProvisionID");

        //        entity.HasOne(d => d.Provision)
        //            .WithMany(p => p.Servises)
        //            .HasForeignKey(d => d.ProvisionId)
        //            .OnDelete(DeleteBehavior.Cascade)
        //            .HasConstraintName("FK_Name_Servises");
        //    });

        //    modelBuilder.Entity<Treatment>(entity =>
        //    {
        //        entity.Property(e => e.TreatmentId).HasColumnName("TreatmentID");

        //        entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

        //        entity.Property(e => e.Diagnosis).HasMaxLength(20);

        //        entity.Property(e => e.DischargeDate).HasColumnType("date");

        //        entity.Property(e => e.DoctorsId).HasColumnName("DoctorsID");

        //        entity.Property(e => e.PacientsId).HasColumnName("PacientsID");

        //        entity.Property(e => e.ReceiptDate).HasColumnType("date");

        //        entity.HasOne(d => d.Department)
        //            .WithMany(p => p.Treatment)
        //            .HasForeignKey(d => d.DepartmentId)
        //            .HasConstraintName("FK_Treatment_Departments");

        //        entity.HasOne(d => d.Doctors)
        //            .WithMany(p => p.Treatment)
        //            .HasForeignKey(d => d.DoctorsId)
        //            .OnDelete(DeleteBehavior.Cascade)
        //            .HasConstraintName("FK_Name_Doctors");

        //        entity.HasOne(d => d.Pacients)
        //            .WithMany(p => p.Treatment)
        //            .HasForeignKey(d => d.PacientsId)
        //            .OnDelete(DeleteBehavior.Cascade)
        //            .HasConstraintName("FK_Name_Pacients");
        //    });
        //}
    }
}
