using DAL.Modul_1;
using DML;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Evolve.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class categoryController : ControllerBase
    {
        [HttpGet]
        public clsResult getCategory()
        {
            clsResult r = new clsResult();
            try
            {
                r.Data = categoryDAL.GetAllCategories();
                r.Error = "Ok";
            }
            catch (System.Exception ex)
            {
                r.Error= ex.Message;
                r.Message = "Errors coming";
            }
            return r;
        }

        [HttpGet("id")]
        public clsResult getCategory(int id)
        {
            clsResult r = new clsResult();
            try
            {
                r.Data = categoryDAL.GetCategoryById(id);
                r.Error = "Ok";
            }
            catch (System.Exception ex)
            {
                r.Error = ex.Message;
                r.Message = "Errors coming";
            }
            return r;
        }

        [HttpPost("AddCategory|UpdateCategory")]
        public clsResult postCategory(clsCategory model)
        {
            clsResult r = new clsResult();
            try
            {
                if (model.Id > 0)
                {
                    r.Data = categoryDAL.UpdateCategory(model);
                    r.Message = "Updated Successfuly";
                }
                else
                {
                    r.Data = categoryDAL.InsertCategory(model);
                    r.Message = "Insert Successfuly";
                }

            }
            catch (System.Exception ex)
            {
                r.Error = ex.Message;
                r.Message = "Errors coming";
            }
            return r;
        }
    }
}
