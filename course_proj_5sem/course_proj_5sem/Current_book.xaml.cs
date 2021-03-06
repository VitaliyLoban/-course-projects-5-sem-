﻿using System;
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
    public partial class Current_book : Window
    {
        Book_inf book;
        string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        string get_curr_bask = "get_curr_bask";
        string add_book_in_bask = "add_book_in_bask";
        int id_for_ins;
        public Current_book(Book_inf book)
        {
            this.book = book;
            InitializeComponent();
            this.DataContext = book;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            id_for_ins = book.Id;
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
            cost.Content = book.Cost.ToString();
            count.Content = book.Count.ToString();
            lang.Content = book.Lang;
            num.Maximum = book.Count;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (num.Value!=null)
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    SqlCommand command;
                    SqlCommand command1;
                    command = new SqlCommand(get_curr_bask, connect);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@id", Curent_us.Id));
                    var id_bask=command.ExecuteScalar();
                    command1 = new SqlCommand(add_book_in_bask, connect);
                    command1.CommandType = System.Data.CommandType.StoredProcedure;
                    command1.Parameters.Add(new SqlParameter("@id_bask", id_bask));
                    command1.Parameters.Add(new SqlParameter("@id_book",id_for_ins));
                    command1.Parameters.Add(new SqlParameter("@count", num.Value));
                    command1.ExecuteNonQuery();
                    MessageBox.Show("Добавлено в корзину");
                }
            }
            else
                MessageBox.Show("Проверьте корректность ввода");
        }

        private void num_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if(num.Value!=null)
            {
                total.Content = $"{(book.Cost * num.Value)} бел/руб";
            }
        }
    }
}
