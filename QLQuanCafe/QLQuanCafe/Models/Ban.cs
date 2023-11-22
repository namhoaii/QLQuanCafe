using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLQuanCafe.Models
{
    public class Ban
    {
        [PrimaryKey, AutoIncrement]
        public int IDBan { get; set; }
        public int TinhTrang { get; set; }
    }
}
