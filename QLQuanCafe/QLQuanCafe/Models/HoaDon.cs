using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLQuanCafe.Models
{
    public class HoaDon
    {
        [PrimaryKey, AutoIncrement]
        public int IDHoaDon { get; set; }
        public string NgayLap { get; set; }
        public int IDBan { get; set; }
        public int MaNV { get; set; }
    }
}
