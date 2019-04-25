using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_5sem
{
    public class Book_inf
    {
        int id;
        string title;
        string lang;
        int year;
        int cost;
        int count;
        string auth;
        string pu_house;
        string img;

       

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Lang { get => lang; set => lang = value; }
        public int Year { get => year; set => year = value; }
        public int Cost { get => cost; set => cost = value; }
        public int Count { get => count; set => count = value; }
        public string Auth { get => auth; set => auth = value; }
        public string Pu_house { get => pu_house; set => pu_house = value; }
        public string Img { get => img; set => img = value; }
    }
}
