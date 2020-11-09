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

		[Theory]
		[InlineData(1,2,"(1 - 2)")]
		[InlineData(1, -2, "(1 - -2)")]
		[InlineData(-1, 2, "(-1 - 2)")]
		[InlineData(-1, -2, "(-1 - -2)")]
		public void SqlIntSubTest(int left,int right,string expected)
        {
			var leftSql = new SqlIntValue(left);
			var rightSql = new SqlIntValue(right);
			var sqlSub = new SqlIntSub(leftSql, rightSql);

			var sql = sqlSub.CompileExpr();

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
	}
}
