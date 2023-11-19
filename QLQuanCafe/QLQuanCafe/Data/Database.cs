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

    }
}
