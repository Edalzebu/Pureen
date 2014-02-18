using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Pureen.Web.Utils
{
    public class Md5Encryption
    {
        public Md5Encryption()
        {
            //Am not sure if i need to do a constructor here, but meh ill just do it lol    
        }

        public static string Encriptar(string value) //Funcion para encriptar cadenas de datos con MD5
        {
            var x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(value);
            data = x.ComputeHash(data);
            string ret = "";

            for (int i = 0; i < data.Length; i++)
                ret += data[i].ToString("x2").ToLower();
            return ret;
        }

        public static string CreateRandomPassword(int passwordLength) //Funcion para generar un password automaticamente
        {
            const string allowedChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ0123456789";
            var randomBytes = new Byte[passwordLength];
            var chars = new char[passwordLength];
            int allowedCharCount = allowedChars.Length;

            for (int i = 0; i < passwordLength; i++)
            {
                var randomObj = new Random();
                randomObj.NextBytes(randomBytes);
                chars[i] = allowedChars[randomBytes[i] % allowedCharCount];
            }

            return new string(chars);
        }
    }
}