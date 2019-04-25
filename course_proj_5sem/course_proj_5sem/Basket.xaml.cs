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
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket : Page
    {
        string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        List<Book_inf> li = new List<Book_inf>();
        Book_inf b1;
        int id_bask;
        string get_curr_bask = "get_curr_bask";
        string books_from_bask = "books_from_bask";
        string get_auth = "get_auth_on_id";
        string get_publ = "get_publ_on_id";
        string add_book_in_order = "add_book_in_order";
        string add_order = "add_order";
        int order;
        public Basket()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (order > 0 && adr.Text!=string.Empty)
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    SqlCommand command;
                    command = new SqlCommand(add_order, connect);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@id_cust", Curent_us.Id));
                    command.Parameters.Add(new SqlParameter("@sum", order));
                    command.Parameters.Add(new SqlParameter("@date", $"{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}"));
                    command.Parameters.Add(new SqlParameter("@adress", adr.Text));
                    var cur_ord =command.ExecuteScalar();
                    foreach(Book_inf x in li)
                    {
                        command = new SqlCommand(add_book_in_order, connect);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@id_order", cur_ord));
                        command.Parameters.Add(new SqlParameter("@id_book", x.Id));
                        command.Parameters.Add(new SqlParameter("@count", x.Count));
                        command.ExecuteNonQuery();
                        MessageBox.Show("Заказ в обработке");
                    }
                }
            }
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
                SqlCommand command4;
                command4 = new SqlCommand(get_curr_bask, connect);
                command4.CommandType = System.Data.CommandType.StoredProcedure;
                command4.Parameters.Add(new SqlParameter("@id", Curent_us.Id));
                id_bask = Convert.ToInt32(command4.ExecuteScalar());
                ////////
                command = new SqlCommand(books_from_bask, connect);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@id_bask", id_bask));
                SqlDataReader books = command.ExecuteReader();
                if (books.HasRows)
                {
                    while (books.Read())
                    {
                        b1 = new Book_inf();
                        b1.Id = books.GetInt32(0);
                        b1.Title = books.GetString(1);
                        b1.Lang = books.GetString(2);
                        b1.Year = books.GetInt32(3);
                        b1.Cost = books.GetInt32(4);
                        b1.Count = books.GetInt32(5);
                        b1.Img= books.GetString(6);
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
                        li.Add(b1);
                    }
                }
            }
            l_b.ItemsSource = li;
            foreach (Book_inf k in li)
            {
                order += k.Cost * k.Count;
            }
            order_cost.Content = order.ToString() + " бел/руб";
        }

        private void l_b_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book_inf cur_book = l_b.SelectedItem as Book_inf;
            Book_in_bask b = new Book_in_bask(cur_book);
            b.Show();
        }
    }
}
