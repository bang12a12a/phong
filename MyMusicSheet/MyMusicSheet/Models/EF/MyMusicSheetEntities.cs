namespace MyMusicSheet.Models.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyMusicSheetEntities : DbContext
    {
        public MyMusicSheetEntities()
            : base("name=MyMusicSheetEntities")
        {
        }

        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<Loai> Loais { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<SanPham_Loai> SanPham_Loai { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HoaDon>()
                .Property(e => e.Gia)
                .HasPrecision(3, 2);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.Gia)
                .HasPrecision(3, 2);
        }
    }
}
