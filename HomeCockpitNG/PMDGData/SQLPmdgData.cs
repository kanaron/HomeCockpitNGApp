using Dapper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace HomeCockpitNG
{
    public class SQLPmdgData : SQLiteDataAccess
    {
        public static List<PMDG_Data> LoadData()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PMDG_Data>("select * from PMDG_Variables", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<PMDG_Data> LoadDataFromModule(MODULES module)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PMDG_Data>($"select * from PMDG_Variables where Module = '{module}'", new DynamicParameters());
                return output.ToList();
            }
        }
    }
}
