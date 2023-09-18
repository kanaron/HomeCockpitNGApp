﻿using Dapper;
using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace DataAccess
{
    public class SQLiteDataAccess
    {
        protected static readonly string databaseName = "CockpitDB.db";

        public static void Update(string sql)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(sql);
            }
        }

        public static void Insert(string sql)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(sql);
            }
        }

        public static string LoadFirstValue(string sql)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var query = cnn.QueryFirst<string>(sql).ToString();
                if (query != null)
                {
                    return query;
                }
                return "";
            }
        }

        /// <summary>
        /// Takes connection string for database
        /// </summary>
        /// <param name="id">
        /// which connection string get
        /// </param>
        /// <returns>
        /// connection string
        /// </returns>
        public static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString
                .Replace("%databasePath%", databaseName);
        }
    }
}
