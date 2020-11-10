using System;

namespace SqlDsl.Core
{
	public static class SqlCompiler
	{
		public static string CompileExpr(this SqlExpr expr) =>
			expr switch
			{
				// Values
				SqlIntValue(var value) => value.ToString(),
				SqlBoolValue(var value) => value.ToString().ToUpper(),
				SqlStringValue(var value) => $"'{value}'",

				// Expressions - Boolean
				SqlBoolNot(var value) => $"NOT ({CompileExpr(value)})",
				SqlBoolAnd(var left, var right) => $"({CompileExpr(left)} AND {CompileExpr(right)})",
				SqlBoolOr(var left, var right) => $"({CompileExpr(left)} OR {CompileExpr(right)})",

				// Expressions - Numeric
				SqlIntAdd(var left, var right) => $"({CompileExpr(left)} + {CompileExpr(right)})",
				SqlIntMult(var left, var right) => $"({CompileExpr(left)} * {CompileExpr(right)})",

				// Expressions - String
				SqlStringConcat(var left, var right) => $"{CompileExpr(left)} + {CompileExpr(right)}",
				SqlStringToUpper(var value) => $"(UPPER({CompileExpr(value)}))",
				SqlStringToLower(var value) => $"(LOWER({CompileExpr(value)}))",

				_ => throw new Exception($"Not supported {expr}")
			};
	}
}
