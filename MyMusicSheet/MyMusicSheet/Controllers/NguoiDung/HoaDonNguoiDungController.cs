using MyMusicSheet.Common.Function;
using MyMusicSheet.Models.Dao.NguoiDung;
using MyMusicSheet.Models.EF;
using MyMyMusicSheet.Models.Dao.NguoiDung;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMusicSheet.Controllers.NguoiDung
{
    public class HoaDonNguoiDungController : Controller
    {
        // GET: HoaDonNguoiDung
        SanPhamNguoiDungDao dao = new SanPhamNguoiDungDao();
        GioHangDao giohangdao = new GioHangDao();
        MyMusicSheetEntities db = new MyMusicSheetEntities();
        public ActionResult TaoHoaDon()
        {
            var session = (MyMusicSheet.Common.SessionModel.NguoiDungSesssionModel)Session[MyMusicSheet.Common.SessionList.SessionList.NGUOIDUNG_SESSION];
            if (session != null)
            {
                // tạo idbill
                DateTime now = DateTime.Now;
                var idbill = session.Id.ToString() + now.Day.ToString() + now.Month.ToString() + now.Year.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString();
                //insert order
                var listsanpham  = dao.getListSanPhamByIdNguoiDung(session.Id);
                var listsanpham_giohang_nguoidung = db.GioHangs.Where(x => x.IdNguoiDung == session.Id).ToList();
                
                var tongtiendonhang = listsanpham.Sum(x => x.Gia);
                var detail = "";
                foreach(var itemsanpham in listsanpham)
                {
                    detail += itemsanpham.Ten + ": </br> - Link pdf:" + itemsanpham.DuongDan;
                }
                var khachhang = db.NguoiDungs.FirstOrDefault(x=>x.Id ==session.Id);
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Template/hoadonnguoidung.html"));
                content = content.Replace("{{CustomerName}}", khachhang.HoTen);
                content = content.Replace("{{Order}}", tongtiendonhang.ToString());
                content = content.Replace("{{Detail}}",  detail);

                try
                {
                    var toEmail = "bang12a12a1@gmail.com";
                    new MailHelper().SendMail(khachhang.Email, "Đơn hàng mới từ Estore", content);
                    new MailHelper().SendMail(toEmail, "Đơn hàng mới từ Estore", content);
                    foreach (var item in listsanpham_giohang_nguoidung)
                    {
                        var hoadon = new HoaDon();
                        hoadon.Id = session.Id.ToString() + item.Id.ToString() + now.Day.ToString() + now.Month.ToString() + now.Year.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString();
                        hoadon.IdNguoiDung = session.Id;
                        var sanpham = db.SanPhams.FirstOrDefault(x => x.Id == item.IdSanPham);
                        hoadon.IdSanPham = sanpham.Id;
                        hoadon.Ngay = now;
                        hoadon.Gia = sanpham.Gia;
                        db.HoaDons.Add(hoadon);
                        giohangdao.XoaGioHang(item.Id);
                    }
                    db.SaveChanges();

                    return View("ThanhCong","HomeNguoiDung");
                }
                catch (Exception e)
                {
                    return RedirectToAction("ThatBai","HomeNguoiDung");
                }

            }
            else
            {
                return View("ThanhCong", "HomeNguoiDung");
            }
        }  
    }
}