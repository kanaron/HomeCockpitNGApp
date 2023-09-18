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

        public static void UpdateOption(PMDG_Data option)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                if (cnn.Execute("update Options set OptionValue = @OptionValue where OptionName = @OptionName", option) <= 0)
                {
                    cnn.Execute("insert into Options (OptionName, OptionValue) values (@OptionName, @OptionValue)", option);
                }
            }
        }

        public static string LoadOptionValue(string optionName)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var query = cnn.QueryFirst<string>(String.Format("select OptionValue from Options where OptionName = '{0}'", optionName));
                    return query;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
