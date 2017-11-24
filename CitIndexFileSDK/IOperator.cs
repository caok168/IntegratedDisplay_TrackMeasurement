using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CitIndexFileSDK
{
    public interface IOperator
    {
        string IndexFilePath { get; set; }
        void CreateDB(string filePath);
        DataTable Query(string queryString);
        object ExecuteScalar(string cmdText);
        bool ExcuteSql(string cmdText);
        void CheckAndCreateTable(string tableName, string cmdText);
    }
}
