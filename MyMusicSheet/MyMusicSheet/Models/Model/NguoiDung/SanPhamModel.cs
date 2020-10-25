using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MyMusicSheet.Models.Model.NguoiDung
{
    public class SanPhamModel
    {
        [Key]
        public string Id { get; set; }

        public int STT { get; set; }
        [Required]
        public string Ten { get; set; }

        public string Anh { get; set; }

        public string Video { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Gia { get; set; }

        [Required]
        [StringLength(50)]
        public string IdNguoiDung { get; set; }

        [Required]
        [StringLength(50)]
        public string IdLoai { get; set; }

        public string MoTa { get; set; }

        [StringLength(10)]
        public string Ngay { get; set; }
        public string DuongDan { get; set; }
        public string ListLoai { set; get; }
        public string ListTenLoai { get; set; }
        public string TenNguoiDung { get; set; }
        public string DuongDanFileMidi { get; set; }
        public int? SoTrang { get; set; }
    }
}