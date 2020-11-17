namespace SqlDsl.Core
{
    public record SqlIntAdd(SqlExpr<SqlInt> Left, SqlExpr<SqlInt> Right) : SqlExprInt, SqlBinExpr<SqlInt>;
    public record SqlIntSub(SqlExpr<SqlInt> Left, SqlExpr<SqlInt> Right):SqlExprInt, SqlBinExpr<SqlInt>;
    public record SqlIntMult(SqlExpr<SqlInt> Left, SqlExpr<SqlInt> Right) : SqlExprInt, SqlBinExpr<SqlInt>;
    public record SqlIntPlus(SqlExpr<SqlInt> Value) : SqlExprInt,SqlUnaryExpr<SqlInt>;
    public record SqlIntMinus(SqlExpr<SqlInt> Value) : SqlExprInt,SqlUnaryExpr<SqlInt>;
    public record SqlIntValue(int Value) : SqlExprInt;
    public record SqlIntAbs(SqlExpr<SqlInt> Value);
}
