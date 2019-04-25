using Microsoft.Win32;
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
    /// Логика взаимодействия для add_book.xaml
    /// </summary>
    public partial class add_book : Page
    {

        string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        string[] language = { "Английский", "Испанский", "Китайский", "Русский", "Арабский", "Французский" };
        string ins_book = "insert_book";
        string ins_auth = "insert_author";
        string auth_exist = "auth_exist";
        string ins_autship = "ins_autship";
        string insert_p_house = "insert_p_house";
        string p_house_exist = "p_house_exist";
        string ins_edition = "ins_edition";
        string pict = "D:\\img\\1_k-spisku.jpg";
        public add_book()
        {

            InitializeComponent();
            lang.ItemsSource = language;
            year.Maximum = DateTime.Now.Year;
            picture.Source = new BitmapImage(new Uri(pict));
            //using (SqlConnection connect = new SqlConnection(conn))
            //{
            //    connect.Open();
            //    for (int i =2; i < 100; i++)
            //    {
                    
            //        SqlCommand command;
            //        command = new SqlCommand(ins_auth, connect);
            //        command.CommandType = System.Data.CommandType.StoredProcedure;
            //        command.Parameters.Add(new SqlParameter("@name",$"Name{i}"));
            //        command.Parameters.Add(new SqlParameter("@f_name", $"Fname{i}"));
            //        command.ExecuteNonQuery();
            //        if (i == 99)
            //            MessageBox.Show("ddd");
            //    }
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listb_aut.Items.Add($"{au_name.Text}_{au_fname.Text}");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            listb_publ.Items.Add($"{publ_name.Text}_{publ_adr.Text}");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string u = new TextRange(content.Document.ContentStart, content.Document.ContentEnd).Text;
            if (book_name.Text != string.Empty &&
               year.Value != null &&
               cost.Value != null &&
               count.Value != null &&
               u != string.Empty &&
               listb_aut.Items.Count != 0 &&
               listb_publ.Items.Count != 0 &&
               pict != string.Empty)
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    SqlCommand command;
                    command = new SqlCommand(ins_book, connect);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@title", book_name.Text));
                    command.Parameters.Add(new SqlParameter("@lang", lang.SelectedItem));
                    command.Parameters.Add(new SqlParameter("@year", year.Value));
                    command.Parameters.Add(new SqlParameter("@cost", cost.Value));
                    command.Parameters.Add(new SqlParameter("@count", count.Value));
                    command.Parameters.Add(new SqlParameter("@content", u));
                    command.Parameters.Add(new SqlParameter("@imag", pict));
                    int id_b = Convert.ToInt32(command.ExecuteScalar());
                    foreach (string z in listb_aut.Items)
                    {
                        string[] a = z.Split('_');
                        command = new SqlCommand(auth_exist, connect);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@name", a[0]));
                        command.Parameters.Add(new SqlParameter("@f_name", a[1]));
                        var id_a = command.ExecuteScalar();
                        if (id_a == null)
                        {
                            command = new SqlCommand(ins_auth, connect);
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@name", a[0]));
                            command.Parameters.Add(new SqlParameter("@f_name", a[1]));
                            int id_au = Convert.ToInt32(command.ExecuteScalar());
                            command = new SqlCommand(ins_autship, connect);
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@id_book", id_b));
                            command.Parameters.Add(new SqlParameter("@id_auth", id_au));
                            command.ExecuteNonQuery();
                        }
                        else
                        {
                            command = new SqlCommand(ins_autship, connect);
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@id_book", id_b));
                            command.Parameters.Add(new SqlParameter("@id_auth", id_a));
                            command.ExecuteNonQuery();
                        }
                    }
                    foreach (string z in listb_publ.Items)
                    {
                        string[] a = z.Split('_');
                        command = new SqlCommand(p_house_exist, connect);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@name", a[0]));
                        command.Parameters.Add(new SqlParameter("@adress", a[1]));
                        var id_a = command.ExecuteScalar();
                        if (id_a == null)
                        {
                            command = new SqlCommand(insert_p_house, connect);
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@p_name", a[0]));
                            command.Parameters.Add(new SqlParameter("@adress", a[1]));
                            int id_au = Convert.ToInt32(command.ExecuteScalar());
                            command = new SqlCommand(ins_edition, connect);
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@id_book", id_b));
                            command.Parameters.Add(new SqlParameter("@id_p_house", id_au));
                            command.ExecuteNonQuery();
                        }
                        else
                        {
                            command = new SqlCommand(ins_edition, connect);
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@id_book", id_b));
                            command.Parameters.Add(new SqlParameter("@id_p_house", id_a));
                            command.ExecuteNonQuery();
                        }
                    }

                }
                MessageBox.Show("Успешно");
            }
            else
            {
                MessageBox.Show("заполните все поля");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            OpenFileDialog b = new OpenFileDialog();
            b.Title = "Выберите файл";
            b.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (b.ShowDialog() == true)
            {
                pict = b.FileName;
                picture.Source = new BitmapImage(new Uri(pict));
            }
        }
    }
}
