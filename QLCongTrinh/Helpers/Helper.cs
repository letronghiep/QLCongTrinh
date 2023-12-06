using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace QLCongTrinh.Helpers
{
    public class Helper
    {
        public static string GetMD5(string password)
        {
            MD5 mD5 = new MD5CryptoServiceProvider();
            byte[] formData = Encoding.UTF8.GetBytes(password);
            byte[] targetData = mD5.ComputeHash(formData);
            string byteToString = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byteToString += targetData[i].ToString("x2");
            }
            return byteToString;
        }
    }
}