using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_api.Models
{
    public class AppMessage
    {
        public static string NotFoundData()
          => $"Không tìm thấy dữ liệu.";
        public static string Error()
           => $"Đã có lỗi hệ thống xảy ra. Vui lòng kiểm tra lại.";
        public static string Success(string note)
           => $"{note} thành công.";
    }
}
