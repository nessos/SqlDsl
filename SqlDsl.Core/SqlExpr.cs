using System;
using System.Collections.Generic;
using System.Text;

namespace SqlDsl.Core
{
    public interface SqlExpr<TSqlType> where TSqlType : SqlType { }
    public interface SqlBinExpr<TSqlType> : SqlExpr<TSqlType> where TSqlType : SqlType
    {
        SqlExpr<TSqlType> Left { get; }
        SqlExpr<TSqlType> Right { get; }
    }

    public class SqlIntAdd : SqlBinExpr<SqlInt>
    {
        public SqlExpr<SqlInt> Left { get; }
        public SqlExpr<SqlInt> Right { get; }
        public SqlIntAdd(SqlExpr<SqlInt> left, SqlExpr<SqlInt> right)
        { }
    }

    public class SqlIntValue : SqlExpr<SqlInt>
    {
        public int Value { get; }
        public SqlIntValue(int v)
        { }
    }

    public class SqlStringConcat : SqlBinExpr<SqlString>
    {
        public SqlExpr<SqlString> Left { get; }
        public SqlExpr<SqlString> Right { get; }
        public SqlStringConcat(SqlExpr<SqlString> left, SqlExpr<SqlString> right)
        { }
    }

}
