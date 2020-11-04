using SqlDsl.Core;
using Xunit;

namespace SqlNumericTests
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
	}
}
