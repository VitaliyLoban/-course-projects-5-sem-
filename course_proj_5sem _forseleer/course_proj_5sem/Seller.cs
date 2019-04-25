using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace course_proj_5sem
{
    [Serializable]
    public class Seller
    {
        string name;
        string f_name;
        string login;
        string pass;
        string phone;

        public string Name { get => name; set => name = value; }
        public string F_name { get => f_name; set => f_name = value; }
        public string Login { get => login; set => login = value; }
        public string Pass { get => pass; set => pass = value; }
        public string Phone { get => phone; set => phone = value; }
    }
}