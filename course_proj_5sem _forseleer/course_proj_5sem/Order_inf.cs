using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_5sem
{
    public class Order_inf
    {
        int id;
        int id_cust;
        int id_seller;
        int sum;
        string date;
        string addr;
        string status;
        List<Book_inf> list;

        public int Id { get => id; set => id = value; }
        public int Id_cust { get => id_cust; set => id_cust = value; }
        public int Id_seller { get => id_seller; set => id_seller = value; }
        public int Sum { get => sum; set => sum = value; }
        public string Date { get => date; set => date = value; }
        public string Addr { get => addr; set => addr = value; }
        public string Status { get => status; set => status = value; }
        public List<Book_inf> List { get => list; set => list = value; }
    }
}
