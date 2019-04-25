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
using System.Windows.Shapes;

namespace course_proj_5sem
{
    /// <summary>
    /// Логика взаимодействия для Current_order.xaml
    /// </summary>
    public partial class Current_order : Window
    {
        Order_inf inf;
        string check_count_books = "check_count_books";
        string update_order = "update_order";
        string update_book_count = "update_book_count";
        string cust_inf = "cust_inf";
        bool c = true;
        int cur = Curent_us.Id;
        
        MainWindow main;

        string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public Current_order(Order_inf inf,MainWindow main)
        {
            this.inf = inf;
            this.main = main;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int cur_cust = inf.Id_cust;
            foreach (Book_inf z in inf.List)
            {
                l_books.Items.Add($"{z.Title},({z.Count} шт.)");
            }
            using (SqlConnection connect = new SqlConnection(conn))
            {
                connect.Open();
                SqlCommand command;
                command = new SqlCommand(cust_inf, connect);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@id",cur));
                SqlDataReader seller = command.ExecuteReader();
                if (seller.HasRows)
                {
                    while (seller.Read())
                    {
                        na.Content = seller.GetString(0);
                        f_na.Content = seller.GetString(1);
                        phone.Content = seller.GetString(2);
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (inf.Status != "Доставлен")
            {
                foreach (Book_inf z in inf.List)
                {
                    using (SqlConnection connect = new SqlConnection(conn))
                    {
                        connect.Open();
                        SqlCommand command;
                        command = new SqlCommand(check_count_books, connect);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@id_book", z.Id));
                        var res = Convert.ToInt32(command.ExecuteScalar());
                        if (res < z.Count)
                            c = c && false;
                        else
                            c = c && true;
                    }
                }
                if (c == true || (c == false && combo.SelectedIndex == 1))
                {
                    using (SqlConnection connect = new SqlConnection(conn))
                    {
                        connect.Open();
                        SqlCommand command;
                        command = new SqlCommand(update_order, connect);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@id_order", inf.Id));
                        command.Parameters.Add(new SqlParameter("@id_seller", cur));
                        if (combo.SelectedIndex == 0)
                            command.Parameters.Add(new SqlParameter("@status", "Одобрен"));
                        else if (combo.SelectedIndex == 1)
                            command.Parameters.Add(new SqlParameter("@status", "Отклонен"));
                        else
                            command.Parameters.Add(new SqlParameter("@status", "Доставлен"));
                        command.ExecuteNonQuery();
                        /////
                        if ((combo.SelectedIndex == 0 && inf.Status == null)|| (combo.SelectedIndex == 0 && inf.Status == "Отклонен"))
                        {
                            foreach (Book_inf z in inf.List)
                            {
                                int p = z.Id;
                                int pp = -z.Count;
                                SqlCommand command1;
                                command1 = new SqlCommand(update_book_count, connect);
                                command1.CommandType = System.Data.CommandType.StoredProcedure;
                                command1.Parameters.Add(new SqlParameter("@id_book", p));
                                command1.Parameters.Add(new SqlParameter("@count", pp));
                                command1.ExecuteNonQuery();
                            }
                        }
                        
                        ///
                        if (combo.SelectedIndex == 1 && inf.Status == "Одобрен")
                        {
                            foreach (Book_inf z in inf.List)
                            {
                                int p = z.Id;
                                int pp = z.Count;
                                SqlCommand command1;
                                command1 = new SqlCommand(update_book_count, connect);
                                command1.CommandType = System.Data.CommandType.StoredProcedure;
                                command1.Parameters.Add(new SqlParameter("@id_book", p));
                                command1.Parameters.Add(new SqlParameter("@count", pp));
                                command1.ExecuteNonQuery();
                            }
                        }


                    }
                    main.smth();
                }
                else
                    MessageBox.Show("Данного количества книг нет на складе");
            }
            this.Close();
        }
    }
}
