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

			Assert.Equal(expected, sqlValue.CompileExpr());
		}

		[Theory]
		[InlineData("testing", "LOWER('testing')")]
		[InlineData("a whole new world", "LOWER('a whole new world')")]
		public void SqlStringToLowerTests(string value, string expected)
		{
			SqlExprString sqlString = value;

			SqlStringToLower sqlToLower = new(sqlString);

			Assert.Equal(expected, sqlToLower.CompileExpr());
		}

		[Theory]
		[InlineData("TestING", "'testing'")]
		[InlineData("A wHoLE new WoRLD", "'a whole new world'")]
		public void SqlStringToLowerOptimizedTests(string value, string expected)
		{
			SqlExprString sqlString = value;

			SqlStringToLower sqlToLower = new(sqlString);

			Assert.Equal(expected, sqlToLower.CompileOptimizedExpr());
		}

		[Theory]
		[InlineData("testing", "UPPER('testing')")]
		[InlineData("a whole new world", "UPPER('a whole new world')")]
		public void SqlStringToUpperTests(string value, string expected)
		{
			SqlExprString sqlString = value;

			SqlStringToUpper sqlToUpper = new(sqlString);

			Assert.Equal(expected, sqlToUpper.CompileExpr());
		}

		[Theory]
		[InlineData("TestING", "'TESTING'")]
		[InlineData("A wHoLE new WoRLD", "'A WHOLE NEW WORLD'")]
		public void SqlStringToUpperOptimizedTests(string value, string expected)
		{
			SqlExprString sqlString = value;

			SqlStringToUpper sqlToUpper = new(sqlString);

			Assert.Equal(expected, sqlToUpper.CompileOptimizedExpr());
		}

		[Theory]
		[InlineData("hello", "world", "CONCAT('hello', 'world')")]
		[InlineData("let's go","party", "CONCAT('let's go', 'party')")]
		public void SqlStringConcatTests(string left, string right, string expected)
		{
			SqlExprString leftSql = left;
			SqlExprString rightSql = right;
			SqlStringConcat sqlConcat = new(leftSql, rightSql);

			Assert.Equal(expected, sqlConcat.CompileExpr());
		}

		[Theory]
		[InlineData("hello", " world", "'hello world'")]
		[InlineData("let's go", " party", "'let's go party'")]
		public void SqlStringConcatOptimizedTests(string left, string right, string expected)
		{
			SqlExprString leftSql = left;
			SqlExprString rightSql = right;
			SqlStringConcat sqlConcat = new(leftSql, rightSql);

			Assert.Equal(expected, sqlConcat.CompileOptimizedExpr());
		}
	}
}
