namespace SqlDsl.Core
{
	public interface SqlExpr
	{
	}

	public interface SqlExpr<TSqlType> : SqlExpr where TSqlType : SqlType
	{
	}

	public interface SqlUnaryExpr<TSqlType> : SqlExpr<TSqlType> where TSqlType : SqlType
	{
		SqlExpr<TSqlType> Value { get; }
	}

	public interface SqlBinExpr<TSqlType> : SqlExpr<TSqlType> where TSqlType : SqlType
	{
		SqlExpr<TSqlType> Left { get; }
		SqlExpr<TSqlType> Right { get; }
	}

	public abstract record SqlExprBool : SqlExpr<SqlBool>
	{
		public static implicit operator SqlExprBool(bool value) => new SqlBoolValue(value);
		public static SqlExprBool operator &(SqlExprBool left, SqlExprBool right) => new SqlBoolAnd(left, right);
		public static SqlExprBool operator |(SqlExprBool left, SqlExprBool right) => new SqlBoolOr(left, right);
		public static SqlExprBool operator !(SqlExprBool value) => new SqlBoolNot(value);
		public static bool operator false(SqlExprBool _) => false;
		public static bool operator true(SqlExprBool _) => false;
	}

	public abstract record SqlExprInt : SqlExpr<SqlInt>
	{
		public static implicit operator SqlExprInt(int x) => new SqlIntValue(x);
		public static SqlExprInt operator +(SqlExprInt left, SqlExprInt right) => new SqlIntAdd(left, right);
		public static SqlExprInt operator -(SqlExprInt left, SqlExprInt right) => new SqlIntSub(left, right);
		public static SqlExprInt operator *(SqlExprInt left, SqlExprInt right) => new SqlIntMult(left, right);
		public static SqlExprInt operator /(SqlExprInt left, SqlExprInt right) => new SqlIntDiv(left, right);
	}
}
