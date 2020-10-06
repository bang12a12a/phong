using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyMusicSheet.Models.Model.NguoiDung;
using MyMusicSheet.Common.Function;
using MyMusicSheet.Common.SessionModel;
using MyMusicSheet.Common.SessionList;
using System.Web.Mvc;
using MyMusicSheet.Models.EF;

namespace MyMusicSheet.Models.Dao.NguoiDung
{
    
    public class LoginDao
    {
        MyMusicSheetEntities db = new MyMusicSheetEntities();
        public int checkEmail(string email)
        {
            var nguoidung = db.NguoiDungs.Where(x => x.Email == email).FirstOrDefault();
            if(nguoidung != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int DangKy(SignUpModel signmodel)
        {
            if(checkEmail(signmodel.Email) == 1)
            {
                return 0;
            }
            else
            {
                var nguoidung = new MyMusicSheet.Models.EF.NguoiDung();
                var now = DateTime.Now;

                nguoidung.Id = now.Day.ToString() + now.Month.ToString() + now.Year.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString();
                nguoidung.Email = signmodel.Email;
                nguoidung.HoTen = signmodel.HoTen;
                nguoidung.TenDangNhap = signmodel.Email;
                nguoidung.MatKhau = Encrytor.MD5Hash(signmodel.MatKhau);
                nguoidung.Quyen = 1;
                db.NguoiDungs.Add(nguoidung);
                db.SaveChanges();
                return 1;
            }
            
        }
        public int DangNhap(SignUpModel signmodel)
        {
            var matkhau = Encrytor.MD5Hash(signmodel.MatKhau);
            var nguoidung = db.NguoiDungs.Where(x => x.Email == signmodel.Email && x.MatKhau == matkhau).FirstOrDefault();
            if(nguoidung != null){
                var nguoidungsessionmodel = new NguoiDungSesssionModel();
                nguoidungsessionmodel.Id = nguoidung.Id;
                nguoidungsessionmodel.Name = nguoidung.HoTen;
                
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}