using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Models;
namespace SimpltyTravelBLL
{
   public class Validation
    {
        //id
        public static bool LegalId(int id)
        {
            int x;
            string s = id.ToString();
            if (!int.TryParse(s, out x))
                return false;
            if (s.Length < 5 || s.Length > 9)
                return false;
            for (int i = s.Length; i < 9; i++)
                s = "0" + s;
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                int k = ((i % 2) + 1) * (Convert.ToInt32(s[i]) - '0');
                if (k > 9)
                    k -= 9;
                sum += k;

            }
            return sum % 10 == 0;
        }
        //letters only
        public static bool IsHebrew(string word)
        {
            string pattern = @"\b[א-ת-\s ]+$";
            Regex reg = new Regex(pattern);
            return reg.IsMatch(word);

        }
        //telephon
        public static bool IsTelephone(string tel)
        {
            string pattern = @"\b0[ 2 4 7 8 3 77]-[2-9]\d{6}$";
            Regex reg = new Regex(pattern);
            return reg.IsMatch(tel);
        }
        //password
        public static bool IsPassword(string pass,int id)
        {
           DBConnection db = new DBConnection();
            if (pass.Length < 6)
                return false;
            if (pass.Contains(id.ToString()))
                return false;
            if (db.GetDbSet<Customers>().First(c => c.passwordCustomer == pass) != null)
                return false;
            return VerifyLettersAndNumbers(pass);
        }
        //cellphone
        public static bool IsCellPhone(string tel)
        {
            string pattern = @"\b05[0 2 4 6 7 8 3 5]-[2-9]\d{6}$";
            Regex reg = new Regex(pattern);
            return reg.IsMatch(tel);
        }
        //logic age by birht date
        public static int GetAge(DateTime d)
        {
            DateTime t = DateTime.Today;
            int age = t.Year - d.Year;
            if (t < d.AddYears(age)) age--;
            return age;
        }
        //mail
        public static bool CheackMail(string t)
        {
            if (!t.Contains("@gmail."))
                return false;
            Regex regex1 = new Regex("@gmail");
            string[] strSplit = regex1.Split(t);
            Regex regex2 = new Regex(".com|.co.il");
            bool check1 = regex2.IsMatch(strSplit[1]);
            Regex regex3 = new Regex("^[a-zA-Z0-9._]*$");
            bool check2 = regex3.IsMatch(strSplit[0]);
            if (check2 && check1)
                return true;
            return false;
        }
        //numbers only
        public static bool IsNumber(string num)
        {
            string pattern = @"\b[0-9-\s]+$";
            Regex reg = new Regex(pattern);
            return reg.IsMatch(num);
        }
        //not numbers
        public static bool VerifyNotNumbers(string name)
        {
            Regex regex3 = new Regex("^[^0-9]*$");
            return regex3.IsMatch(name);
        }
        //contains letters and numbers
        public static bool VerifyLettersAndNumbers(string add)
        {
            Regex regex3 = new Regex("[0-9]");
            Regex regex4 = new Regex("[a-zA-Zא-ת]");
            return regex3.IsMatch(add) && regex4.IsMatch(add);
        }
        //contains just letters and numbers
        public static bool VerifyNotSpeciall(string add)
        {
            Regex regex3 = new Regex("^[0-9 a-zA-Zא-ת]*$");
            return regex3.IsMatch(add);
        }

    }
}
