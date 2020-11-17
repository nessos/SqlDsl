using Xunit;

namespace SqlDsl.Core.Tests
{
	public class SqlStringTests
	{
		public static SqlExpr<SqlString> SqlStr = new SqlStringValue("testAbCd");


		[Fact]
		public void SqlStringToLowerTest()
		{

			var str = "testAbCd";

			var sqlStr = new SqlStringValue(str);
			var sqlToLower = new SqlStringToLower(sqlStr);
			var sql = sqlToLower.CompileExpr();

			Assert.Equal($"'{"testAbCd".ToLower()}'", sql);
		}

		[Fact]
		public void SqlStringToUpperTest()
		{
			var sqlStr = new SqlStringValue("testAbCd");
			var sqlToUpper = new SqlStringToUpper(sqlStr);
			var sql = sqlToUpper.CompileExpr();

			Assert.Equal($"'{"testAbCd".ToUpper()}'", sql);
		}
	}
}
