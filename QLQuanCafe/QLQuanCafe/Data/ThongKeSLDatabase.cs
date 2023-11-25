using QLQuanCafe.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCafe.Data
{
    public class ThongKeSLDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ThongKeSLDatabase(SQLiteAsyncConnection connection)
        {
            this.database = connection;
        }

        public Task<List<ThongKeSL>> GetSLSP()
        {
            var query = @"SELECT sp.IDSanPham, sp.tensanpham, sum(soluong) soluong,sum(thanhtien) thanhtien
                        FROM SanPham sp
                        LEFT JOIN ChiTietHD ct
                        on sp.IDSanPham = ct.IDSanPham
                        GROUP BY  sp.IDSanPham, sp.tensanpham";

            return database.QueryAsync<ThongKeSL>(query);
        }
        
        public Task<List<ThongKeSL>> GetSP()
        {
            var query = @"SELECT * FROM SanPham";

            return database.QueryAsync<ThongKeSL>(query);
        }

    }
}
