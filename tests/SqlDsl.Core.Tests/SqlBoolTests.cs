using Xunit;

namespace SqlDsl.Core.Tests
{
	public class SqlBoolTests
	{
		public static SqlExpr<SqlBool> SqlTrue = new SqlBoolValue(true);
		public static SqlExpr<SqlBool> SqlFalse = new SqlBoolValue(false);

		[Fact]
		public void SqlBoolValue()
		{
			var sql = SqlCompiler.EmitExpr(SqlTrue);
			Assert.Equal("TRUE", sql);

			sql = SqlFalse.CompileExpr();
			Assert.Equal("FALSE", sql);
		}

		[Fact]
		public void SqlBoolAnd()
		{
			var and = new SqlBoolAnd(SqlTrue, SqlFalse);
			var sql = SqlCompiler.EmitExpr(and);

			Assert.Equal("(TRUE AND FALSE)", sql);
		}

		[Fact]
		public void SqlBoolOr()
		{
			var or = new SqlBoolOr(SqlTrue, SqlFalse);
			var sql = SqlCompiler.EmitExpr(or);

			Assert.Equal("(TRUE OR FALSE)", sql);
		}

		[Fact]
		public void SqlNotTest()
		{
			var notSql = new SqlBoolNot(SqlFalse);
			var sql = SqlCompiler.EmitExpr(notSql);

			Assert.Equal("NOT (FALSE)", sql);
		}
	}
}
