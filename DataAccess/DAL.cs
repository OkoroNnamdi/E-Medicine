using System.Data.SqlClient;
using EMedicine.Model;
using System.Data;
using System;
using System.Resources;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using EMedicine.Controllers;

namespace EMedicine.DataAccess
{
    public class DAL
    {
        public Response Register(User user, SqlConnection connection)
        {
            Response response = new Response();

            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@Password", user.Pasword);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Fund", 0);
            cmd.Parameters.AddWithValue("@Type ", "User");
            cmd.Parameters.AddWithValue("Type", "Pending");
            cmd.Parameters.AddWithValue ("@createon",user.createOn );
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.status = 200;
                response.message = " User Registered Sucessfully";
            }
            else
            {
                response.status = 100;
                response.message = "User Registration failed";
            }
            return response;
        }
        public Response Login(User user, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_Login", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Email", user.Email);
            da.SelectCommand.Parameters.AddWithValue("@Password", user.Pasword);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            var user1 = new User();
            if (dt.Rows.Count > 0)
            {
                user1.Id = Convert.ToInt32(dt.Rows[0]["ID"]);
                user1.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user1.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user1.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user1.Type = Convert.ToString(dt.Rows[0]["Type"]);
                user1.createOn = Convert.ToDateTime(dt.Rows[0]["createdon"]);
                user1.Fund = Convert.ToDecimal(dt.Rows[0]["Fund"]);
                user1.Status = Convert.ToInt32(dt.Rows[0]["Status"]);
                response.status = 200;
                response.message = "Valid user";
            }
            else
            {
                response.status = 100;
                response.message = "Invalid user";
                response.User = user1;
            }
            return response;
        }
        public Response ViewUser(User user, SqlConnection connection)
        {
            connection.Open();
            SqlDataAdapter da = new SqlDataAdapter("sp_ViewUser", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ID", user.Id);
            DataTable dt = new DataTable();
            da.Fill(dt);

            var response = new Response();
            if (dt.Rows.Count > 0)
            {
                response.status = 200;
                response.message = "User exist";
            }
            else
            {
                response.status = 100;
                response.message = "User Doesnot exist";
            }
            return response;
        }
        public Response UpdateProfile(User user, SqlConnection connection)
        {
            connection.Open();
            var response = new Response();
            SqlCommand cmd = new SqlCommand("SP_UpdateProfile", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@Password", user.Pasword);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.status = 200;
                response.message = "User Profile updated sucessfully";
            }
            else
            {
                response.status = 100;
                response.message = "Update Failed";

            }



            return response;
        }
        public Response AddCart(Cart cart, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_Addcart", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Userid", cart.UserId);
            cmd.Parameters.AddWithValue("@UnitPrice", cart.unitPrice);
            cmd.Parameters.AddWithValue("@Discount", cart.Discount);
            cmd.Parameters.AddWithValue("@Quanity", cart.Quantity);
            cmd.Parameters.AddWithValue("@Totalprice", cart.TotalPrice);
            cmd.Parameters.AddWithValue("@MedicineID ", cart.MedicineID);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.status = 200;
                response.message = "Cart addition was sucessfully";
            }
            else
            {
                response.status = 100;
                response.message = " Addition to the cart failed";
            }
            return response;

        }
        public Response PlaceOrder(User user, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Sp_Placeorder", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", user.Id);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.status = 200;
                response.message = "Order has been placed sucessfully";
            }
            else
            {
                response.status = 100;
                response.message = "Order could not be placed";
            }
            return response;

        }
        public Response OrderList(User user, SqlConnection connection)
        {
            var response = new Response();
            List<Orders> orderList = new List<Orders>();
            SqlDataAdapter da = new SqlDataAdapter("Sp_OrderList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Types", user.Type);
            da.SelectCommand.Parameters.AddWithValue("@ID", user.Id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Orders order = new Orders();
                    order.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    order.OrderNo = Convert.ToString(dt.Rows[i]["OrderNo"]);
                    order.OrderTotal = Convert.ToInt32(dt.Rows[i]["OrderTotal"]);
                    order.orderStatus = Convert.ToString(dt.Rows[i]["OrderStatus"]);
                    orderList.Add(order);
                }
                if (orderList.Count > 0)
                {
                    response.status = 200;
                    response.message = "Order details fetched";
                    response.Listorders = orderList;
                }
                else
                {
                    response.status = 100;
                    response.message = "Order details are not available";
                    response.Listorders = null;
                }
            }
            else
            {
                response.status = 100;
                response.message = "Order details are not available";
                response.Listorders = null;
            }
            return response;
        }

        public Response addUpdateMedicine(Medicines medicine,SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("SP_AddUpdateMedicine",connection );
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue ("@Name",medicine.Name );
            cmd.Parameters.AddWithValue ("@Manufacturer",medicine.Manufacturer );
            cmd.Parameters.AddWithValue ("@UnitPrice", medicine.UnitPrice );
            cmd.Parameters .AddWithValue ("@Discount", medicine.Discount );
            cmd.Parameters.AddWithValue ("@ExpDate",medicine.ExpDate );
            cmd.Parameters.AddWithValue("@ImageUrl", medicine.ImageUrl);
            cmd.Parameters.AddWithValue ("@Status",medicine.status );
            cmd.Parameters.AddWithValue("@Type", medicine.Type);
            connection.Open();
            int i = cmd.ExecuteNonQuery ();
            connection.Close();
            if (i> 0)
            {
                response.status = 200;
                response.message = "Medicine added sucessfully";
            }
            else
            {
                response.status = 100;
                response.message = "Medicine failed to be added";
            }
            return response;
        }
        public Response userList(SqlConnection connection)
        {
            var response = new Response();
            List<User> userList = new List<User>();
            SqlDataAdapter da = new SqlDataAdapter("Sp_userList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
           
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    User user1 =new User();
                    user1.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    user1.FirstName  = Convert.ToString(dt.Rows[i]["FirstName"]);
                    user1.LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                    user1.createOn = Convert.ToDateTime(dt.Rows[i]["createdon"]);
                    user1.Email = Convert.ToString (dt.Rows[i]["Email"]);
                    user1.Status = Convert.ToInt32(dt.Rows[i]["Status"]);
                    user1.Fund = Convert.ToDecimal(dt.Rows[i]["Fund"]);
                    user1.Type = Convert .ToString (dt.Rows[i]["Type"]);

                    user1.Pasword  = Convert.ToString(dt.Rows[i]["Pasword"]);
                    userList.Add(user1);
                }
                if (userList.Count > 0)
                {
                    response.status = 200;
                    response.message = "Users details fetch";
                    response.Listusers = userList;
                }
                else
                {
                    response.status = 100;
                    response.message = "User details are not available";
                    response.Listorders = null;
                }
            }
            else
            {
                response.status = 100;
                response.message = "User details are not available";
                response.Listorders = null;
            }
            return response;
        }

    }
}

