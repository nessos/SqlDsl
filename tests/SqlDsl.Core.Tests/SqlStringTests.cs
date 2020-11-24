using Xunit;

namespace SqlDsl.Core.Tests
{
	public class SqlStringTests
	{
		[Theory]
		[InlineData("testing", "'testing'")]
		[InlineData("a whole new world", "'a whole new world'")]
		public void SqlStringValueTests(string value, string expected)
		{
			SqlExprString sqlValue = value;

			Assert.Equal(expected, SqlCompiler.EmitExpr(sqlValue));
		}

		[Theory]
		[InlineData("testing", "LOWER('testing')")]
		[InlineData("a whole new world", "LOWER('a whole new world')")]
		public void SqlStringToLowerTest(string value, string expected)
		{
			SqlExprString sqlString = value;

			SqlStringToLower sqlToLower = new(sqlString);

			Assert.Equal(expected, SqlCompiler.EmitExpr(sqlToLower));
		}

		[Theory]
		[InlineData("testing", "UPPER('testing')")]
		[InlineData("a whole new world", "UPPER('a whole new world')")]
		public void SqlStringToUpperTest(string value, string expected)
		{
			SqlExprString sqlString = value;

			SqlStringToUpper sqlToUpper = new(sqlString);

			Assert.Equal(expected, SqlCompiler.EmitExpr(sqlToUpper));
		}

		[Theory]
		[InlineData("hello", "world", "CONCAT('hello', 'world')")]
		[InlineData("let's go","party", "CONCAT('let's go', 'party')")]
		public void SqlStringConcatTest(string left, string right, string expected)
		{
			SqlExprString leftSql = left;
			SqlExprString rightSql = right;
			SqlStringConcat sqlConcat = new(leftSql, rightSql);

			Assert.Equal(expected, SqlCompiler.EmitExpr(sqlConcat));
		}
	}
}
