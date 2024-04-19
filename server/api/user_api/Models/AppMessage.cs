using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_api.Models
{
    public static class AppMessage
    {
        public static class Save
        {
            public static string Success => "Lưu dữ liệu thành công";
            public static string Error => "Lưu dữ liệu thất bại!";
        }
        public static class Delete
        {
            public static string Success => "Xóa dữ liệu thành công";
            public static string Error => "Xóa dữ liệu thất bại!";
        }
    }
}
