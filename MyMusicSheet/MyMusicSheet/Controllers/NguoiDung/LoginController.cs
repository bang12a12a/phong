using MyMusicSheet.Models.EF;
using MyMusicSheet.Models.Model.NguoiDung;
using MyMusicSheet.Common.Function;
using MyMusicSheet.Common.SessionList;
using MyMusicSheet.Common.SessionModel;
using MyMusicSheet.Models.Dao.NguoiDung;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMusicSheet.Controllers.NguoiDung
{
    public class LoginController : Controller
    {
        // GET: Login
        LoginDao dao = new LoginDao();
        MyMusicSheetEntities db = new MyMusicSheetEntities();
        // GET: Login
        public ActionResult SignUp(SignUpModel model)
        {
            return Json(dao.DangKy(model), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Login(SignUpModel model)
        {
            var matkhau = Encrytor.MD5Hash(model.MatKhau);
            var nguoidung = db.NguoiDungs.Where(x => x.Email == model.Email && x.MatKhau == matkhau).FirstOrDefault();
            if (nguoidung != null)
            {
                var nguoidungsessionmodel = new NguoiDungSesssionModel();
                nguoidungsessionmodel.Id = nguoidung.Id;
                nguoidungsessionmodel.Name = nguoidung.HoTen;
                Session.Add(SessionList.NGUOIDUNG_SESSION, nguoidungsessionmodel);
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);

        }
    }
}