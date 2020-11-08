using System.Runtime.InteropServices.ComTypes;
using Xunit;

namespace SqlDsl.Core.Tests
{
	public class SqlIntTests
	{
		[Fact]
		public void SqlIntAddTest()
		{
			var left = 1;
			var right = 15;

			var leftSql = new SqlIntValue(left);
			var rightSql = new SqlIntValue(right);
			var sqlAdd = new SqlIntAdd(leftSql, rightSql);

			var sql = sqlAdd.CompileExpr();

			Assert.Equal($"({left} + {right})", sql);
		}

		[Fact]
		public void SqlIntMultTest()
		{
			var left = 1;
			var right = 15;

			var leftSql = new SqlIntValue(left);
			var rightSql = new SqlIntValue(right);
			var sqlMult = new SqlIntMult(leftSql, rightSql);

			var sql = sqlMult.CompileExpr();

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
			var sql = sqlPlus.CompileExpr();

			// Assert
			Assert.Equal($"({value})", sql);
		}

		[Theory]
		[InlineData(32,"(ABS(32))")]
		[InlineData(-32, "(ABS(-32))")]
		[InlineData(0, "(ABS(0))")]
		public void SqlIntAbsTest(int testValue,string expected)
		{
			var valueSql = new SqlIntValue(testValue);
			var sqlAbs = new SqlIntAbs(valueSql);

			var sql = sqlAbs.CompileExpr();

			Assert.Equal(expected, sql);
		}
	}
}
