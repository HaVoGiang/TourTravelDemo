using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace BinhBaDemo.CodeHelpers
{
    public class Common
    {
        public static string ReadFileText(string path)
        {
            FileStream fs = new FileStream( path, FileMode.Open);
            StreamReader rd = new StreamReader(fs, Encoding.UTF8);
            String giatri = rd.ReadToEnd();// ReadLine() chỉ đọc 1 dòng đầu thoy, ReadToEnd là đọc hết
            rd.Close();
            return giatri;
        }

        public static string StringStandard(string strSource)
        {
            strSource = strSource.Trim(); // xử lý khoảng trắng ở đầu và cuối
            int len = strSource.Length;
            int i = 0;
            while (i < len)
            {
                if (strSource[i] == ' ' && strSource[i] == strSource[i + 1])
                {
                    strSource = strSource.Remove(i, 1);
                    len = strSource.Length;
                }
                else
                {
                    i++;
                }
            }
            //return strSource.ToLower();
            return strSource;
        }

        public static string NameStandard(string str) //Loại bỏ những ký tự đặc biệt và những khoảng trắng giữa chuỗi.
        {
            string KyTuDacBiet = "~!#$^*=|\\\"\',.:;`/"; //Dấu &,@,? thường gặp trong tên quán.
            for (byte i = 0; i < KyTuDacBiet.Length; i++)
            {
                str = str.Replace(KyTuDacBiet.Substring(i, 1), " ");
            }
            return StringStandard(str.ToLower());
        }

        
        public static string TiegVietKhongDauUrl(string str)
        {
            str = TiegVietKhongDau(str);
            string KyTuDacBiet = "~!?#$&@%^*()[]{}<>+-_=|\\\"\',./:;`";//Dấu &,@,? thường gặp trong tên quán cũng bị remove
            for (byte i = 0; i < KyTuDacBiet.Length; i++)
            {
                str = str.Replace(KyTuDacBiet.Substring(i, 1), "");
            }
            str = StringStandard(str);
            str = str.Replace(" ", "-");
            return str.ToLower();
        }

        public static string TiegVietKhongDau(string str)
        {
            string[,] arr = new string[14, 18]; //Tạo mảng có 14 hàng và 18 cột, mỗi hàng chứa các ký tự cùng nhóm
            const string chuoi = "aAeEoOuUiIdDyY";
            const string thga = "áàạảãâấầậẩẫăắằặẳẵ";
            const string hoaA = "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ";
            const string thge = "éèẹẻẽêếềệểễeeeeee";
            const string hoaE = "ÉÈẸẺẼÊẾỀỆỂỄEEEEEE";
            const string thgo = "óòọỏõôốồộổỗơớờợởỡ";
            const string hoaO = "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ";
            const string thgu = "úùụủũưứừựửữuuuuuu";
            const string hoaU = "ÚÙỤỦŨƯỨỪỰỬỮUUUUUU";
            const string thgi = "íìịỉĩiiiiiiiiiiii";
            const string hoaI = "ÍÌỊỈĨIIIIIIIIIIII";
            const string thgd = "đdddddddddddddddd";
            const string hoaD = "ĐDDDDDDDDDDDDDDDD";
            const string thgy = "ýỳỵỷỹyyyyyyyyyyyy";
            const string hoaY = "ÝỲỴỶỸYYYYYYYYYYYY";

            //Nạp vào trong Mảng các ký tự
            //Nạp vào từng đầu hàng các ký tự không dấu
            //Nạp vào cột đầu tiên
            for (byte i = 0; i < 14; i++)
                arr[i, 0] = chuoi.Substring(i, 1);

            //Nạp vào từng ô các ký tự có dấu
            for (byte j = 1; j < 18; j++)
                for (byte i = 1; i < 18; i++)
                {
                    arr[0, i] = thga.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thga vào từng ô trong hàng 0
                    arr[1, i] = hoaA.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaA vào từng ô trong  hàng 1
                    arr[2, i] = thge.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thge vào từng ô trong  hàng 2
                    arr[3, i] = hoaE.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaE vào từng ô trong  hàng 3
                    arr[4, i] = thgo.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thgo vào từng ô trong  hàng 4
                    arr[5, i] = hoaO.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaO vào từng ô trong  hàng 5
                    arr[6, i] = thgu.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thgu vào từng ô trong  hàng 6
                    arr[7, i] = hoaU.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaU vào từng ô trong  hàng 7
                    arr[8, i] = thgi.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thgi vào từng ô trong  hàng 8
                    arr[9, i] = hoaI.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaI vào từng ô trong  hàng 9
                    arr[10, i] = thgd.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thgd vào từng ô trong  hàng 10
                    arr[11, i] = hoaD.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaD vào từng ô trong  hàng 11
                    arr[12, i] = thgy.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi Thgy vào từng ô trong  hàng 12
                    arr[13, i] = hoaY.Substring(i - 1, 1); //Nạp từng ký tự trong chuỗi HoaY vào từng ô trong  hàng 13
                }

            //Tiến hành thay thế
            for (byte j = 0; j < 14; j++)
                for (byte i = 1; i < 18; i++)
                    str = str.Replace(arr[j, i], arr[j, 0]);

            return str;
        }

        public static string DateAgo(DateTime theDate)
        {
            const int second = 1;
            const int minute = 60 * second;
            const int hour = 60 * minute;
            const int day = 24 * hour;
            const int month = 30 * day;

            var ts = new TimeSpan(DateTime.Now.Ticks - theDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * minute)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * minute)
                return "a minute ago";

            if (delta < 45 * minute)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * minute)
                return "an hour ago";

            if (delta < 24 * hour)
                return ts.Hours + " hours ago";

            if (delta < 48 * hour)
                return "yesterday";

            if (delta < 30 * day)
                return ts.Days + " days ago";

            if (delta < 12 * month)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }


        public static string GetKey()
        {
            Random random = new Random();
            return DateTime.Now.Year +
                   DateTime.Now.Month.ToString() +
                   DateTime.Now.Day +
                   DateTime.Now.Hour +
                   DateTime.Now.Minute +
                   DateTime.Now.Second +
                   DateTime.Now.Millisecond +
                   random.Next(0, 1000);
        }


        public static string UppercaseWords(string value)
        {
            char[] array = value.ToLower().ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

        

        public static string UpLoadImg(HttpPostedFileBase fileUpload, int length, string title)
        {
            if (fileUpload.ContentLength >= length) //500 000 = 500 000 byte = 500kb
            {
                string result = "length";
                return result;
            }
            var originalDirectory = new DirectoryInfo($"{System.Web.HttpContext.Current.Server.MapPath(@"\")}Images\\uploads");

            string pathString = Path.Combine(originalDirectory.ToString());
            //var fileName1 = Path.GetFileName(file.FileName);
            var newpath = System.Web.HttpContext.Current.Server.MapPath("~/Images/uploads");
            bool isExists = Directory.Exists(newpath);

            if (!isExists)
                Directory.CreateDirectory(newpath);
            var date = DateTime.Now.ToString("MMddyyyyhhmmssfff");
            var fileName1 = Path.GetFileName(fileUpload.FileName);
            if (fileName1 != null)
            {
                var filename = date + "-" + Common.TiegVietKhongDauUrl(title) +
                               fileName1.Substring(fileName1.LastIndexOf('.'));

                var path = $"{pathString}\\{filename}";
                if (System.IO.File.Exists(path))
                {
                    path = $"{pathString}\\1-{filename}";
                }
                fileUpload.SaveAs(path);
                return filename;
            }

            return null;
        }



        public static string GetUserBrowser()
        {
            string result = "Unknow";
            if (HttpContext.Current.Request.Browser.Browser != null)
                result = HttpContext.Current.Request.Browser.Browser;
            return result;
        }

        public static string GetUserDevice()
        {
            var device = HttpContext.Current.Request.Browser.IsMobileDevice ? "mobile" : "Desktop";
            return device;
        }

        public static string GetUserIp()
        {
            string result = "Unknow";
            if (HttpContext.Current.Request.UserHostAddress != null)
                result = HttpContext.Current.Request.UserHostAddress;
            return result;
        }

        public static string GetUserOs()
        {
            string os = "Unknow";
            var ua = HttpContext.Current.Request.UserAgent;
            if (ua != null)
            {
                if (ua.Contains("Android"))
                    os = "Android";

                if (ua.Contains("iPad"))
                    os = "iPad";

                if (ua.Contains("iPhone"))
                    os = "iphone";

                if (ua.Contains("Linux") && ua.Contains("KFAPWI"))
                    os = "Kindle Fire";

                if (ua.Contains("RIM Tablet") || (ua.Contains("BB") && ua.Contains("Mobile")))
                    os = "Black Berry";

                if (ua.Contains("Windows Phone"))
                    os = "Windows Phone";

                if (ua.Contains("Mac OS"))
                    os = "Mac OS";

                if (ua.Contains("Windows NT 5.1") || ua.Contains("Windows NT 5.2"))
                    os = "Windows XP";

                if (ua.Contains("Windows NT 6.0"))
                    os = "Windows Vista";

                if (ua.Contains("Windows NT 6.1"))
                    os = "Windows 7";

                if (ua.Contains("Windows NT 6.2"))
                    os = "Windows 8";

                if (ua.Contains("Windows NT 6.3"))
                    os = "Windows 8.1";

                if (ua.Contains("Windows NT 10"))
                    os = "Windows 10";
            }
            return os;
        }
    }
}