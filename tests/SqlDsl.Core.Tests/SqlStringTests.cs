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
	}
}
