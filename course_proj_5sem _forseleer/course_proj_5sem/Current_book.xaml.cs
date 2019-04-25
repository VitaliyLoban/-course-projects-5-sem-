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
    /// Логика взаимодействия для Current_book.xaml
    /// </summary>
    /// 
    public partial class Current_book : Window
    {
        Book_inf book;
        string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        string update_book = "update_book";
        string del_book = "del_book";
        public Current_book(Book_inf book)
        {
            this.book = book;
            InitializeComponent();       
            this.DataContext = book;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            title.Content = book.Title;
            if (book.Auth != null)
            {
                foreach (var k in book.Auth.Split(';'))
                {
                    if (k != string.Empty)
                        au_list.Items.Add($"{k.Replace(' ', '_')}");
                }
            }
            if (book.Pu_house != null)
            {
                foreach (var k in book.Pu_house.Split(';'))
                {
                    if (k != string.Empty)
                        p_h_list.Items.Add($"{k.Replace(' ', '_')}");

                }
            }
            year.Content = book.Year.ToString();
            cost.Value = book.Cost;
            count.Value = book.Count;
            lang.Content = book.Lang;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (count.Value != null && cost.Value != null)
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    SqlCommand command;
                    command = new SqlCommand(update_book, connect);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@id", book.Id));
                    command.Parameters.Add(new SqlParameter("@count", count.Value));
                    command.Parameters.Add(new SqlParameter("@cost", cost.Value));
                    command.ExecuteNonQuery();
                    MessageBox.Show("Данные обновлены");
                }
            }
            else
                MessageBox.Show("Проверьте корректность ввода");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connect = new SqlConnection(conn))
            {
                connect.Open();
                SqlCommand command;
                command = new SqlCommand(del_book, connect);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@id", book.Id));
                var res=command.ExecuteNonQuery();
                if(res>0)
                    MessageBox.Show("Данные удалены");
                else
                    MessageBox.Show("Удаление не произошло");
            }
        }
    }
}
