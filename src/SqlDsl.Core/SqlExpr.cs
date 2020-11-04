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
}
