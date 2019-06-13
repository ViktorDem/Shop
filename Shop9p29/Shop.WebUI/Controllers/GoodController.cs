using Shop.BLL.Models;
using Shop.BLL.Services;
using Shop.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUI.Controllers
{
    public class GoodController : Controller
    {
        IGenericService<CategoryDTO> categoryService;
        IGenericService<ManufacturerDTO> manufacturerService;
        IGenericService<GoodDTO> goodService;
        IGenericService<PhotoDTO> photoService;
        public GoodController(IGenericService<CategoryDTO> categoryService,
                                    IGenericService<ManufacturerDTO> manufacturerService,
                                    IGenericService<GoodDTO> goodService,
                                    IGenericService<PhotoDTO> photoService)
        {
            this.categoryService = categoryService;
            this.manufacturerService = manufacturerService;
            this.goodService = goodService;
            this.photoService = photoService;
        }

        public ActionResult Index()
        {
            var model = goodService.GetAll();
            return View(model);
        }

        public ActionResult PhotoList(int id=1)
        {

            ViewBag.GoodId = id;
            var model = photoService.FindBy(g => g.GoodId == id);
            return View(model);
        }
        public ActionResult Upload(int id)
        {
            ViewBag.GoodId = id;
            return View();
        }

        [HttpPost]
        public ActionResult Upload(int id, IEnumerable<HttpPostedFileBase> fileUpload)
        {
            //string idstr = Request.Params["id"];
            //int id = Convert.ToInt32(idstr);
            foreach (var file in fileUpload)
            {
                if (file == null) continue;
                string path = AppDomain.CurrentDomain.BaseDirectory + "Files/";
                string filename = Path.GetFileName(file.FileName);
               
                if (filename != null)
                {
                    //1. Получаем расширение
                    string ext = Path.GetExtension(filename);
                    //2. Генерируем уникальный ключ...
                    var partname = Guid.NewGuid().ToString() + ext;
                    file.SaveAs(Path.Combine(path, partname));
                    PhotoDTO photo = new PhotoDTO
                    {
                        GoodId = id,
                        PhotoPath = "/Files/" + partname
                    };
                    photoService.Add(photo);
                }
               
            }

            return RedirectToAction("PhotoList",  new { id= id  });
        }
        [HttpPost]
        public ActionResult DeletePhoto(int id)
        {
            photoService.Delete(id);
            return Json("OK");
        }

        public ActionResult GoodFind()
        {
            VmGoodFind model = new VmGoodFind(categoryService, manufacturerService);
            return View(model);
        }
        [HttpPost]
        public ActionResult GoodFind(VmGoodFind data)
        {
            TempData["data"] = data;
            return Json("OK");
        }
        public ActionResult FindByFilter()
        {
            VmGoodFind data= (VmGoodFind)TempData["data"];
            VmFindByFilter vmFind = new VmFindByFilter(goodService, photoService, data);
            return PartialView(vmFind);
        }
    }
}