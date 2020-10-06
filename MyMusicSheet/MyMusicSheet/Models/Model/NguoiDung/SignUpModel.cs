using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMusicSheet.Models.Model.NguoiDung
{
    public class SignUpModel
    {
        public string HoTen { get; set; }

        [StringLength(50)]
        public string TenDangNhap { get; set; }

        public string MatKhau { get; set; }

        public string XacNhanMatKhau { get; set; }

        public string Email { get; set; }
    }
}