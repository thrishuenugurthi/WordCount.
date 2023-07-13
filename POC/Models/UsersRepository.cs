using Microsoft.Data.SqlClient;

namespace POC.Models
{
    public class UsersRepository
    {
        //private IConfiguration _configuration;
        private SqlConnection con = new SqlConnection("Data Source=(localdb)\\mssqllocaldb; Database=LoginPage;");
        private SqlCommand com = new SqlCommand();
        private SqlDataReader dr;

        //public UsersRepository(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //    con = new SqlConnection(_configuration.GetSection("ConnectionStrings:ConnString").ToString());
        //}
        public UsersRepository()
        {
            
        }

        public bool ValidateEmail(string email)
        {
            try
            {
                //Connection open here 
                con.Open();
                com.Connection = con;
                com.CommandText = "Select * From tblUsers where email='" + email + "'";
                dr = com.ExecuteReader();

                //List<Users> user = new List<Users>();

                if (dr.HasRows)
                {
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }


            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool Register(Users user)
        {




            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "Insert into tblUsers values ('" + user.fullname + "','" + user.email + "','" + user.password + "','" + DateTime.Today + "','" + "admin" + "','" + DateTime.Today + "','" + "admin" + "')";
                com.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                return false;
            }
        }

        public bool Verify(string email, string password)
        {

            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from tblUsers where email='" + email + "' and password='" + password + "'";
            dr = com.ExecuteReader();

            List<Users> user = new List<Users>();

            while (dr.Read())
            {
                var x = new Users()
                {
                    fullname = dr.GetString(0),
                    email = dr.GetString(1),
                    password = dr.GetString(2),

                };

                user.Add(x);
            }

            con.Close();
            //after successful it will redirect  to next page .

            if (user.Count == 1)
            {


                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ForgetPassword(string email, string newpassword)
        {
            try
            {
                //Creating DB connection
                con.Open();
                com.Connection = con;
                com.CommandText = "update tblUsers set PASSWORD='" + newpassword + "' where EMAIL='" + email + "'";

                int rows_count = com.ExecuteNonQuery();

                con.Close();
                if (rows_count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                return false;
            }
        }
    }
}
