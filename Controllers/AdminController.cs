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
    public class AdminController : ControllerBase
    {
        public  readonly IConfiguration _configuration;
        public AdminController( IConfiguration configuration )
        {
            _configuration = configuration;
        }
        [HttpPost ]
        [Route("addUpdateMedicine")]
        public Response AddUpdateMedicine(Medicines medicines)
        {
            var response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedics").ToString());
            DAL dAL = new DAL();
            dAL.addUpdateMedicine (medicines , connection);
            return response;

        }
        [HttpGet]
        [Route ("userList")]
        public Response userList()
        {
            var response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedics").ToString());
            DAL dAL = new DAL();
            response = dAL.userList( connection);
            return response;
        }
    }
}
