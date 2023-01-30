using System.Data.SqlClient;
using EMedicine.DataAccess;
using EMedicine.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EMedicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IConfiguration _Configuration;
        public UsersController(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        [HttpPost]
        [Route ("Register")]
        public Response register(User users)
        {
            var response = new Response();
            SqlConnection connection = new SqlConnection(_Configuration.GetConnectionString("EMedics").ToString());
            DAL dAL = new DAL();
            dAL.Register(users, connection);
            return response;

        }
        [HttpPost]
        [Route ("Login")]
        public Response Login (User users)
        {
            var response = new Response();
            var connection = new SqlConnection(_Configuration.GetConnectionString("EMedics").ToString());
            var Dal = new DAL();
            response = Dal.Login (users, connection);
            return response;
        }
        [HttpPost ]
        [Route ("ViewUser")]
        public Response ViewUser(User users)
        {
            DAL dAL = new DAL();
            var connection = new SqlConnection(_Configuration.GetConnectionString("EMedics").ToString());
            var response = new Response();
           response = dAL.ViewUser (users, connection);
            return response;
            

        }
        [HttpPatch ]
        [Route ("UpdateProfile")]
        public Response UpdateProfile (User user )
        {
            var response = new Response();
           var  connection = new SqlConnection(_Configuration.GetConnectionString("EMedics").ToString());
            var Dal = new DAL();
            response = Dal.UpdateProfile (user, connection);
            return response;

        }
    }
}

