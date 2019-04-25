using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace course_proj_5sem
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        string get_order_by_id = "get_order_by_id";
        string books_from_order = "books_from_order";
        Order_inf b2;
        Book_inf b1;
        List<Book_inf> c;
        List<Order_inf> z = new List<Order_inf>();
        string get_auth = "get_auth_on_id";
        string get_publ = "get_publ_on_id";
        int mmm = Curent_us.Id;
        public Orders()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connect = new SqlConnection(conn))
            {
                connect.Open();
                SqlCommand command;
                SqlCommand command2;
                SqlCommand command3;
                ////////

                ////////

                ////////
                command = new SqlCommand(get_order_by_id, connect);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@id", mmm));
                SqlDataReader res = command.ExecuteReader();
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        b2 = new Order_inf();
                        b2.Id = res.GetInt32(0);
                        b2.Id_cust = res.GetInt32(1);
                        if (res.GetValue(2) != DBNull.Value)
                            b2.Id_seller = res.GetInt32(2);
                        b2.Sum = res.GetInt32(3);
                        b2.Date = res.GetDateTime(4).ToShortDateString();
                        b2.Addr = res.GetString(5);
                        if (res.GetValue(6) != DBNull.Value)
                            b2.Status = res.GetString(6);
                        //
                        command = new SqlCommand(books_from_order, connect);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@id_bask", res.GetInt32(0)));
                        SqlDataReader books = command.ExecuteReader();
                        if (books.HasRows)
                        {
                            c = new List<Book_inf>();
                            while (books.Read())
                            {
                                b1 = new Book_inf();
                                b1.Id = books.GetInt32(0);
                                b1.Title = books.GetString(1);
                                b1.Lang = books.GetString(2);
                                b1.Year = books.GetInt32(3);
                                b1.Cost = books.GetInt32(4);
                                b1.Count = books.GetInt32(5);
                                //
                                command2 = new SqlCommand(get_auth, connect);
                                command2.CommandType = System.Data.CommandType.StoredProcedure;
                                command2.Parameters.Add(new SqlParameter("@id", books.GetInt32(0)));
                                SqlDataReader auth = command2.ExecuteReader();
                                if (auth.HasRows)
                                {
                                    string a;
                                    while (auth.Read())
                                    {
                                        b1.Auth += $"{auth.GetString(0)} {auth.GetString(1)};";
                                    }
                                }
                                command3 = new SqlCommand(get_publ, connect);
                                command3.CommandType = System.Data.CommandType.StoredProcedure;
                                command3.Parameters.Add(new SqlParameter("@id", books.GetInt32(0)));
                                SqlDataReader publ = command3.ExecuteReader();
                                if (publ.HasRows)
                                {
                                    string a;
                                    while (publ.Read())
                                    {
                                        b1.Pu_house += $"{publ.GetString(0)}:{publ.GetString(1)};";
                                    }
                                }
                                c.Add(b1);
                            }
                            b2.List = c;
                        }
                        z.Add(b2);
                    }

                }
                l_b.ItemsSource = z;
            }
        }
        private void l_b_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Order_inf cur_ord = l_b.SelectedItem as Order_inf;
            Current_order b = new Current_order(cur_ord);
            b.Show();
        }
    }
}