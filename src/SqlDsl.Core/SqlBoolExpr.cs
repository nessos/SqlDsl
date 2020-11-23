namespace SqlDsl.Core
{
	public record SqlBoolValue(bool Value) : SqlExprBool;

	public record SqlBoolNot(SqlExpr<SqlBool> Value) : SqlExprBool, SqlUnaryExpr<SqlBool>;

	public record SqlBoolAnd(SqlExpr<SqlBool> Left, SqlExpr<SqlBool> Right) : SqlExprBool, SqlBinExpr<SqlBool>;

	public record SqlBoolOr(SqlExpr<SqlBool> Left, SqlExpr<SqlBool> Right) : SqlExprBool, SqlBinExpr<SqlBool>;
}
