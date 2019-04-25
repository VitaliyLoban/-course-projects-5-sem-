using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
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
using System.Xml.Serialization;

namespace course_proj_5sem
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        XmlSerializer formatter = new XmlSerializer(typeof(List<Seller>));
        MainWindow _mainWindow;
        bool isOk = true;
        string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        string ch_pass = "check_password_adm";
        string change_pass = "change_pass_adm";
        string get_sellers = "get_sellers";
        string import_sellers = "import_sellers";
        List<Seller> l_s;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream("sellers.xml", FileMode.Create))
            {

                formatter.Serialize(fs, new RepositoryBook().GetSellersList());
                MessageBox.Show("Успешно");
                
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            l_s = new List<Seller>();
            using (FileStream fs = new FileStream("sellers.xml", FileMode.OpenOrCreate))
            {
                l_s = (List<Seller>)formatter.Deserialize(fs);
            }
            using (SqlConnection connect = new SqlConnection(conn))
            {
                connect.Open();
                foreach (var z in l_s)
                {
                    SqlCommand command;
                    command = new SqlCommand(import_sellers, connect);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@name", z.Name));
                    command.Parameters.Add(new SqlParameter("@f_name", z.F_name));
                    command.Parameters.Add(new SqlParameter("@login", z.Login));
                    command.Parameters.Add(new SqlParameter("@phone_num", z.Phone));
                    command.Parameters.Add(new SqlParameter("@password", z.Pass));
                    var result = command.ExecuteNonQuery();
                }

            }

        }

        
    }
}
