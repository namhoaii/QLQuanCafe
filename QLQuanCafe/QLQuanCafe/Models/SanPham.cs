using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLQuanCafe.Models
{
    public class SanPham
    {
        [PrimaryKey, AutoIncrement]
        public int IDSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int DonGia { get; set; }

    }
}
