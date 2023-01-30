using EMedicine.DataAccess;
using System.Data.SqlClient;
using EMedicine.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EMedicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly  IConfiguration _configuration;

        public MedicineController( IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route ("AddCart")]
        public Response AddCart(Cart cart)
        {
            var response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedics").ToString());
            DAL dAL = new DAL();
            dAL.AddCart(cart, connection);
            return response;

        }
        [HttpPost ]
        [Route ("PlaceOrde")]
        public Response PlacseOrder(User user)
        {
            var response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedics").ToString());
            DAL dAL = new DAL();
            dAL.PlaceOrder (user, connection);
            return response;

        }
        [HttpPost ]
        [Route ("OrderList")]
        public Response OrderList(User user)
        {
            var response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedics").ToString());
            DAL dAL = new DAL();
            dAL.OrderList (user, connection);
            return response;
        }



    }
}
