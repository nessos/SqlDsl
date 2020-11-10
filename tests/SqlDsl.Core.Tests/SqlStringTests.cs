using Xunit;

namespace SqlDsl.Core.Tests
{
	public class SqlStringTests
	{
		[Fact]
		public void SqlStringConcatTest()
		{
			var left = "Hello ";
			var right = "world!";

			var sqlLeft = new SqlStringValue(left);
			var sqlRight = new SqlStringValue(right);
			var sqlConcat = new SqlStringConcat(sqlLeft, sqlRight);
			var sql = sqlConcat.CompileExpr();

			Assert.Equal(sql, $"'{left}' + '{right}'");
		}

		[Fact]
		public void SqlStringToUpperTest()
		{
			var value = "TestAbCd";

			var sqlStr = new SqlStringValue(value);
			var sqlToUpper = new SqlStringToUpper(sqlStr);
			var sql = sqlToUpper.CompileExpr();

			Assert.Equal($"UPPER('{value}')", sql);
		}

		[Fact]
		public void SqlStringToLowerTest()
		{
			var value = "TestAbCd";

			var sqlStr = new SqlStringValue(value);
			var sqlToLower = new SqlStringToLower(sqlStr);
			var sql = sqlToLower.CompileExpr();

			Assert.Equal($"LOWER('{value}')", sql);
		}
	}
}
