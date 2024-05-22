using API.Repository.Interface;
using Common.Models;
using System.Data;

namespace API.Repository
{
    public class UserServiceBusiness : IUserService
    {
        public UserServiceBusiness() { }


        public string AuthenticatedUser(string id, string pass)
        {
            string status = ActionStatus.Success;
            string param = string.Format(" and uMob='{0}'", id);

            string query = string.Format("SELECT * FROM `tps_users` where 1=1 {0}", Utils.InjectionChecker(param));

            MySqlDataAdapter sdaCustomer = new MySqlDataAdapter(query, Utils.ConnectRudo());
            DataTable dtCustomer = new DataTable();
            sdaCustomer.Fill(dtCustomer);

            if (dtCustomer.Rows.Count > 0)
            {
                //string pa = tpsCryptor.Decryptor(dtCustomer.Rows[0]["uPass"].ToString());
                string pa = dtCustomer.Rows[0]["uPass"].ToString();

                if (pa == pass)
                {
                    status = ActionStatus.Success;
                }
                else
                {
                    status = "Incorrect information.";
                }
            }
            else
            {
                status = "No account available.";
            }

            return status;
        }

        public User User(string Id)
        {
            User user = new User();

            MySqlDataAdapter sdaCustomer = new MySqlDataAdapter("SELECT * FROM `tps_users` where uMob='" + Id + "'", Utils.ConnectRudo());
            DataTable dtCustomer = new DataTable();
            sdaCustomer.Fill(dtCustomer);

            if (dtCustomer.Rows.Count > 0)
            {
                user.Id = dtCustomer.Rows[0]["Id"].ToString();
                user.Mob = dtCustomer.Rows[0]["uMob"].ToString();
                user.Name = dtCustomer.Rows[0]["uName"].ToString();
                user.Mob = dtCustomer.Rows[0]["uMob"].ToString();
                user.Email = dtCustomer.Rows[0]["uEmail"].ToString();
            }

            return user;
        }
    }
}
