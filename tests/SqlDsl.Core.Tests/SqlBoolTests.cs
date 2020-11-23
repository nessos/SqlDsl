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
		public void SqlBoolAndTest()
		{
			var and = new SqlBoolAnd(SqlTrue, SqlFalse);
			var sql = SqlCompiler.EmitExpr(and);

			Assert.Equal("(TRUE AND FALSE)", sql);

			and = new SqlBoolAnd(SqlFalse, SqlTrue);
			sql = SqlCompiler.EmitExpr(and);
			Assert.Equal("(FALSE AND TRUE)", sql);
		}

		[Fact]
		public void SqlBoolAndOptimizedTest()
		{
			SqlBoolAnd trueAndFalse = new(SqlTrue, SqlFalse);
			SqlBoolAnd falseAndTrue = new(SqlFalse, SqlTrue);
			SqlBoolAnd falseAndFalse = new(SqlFalse, SqlFalse);
			SqlBoolAnd trueAndTrue = new(SqlTrue, SqlTrue);

			Assert.Equal("FALSE",trueAndFalse.CompileExpr());
			Assert.Equal("FALSE", falseAndTrue.CompileExpr());
			Assert.Equal("FALSE", falseAndFalse.CompileExpr());
			Assert.Equal("TRUE", trueAndTrue.CompileExpr());
		}

		[Fact]
		public void SqlBoolOrTest()
		{
			var or = new SqlBoolOr(SqlTrue, SqlFalse);
			var sql = SqlCompiler.EmitExpr(or);

			Assert.Equal("(TRUE OR FALSE)", sql);
		}

		[Fact]
		public void SqlBoolOrOptimizedTest()
		{
			SqlBoolOr trueOrFalse = new(SqlTrue, SqlFalse);
			SqlBoolOr falseOrTrue = new(SqlFalse, SqlTrue);
			SqlBoolOr falseOrFalse = new(SqlFalse, SqlFalse);
			SqlBoolOr trueOrTrue = new(SqlTrue, SqlTrue);

			Assert.Equal("TRUE", trueOrTrue.CompileExpr());
			Assert.Equal("TRUE", falseOrTrue.CompileExpr());
			Assert.Equal("TRUE", trueOrFalse.CompileExpr());
			Assert.Equal("FALSE", falseOrFalse.CompileExpr());
		}

		[Fact]
		public void SqlNotTest()
		{
			var notSql = new SqlBoolNot(SqlFalse);
			var sql = SqlCompiler.EmitExpr(notSql);

			Assert.Equal("NOT (FALSE)", sql);
		}

		[Fact]
		public void SqlNotOptimizedTest()
		{
			SqlBoolNot notTrue = new(SqlTrue);
			SqlBoolNot notFalse = new(SqlFalse);

			Assert.Equal("TRUE", notFalse.CompileExpr());
			Assert.Equal("FALSE", notTrue.CompileExpr());
		}

		[Fact]
		public void SqlBoolValsImplicitTests()
		{
			SqlExprBool sqlTrue = true;
			SqlExprBool sqlFalse = false;

			Assert.Equal("TRUE", sqlTrue.CompileExpr());
			Assert.Equal("FALSE", sqlFalse.CompileExpr());
		}

		[Fact]
		public void SqlImplicitAndTest()
		{
			SqlExprBool left = true;
			SqlExprBool right = false;
			var and = left & right;

			Assert.Equal("(TRUE AND FALSE)", SqlCompiler.EmitExpr(and));
		}

		[Fact]
		public void SqlImplicitOrTest()
		{
			SqlExprBool left = true;
			SqlExprBool right = false;
			var or = left | right;

			Assert.Equal("(TRUE OR FALSE)", SqlCompiler.EmitExpr(or));
		}

		[Fact]
		public void SqlImplicitNotTest()
		{
			SqlExprBool left = true;
			SqlExprBool right = false;

			var notLeft = SqlCompiler.EmitExpr(!left);
			var notRight = !right;

			Assert.Equal("NOT (TRUE)", notLeft);
			Assert.Equal("NOT (FALSE)", SqlCompiler.EmitExpr(!right));
		}
	}
}
