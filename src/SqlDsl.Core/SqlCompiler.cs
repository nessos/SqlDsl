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

				// Expressions - Boolean
				SqlBoolNot(var value) => $"NOT ({CompileExpr(value)})",
				SqlBoolAnd(var left, var right) => $"({CompileExpr(left)} AND {CompileExpr(right)})",
				SqlBoolOr(var left, var right) => $"({CompileExpr(left)} OR {CompileExpr(right)})",

				// Expressions - Numeric
				SqlIntAdd(var left, var right) => $"({CompileExpr(left)} + {CompileExpr(right)})",
				SqlIntSub(var left, var right) => $"({CompileExpr(left)} - {CompileExpr(right)})",
				SqlIntMult(var left, var right) => $"({CompileExpr(left)} * {CompileExpr(right)})",
				SqlIntPlus(var value) => $"({CompileExpr(value)})",
				SqlIntMinus(var value) => $"(-({CompileExpr(value)}))",
				SqlIntGreater(var left, var right) => $"({CompileExpr(left)} > {CompileExpr(right)})",
				SqlIntGreaterOrEqual(var left, var right) => $"({CompileExpr(left)} >= {CompileExpr(right)})",
				SqlIntLesser(var left, var right) => $"({CompileExpr(left)} < {CompileExpr(right)})",
				SqlIntLesserOrEqual(var left, var right) => $"({CompileExpr(left)} <= {CompileExpr(right)})",

				SqlIntAbs(var value) => $"(ABS({CompileExpr(value)}))",
          
				_ => throw new Exception($"Not supported {expr}")
			};
	}
}
