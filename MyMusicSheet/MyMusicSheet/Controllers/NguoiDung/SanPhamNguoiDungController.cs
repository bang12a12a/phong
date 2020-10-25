using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMusicSheet.Models.EF;
using MyMusicSheet.Models.Model.NguoiDung;
using PagedList.Mvc;
using PagedList;
namespace MyMusicSheet.Controllers.NguoiDung
{
    public class SanPhamNguoiDungController : Controller
    {
        // GET: SanPhamNguoiDung
        MyMusicSheetEntities db = new MyMusicSheetEntities();
        public ActionResult Index(string text)
        {
            ViewBag.Text = text;
            return View();
        }
        public JsonResult List(string text, int? page, string type)
        {
            if (type == "") type = null;
            var model = new List<SanPham>();
            var listSanPham = new List<SanPhamModel>();
            var listsanpham = db.SanPhams.ToList();
            
            
            if (type != null && type != "")
            {
                var list_SanPham_Loai = db.SanPham_Loai.Where(x => x.IdLoai == type).Distinct().ToList();
                foreach (var item in list_SanPham_Loai)
                {
                    var sanphammodel = new SanPhamModel();
                    var sanpham = db.SanPhams.FirstOrDefault(x => x.Id == item.IdSanPham);
                    sanphammodel.Id = sanpham.Id;
                    sanphammodel.Ten = sanpham.Ten;
                    sanphammodel.MoTa = sanpham.MoTa;
                    sanphammodel.SoTrang = sanpham.SoTrang;
                    sanphammodel.Anh = sanpham.Anh;
                    sanphammodel.Gia = sanpham.Gia;
                    listSanPham.Add(sanphammodel);
                }
                if (text != null)
                {
                    listSanPham = listSanPham.Where(x => x.Ten.Contains(text)).ToList();
                }
            }
            else
            {
                foreach (var item in listsanpham)
                {
                    var sanphammodel = new SanPhamModel();
                    sanphammodel.Id = item.Id;
                    sanphammodel.Ten = item.Ten;
                    sanphammodel.MoTa = item.MoTa;
                    sanphammodel.SoTrang = item.SoTrang;
                    sanphammodel.Anh = item.Anh;
                    sanphammodel.Gia = item.Gia;
                    listSanPham.Add(sanphammodel);
                }
                if (text != null)
                {
                    listSanPham = listSanPham.Where(x => x.Ten.Contains(text)).ToList();
                }
            }
            if (page > 0)
            {
                page = page;
            }
            else
            {
                page = 1;
            }
            int pageSize = 4;
            int start = (int)(page - 1) * pageSize;
            ViewBag.pageCurrent = page;
            int totalPage = listSanPham.Count();
            float totalNumsize = (totalPage / (float)pageSize);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            var datamodel = listSanPham.Skip(start).Take(pageSize);
            return Json(new { data = datamodel, pageCurrent = page, numSize = numSize }, JsonRequestBehavior.AllowGet);
        }

    }
}