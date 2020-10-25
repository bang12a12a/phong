using MyMusicSheet.Models.Dao.NguoiDung;
using MyMusicSheet.Models.EF;
using MyMusicSheet.Models.Model.NguoiDung;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PayPal.Api;
using MyMusicSheet.Common.Function;
using System.Configuration;

namespace MyMusicSheet.Controllers.NguoiDung
{
    public class GioHangNguoiDungController : Controller
    {
        // GET: GioHangNguoiDung
        MyMusicSheetEntities db = new MyMusicSheetEntities();
        GioHangDao dao = new GioHangDao();
        public ActionResult Index()
        {
            var session = (MyMusicSheet.Common.SessionModel.NguoiDungSesssionModel)Session[MyMusicSheet.Common.SessionList.SessionList.NGUOIDUNG_SESSION];
            if(session != null)
            {
                int i = 0;
                decimal? tongtien = 0;
                var giohang = dao.getByUserId(session.Id);
                var model = new List<GioHangModel>();
                foreach(var item in giohang)
                {
                    i++;
                    var itemmodel = new GioHangModel();
                    itemmodel.Id = item.Id;
                    var sanpham = db.SanPhams.FirstOrDefault(x => x.Id == item.IdSanPham);
                    itemmodel.TenSanPham = sanpham.Ten;
                    itemmodel.Gia = sanpham.Gia;
                    itemmodel.STT = i;
                    itemmodel.TenTacGia = db.NguoiDungs.FirstOrDefault(x => x.Id == sanpham.IdNguoiDung).HoTen;
                    itemmodel.Anh = sanpham.Anh;
                    model.Add(itemmodel);
                    tongtien += sanpham.Gia;
                }
                ViewBag.TongTien = tongtien;
                return View(model);
            }
            return View();
            
        }
        public ActionResult ThemGioHang(string id)
        {
            var session = (MyMusicSheet.Common.SessionModel.NguoiDungSesssionModel)Session[MyMusicSheet.Common.SessionList.SessionList.NGUOIDUNG_SESSION];
            //var giohang = dao.getByUserId(session.Id);
            //if(giohang.Where(x=>x.IdSanPham == id).ToList().Count > 0)
            //{

            //}
            //else
            //{
               
            //}
            dao.ThemGioHang(session.Id, id);
            return RedirectToAction("Index","HomeNguoiDung");
        }
        public ActionResult XoaGioHang(string idgiohang)
        {
            dao.XoaGioHang(idgiohang);
            return View();
        }
    }
}