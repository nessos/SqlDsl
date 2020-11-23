using System;

namespace SqlDsl.Core
{
	public static class SqlCompiler
	{

		private static SqlExpr OptimizeExpr(SqlExpr expr) =>
			expr switch
			{
				SqlIntAdd(SqlIntValue(var left), SqlIntValue(var right)) => new SqlIntValue(left + right),
				SqlIntAdd(var left, SqlIntValue(0)) => OptimizeExpr(left),
				SqlIntAdd(SqlIntValue(0), var right) => OptimizeExpr(right),
				SqlIntAdd(var left, var right) => new SqlIntAdd(OptimizeExpr(left) as SqlExprInt, OptimizeExpr(right) as SqlExprInt),
				_ => expr
			};

		public static SqlExpr MultiOptimizer(SqlExpr expr)
		{
			var optExpr = OptimizeExpr(expr);
			if (expr == optExpr)
				return optExpr;
			else return MultiOptimizer(optExpr);
		}

		private static string EmitExpr(SqlExpr expr) =>
			expr switch
			{
				// Values
				SqlIntValue(var value) => value.ToString(),
				SqlBoolValue(var value) => value.ToString().ToUpper(),

				// Expressions - Boolean
				SqlBoolNot(var value) => $"NOT ({EmitExpr(value)})",
				SqlBoolAnd(var left, var right) => $"({EmitExpr(left)} AND {EmitExpr(right)})",
				SqlBoolOr(var left, var right) => $"({EmitExpr(left)} OR {EmitExpr(right)})",

				SqlStringValue(var value) => $"'{value}'",
				SqlStringToUpper(var value) => $"{EmitExpr(value).ToUpper()}",
				SqlStringToLower(var value) => $"{EmitExpr(value).ToLower()}" ,
          
				// Expressions - Numeric
				SqlIntAdd(var left, var right) => $"({CompileExpr(left)} + {CompileExpr(right)})",
				SqlIntSub(var left, var right) => $"({CompileExpr(left)} - {CompileExpr(right)})",
				SqlIntMult(var left, var right) => $"({CompileExpr(left)} * {CompileExpr(right)})",
				SqlIntPlus(var value) => $"({CompileExpr(value)})",
				SqlIntMinus(var value) => $"(-({CompileExpr(value)}))",
				SqlIntAbs(var value) => $"(ABS({CompileExpr(value)}))",
          
				_ => throw new Exception($"Not supported {expr}")
			};
		public static string CompileExpr(this SqlExpr expr) => EmitExpr(MultiOptimizer(expr));
	}
}
