using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCongTrinh.Models
{
    public class TT
    {
        public List<CongTrinh> congTrinhs { get; set; }
        public List<NhanVien> nhanViens { get; set; }
        public List<TaiKhoan> taiKhoans { get; set; }


    }
}