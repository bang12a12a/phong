namespace MyMusicSheet.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string MatKhau { get; set; }

        [StringLength(50)]
        public string HoTen { get; set; }

        [StringLength(50)]
        public string TenDangNhap { get; set; }

        public int? Quyen { get; set; }

        [StringLength(50)]
        public string EmailPaypal { get; set; }

        public string AnhBia { get; set; }

        public string AnhDaiDien { get; set; }

        public string MoTa { get; set; }
    }
}
