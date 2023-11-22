using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLQuanCafe.Models
{
    public class ChiTietHD
    {
        [PrimaryKey, AutoIncrement]
        public int IDChiTietHD { get; set; }
        public int IDSanPham { get; set; }
        public int IDHoaDon { get; set; }
        public int SoLuong { get; set; }
        public int ThanhTien { get; set; }
    }
}
