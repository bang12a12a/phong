using GroupDocs.Conversion.Config;
using GroupDocs.Conversion.Converter.Option;
using GroupDocs.Conversion.Handler;
using MyMusicSheet.Models.EF;
using MyMusicSheet.Models.Model.NguoiDung;
using MyMyMusicSheet.Models.Dao.NguoiDung;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMusicSheet.Controllers.NguoiDung
{
    public class UploadNguoiDungController : Controller
    {
        
        // GET: UploadNguoiDung
        MyMusicSheetEntities db = new MyMusicSheetEntities();
        SanPhamNguoiDungDao dao = new SanPhamNguoiDungDao();
        public string UserId;
        public int sotrang = 0;
        public string ProcessUpload(HttpPostedFileBase file)
        {
            var now = DateTime.Now;
            var url1 = now.Day.ToString() + now.Month.ToString() + now.Year.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString();
            string[] stringanh = file.FileName.Split('.');
            var tenfile = UserId + url1 + stringanh[0];
            file.SaveAs(Server.MapPath(@"~/Content/FileUpload/" + tenfile + ".pdf"));
            var filePdf = ConvertPdfToJpg(tenfile, ImageSaveOptions.ImageFileType.Jpg);
            return tenfile + "/" + filePdf.sotrang;

        }
        public string UploadFileMidi(HttpPostedFileBase file)
        {
            var now = DateTime.Now;
            var url1 = now.Day.ToString() + now.Month.ToString() + now.Year.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString();
            file.SaveAs(Server.MapPath(@"~/Content/FileMidiUpload/" + url1 + file.FileName));
            return (url1 + "_" + file.FileName).ToString();
        }
        public static FilePDFModel ConvertPdfToJpg(string filename, ImageSaveOptions.ImageFileType outputFileType)
        {
            //string nguon = @"F:\Phong\phong\MyMusicSheet\MyMusicSheet\FileUpload\";
            //string dich = @"F:\Phong\phong\MyMusicSheet\MyMusicSheet\Images\";            
            string nguon = Environment.CurrentDirectory ;
            string dich = @"F:\Phong\phong\MyMusicSheet\MyMusicSheet\Content\Images\";
            var conversionConfig = new ConversionConfig { StoragePath = nguon, OutputPath = dich };
            var conversionHandler = new ConversionHandler(conversionConfig);
            var saveOptions = new ImageSaveOptions
            {
                ConvertFileType = outputFileType
            };
            var conDocPath = conversionHandler.Convert(nguon + filename + ".pdf", saveOptions);
            var urlAnhSanPham = "";
            for (int pageNum = 1; pageNum <= conDocPath.PageCount; pageNum++)
            {
                conDocPath.Save(Path.GetFileNameWithoutExtension(filename) + pageNum.ToString() + "." + outputFileType, pageNum);
            }
            urlAnhSanPham = Path.GetFileNameWithoutExtension(filename);
            var filePdf = new FilePDFModel();
            filePdf.filename = filename;
            filePdf.sotrang = conDocPath.PageCount;
            return filePdf;
        }
        public ActionResult Index()
        {
            var session = (MyMusicSheet.Common.SessionModel.NguoiDungSesssionModel)Session[MyMusicSheet.Common.SessionList.SessionList.NGUOIDUNG_SESSION];
            if (session != null)
            {
                ViewBag.Loai = db.Loais.ToList();
                UserId = session.Id.ToString();
                return View();
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        public JsonResult List(string txtSearch, int? page)
        {
            var session = (MyMusicSheet.Common.SessionModel.NguoiDungSesssionModel)Session[MyMusicSheet.Common.SessionList.SessionList.NGUOIDUNG_SESSION];
            var list = db.SanPhams.Where(x => x.IdNguoiDung == session.Id).OrderByDescending(x => x.Ngay).ToList();
            int pageSize = 10;
            if (!String.IsNullOrEmpty(txtSearch))
            {
                ViewBag.txtSearch = txtSearch;
                list = list.Where(x => x.Ten.Contains(txtSearch)).OrderByDescending(x => x.Ngay).ToList();
            }
            var data = new List<SanPhamModel>();
            int i = 0;
            foreach (var item in list)
            {
                i++;
                var sanpham = new SanPhamModel();
                sanpham.Id = item.Id;
                sanpham.Ten = item.Ten;
                sanpham.Gia = item.Gia;
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
        public JsonResult Upload(SanPhamModel sanpham)
        {
            var session = (MyMusicSheet.Common.SessionModel.NguoiDungSesssionModel)Session[MyMusicSheet.Common.SessionList.SessionList.NGUOIDUNG_SESSION];
            sanpham.IdNguoiDung = session.Id;
            return Json(dao.ThemMoi(sanpham), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(string ID)
        {
            var sanpham = db.SanPhams.FirstOrDefault(x => x.Id == ID);
            return Json(sanpham, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(SanPhamModel sp)
        {
            return Json(dao.updateSanPham(sp), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string ID)
        {
            return Json(dao.Delete(ID), JsonRequestBehavior.AllowGet);
        }
    }
}