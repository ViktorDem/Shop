using Shop.BLL.Models;
using Shop.BLL.Services;
using Shop.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop.WebAPI.Controllers
{
    public class GoodsController : ApiController
    {
        IGenericService<GoodDTO> goodService;

        public GoodsController(IGenericService<GoodDTO> goodService)
        {
            this.goodService = goodService;
        }
        /// <summary>
        /// Получаю список всех товаров
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage GetAll()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, goodService.GetAll());
            }
            catch (Exception exc)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, "Не смогли получить список ВСЕХ товаров");
            }
        }
        /// <summary>
        /// Получаю товар по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var good = goodService.Get(id);
                return (good != null) ? Request.CreateResponse(HttpStatusCode.OK, good)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, $"Не смогли получить товар с id = {id}");
            }
            catch (Exception exc)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Не смогли получить товар с id = {id}");
            }
        }
        //[HttpGet]
        //[Route("api/Goods/Filter")]
        //public HttpResponseMessage FindBy(string name, decimal? priceFrom, decimal? priceTo)
        //{
        //    ViewModelFind find = new ViewModelFind
        //    {
        //        GoodName = name,
        //        PriceFrom = priceFrom,
        //        PriceTo = priceTo
        //    };

        //    try
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, goodService.FindBy(find.Predicate));
        //    }
        //    catch (Exception exc)
        //    {

        //        return Request.CreateResponse(HttpStatusCode.BadRequest, $"Не смогли получить список товаров");
        //    }
        //}
        [HttpPost]
        public HttpResponseMessage Post(GoodDTO good)
        {
            try
            {
                var g = goodService.Add(good);
                HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Accepted, g);
                message.Headers.Add("GoodId", g.GoodId.ToString());
                return message;
            }
            catch (Exception exc)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, "Не смогли добавить категорию");
            }

        }

        [HttpPut]
        public HttpResponseMessage Update(GoodDTO good)
        {
            try
            {
                var g = goodService.Update(good);
                return (good != null) ? Request.CreateResponse(HttpStatusCode.OK, good)
                   : Request.CreateResponse(HttpStatusCode.BadRequest, $"Не смогли обновить товар с id = {good.GoodId}");
            }
            catch (Exception exc)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Не смогли обновить товар с id = {good.GoodId}");
            }

        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var g = goodService.Delete(id);
                return (g != null) ? Request.CreateResponse(HttpStatusCode.OK, g.GoodId)
                   : Request.CreateResponse(HttpStatusCode.BadRequest, "Не смогли удалить категорию");
            }
            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Не смогли удалить категорию");
            }

        }
    }
}
        
    

