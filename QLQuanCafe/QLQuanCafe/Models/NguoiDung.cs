using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLQuanCafe.Models
{
    public class NguoiDung
    {
        [PrimaryKey, AutoIncrement]
        public int IDNguoiDung { get; set; }
        public string TenNguoiDung { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public int Quyen { get; set; }
    }
}
