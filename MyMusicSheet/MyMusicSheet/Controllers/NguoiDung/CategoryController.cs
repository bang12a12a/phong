using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMusicSheet.Models.EF;
using MyMusicSheet.Models.Model.NguoiDung;
namespace MyMusicSheet.Controllers.NguoiDung
{
    public class CategoryController : Controller
    {
        MyMusicSheetEntities db = new MyMusicSheetEntities();
        // GET: Category
        [ChildActionOnly]
        public PartialViewResult Index()
        {
            var list = db.Loais.ToList();
            var model = new List<LoaiModel>();
            foreach(var item in list)
            {
                var soluong = db.SanPham_Loai.Where(x => x.IdLoai == item.Id).Count();
                var itemmodel = new LoaiModel();
                itemmodel.Id = item.Id;
                itemmodel.TenLoai = item.TenLoai;
                itemmodel.SoLuong = soluong;
                model.Add(itemmodel);
            }
            return PartialView(model);
        }
    }
}