using Core.Entities.Concrete;
using Entities.Concretes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=APBS_Database;Trusted_Connection=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x => x.NationalityId).IsUnique();

            modelBuilder.Entity<IlanBasvuru>()
            .HasOne(b => b.Basvuran)
            .WithMany()
            .HasForeignKey(b => b.BasvuranId)
            .OnDelete(DeleteBehavior.Restrict); // ON DELETE CASCADE yerine RESTRICT kullanıldı

            modelBuilder.Entity<BasvuruJuri>()
            .HasOne(b => b.Kullanici)
            .WithMany()
            .HasForeignKey(b => b.KullaniciId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RaporDosya>()
                .HasOne(r => r.Basvuru)
                .WithMany()
                .HasForeignKey(r => r.BasvuruId)
                .OnDelete(DeleteBehavior.Restrict);  // veya .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<RaporDosya>()
                .HasOne(r => r.Juri)
                .WithMany()
                .HasForeignKey(r => r.JuriId)
                .OnDelete(DeleteBehavior.Restrict);  // veya .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<Ilan>()
                .HasIndex(i => i.Baslik)
                .HasDatabaseName("IX_Ilanlar_Baslik"); // Index adı belirleme


            modelBuilder.Entity<AlanKriteri>()
                .HasIndex(ak => new { ak.KriterId, ak.AlanId, ak.PozisyonId })
                .IsUnique();

            modelBuilder.Entity<PuanKriteri>()
                .HasIndex(ak => new { ak.KriterId, ak.AlanId, ak.PozisyonId })
                .IsUnique();
            base.OnModelCreating(modelBuilder);
        }

        //Auth

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        // Bildirimler
        public DbSet<Bildirim> Bildirimler { get; set; }

        // İlanlar
        public DbSet<Ilan> Ilanlar { get; set; }
        public DbSet<Bolum> Bolumler { get; set; }
        public DbSet<Pozisyon> Pozisyonlar { get; set; }
        public DbSet<Alan> Alanlar { get; set; }

        // Ön Değerlendirme
        public DbSet<Kriter> Kriterler { get; set; }
        public DbSet<AlanKriteri> AlanKriterleri { get; set; }
        public DbSet<PuanKriteri> PuanKriterleri { get; set; }

        // Başvuru
        public DbSet<IlanBasvuru> IlanBasvurulari { get; set; }
        public DbSet<BasvuruDurumu> BasvuruDurumlari { get; set; }
        public DbSet<BasvuruJuri> BasvuruJurileri { get; set; }
        public DbSet<IlanBasvuruDosya> BasvuruDosyalari { get; set; }
        public DbSet<RaporDosya> RaporDosyalari { get; set; }
    }
}
