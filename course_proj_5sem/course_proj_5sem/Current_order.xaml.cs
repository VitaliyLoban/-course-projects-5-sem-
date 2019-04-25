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
        string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        string seller_inf = "seller_inf";
        int cc = Curent_us.Id;
        public Current_order(Order_inf inf)
        {
            this.inf = inf;
            InitializeComponent();

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Book_inf z in inf.List)
            {
                l_books.Items.Add($"{z.Title},({z.Count} шт.)");
            }
            using (SqlConnection connect = new SqlConnection(conn))
            {
                connect.Open();
                SqlCommand command;
                command = new SqlCommand(seller_inf, connect);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@id",inf.Id_seller ));
                SqlDataReader seller = command.ExecuteReader();
                if (seller.HasRows)
                {
                    while(seller.Read())
                    {
                        na.Content = seller.GetString(0);
                        f_na.Content= seller.GetString(1);
                    }
                }
            }
        }
    }
}
