using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static List<IDataConnection> Connections { get; private set; } = new List<IDataConnection>();

        public static void InitalizeConnections(bool database, bool textFiles)
        {
            if (database)
            {
                SQLConnector sql = new SQLConnector();
                Connections.Add(sql);
                // connect to db
            }
            if (textFiles)
            {
                TextConnector text = new TextConnector();
                Connections.Add(text);
                // connect to txt
            }
        }
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
