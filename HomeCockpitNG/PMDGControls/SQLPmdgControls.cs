using Dapper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace HomeCockpitNG
{
    public class SQLPmdgControls : SQLiteDataAccess
    {
        public static List<PMDG_Control> LoadControls()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PMDG_Control>("select * from PMDG_Controls", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<PMDG_Control> LoadControlsFromModule(MODULES module)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PMDG_Control>($"select * from PMDG_Controls where Module = '{module}'", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void InsertControls(PMDG_Control control)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into PMDG_Controls (Name) values (@Name)", control);
            }
        }
    }
}
