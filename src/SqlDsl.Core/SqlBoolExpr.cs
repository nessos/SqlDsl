namespace SqlDsl.Core
{

	public class SqlBoolValue : SqlExpr<SqlBool>
	{
		public bool Value { get; private set; }
		public SqlBoolValue(bool value)
		{
			Value = value;
		}

		public void Deconstruct(out bool value)
		{
			value = Value;
		}
	}

	public class SqlBoolNot : SqlUnaryExpr<SqlBool>
	{
		public SqlExpr<SqlBool> Value { get; }

		public SqlBoolNot(SqlExpr<SqlBool> value)
		{
			Value = value;
		}

		public void Deconstruct(out SqlExpr<SqlBool> value)
		{
			value = Value;
		}
	}

	public class SqlBoolAnd : SqlBinExpr<SqlBool>
	{
		public SqlExpr<SqlBool> Left { get; }
		public SqlExpr<SqlBool> Right { get; }

		public SqlBoolAnd(SqlExpr<SqlBool> left, SqlExpr<SqlBool> right)
		{
			Left = left;
			Right = right;
		}

		public void Deconstruct(out SqlExpr<SqlBool> left, out SqlExpr<SqlBool> right)
		{
			left = Left;
			right = Right;
		}
	}

	public class SqlBoolOr : SqlBinExpr<SqlBool>
	{
		public SqlExpr<SqlBool> Left { get; }
		public SqlExpr<SqlBool> Right { get; }

		public SqlBoolOr(SqlExpr<SqlBool> left, SqlExpr<SqlBool> right)
		{
			Left = left;
			Right = right;
		}

		public void Deconstruct(out SqlExpr<SqlBool> left, out SqlExpr<SqlBool> right)
		{
			left = Left;
			right = Right;
		}
	}

}
