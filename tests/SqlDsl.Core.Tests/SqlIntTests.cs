using System.Runtime.InteropServices;
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
		public void SqlIntValueTests(int input, string expected)
		{
			var value = new SqlIntValue(input);
			SqlExprInt implicitValue = input;

			var sql = value.CompileExpr();

			Assert.Equal(expected, sql);
			Assert.Equal(value, implicitValue);
		}

		[Theory]
		[InlineData(1, 5, "(1 + 5)")]
		[InlineData(0, 2, "(0 + 2)")]
		[InlineData(-2, 5, "(-2 + 5)")]
		[InlineData(3, -5, "(3 + -5)")]
		public void SqlIntAddTests(int left, int right, string expected)
		{
			SqlExprInt leftSql = left;
			SqlExprInt rightSql = right;

			var add = leftSql + rightSql;
			var sql = add.CompileExpr();

			Assert.Equal(expected, sql);
		}

		[Theory]
		[InlineData(5, 2, "7")]
		[InlineData(5, 0, "5")]
		[InlineData(-5, 2, "-3")]
		[InlineData(0, 5, "5")]
		public void SqlIntAddOptimizedTests(int left, int right, string expected)
		{
			SqlExprInt leftSql = left;
			SqlExprInt rightSql = right;

			var add = leftSql + rightSql;

			Assert.Equal(expected, add.CompileOptimizedExpr());
		}

		[Theory]
		[InlineData(1, 2, "(1 - 2)")]
		[InlineData(1, -2, "(1 - -2)")]
		[InlineData(-1, 2, "(-1 - 2)")]
		[InlineData(-1, -2, "(-1 - -2)")]
		public void SqlIntSubTests(int left, int right, string expected)
		{
			SqlExprInt leftSql = left;
			SqlExprInt rightSql = right;

			var sqlSub = leftSql - rightSql;
			var sql = sqlSub.CompileExpr();

			Assert.Equal(expected, sql);
		}

		[Theory]
		[InlineData(5, 2, "3")]
		[InlineData(5, 0, "5")]
		[InlineData(-5, 2, "-7")]
		[InlineData(0, 5, "-5")]
		public void SqlIntSubOptimizedTests(int left, int right, string expected)
		{
			SqlExprInt leftSql = left;
			SqlExprInt rightSql = right;

			var sqlSub = leftSql - rightSql;

			Assert.Equal(expected, sqlSub.CompileOptimizedExpr());
		}

		[Theory]
		[InlineData(1, 2, "(1 * 2)")]
		[InlineData(0, 3, "(0 * 3)")]
		[InlineData(5, 2, "(5 * 2)")]
		[InlineData(-1, 2, "(-1 * 2)")]
		[InlineData(1, -2, "(1 * -2)")]
		public void SqlIntMultTests(int left, int right, string expected)
		{
			SqlExprInt leftSql = left;
			SqlExprInt rightSql = right;

			var sqlMult = leftSql * rightSql;
			var sql = sqlMult.CompileExpr();

			Assert.Equal(expected, sql);
		}

		[Theory]
		[InlineData(1, 2, "2")]
		[InlineData(0, 3, "0")]
		[InlineData(5, 2, "10")]
		[InlineData(-1, 2, "-2")]
		[InlineData(1, -2, "-2")]
		public void SqlIntMultOptimizedTests(int left, int right, string expected)
		{
			SqlExprInt leftSql = left;
			SqlExprInt rightSql = right;

			var sqlMult = leftSql * rightSql;

			Assert.Equal(expected, sqlMult.CompileOptimizedExpr());
		}

		[Theory]
		[InlineData(1, 2, "(1 / 2)")]
		[InlineData(0, 3, "(0 / 3)")]
		[InlineData(5, 2, "(5 / 2)")]
		[InlineData(-1, 2, "(-1 / 2)")]
		[InlineData(1, -2, "(1 / -2)")]
		public void SqlIntDivTests(int left, int right, string expected)
		{
			SqlExprInt leftSql = left;
			SqlExprInt rightSql = right;

			var sqlMult = leftSql / rightSql;
			var sql = sqlMult.CompileExpr();

			Assert.Equal(expected, sql);
		}

		[Theory]
		[InlineData(1, 2, "0")]
		[InlineData(0, 3, "0")]
		[InlineData(10, 2, "5")]
		[InlineData(-8, 1, "-8")]
		//[InlineData(2, 0, "(2 / 0)")]
		public void SqlIntDivOptimizedTests(int left, int right, string expected)
		{
			SqlExprInt leftSql = left;
			SqlExprInt rightSql = right;

			var sqlMult = leftSql / rightSql;

			Assert.Equal(expected, sqlMult.CompileOptimizedExpr());
		}

		[Theory]
		[InlineData(-32, "(-32)")]
		[InlineData(32, "(32)")]
		[InlineData(0, "(0)")]
		public void SqlIntPlusTests(int value, string output)
		{
			SqlExprInt valueSql = value;

			var sqlPlus = new SqlIntPlus(valueSql);

			var sql = sqlPlus.CompileExpr();

			Assert.Equal(output, sql);
		}

		[Theory]
		[InlineData(-32, "-32")]
		[InlineData(32, "32")]
		[InlineData(0, "0")]
		public void SqlIntPlusOptimizedTests(int value, string output)
		{
			SqlExprInt valueSql = value;

			var sqlPlus = new SqlIntPlus(valueSql);

			var sql = sqlPlus.CompileOptimizedExpr();

			Assert.Equal(output, sql);
		}

		[Theory]
		[InlineData(-32, "(-(-32))")]
		[InlineData(32, "(-(32))")]
		[InlineData(0, "(-(0))")]
		public void SqlIntMinusTests(int value, string output)
		{
			SqlExprInt valueSql = value;

			var sqlMinus = new SqlIntMinus(valueSql);

			var sql = sqlMinus.CompileExpr();

			Assert.Equal(output, sql);
		}

		[Theory]
		[InlineData(-32, "32")]
		[InlineData(32, "-32")]
		[InlineData(0, "0")]
		public void SqlIntMinusOptimizedTests(int value, string output)
		{
			SqlExprInt valueSql = value;

			var sqlMinus = new SqlIntMinus(valueSql);

			var sql = sqlMinus.CompileOptimizedExpr();

			Assert.Equal(output, sql);
		}

		[Theory]
		[InlineData(32, "(ABS(32))")]
		[InlineData(-32, "(ABS(-32))")]
		[InlineData(0, "(ABS(0))")]
		public void SqlIntAbsTests(int testValue, string expected)
		{
			var valueSql = new SqlIntValue(testValue);
			var sqlAbs = new SqlIntAbs(valueSql);

			var sql = sqlAbs.CompileExpr();

			Assert.Equal(expected, sql);
		}

		[Theory]
		[InlineData(32, "32")]
		[InlineData(-32, "32")]
		[InlineData(0, "0")]
		public void SqlIntAbsOptimizedTests(int value, string expected)
		{
			SqlExprInt valueSql = value;
			var sqlAbs = new SqlIntAbs(valueSql);

			var sql = sqlAbs.CompileOptimizedExpr();

			Assert.Equal(expected, sql);
		}

		[Theory]
		[InlineData(3, 5, "(3 > 5)")]
		[InlineData(10,6,"(10 > 6)")]
		public void SqlIntGreaterThanTests(int left, int right, string expected)
		{
			SqlExprInt leftSql = left;
			SqlExprInt rightSql = right;

			var sqlGreaterThan = leftSql > rightSql;
			var sql = sqlGreaterThan.CompileExpr();

			Assert.Equal(expected, sql);
		}

		[Theory]
		[InlineData(3, 5, "FALSE")]
		[InlineData(10, 6, "TRUE")]
		[InlineData(10, 10, "FALSE")]
		public void SqlIntGreaterThanOptimizedTests(int left, int right, string expected)
		{
			SqlExprInt leftSql = left;
			SqlExprInt rightSql = right;

			var sqlGreaterThan = leftSql > rightSql;

			Assert.Equal(expected, sqlGreaterThan.CompileOptimizedExpr());
		}

		[Theory]
		[InlineData(3, 5, "(3 >= 5)")]
		[InlineData(10, 6, "(10 >= 6)")]
		public void SqlIntGreaterThanOrEqualToTests(int left, int right, string expected)
		{
			SqlExprInt leftSql = left;
			SqlExprInt rightSql = right;

			var sqlGreaterThanOrEqual = leftSql >= rightSql;
			var sql = sqlGreaterThanOrEqual.CompileExpr();

			Assert.Equal(expected, sql);
		}

		[Theory]
		[InlineData(3, 5, "FALSE")]
		[InlineData(10, 6, "TRUE")]
		[InlineData(10, 10, "TRUE")]
		public void SqlIntGreaterThanOrEqualToOptimizedTests(int left, int right, string expected)
		{
			SqlExprInt leftSql = left;
			SqlExprInt rightSql = right;

			var sqlGreaterThanOrEqual = leftSql >= rightSql;

			Assert.Equal(expected, sqlGreaterThanOrEqual.CompileOptimizedExpr());
		}

		[Theory]
		[InlineData(3, 5, "(3 < 5)")]
		[InlineData(10, 6, "(10 < 6)")]
		public void SqlIntLessThanTests(int left, int right, string expected)
		{
			SqlExprInt leftSql = left;
			SqlExprInt rightSql = right;

			var sqlLessThan = leftSql < rightSql;
			var sql = sqlLessThan.CompileExpr();

			Assert.Equal(expected, sql);
		}

		[Theory]
		[InlineData(3, 5, "TRUE")]
		[InlineData(10, 6, "FALSE")]
		[InlineData(10, 10, "FALSE")]
		public void SqlIntLessThanOptimizedTests(int left, int right, string expected)
		{
			SqlExprInt leftSql = left;
			SqlExprInt rightSql = right;

			var sqlLessThan = leftSql < rightSql;

			Assert.Equal(expected, sqlLessThan.CompileOptimizedExpr());
		}

		[Theory]
		[InlineData(3, 5, "(3 <= 5)")]
		[InlineData(10, 6, "(10 <= 6)")]
		public void SqlIntLessThanOrEqualToTests(int left, int right, string expected)
		{
			SqlExprInt leftSql = left;
			SqlExprInt rightSql = right;

			var sqlLessThanOrEqual = leftSql <= rightSql;
			var sql = sqlLessThanOrEqual.CompileExpr();

			Assert.Equal(expected, sql);
		}

		[Theory]
		[InlineData(3, 5, "TRUE")]
		[InlineData(10, 6, "FALSE")]
		[InlineData(10, 10, "TRUE")]
		public void SqlIntLessThanOrEqualToOptimizedTests(int left, int right, string expected)
		{
			SqlExprInt leftSql = left;
			SqlExprInt rightSql = right;

			var sqlLessThanOrEqual = leftSql <= rightSql;

			Assert.Equal(expected, sqlLessThanOrEqual.CompileOptimizedExpr());
		}
	}
}
