using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PhongMachTu.Common.Security
{
    public static class EncryptionHelper
    {
        private static byte[] ?Key;
        private static byte[] ?IV;

        /// <summary>
        /// Khởi tạo Key và IV từ một chuỗi bí mật.
        /// Phương thức này phải được gọi trước khi sử dụng Encrypt hoặc Decrypt.
        /// </summary>
        /// <param name="secretKey">Chuỗi bí mật để tạo Key và IV</param>
        public static void Initialize(string secretKey)
        {
            using (var sha256 = SHA256.Create())
            {
                // Tạo Key từ secretKey (256-bit)
                Key = sha256.ComputeHash(Encoding.UTF8.GetBytes(secretKey));
                // Tạo IV từ secretKey (lấy 16 byte đầu tiên)
                IV = sha256.ComputeHash(Encoding.UTF8.GetBytes(secretKey)).AsSpan(0, 16).ToArray();
            }
        }

        /// <summary>
        /// Mã hóa một chuỗi văn bản thành chuỗi Base64.
        /// </summary>
        /// <param name="plainText">Văn bản cần mã hóa</param>
        /// <returns>Chuỗi Base64 mã hóa</returns>
        public static string Encrypt(string plainText)
        {
            if (Key == null || IV == null)
            {
                Initialize("uit");
            }

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                using (var memoryStream = new MemoryStream())
                using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                    cryptoStream.FlushFinalBlock();

                    // Trả về chuỗi Base64
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        /// <summary>
        /// Giải mã một chuỗi Base64 thành văn bản gốc.
        /// </summary>
        /// <param name="cipherText">Chuỗi Base64 mã hóa</param>
        /// <returns>Chuỗi văn bản gốc</returns>
        public static string Decrypt(string cipherText)
        {
            if (Key == null || IV == null)
            {
                Initialize("uit");
            }   

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                using (var memoryStream = new MemoryStream())
                using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    byte[] cipherBytes = Convert.FromBase64String(cipherText);
                    cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                    cryptoStream.FlushFinalBlock();

                    // Trả về chuỗi gốc
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
        }
    }
}
