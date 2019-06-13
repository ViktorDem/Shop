using Shop.BLL.Models;
using Shop.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.WebUI.Models
{
    public class PhotoFindByFilter
    {
        public int GoodId { get; set; }
        public string GoodName { get; set; }
        public string PhotoPath { get; set; }

    }

    public class VmFindByFilter
    {
        IGenericService<GoodDTO> goodService;
        IGenericService<PhotoDTO> photoService;
        VmGoodFind vmFind;
        public VmFindByFilter(  IGenericService<GoodDTO> goodService,
                                IGenericService<PhotoDTO> photoService,
                                VmGoodFind vmFind)
        {
            this.goodService = goodService;
            this.photoService = photoService;
            this.vmFind = vmFind;
            var goods = goodService.FindBy(vmFind.Predicate);
            if (goods.Count() == 0 || goods == null) return;
            Photos = new List<PhotoFindByFilter>();

            foreach (var good in goods)
            {
               var photo = photoService.FindBy(p => p.GoodId == good.GoodId).FirstOrDefault();
                string photopath = (photo == null) ?
                    "/Files/NoImage.png" : 
                    photo.PhotoPath;
                var photoFindByFilter = new PhotoFindByFilter
                {
                    GoodId = good.GoodId,
                    GoodName = good.GoodName,
                    PhotoPath = photopath
                };
                Photos.Add(photoFindByFilter);

            }

        }

        public List<PhotoFindByFilter> Photos { get; set; }
    }
}