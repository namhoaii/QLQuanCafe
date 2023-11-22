using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLQuanCafe.Models
{
    public class NhanVien
    {
        [PrimaryKey]
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public string GioiTinh { get; set; }
        public int IDNguoiDung { get; set; }
    }
}
