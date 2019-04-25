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
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        MainWindow _mainWindow;
        bool isOk = true;
        string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        string ch_pass = "check_password";
        string change_pass = "change_pass";
        public Settings(MainWindow _mainWindow)
        {
            this._mainWindow = _mainWindow;
            InitializeComponent();
        }
        private void Change_pass(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connect = new SqlConnection(conn))
            {
                connect.Open();
                SqlCommand command;
                command = new SqlCommand(ch_pass, connect);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@id", Curent_us.Id));
                command.Parameters.Add(new SqlParameter("@pass", pas.Password));
                var res = command.ExecuteReader();
                if (res.HasRows)
                {
                    if (pas1.Password == pas2.Password)
                    {
                        res.Close();
                        command = new SqlCommand();
                        command = new SqlCommand(change_pass, connect);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@pass", pas1.Password));
                        command.Parameters.Add(new SqlParameter("@id", Curent_us.Id));
                        var end = command.BeginExecuteNonQuery();
                        MessageBox.Show("Пароль изменен");
                    }
                    else
                        MessageBox.Show("Пароли не совпадают!");
                }
                else
                    MessageBox.Show("Старый пароль введен не правильно!");
            }
        }
        private void col_change(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            //tema.col = tem.SelectedColor.ToString();
            _mainWindow.SetColor(tem.SelectedColor.ToString());
        }
    }
}
