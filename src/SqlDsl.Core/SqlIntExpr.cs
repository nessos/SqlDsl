namespace SqlDsl.Core
{
	public record SqlIntValue(int Value) : SqlExprInt;

	public record SqlIntPlus(SqlExpr<SqlInt> Value) : SqlExprInt, SqlUnaryExpr<SqlInt>;

	public record SqlIntMinus(SqlExpr<SqlInt> Value) : SqlExprInt, SqlUnaryExpr<SqlInt>;

	public record SqlIntAbs(SqlExpr<SqlInt> Value) : SqlExprInt, SqlUnaryExpr<SqlInt>;

	public record SqlIntAdd(SqlExpr<SqlInt> Left, SqlExpr<SqlInt> Right) : SqlExprInt, SqlBinExpr<SqlInt>;

	public record SqlIntSub(SqlExpr<SqlInt> Left, SqlExpr<SqlInt> Right) : SqlExprInt, SqlBinExpr<SqlInt>;

	public record SqlIntMult(SqlExpr<SqlInt> Left, SqlExpr<SqlInt> Right) : SqlExprInt, SqlBinExpr<SqlInt>;

	public record SqlIntDiv(SqlExpr<SqlInt> Left, SqlExpr<SqlInt> Right) : SqlExprInt, SqlBinExpr<SqlInt>;


	public record SqlIntGreaterThan(SqlExpr<SqlInt> Left, SqlExpr<SqlInt> Right) : SqlExprBool, SqlBinExpr<SqlInt>;

	public record SqlIntLessThan(SqlExpr<SqlInt> Left, SqlExpr<SqlInt> Right) : SqlExprBool, SqlBinExpr<SqlInt>;



	public record SqlIntGreaterThanOrEqualTo(SqlExpr<SqlInt> Left, SqlExpr<SqlInt> Right) : SqlExprInt,
		SqlBinExpr<SqlInt>;

	public record SqlIntLessThanOrEqualTo(SqlExpr<SqlInt> Left, SqlExpr<SqlInt> Right) : SqlExprInt, SqlBinExpr<SqlInt>;
}
