using HaberSistemi.Admin.Class;
using HaberSistemi.Core.Infrastructure;
using HaberSistemi.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using HaberSistemi.Admin.CustomFilter;

namespace HaberSistemi.Admin.Controllers
{
    public class KategoriController : Controller
    {

        #region Tüm Kategoriler
        private readonly IKategoriRepository _kategoriRepository;

        public KategoriController(IKategoriRepository kategoriRepository)
        {
            _kategoriRepository = kategoriRepository;
        }
        #endregion

        #region Kategori Listeleme
        [HttpGet]
        [LoginFilter]
        public ActionResult Index(int Sayfa=1)
        {
            return View(_kategoriRepository.GetAll().OrderByDescending(x => x.ID).ToPagedList(Sayfa , 10));
        }
        #endregion

        #region Kategori Ekle
        [HttpGet]
        [LoginFilter]
        public ActionResult Ekle(int? id)
        {
            if (id == null)
            {
                SetKategoriListele();
                ViewBag.Title = "Kategori Ekle";
                return View();
            }
            else
            {
                SetKategoriListele();
                ViewBag.Title = "Kategori Düzenle";
                Kategori dbKategori = _kategoriRepository.GetById(Convert.ToInt32(id));
                if (dbKategori == null)
                {
                    throw new Exception("Kategori Bulunamadı");
                }

                return View(dbKategori);
            }
            
           
        }

        [HttpPost]
        [LoginFilter]
        public JsonResult Ekle(Kategori kategori)
        {
            try
            {
                _kategoriRepository.Insert(kategori);
                _kategoriRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Kategori Ekleme İşleminiz Başarılı" },JsonRequestBehavior.AllowGet);
            }
            catch (Exception )
            {
                // Loglama yaptırabiliriz
                return Json(new ResultJson { Success = false, Message = "Kategori Eklerken Hata Oluştu" });
            }
            
        }

        #endregion

        #region Kategori Düzenle
        [HttpGet]
        [LoginFilter]
        public ActionResult  Duzenle(int id)
        {
            Kategori dbKategori = _kategoriRepository.GetById(id);
            if (dbKategori == null)
            {
                throw new Exception("Kaktegori Bulunamadı");
            }
            SetKategoriListele();
            return View(dbKategori);
        }

        [HttpPost]
        [LoginFilter]
        public JsonResult Duzenle(Kategori kategori)
        {
       
                Kategori dbKategori = _kategoriRepository.GetById(kategori.ID);
                dbKategori.AktifMi = kategori.AktifMi;
                dbKategori.KategoriAdi = kategori.KategoriAdi;
                dbKategori.ParentID = kategori.ParentID;
                dbKategori.URL = kategori.URL;
            _kategoriRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Düzenleme İşlemi Başarılı" });
          
          //  return Json(new ResultJson { Success = false, Message = "Düzenleme işlemi sırasında bir hata oluştu" });
        }
        #endregion

        #region Set Kategori
        public void SetKategoriListele(object kategori = null)
        {
            var KategorList = _kategoriRepository.GetMany(x => x.ParentID == 0).ToList();
            ViewBag.Kategori = KategorList;
        }

        #endregion

        #region Sil
        [LoginFilter]
        public JsonResult Sil(int ID)
        {
            Kategori dbKategori = _kategoriRepository.GetById(ID);
            if (dbKategori == null)
            {
                return Json(new ResultJson { Success = true, Message = "Kategori Bulunamadı" });
            }
            _kategoriRepository.Delete(ID);
            _kategoriRepository.Save();

            return Json(new ResultJson { Success= true , Message="Kategori Silme İşleminiz Başarılı"});
        }

        #endregion

    }
}