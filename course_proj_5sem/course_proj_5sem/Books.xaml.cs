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
    /// Логика взаимодействия для Books.xaml
    /// </summary>
    public partial class Books : Page
    {
        string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        string get_books = "get_books";
        string get_auth = "get_auth_on_id";
        string get_publ = "get_publ_on_id";
        string find_auth = "find_auth";
        string get_book_on_id = "get_book_on_id";
        string find_content = "find_content";
        string find_title = "find_title";
        List<Book_inf> list_b = new List<Book_inf>();
        Book_inf b1;
        public Books()
        {
            InitializeComponent();
        }
        public void spis(int id, List<Book_inf> c)
        {

            using (SqlConnection connect = new SqlConnection(conn))
            {
                connect.Open();
                SqlCommand command;
                SqlCommand command2;
                SqlCommand command3;
                command = new SqlCommand(get_book_on_id, connect);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader books = command.ExecuteReader();
                if (books.HasRows)
                {
                    List<Book_inf> list_b = new List<Book_inf>();
                    while (books.Read())
                    {
                        b1 = new Book_inf();
                        b1.Id = books.GetInt32(0);
                        b1.Title = books.GetString(1);
                        b1.Lang = books.GetString(2);
                        b1.Year = books.GetInt32(3);
                        b1.Cost = books.GetInt32(4);
                        b1.Count = books.GetInt32(5);
                        b1.Img = books.GetString(7);
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

                ////////
                command = new SqlCommand(get_books, connect);
                command.CommandType = System.Data.CommandType.StoredProcedure;
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
                        b1.Img = books.GetString(7);
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
                        list_b.Add(b1);
                    }
                }
            }
            l_b.ItemsSource = list_b;
        }

        private void l_b_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book_inf cur_book = l_b.SelectedItem as Book_inf;
            Current_book b = new Current_book(cur_book);
            b.Show();
        }

        private void find1_Click(object sender, RoutedEventArgs e)
        {
            if (a_n.Text == string.Empty && a_f.Text == string.Empty)
                MessageBox.Show("заполните поля");
            else
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    SqlCommand command;
                    command = new SqlCommand(find_auth, connect);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    if (a_n.Text != string.Empty && a_f.Text != string.Empty)
                    {
                        command.Parameters.Add(new SqlParameter("@name", $"{a_n.Text}"));
                        command.Parameters.Add(new SqlParameter("@f_name", $"{a_f.Text}"));
                    }
                    else if (a_n.Text == string.Empty && a_f.Text != string.Empty)
                    {
                        command.Parameters.Add(new SqlParameter("@name", "no"));
                        command.Parameters.Add(new SqlParameter("@f_name", $"{a_f.Text}"));
                    }
                    else if (a_n.Text != string.Empty && a_f.Text == string.Empty)
                    {
                        command.Parameters.Add(new SqlParameter("@name", $"{a_n.Text}"));
                        command.Parameters.Add(new SqlParameter("@f_name", "no"));
                    }
                    SqlDataReader books = command.ExecuteReader();
                    if (books.HasRows)
                    {
                        List<Book_inf> kkk = new List<Book_inf>();
                        while (books.Read())
                        {
                            spis(books.GetInt32(0), kkk);
                        }
                        l_b.ItemsSource = kkk;
                    }
                    else
                        MessageBox.Show("Поиск не дал результатов");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tit.Text != string.Empty)
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    SqlCommand command;
                    command = new SqlCommand(find_content, connect);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@content", tit.Text));
                    SqlDataReader books = command.ExecuteReader();
                    if (books.HasRows)
                    {
                        List<Book_inf> kkk = new List<Book_inf>();
                        while (books.Read())
                        {
                            spis(books.GetInt32(0), kkk);
                        }
                        l_b.ItemsSource = kkk;
                    }
                    else
                        MessageBox.Show("Поиск не дал результатов");
                }
            }
            else
                MessageBox.Show("заполните поля");
        }

        private void find3_Click(object sender, RoutedEventArgs e)
        {
            if (qwe.Text != string.Empty)
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    SqlCommand command;
                    command = new SqlCommand(find_title, connect);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@content", $"\"{qwe.Text}*\""));
                    SqlDataReader books = command.ExecuteReader();
                    if (books.HasRows)
                    {
                        List<Book_inf> kkk = new List<Book_inf>();
                        while (books.Read())
                        {
                            spis(books.GetInt32(0), kkk);
                        }
                        l_b.ItemsSource = kkk;
                    }
                    else
                        MessageBox.Show("Поиск не дал результатов");
                }
            }
            else
                MessageBox.Show("заполните поля");
        }
    }
}
