using QLQuanCafe.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCafe.Data
{
    public class NguoiDungDatabase
    {
        readonly SQLiteAsyncConnection database;

        public NguoiDungDatabase(SQLiteAsyncConnection connection)
        {
            this.database = connection;
            database.CreateTableAsync<NguoiDung>().Wait();
        }

        public Task<List<NguoiDung>> GetNguoiDungAsync()
        {
            //Get all 
            return database.Table<NguoiDung>().ToListAsync();
        }

        public Task<NguoiDung> GetNguoiDungAsync(string tenNguoiDung)
        {
            // Get a specific
            return database.Table<NguoiDung>()
                            .Where(i => i.TenNguoiDung == tenNguoiDung)
                            .FirstOrDefaultAsync();
        }
        public Task<NguoiDung> GetNguoiDungEmailAsync(string email)
        {
            // Get a specific 
            return database.Table<NguoiDung>()
                            .Where(i => i.Email == email)
                            .FirstOrDefaultAsync();
        }
        
        public Task<NguoiDung> GetNguoiDungSDTAsync(string sdt)
        {
            // Get a specific 
            return database.Table<NguoiDung>()
                            .Where(i => i.SDT == sdt)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNguoiDungAsync(NguoiDung nguoiDung)
        {
            if (nguoiDung.IDNguoiDung != 0)
            {
                // Update an existing 
                return database.UpdateAsync(nguoiDung);
            }
            else
            {
                // Save a new 
                return database.InsertAsync(nguoiDung);
            }
        }

        public Task<int> DeleteNguoiDungAsync(NguoiDung nguoiDung)
        {
            // Delete 
            return database.DeleteAsync(nguoiDung);
        }


    }
}
