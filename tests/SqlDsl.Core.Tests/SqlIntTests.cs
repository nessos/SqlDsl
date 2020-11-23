using System.Runtime.InteropServices.ComTypes;
using Xunit;

namespace SqlDsl.Core.Tests
{
	public class SqlIntTests
	{
		[Theory]
		[InlineData(-32, "-32")]
		[InlineData(32, "32")]
		[InlineData(0, "0")]
		public void SqlIntValueTest(int input, string expected)
		{
			var value = new SqlIntValue(input);

			var sql = SqlCompiler.EmitExpr(value);

			Assert.Equal(expected, sql);
		}

		[Fact]
		public void SqlIntAddTest()
		{
			var left = 1;
			var right = 15;

			var leftSql = new SqlIntValue(left);
			var rightSql = new SqlIntValue(right);
			var sqlAdd = new SqlIntAdd(leftSql, rightSql);

			var sql = SqlCompiler.EmitExpr(sqlAdd);

			Assert.Equal($"({left} + {right})", sql);
		}

		[Theory]
		[InlineData(1, 2, "(1 - 2)")]
		[InlineData(1, -2, "(1 - -2)")]
		[InlineData(-1, 2, "(-1 - 2)")]
		[InlineData(-1, -2, "(-1 - -2)")]
		public void SqlIntSubTest(int left, int right, string expected)
		{
			var leftSql = new SqlIntValue(left);
			var rightSql = new SqlIntValue(right);
			var sqlSub = new SqlIntSub(leftSql, rightSql);

			var sql = SqlCompiler.EmitExpr(sqlSub);

			Assert.Equal(expected, sql);
		}

		[Fact]
		public void SqlIntMultTest()
		{
			var left = 1;
			var right = 15;

			var leftSql = new SqlIntValue(left);
			var rightSql = new SqlIntValue(right);
			var sqlMult = new SqlIntMult(leftSql, rightSql);

			var sql = SqlCompiler.EmitExpr(sqlMult);

			Assert.Equal($"({left} * {right})", sql);
		}

		[Fact]
		public void SqlIntPlusPositiveTest()
		{
			// Arrange
			var value = -32;
			var valueSql = new SqlIntValue(value);

			var sqlPlus = new SqlIntPlus(valueSql);

			// Act
			var sql = SqlCompiler.EmitExpr(sqlPlus);

			// Assert
			Assert.Equal($"({value})", sql);
		}

		[Fact]
		public void SqlIntMinusTest()
		{
			var value = -32;
			var valueSql = new SqlIntValue(value);

			var sqlMinus = new SqlIntMinus(valueSql);

			var sql = SqlCompiler.EmitExpr(sqlMinus);

			Assert.Equal($"(-({value}))", sql);
		}

		[Theory]
		[InlineData(32, "(ABS(32))")]
		[InlineData(-32, "(ABS(-32))")]
		[InlineData(0, "(ABS(0))")]
		public void SqlIntAbsTest(int testValue, string expected)
		{
			var valueSql = new SqlIntValue(testValue);
			var sqlAbs = new SqlIntAbs(valueSql);

			var sql = SqlCompiler.EmitExpr(sqlAbs);

			Assert.Equal(expected, sql);
		}
	}
}
