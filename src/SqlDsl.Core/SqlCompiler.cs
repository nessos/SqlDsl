using System;

namespace SqlDsl.Core
{
	public static class SqlCompiler
	{
		private static SqlExpr OptimizeExpr(SqlExpr expr) =>
			expr switch
			{
				// Bool Optimizations
				SqlBoolAnd(SqlBoolValue(false), _) => new SqlBoolValue(false),
				SqlBoolAnd(_, SqlBoolValue(false)) => new SqlBoolValue(false),
				SqlBoolAnd(SqlBoolValue(true), SqlBoolValue(true)) => new SqlBoolValue(true),

				SqlBoolOr(SqlBoolValue(true), _) => new SqlBoolValue(true),
				SqlBoolOr(_, SqlBoolValue(true)) => new SqlBoolValue(true),
				SqlBoolOr(SqlBoolValue(false), SqlBoolValue(false)) => new SqlBoolValue(false),

				SqlBoolNot(SqlBoolValue(true)) => new SqlBoolValue(false),
				SqlBoolNot(SqlBoolValue(false)) => new SqlBoolValue(true),

				// Int Optimizations
				SqlIntPlus(SqlIntValue(var value)) => new SqlIntValue(value),
				SqlIntMinus(SqlIntValue(var value)) => new SqlIntValue(-value),
				SqlIntAbs(SqlIntValue(var value)) => new SqlIntValue(value > 0 ? value : -value),

				SqlIntAdd(var left, SqlIntValue(0)) => OptimizeExpr(left),
				SqlIntAdd(SqlIntValue(0), var right) => OptimizeExpr(right),
				SqlIntAdd(SqlIntValue(var left), SqlIntValue(var right)) => new SqlIntValue(left + right),
				SqlIntAdd(var left, var right) => new SqlIntAdd(OptimizeExpr(left) as SqlExprInt,
					OptimizeExpr(right) as SqlExprInt),

				SqlIntSub(var left, SqlIntValue(0)) => OptimizeExpr(left),
				SqlIntSub(SqlIntValue(0), var right) => OptimizeExpr(new SqlIntMinus(right)),
				SqlIntSub(SqlIntValue(var left), SqlIntValue(var right)) => new SqlIntValue(left - right),
				SqlIntSub(var left, var right) => new SqlIntSub(OptimizeExpr(left) as SqlExprInt,
					OptimizeExpr(right) as SqlExprInt),

				SqlIntMult(var left, SqlIntValue(1)) => OptimizeExpr(left),
				SqlIntMult(SqlIntValue(1), var right) => OptimizeExpr(right),
				SqlIntMult(_, SqlIntValue(0)) => new SqlIntValue(0),
				SqlIntMult(SqlIntValue(0), _) => new SqlIntValue(0),
				SqlIntMult(SqlIntValue(var left), SqlIntValue(var right)) => new SqlIntValue(left * right),
				SqlIntMult(var left, var right) => new SqlIntMult(OptimizeExpr(left) as SqlExprInt,
					OptimizeExpr(right) as SqlExprInt),

				SqlIntDiv(SqlIntValue(0), _) => new SqlIntValue(0),
				SqlIntDiv(var left, SqlIntValue(1)) => OptimizeExpr(left),
				SqlIntDiv(var left, SqlIntValue(0)) => new SqlIntDiv(OptimizeExpr(left) as SqlExprInt,
					new SqlIntValue(0)),
				SqlIntDiv(SqlIntValue(var left), SqlIntValue(var right)) => new SqlIntValue(left / right),

				_ => expr
			};

		public static SqlExpr MultiOptimizer(SqlExpr expr)
		{
			var optExpr = OptimizeExpr(expr);
			return expr == optExpr
				? optExpr
				: MultiOptimizer(optExpr);
		}

		public static string EmitExpr(SqlExpr expr) =>
			expr switch
			{
				// Values
				SqlIntValue(var value) => value.ToString(),
				SqlBoolValue(var value) => value.ToString().ToUpper(),

				// Expressions - Boolean
				SqlBoolNot(var value) => $"NOT ({EmitExpr(value)})",
				SqlBoolAnd(var left, var right) => $"({EmitExpr(left)} AND {EmitExpr(right)})",
				SqlBoolOr(var left, var right) => $"({EmitExpr(left)} OR {EmitExpr(right)})",

				// Expressions - String
				SqlStringValue(var value) => $"'{value}'",
				SqlStringToUpper(var value) => $"{EmitExpr(value).ToUpper()}",
				SqlStringToLower(var value) => $"{EmitExpr(value).ToLower()}",

				// Expressions - Numeric
				SqlIntAdd(var left, var right) => $"({CompileExpr(left)} + {CompileExpr(right)})",
				SqlIntSub(var left, var right) => $"({CompileExpr(left)} - {CompileExpr(right)})",
				SqlIntMult(var left, var right) => $"({CompileExpr(left)} * {CompileExpr(right)})",
				SqlIntDiv(var left, var right) => $"({CompileExpr(left)} / {CompileExpr(right)})",

				SqlIntPlus(var value) => $"({CompileExpr(value)})",
				SqlIntMinus(var value) => $"(-({CompileExpr(value)}))",
				SqlIntAbs(var value) => $"(ABS({CompileExpr(value)}))",

				_ => throw new Exception($"Not supported {expr}")
			};

		public static string CompileExpr(this SqlExpr expr) => EmitExpr(MultiOptimizer(expr));
	}
}
