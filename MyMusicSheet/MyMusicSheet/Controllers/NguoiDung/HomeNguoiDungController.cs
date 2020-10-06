using MyMusicSheet.Models.Dao.NguoiDung;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMusicSheet.Models.EF;
using MyMusicSheet.Models.Model.NguoiDung;

namespace MyMusicSheet.Controllers
{
    public class HomeNguoiDungController : Controller
    {
        // GET: HomeNguoiDung
        MyMusicSheetEntities db = new MyMusicSheetEntities();
        public ActionResult Index()
        {

            return View();
        }
        [ChildActionOnly]
        public PartialViewResult GioHang()
        {
            var session = (MyMusicSheet.Common.SessionModel.NguoiDungSesssionModel)Session[MyMusicSheet.Common.SessionList.SessionList.NGUOIDUNG_SESSION];
            if (session != null)
            {
                var cart = new GioHangDao().getByUserId(session.Id);
                var countitem_giohang = cart.Count;
                ViewBag.CountItem_GioHang = countitem_giohang;
            }
            return PartialView();

        }
        [ChildActionOnly]
        public PartialViewResult Upload()
        {
            var session = (MyMusicSheet.Common.SessionModel.NguoiDungSesssionModel)Session[MyMusicSheet.Common.SessionList.SessionList.NGUOIDUNG_SESSION];
            if (session != null)
            {
                ViewBag.Session = 1;
            }
            else
            {
                ViewBag.Session = 1;
            }
            return PartialView();

        }
        [ChildActionOnly]
        public PartialViewResult SanPhamMoi()
        {
            var sanpham = db.SanPhams.OrderByDescending(x => x.Ngay).Take(12).ToList();
            var model = new List<SanPhamModel>();
            foreach(var item in sanpham)
            {
                var itemmodel = new SanPhamModel();
                itemmodel.Id = item.Id;
                itemmodel.Ten = item.Ten;
                itemmodel.Anh = item.Anh;
                itemmodel.Gia = item.Gia;
                itemmodel.IdNguoiDung = item.IdNguoiDung;
                itemmodel.TenNguoiDung = db.NguoiDungs.FirstOrDefault(x => x.Id == item.IdNguoiDung).HoTen;
                model.Add(itemmodel);
            }
            return PartialView(model);
        }
        public JsonResult List(string txtSearch, int? page)
        {
            var session = (MyMusicSheet.Common.SessionModel.NguoiDungSesssionModel)Session[MyMusicSheet.Common.SessionList.SessionList.NGUOIDUNG_SESSION];
            var list = db.SanPhams.OrderByDescending(x => x.Ngay).ToList();
            int pageSize = 10;
            var data = new List<SanPhamModel>();
            int i = 0;
            foreach (var item in list)
            {
                i++;
                var sanpham = new SanPhamModel();
                sanpham.Id = item.Id;
                sanpham.Ten = item.Ten;
                sanpham.Gia = item.Gia;
                sanpham.IdNguoiDung = item.IdNguoiDung;
                sanpham.TenNguoiDung = db.NguoiDungs.FirstOrDefault(x => x.Id.Contains(item.IdNguoiDung)).HoTen;
                var listLoai = db.SanPham_Loai.Where(x => x.IdSanPham == item.Id).ToList();
                var listtenloai = "";
                foreach (var itemloai in listLoai)
                {
                    listtenloai += db.Loais.FirstOrDefault(x => x.Id == itemloai.IdLoai).TenLoai.ToString() + ", ";
                }
                listtenloai = listtenloai.Substring(0, listtenloai.Length - 2);
                sanpham.STT = i;
                sanpham.ListTenLoai = listtenloai;
                data.Add(sanpham);

            }
            if (page > 0)
            {
                page = page;
            }
            else
            {
                page = 1;
            }
            int start = (int)(page - 1) * pageSize;

            ViewBag.pageCurrent = page;
            int totalPage = data.Count();
            float totalNumsize = (totalPage / (float)pageSize);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            var datamodel = data.Skip(start).Take(pageSize);

            return Json(new { data = datamodel, pageCurrent = page, numSize = numSize }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChiTietSanPham(string id)
        {
            var sanpham = db.SanPhams.FirstOrDefault(x => x.Id == id);
            var model = new SanPhamModel();
            model.Id = sanpham.Id;
            model.Ten = sanpham.Ten;
            var listLoai = db.SanPham_Loai.Where(x => x.IdSanPham == sanpham.Id).ToList();
            var listtenloai = "";
            foreach (var itemloai in listLoai)
            {
                listtenloai += db.Loais.FirstOrDefault(x => x.Id == itemloai.IdLoai).TenLoai.ToString() + ", ";
            }
            listtenloai = listtenloai.Substring(0, listtenloai.Length - 2);
            model.ListTenLoai = listtenloai;
            model.Gia = sanpham.Gia;
            model.TenNguoiDung = db.NguoiDungs.FirstOrDefault(x => x.Id.Contains(sanpham.IdNguoiDung)).HoTen;
            model.Anh = sanpham.Anh;
            model.Video = sanpham.Video;
            var listsanpham = db.SanPhams.Where(x => x.IdNguoiDung == sanpham.IdNguoiDung).OrderByDescending(x => x.Ngay).Take(12).ToList();
            ViewBag.ListSanPham = listsanpham;
            return View(model);
            
        }
        public ActionResult ThanhCong()
        {
            return View();
        }
        public ActionResult ThatBai()
        {
            return View();
        }
    }
}