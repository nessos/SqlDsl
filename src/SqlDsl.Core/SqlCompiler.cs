using System;
using System.Collections.Generic;
using System.Text;

namespace SqlDsl.Core
{
    public static class SqlCompiler
    {
        public static string CompileExpr(this SqlExpr expr) =>
            expr switch
            {
                SqlIntValue(var value) => value.ToString(),
                SqlIntAdd(var left, var right) => $"({CompileExpr(left)} + {CompileExpr(right)})",
                SqlIntMult(var left, var right) => $"({CompileExpr(left)} * {CompileExpr(right)})",
                _ => throw new Exception($"Not supported {expr}")
            };
    }
}
