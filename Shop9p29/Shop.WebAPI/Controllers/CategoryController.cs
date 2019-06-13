using Shop.BLL.Models;
using Shop.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop.WebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        IGenericService<CategoryDTO> categoryService;

        public CategoryController(IGenericService<CategoryDTO> c)
        {
            categoryService = c;
        }

        public HttpResponseMessage GetAll()
        {
            try
            {
                var categories = categoryService.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, categories);
            }
            catch(Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Не смогли получить категории");
            }
        }

        public HttpResponseMessage Post(CategoryDTO category)
        {
            try
            {
                var c = categoryService.Add(category);
                HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, c);
                message.Headers.Add("CategoryId", c.CategoryId.ToString());
                return message;
            }
            catch (Exception exc)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, "Не смогли добавить категорию");
            }
           
        }

        }
}
