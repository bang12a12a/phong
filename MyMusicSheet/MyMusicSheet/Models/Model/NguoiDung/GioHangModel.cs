using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMusicSheet.Models.Model.NguoiDung
{
    public class GioHangModel
    {
        public string Id { get; set; }
        public int STT { get; set; }
        public string IdNguoiDung { get; set; }
        public string IdSanPham { get; set; }
        public string TenSanPham { get; set; }
        public decimal? Gia { get; set; }
        public string Anh { get; set; }
        public string TenTacGia { get; set; }

    }
}