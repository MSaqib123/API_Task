using DAL.Modul_1;
using DML;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Evolve.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {
        [HttpGet]
        public clsResult getProduct()
        {
            clsResult r = new clsResult();
            try
            {
                r.Data = productDAL.GetAllProducts();
                r.Error = "Ok";
            }
            catch (System.Exception ex)
            {
                r.Error = ex.Message;
                r.Message = "Errors coming";
            }
            return r;
        }

        [HttpGet("id")]
        public clsResult getProduct(int id)
        {
            clsResult r = new clsResult();
            try
            {
                r.Data = productDAL.GetProductById(id);
                r.Error = "Ok";
            }
            catch (System.Exception ex)
            {
                r.Error = ex.Message;
                r.Message = "Errors coming";
            }
            return r;
        }

        [HttpPost("AddProduct|UpdateProduct")]
        public clsResult postProduct(clsProduct model)
        {
            clsResult r = new clsResult();
            try
            {
                if (model.Id > 0)
                {
                    r.Data = productDAL.UpdateProduct(model);
                    r.Message = "Updated Successfuly";
                }
                else
                {
                    r.Data = productDAL.InsertProduct(model);
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
