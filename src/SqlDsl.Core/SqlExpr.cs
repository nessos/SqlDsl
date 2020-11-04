using System;
using System.Collections.Generic;
using System.Text;

namespace SqlDsl.Core
{
    public interface SqlExpr { }
    
    public interface SqlExpr<TSqlType> : SqlExpr where TSqlType : SqlType { }

    public interface SqlBinExpr<TSqlType> : SqlExpr<TSqlType> where TSqlType : SqlType
    {
        SqlExpr<TSqlType> Left { get; }
        SqlExpr<TSqlType> Right { get; }
    }

    public class SqlIntAdd : SqlBinExpr<SqlInt>
    {
        public SqlExpr<SqlInt> Left { get; }
        public SqlExpr<SqlInt> Right { get; }
        public SqlIntAdd(SqlExpr<SqlInt> left, SqlExpr<SqlInt> right)
        {
            this.Left = left;
            this.Right = right;
        }
        public void Deconstruct(out SqlExpr<SqlInt> left, out SqlExpr<SqlInt> right)
        {
            left = this.Left;
            right = this.Right;
        }
    }

    public class SqlIntMult : SqlBinExpr<SqlInt>
    {
        public SqlExpr<SqlInt> Left { get; }
        public SqlExpr<SqlInt> Right { get; }
        public SqlIntMult(SqlExpr<SqlInt> left, SqlExpr<SqlInt> right)
        {
            this.Left = left;
            this.Right = right;
        }
        public void Deconstruct(out SqlExpr<SqlInt> left, out SqlExpr<SqlInt> right)
        {
            left = this.Left;
            right = this.Right;
        }
    }

    public class SqlIntValue : SqlExpr<SqlInt>
    {
        public int Value { get; private set; }
        public SqlIntValue(int v)
        {
            this.Value = v;
        }
        public void Deconstruct(out int value)
        {
            value = this.Value;
        }
    }

    public class SqlBoolValue : SqlExpr<SqlBool>
    {
		public bool Value { get; private set; }
	    public SqlBoolValue(bool value)
	    {
		    Value = value;
	    }

	    public void Deconstruct(out bool value)
	    {
		    value = Value;
	    }
    }

    public class SqlStringConcat : SqlBinExpr<SqlString>
    {
        public SqlExpr<SqlString> Left { get; }
        public SqlExpr<SqlString> Right { get; }
        public SqlStringConcat(SqlExpr<SqlString> left, SqlExpr<SqlString> right)
        {
            this.Left = left;
            this.Right = right;
        }
    }

}
