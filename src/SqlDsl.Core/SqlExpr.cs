namespace SqlDsl.Core
{
    public interface SqlExpr { }
    
    public interface SqlExpr<TSqlType> : SqlExpr where TSqlType : SqlType { }

    public interface SqlUnaryExpr<TSqlType> : SqlExpr<TSqlType> where TSqlType : SqlType
    {
	    SqlExpr<TSqlType> Value { get; }
    }

    public interface SqlBinExpr<TSqlType> : SqlExpr<TSqlType> where TSqlType : SqlType
    {
        SqlExpr<TSqlType> Left { get; }
        SqlExpr<TSqlType> Right { get; }
    }

    public record SqlExprInt : SqlExpr<SqlInt>
    {
        public static SqlExprInt operator +(SqlExprInt a, SqlExprInt b) => new SqlIntAdd(a, b);
        public static implicit operator SqlExprInt(int x) => new SqlIntValue(x);
    }
}
