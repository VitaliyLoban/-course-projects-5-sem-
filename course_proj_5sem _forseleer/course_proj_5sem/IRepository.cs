using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_5sem
{
    interface IRepository<T> where T:class
    {
        void Create(T el);
        void Update(T el);
        void Delete(T el);
    }
    interface IRepositorySeller:IRepository<Seller>
    {
        List<Seller> GetSellersList();
    }
    public class RepositoryBook : IRepositorySeller
    {
        public void Create(Seller el)
        {
            throw new NotImplementedException();
        }

        public void Delete(Seller el)
        {
            throw new NotImplementedException();
        }

        public List<Seller> GetSellersList()
        {
            string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string get_sellers = "get_sellers";
            List<Seller> l_s = new List<Seller>();
            using (SqlConnection connect = new SqlConnection(conn))
            {
                connect.Open();
                SqlCommand command;
                command = new SqlCommand(get_sellers, connect);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader sel = command.ExecuteReader();
                if (sel.HasRows)
                {
                    while (sel.Read())
                    {
                        Seller b = new Seller();
                        b.Name = sel.GetString(1);
                        b.F_name = sel.GetString(2);
                        b.Login = sel.GetString(3);
                        b.Pass = sel.GetString(4);
                        b.Phone = sel.GetString(5);
                        l_s.Add(b);
                    }
                }
            }
            return l_s;
        }

        public void Update(Seller el)
        {
            throw new NotImplementedException();
        }
    }
}
