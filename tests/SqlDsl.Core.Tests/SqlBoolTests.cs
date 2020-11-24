using System.Runtime.InteropServices.ComTypes;
using Xunit;

namespace SqlDsl.Core.Tests
{
	public class SqlBoolTests
	{
		private const string SqlTrue = "TRUE";

		private const string SqlFalse = "FALSE";

		[Theory]
		[InlineData(true, SqlTrue)]
		[InlineData(false, SqlFalse)]
		public void SqlBoolValsImplicitTests(bool input, string expected)
		{
			SqlExprBool sqlValue = input;
			
			Assert.Equal(expected, sqlValue.CompileExpr());
		}

		[Theory]
		[InlineData(true, SqlTrue)]
		[InlineData(false, SqlFalse)]
		public void SqlBoolValue(bool input, string expected)
		{
			SqlExprBool sql = input;
			Assert.Equal(expected, sql.CompileExpr());
		}

		[Theory]
		[InlineData(false, false, "(FALSE AND FALSE)")]
		[InlineData(false, true, "(FALSE AND TRUE)")]
		[InlineData(true, false, "(TRUE AND FALSE)")]
		[InlineData(true, true, "(TRUE AND TRUE)")]
		public void SqlBoolAndTest(bool left, bool right, string expected)
		{
			SqlExprBool sqlLeft = left;
			SqlExprBool sqlRight = right;
			SqlBoolAnd sqlAnd = new(sqlLeft, sqlRight);

			Assert.Equal(expected, sqlAnd.CompileExpr());
		}

		[Theory]
		[InlineData(false, false, SqlFalse)]
		[InlineData(false, true, SqlFalse)]
		[InlineData(true, false, SqlFalse)]
		[InlineData(true, true, SqlTrue)]
		public void SqlBoolAndOptimizedTest(bool left, bool right, string expected)
		{
			SqlExprBool sqlLeft = left;
			SqlExprBool sqlRight = right;
			SqlBoolAnd sqlAnd = new(sqlLeft, sqlRight);

			Assert.Equal(expected, sqlAnd.CompileOptimizedExpr());
		}

		[Theory]
		[InlineData(false, false, "(FALSE OR FALSE)")]
		[InlineData(false, true, "(FALSE OR TRUE)")]
		[InlineData(true, false, "(TRUE OR FALSE)")]
		[InlineData(true, true, "(TRUE OR TRUE)")]
		public void SqlBoolOrTest(bool left, bool right, string expected)
		{
			SqlExprBool sqlLeft = left;
			SqlExprBool sqlRight = right;
			SqlBoolOr sqlOr = new(sqlLeft, sqlRight);

			Assert.Equal(expected, sqlOr.CompileExpr());
		}

		[Theory]
		[InlineData(false, false, SqlFalse)]
		[InlineData(true, false, SqlTrue)]
		[InlineData(false, true, SqlTrue)]
		[InlineData(true, true, SqlTrue)]
		public void SqlBoolOrOptimizedTest(bool left, bool right, string expected)
		{
			SqlExprBool sqlLeft = left;
			SqlExprBool sqlRight = right;
			SqlBoolOr sqlOr = new(sqlLeft, sqlRight);

			Assert.Equal(expected, sqlOr.CompileOptimizedExpr());
		}

		[Theory]
		[InlineData(true, SqlTrue)]
		[InlineData(false, SqlFalse)]
		public void SqlNotTest(bool input, string expected)
		{
			SqlExprBool value = input;
			var not = !value;

			Assert.Equal($"NOT ({expected})", not.CompileExpr());
		}

		[Theory]
		[InlineData(true, SqlFalse)]
		[InlineData(false, SqlTrue)]
		public void SqlNotOptimizedTest(bool input, string expected)
		{
			SqlExprBool value = input;
			var not = !value;

			Assert.Equal(expected, not.CompileOptimizedExpr());
		}

		[Fact]
		public void SqlImplicitAndTest()
		{
			SqlExprBool left = true;
			SqlExprBool right = false;
			var and = left && right;

			Assert.Equal("(TRUE AND FALSE)", SqlCompiler.EmitExpr(and));
		}

		[Fact]
		public void SqlImplicitOrTest()
		{
			SqlExprBool left = true;
			SqlExprBool right = false;
			var or = left || right;

			Assert.Equal("(TRUE OR FALSE)", SqlCompiler.EmitExpr(or));
		}

		[Fact]
		public void SqlImplicitNotTest()
		{
			SqlExprBool left = true;
			SqlExprBool right = false;

			var notLeft = SqlCompiler.EmitExpr(!left);
			
			Assert.Equal("NOT (TRUE)", notLeft);
			Assert.Equal("NOT (FALSE)", SqlCompiler.EmitExpr(!right));
		}
	}
}
