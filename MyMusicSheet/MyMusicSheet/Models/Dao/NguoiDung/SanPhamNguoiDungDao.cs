using MyMusicSheet.Models.EF;
using MyMusicSheet.Models.Model.NguoiDung;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMyMusicSheet.Models.Dao.NguoiDung
{
    public class SanPhamNguoiDungDao
    {
        MyMusicSheetEntities db = new MyMusicSheetEntities();
        public List<SanPham> getListSanPhamByIdNguoiDung(string manguoidung)
        {
            var list_idsanpham = db.GioHangs.Where(x => x.IdNguoiDung == manguoidung).ToList();
            var listsanpham = new List<SanPham>();
            foreach(var item in list_idsanpham)
            {
                var sanpham = db.SanPhams.FirstOrDefault(x => x.Id == item.IdSanPham);
                listsanpham.Add(sanpham);
            }
            return listsanpham ;
        }
        public string ThemMoi(SanPhamModel model)
        {
            var sanpham = new SanPham();
            var now = DateTime.Now;

            sanpham.Id = now.Day.ToString() + now.Month.ToString() + now.Year.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString();
            var checkId = db.SanPhams.FirstOrDefault(x => x.Id == sanpham.Id);
            sanpham.DuongDan = model.DuongDan;
            sanpham.Ten = model.Ten;
            sanpham.Ngay = now;
            sanpham.Video = model.Video;
            sanpham.Gia = model.Gia;
            sanpham.MoTa = model.MoTa;
            sanpham.Anh = model.Anh;
            sanpham.IdNguoiDung = model.IdNguoiDung;
            sanpham.DuongDanFileMidi = model.DuongDanFileMidi;
            sanpham.MoTa = model.MoTa;
            db.SanPhams.Add(sanpham);
            string[] listloaiid = model.ListLoai.Split(',');
            int i = 0;
            foreach (var item in listloaiid)
            {
                i++;
                if(item != null || item != "")
                {
                    var loai_sanpham = new SanPham_Loai();
                    loai_sanpham.Id = now.Day.ToString() + now.Month.ToString() + now.Year.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString() + i.ToString();
                    loai_sanpham.IdLoai = item;
                    loai_sanpham.IdSanPham = sanpham.Id;
                    db.SanPham_Loai.Add(loai_sanpham);
                }
            }
            db.SaveChanges();
            return sanpham.Id;
        }
        public int updateSanPham(SanPhamModel sp)
        {
            var sanpham = db.SanPhams.Find(sp.Id);
            sanpham.Ten = sp.Ten;
            sanpham.Gia = sp.Gia;
            db.SaveChanges();
            return 1;
        }
        public int Delete(string masanpham)
        {
            var product = db.SanPhams.Find(masanpham);
            db.SanPhams.Remove(product);
            db.SaveChanges();
            return 1;
        }
    }
}