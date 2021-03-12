using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace BookExchanger.Auth.Service
{
    public class PasswordService
    {
        private static string _encryptionPassword = "!9*(0$1^FrEd";

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string Encryption(string password, string salt)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            if (string.IsNullOrEmpty(salt))
                throw new ArgumentNullException(nameof(salt));

            try
            {
                var key = System.Text.Encoding.UTF8.GetBytes(_encryptionPassword.Substring(0, 8));
                var iv = key;
                var pwd = System.Text.Encoding.UTF8.GetBytes($"{password}_{salt}");

                MemoryStream ms = new MemoryStream();
                DESCryptoServiceProvider desCP = new DESCryptoServiceProvider();
                CryptoStream cryptoStream = new CryptoStream(ms, desCP.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                ms.Write(pwd, 0, pwd.Length);
                ms.Flush();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch
            {
                return password;
            }
        }

        /// <summary>
        /// 校验密码
        /// </summary>
        /// <param name="inputPwd">输入值</param>
        /// <param name="originalPwd">对比值</param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static bool Confirm(string inputPwd, string originalPwd, string salt)
        {
            var pwd = Encryption(inputPwd, salt);
            if (pwd.Equals(originalPwd))
                return true;

            return false;
        }

        /// <summary>
        /// 生成Salt
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetSalt(int length = 8)
        {
            length = length < 8 ? 8 : length;

            var originalString = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890-=!@#$%^&*()_+";

            var charArr = originalString.ToCharArray();

            Random random = new Random();

            List<string> saltArr = new List<string>();

            for (int i = 0; i < length - 1; i++)
            {
                saltArr.Add(charArr[random.Next(0, charArr.Length - 1)].ToString());
            }

            return string.Join("", saltArr);
        }
    }
}