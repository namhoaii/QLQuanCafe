using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using QLQuanCafe.Helpers;

namespace QLQuanCafe.Data
{
    public class Database
    {
        static SQLiteAsyncConnection connection;

        static NguoiDungDatabase nguoiDungDatabase;
        static NhanVienDatabase nhanVienDatabase;

        static BanDatabase banDatabase;
        static SanPhamDatabase sanPhamDatabase;
        static HoaDonDatabase hoaDonDatabase;
        static ChiTietHDDatabase chitietHDDatabase;


        public static SQLiteAsyncConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = new SQLiteAsyncConnection(Globals.PathDB);
                }
                return connection;
            }
        }

        public static NguoiDungDatabase NguoiDungDatabase
        {
            get
            {
                if (nguoiDungDatabase == null)
                {
                    nguoiDungDatabase = new NguoiDungDatabase(Connection);
                }
                return nguoiDungDatabase;
            }
        }
        
        public static NhanVienDatabase NhanVienDatabase
        {
            get
            {
                if (nhanVienDatabase == null)
                {
                    nhanVienDatabase = new NhanVienDatabase(Connection);
                }
                return nhanVienDatabase;
            }
        }
        
        public static BanDatabase BanDatabase
        {
            get
            {
                if (banDatabase == null)
                {
                    banDatabase = new BanDatabase(Connection);
                }
                return banDatabase;
            }
        }

        public static SanPhamDatabase SanPhamDatabase
        {
            get
            {
                if (sanPhamDatabase == null)
                {
                    sanPhamDatabase = new SanPhamDatabase(Connection);
                }
                return sanPhamDatabase;
            }
        }

        public static HoaDonDatabase HoaDonDatabase
        {
            get
            {
                if (hoaDonDatabase == null)
                {
                    hoaDonDatabase = new HoaDonDatabase(Connection);
                }
                return hoaDonDatabase;
            }
        }

        public static ChiTietHDDatabase ChiTietHDDatabase
        {
            get
            {
                if (chitietHDDatabase== null)
                {
                    chitietHDDatabase = new ChiTietHDDatabase(Connection);
                }
                return chitietHDDatabase;
            }
        }




    }
}
