using DbConnection.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConnection
{
    public class DbConnect
    {
        private CookbookDBDataContext _db;
        private static DbConnect _instance;

        private DbConnect()
        {
            this._db = new CookbookDBDataContext("Data Source=DESKTOP-841L133;Initial Catalog=ASPDatabase;Integrated Security=True"); ;
        }

        public static DbConnect GetDatabase()
        {
            if (_instance == null)
                _instance = new DbConnect();
            return _instance;
        }
    }
}
