using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.Helpers
{
    public static class ParseHelpers
    {
        public static List<string> ParseTaiKhoan(string taiKhoan)
        {
            try
            {
                // Kiểm tra nếu là JSON mảng
                if (taiKhoan.StartsWith("[") && taiKhoan.EndsWith("]"))
                {
                    return JsonConvert.DeserializeObject<List<string>>(taiKhoan);
                }
                else
                {
                    // Nếu không phải JSON, chỉ chứa 1 tài khoản
                    return new List<string> { taiKhoan };
                }
            }
            catch (Exception)
            {
                // Nếu lỗi, trả về danh sách rỗng
                return new List<string>();
            }
        }
    }
}
