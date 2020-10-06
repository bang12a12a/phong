namespace MyMusicSheet.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SanPham_Loai
    {
        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(50)]
        public string IdSanPham { get; set; }

        [StringLength(50)]
        public string IdLoai { get; set; }
    }
}
