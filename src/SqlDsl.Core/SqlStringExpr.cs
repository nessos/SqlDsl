namespace SqlDsl.Core
{
	public record SqlStringValue(string Value)
		: SqlExprString;

	public record SqlStringConcat(SqlExpr<SqlString> Left, SqlExpr<SqlString> Right)
		: SqlExprString, SqlBinExpr<SqlString>;

	public record SqlStringToUpper(SqlExpr<SqlString> Value)
		: SqlExprString, SqlUnaryExpr<SqlString>;

	public record SqlStringToLower(SqlExpr<SqlString> Value)
		: SqlExprString, SqlUnaryExpr<SqlString>;
}
