using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace course_proj_5sem
{
    class Validation_class: IDataErrorInfo
    {
        Regex username_reg = new Regex(@"^[a-zA-Z][a-zA-Z0-9-_\.]{2,20}$");
        Regex word_reg = new Regex(@"^[а-яА-ЯёЁ]{2,15}$");
        Regex phone_reg = new Regex(@"^\+375\s\([1-9]{2}\)\s[0-9]{3}-[0-9]{2}-[0-9]{2}$");
        public string Valid_name { get; set; }
        public string Valid_sname { get; set; }
        public string Valid_login { get; set; }
        public string Valid_phone{ get; set; }
        public DateTime Valid_bdate { get; set; }
        public DateTime Valid_rdate { get; set; }

        ////////////////
        public static bool log_ok { get; set; }
        public static bool name_ok { get; set; }
        public static bool sname_ok { get; set; }
        public static bool phone_ok { get; set; }
        public static bool bdate_ok { get; set; }
        public static bool rdate_ok { get; set; }
        ////////
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Valid_login":
                        if (!username_reg.IsMatch(Valid_login))
                        {
                            log_ok = false;
                            error = "Логин может содержать: латинские буквы, цифры, точки и подчёркивания, от 3 до 20 символов,первый символ обязательно буква";
                        }
                        else
                            log_ok = true;
                        break;
                    case "Valid_sname":
                        if (!word_reg.IsMatch(Valid_sname))
                        {
                            sname_ok = false;
                            error = "Фамилия должна содержать: кирилицу, от 2 до 15 символов";
                        }
                        else
                            sname_ok = true;
                        break;
                    case "Valid_name":
                        if (!word_reg.IsMatch(Valid_name))
                        {
                            name_ok = false;
                            error = "Имя должно содержать: кирилицу, от 2 до 15 символов";
                        }
                        else
                            name_ok = true;
                        break;
                    case "Valid_phone":
                        if (!phone_reg.IsMatch(Valid_phone))
                        {
                            phone_ok = false;
                            error = "Неверный номер телефона";
                        }
                        else
                            phone_ok = true;
                        break;
                    case "Valid_bdate":
                        if (Valid_bdate.Date>=DateTime.Now.Date)
                        {
                            bdate_ok = false;
                            error = "Дата рождения не может быть больше либо равной текущей";
                        }
                        else
                            bdate_ok = true;
                        break;
                    case "Valid_rdate":
                        if (Valid_rdate.Date > DateTime.Now.Date|| Valid_rdate.Date <Valid_bdate)
                        {
                            rdate_ok = false;
                            error = "Дата заселения не может быть больше текущей,или меньше чем дата рождения";
                        }
                        else
                            rdate_ok = true;
                        break;

                }
                return error;
            }

        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

    }
}
