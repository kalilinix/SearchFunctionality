using System.Data;
using System.Data.SqlClient;
using Dapper.FastCrud;

namespace SearchFunction.DataLayer
{
    public class DataService : IDataRepository
    {
        private IDbConnection _transactionConnection;
        private IDbTransaction _transaction;
        private string connString;



        public IDataRepository Clone()
        {
            //return new DataService(_currentUser);
            return new DataService(connString);
        }
        internal IDbConnection connection
        {
            get
            {
               
                return new SqlConnection(connString);
            }
        }
        public bool InTransaction
        {
            get
            {
                return !(_transaction == null);
            }
        }


        public DataService(string conString)
        {
            connString = conString;
            _transaction = null;
        }
        public IList<T> QueryEntity<T>(string filter, string sort)
        {
            if (filter == null || filter.Trim() == "")
                throw new InvalidOperationException("A filter must be specified when QueryEntity(string, string) is used");

            FormattableString sqlWhere = $"{filter}";
            FormattableString sqlSort = $"";

            if (sort != null && sort.Trim() != "")
                sqlSort = $"{sort}";

            IList<T> entities = null;
            if (!InTransaction)
                using (IDbConnection conn = connection)
                {
                    entities = conn.Find<T>(statement => statement.Where(sqlWhere).OrderBy(sqlSort)).ToList();
                }
            else
                entities = _transactionConnection.Find<T>(statement => { statement.Where(sqlWhere).OrderBy(sqlSort); statement.AttachToTransaction(_transaction); }).ToList();
            return entities;
        }
    }
}
