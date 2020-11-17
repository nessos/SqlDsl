namespace SqlDsl.Core
{
    public record SqlIntAdd(SqlExpr<SqlInt> Left, SqlExpr<SqlInt> Right) : SqlExprInt, SqlBinExpr<SqlInt>;
    public record SqlIntMult(SqlExpr<SqlInt> Left, SqlExpr<SqlInt> Right) : SqlExprInt, SqlBinExpr<SqlInt>;
    public record SqlIntValue(int Value) : SqlExprInt;

}
