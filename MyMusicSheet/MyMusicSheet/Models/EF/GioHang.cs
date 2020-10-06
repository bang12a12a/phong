namespace MyMusicSheet.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GioHang")]
    public partial class GioHang
    {
        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(50)]
        public string IdNguoiDung { get; set; }

        [StringLength(50)]
        public string IdSanPham { get; set; }
    }
}
