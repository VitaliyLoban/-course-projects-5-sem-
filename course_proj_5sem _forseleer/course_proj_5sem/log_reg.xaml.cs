using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public static class Curent_us
    {
        private static int id;
        public static int Id { get => id; set => id = value; }
    }
    public partial class log_reg : Window
    {

        string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        Validation_class val;
        string add_cust = "insert_adm";
        string is_exists = "IsExist_adm";
        string check_us = "Check_User_adm";
        string get_current = "get_curent_adm";
        public log_reg()
        {
            InitializeComponent();
            val = new Validation_class();
            this.DataContext = val;
        }
        private void signin_click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connect = new SqlConnection(conn))
            {
                connect.Open();
                SqlCommand command;
                command = new SqlCommand(check_us, connect);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@login", login.Text));
                command.Parameters.Add(new SqlParameter("@pass", pass.Password));
                var res = command.ExecuteReader();
                if (res.HasRows)
                {
                    //MessageBox.Show("OK");
                    MainWindow z = new MainWindow();
                    z.Show();
                    get_curr_us(login.Text);
                    this.Close();
                    res.Close();
                }
                else
                {
                    MessageBox.Show("Не верное имя пользователя или пароль");
                }
            }
        }
        

        private void register_click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(f_name.Text) ||
                             String.IsNullOrEmpty(s_name.Text) ||
                             String.IsNullOrEmpty(u_name.Text) ||
                             String.IsNullOrEmpty(Passw.Password) ||
                             String.IsNullOrEmpty(passw1.Password))
            {
                MessageBox.Show("Сперва заполните все строки!");
            }
            else if (Validation_class.log_ok == false ||
                Validation_class.name_ok == false ||
                Validation_class.sname_ok == false ||
                Validation_class.phone_ok == false)
                MessageBox.Show("Проверте коректность ввода");
            else if (Passw.Password != passw1.Password)
            {
                MessageBox.Show("Пароли не совпадают!");
            }
            else
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    SqlCommand command;
                    command = new SqlCommand(is_exists, connect);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@name", u_name.Text));
                    var res = command.ExecuteReader();
                    if (res.HasRows)
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует");
                    }
                    else
                    {
                        res.Close();
                        command = new SqlCommand(add_cust, connect);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@name", f_name.Text));
                        command.Parameters.Add(new SqlParameter("@f_name", s_name.Text));
                        command.Parameters.Add(new SqlParameter("@login", u_name.Text));
                        command.Parameters.Add(new SqlParameter("@password", Passw.Password));
                        command.Parameters.Add(new SqlParameter("@phone_num", phone.Text));
                        var result = command.ExecuteNonQuery();
                        MessageBox.Show(result.ToString());
                        MainWindow z = new MainWindow();
                        z.Show();
                        get_curr_us(u_name.Text);
                        this.Close();
                    }

                }

            }
        }
        public void get_curr_us(string log)
        {
            using (SqlConnection connect = new SqlConnection(conn))
            {
                connect.Open();
                SqlCommand command;
                command = new SqlCommand(get_current, connect);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@login", log));
                int result = (int)command.ExecuteScalar();
                Curent_us.Id = result;
            }
        }

    }
}
