using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;

namespace Travelagency.Model
{
    public static class Sql
    {
        private static string DbName = "Travel.db";

        public static void CreateTable()
        {
            if(!File.Exists(DbName))
                using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.Create))
                { }
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadWrite))
            {
                db.CreateTable<Order>();
                db.CreateTable<Tour>();
            }
        }

        public static int Add(Table table)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadWrite))
            {
                return db.Insert(table);
            }
        }

        public static int Update(Table table)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadWrite))
            {
                return db.Update(table);
            }
        }

        public static int Delete(Table table)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadWrite))
            {
                return db.Delete(table);
            }
        }

        public static List<T> GetTable<T>() where T : Table
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly))
            {
                Type type = typeof(T);

                if (type == typeof(Order))
                    return db.Table<Order>().ToList() as List<T>;
                if (type == typeof(Tour))
                    return db.Table<Tour>().ToList() as List<T>;

                return new List<T>();
            }
        }

        public static bool Contains(Table t)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly))
            {
                Type type = t.GetType();

                if (type == typeof(Order))
                    return db.Table<Order>().ToList().First(f => f.Id == t.Id) != null;
                if (type == typeof(Tour))
                    return db.Table<Tour>().ToList().FirstOrDefault(f => f.Id == t.Id) != null;

                return false;
            }
        }

        public static T GetValue<T>(int? id) where T : Table
        {
            return GetTable<T>().FirstOrDefault(f => f.Id == id) as T;
        }
        //public static T GetValue<T>(string name) where T : Table
        //{
        //    using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly))
        //    {
        //        Type type = typeof(T);

        //        if (type == typeof(Users))
        //            return db.Table<Users>().FirstOrDefault(f => f.Login == name) as T;

        //        return null;
        //    }
        //}



    }
}
