using System;

namespace SqlDsl.Core
{
	public static class SqlCompiler
	{
		private static SqlExpr OptimizeExpr(SqlExpr expr) =>
			expr switch
			{
				// String Optimizations
				SqlStringToUpper(SqlStringValue(var value)) => new SqlStringValue(value.ToUpperInvariant()),
				SqlStringToUpper(var value) => new SqlStringToUpper(OptimizeExpr(value) as SqlExprString),

				SqlStringToLower(SqlStringValue(var value)) => new SqlStringValue(value.ToLowerInvariant()),
				SqlStringToLower(var value) => new SqlStringToLower(OptimizeExpr(value) as SqlExprString),

				SqlStringConcat(SqlStringValue(var left), SqlStringValue(var right)) => new SqlStringValue(
					string.Join(string.Empty, left, right)),
				SqlStringConcat(var left, var right) => new SqlStringConcat(OptimizeExpr(left) as SqlExprString,
					OptimizeExpr(right) as SqlExprString),

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

				SqlIntGreaterThan(SqlIntValue(var left), SqlIntValue(var right)) => new SqlBoolValue(left > right),
				SqlIntGreaterThan(var left, var right) => new SqlIntGreaterThan(OptimizeExpr(left) as SqlExprInt,
					OptimizeExpr(right) as SqlExprInt),
				SqlIntGreaterThanOrEqualTo(SqlIntValue(var left), SqlIntValue(var right)) => new SqlBoolValue(
					left >= right),
				SqlIntGreaterThanOrEqualTo(var left, var right) => new SqlIntGreaterThanOrEqualTo(
					OptimizeExpr(left) as SqlExprInt,
					OptimizeExpr(right) as SqlExprInt),
				SqlIntLessThan(SqlIntValue(var left), SqlIntValue(var right)) => new SqlBoolValue(left < right),
				SqlIntLessThan(var left, var right) => new SqlIntLessThan(OptimizeExpr(left) as SqlExprInt,
					OptimizeExpr(right) as SqlExprInt),
				SqlIntLessThanOrEqualTo(SqlIntValue(var left), SqlIntValue(var right)) => new SqlBoolValue(
					left <= right),
				SqlIntLessThanOrEqualTo(var left, var right) => new SqlIntLessThanOrEqualTo(
					OptimizeExpr(left) as SqlExprInt,
					OptimizeExpr(right) as SqlExprInt),

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
				SqlIntDiv(var left, SqlIntValue(0) right) => new SqlIntDiv(OptimizeExpr(left) as SqlExprInt,
					right),
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
				SqlStringValue(var value) => $"'{value}'",

				// Expressions - Boolean
				SqlBoolNot(var value) => $"NOT ({EmitExpr(value)})",
				SqlBoolAnd(var left, var right) => $"({EmitExpr(left)} AND {EmitExpr(right)})",
				SqlBoolOr(var left, var right) => $"({EmitExpr(left)} OR {EmitExpr(right)})",

				// Expressions - String
				SqlStringToUpper(var value) => $"UPPER({EmitExpr(value)})",
				SqlStringToLower(var value) => $"LOWER({EmitExpr(value)})",
				SqlStringConcat(var left, var right) => $"CONCAT({EmitExpr(left)}, {EmitExpr(right)})",

				// Expressions - Numeric
				SqlIntAdd(var left, var right) => $"({EmitExpr(left)} + {EmitExpr(right)})",
				SqlIntSub(var left, var right) => $"({EmitExpr(left)} - {EmitExpr(right)})",
				SqlIntMult(var left, var right) => $"({EmitExpr(left)} * {EmitExpr(right)})",
				SqlIntDiv(var left, var right) => $"({EmitExpr(left)} / {EmitExpr(right)})",

				SqlIntPlus(var value) => $"({EmitExpr(value)})",
				SqlIntMinus(var value) => $"(-({EmitExpr(value)}))",
				SqlIntAbs(var value) => $"(ABS({EmitExpr(value)}))",

				SqlIntGreaterThan(var left, var right) => $"({EmitExpr(left)} > {EmitExpr(right)})",
				SqlIntGreaterThanOrEqualTo(var left, var right) => $"({EmitExpr(left)} >= {EmitExpr(right)})",
				SqlIntLessThan(var left, var right) => $"({EmitExpr(left)} < {EmitExpr(right)})",
				SqlIntLessThanOrEqualTo(var left, var right) => $"({EmitExpr(left)} <= {EmitExpr(right)})",

				_ => throw new Exception($"Not supported {expr}")
			};

		public static string CompileExpr(this SqlExpr expr) => EmitExpr(MultiOptimizer(expr));
	}
}
