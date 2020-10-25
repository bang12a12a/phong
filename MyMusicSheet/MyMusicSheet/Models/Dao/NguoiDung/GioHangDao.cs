using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyMusicSheet.Models.EF;
namespace MyMusicSheet.Models.Dao.NguoiDung
{
    public class GioHangDao
    {
        MyMusicSheetEntities db = new MyMusicSheetEntities();
        public List<GioHang> getByUserId(string userid)
        {
            var listgiohang = db.GioHangs.Where(x => x.IdNguoiDung == userid).ToList();
            return listgiohang;
        }
        
        public void ThemGioHang(string idnguoidung, string idsanpham)
        {
            var giohang = new GioHang();
            var now = DateTime.Now;
            giohang.Id = now.Day.ToString() + now.Month.ToString() + now.Year.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString();
            giohang.IdNguoiDung = idnguoidung;
            giohang.IdSanPham = idsanpham;
            db.GioHangs.Add(giohang);
            db.SaveChanges();
        }
        public void XoaGioHang(string idgiohang)
        {
            var giohang = db.GioHangs.Find(idgiohang);
            db.GioHangs.Remove(giohang);
            db.SaveChanges();
        }
    }
}