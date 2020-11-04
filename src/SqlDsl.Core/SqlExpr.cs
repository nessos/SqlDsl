namespace SqlDsl.Core
{
    public interface SqlExpr { }
    
    public interface SqlExpr<TSqlType> : SqlExpr where TSqlType : SqlType { }

    public interface SqlUnaryExpr<TSqlType> : SqlExpr<TSqlType> where TSqlType : SqlType
    {
	    SqlExpr<TSqlType> Value { get; }
    }

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

    public class SqlBoolNot : SqlUnaryExpr<SqlBool>
    {
	    public SqlExpr<SqlBool> Value { get; }

	    public SqlBoolNot(SqlExpr<SqlBool> value)
	    {
		    Value = value;
	    }

	    public void Deconstruct(out SqlExpr<SqlBool> value)
	    {
		    value = Value;
	    }
	}

    public class SqlBoolAnd : SqlBinExpr<SqlBool>
    {
	    public SqlExpr<SqlBool> Left { get; }
	    public SqlExpr<SqlBool> Right { get; }

	    public SqlBoolAnd(SqlExpr<SqlBool> left, SqlExpr<SqlBool> right)
	    {
		    Left = left;
		    Right = right;
	    }

	    public void Deconstruct(out SqlExpr<SqlBool> left, out SqlExpr<SqlBool> right)
	    {
		    left = Left;
		    right = Right;
	    }
    }

    public class SqlBoolOr : SqlBinExpr<SqlBool>
    {
	    public SqlExpr<SqlBool> Left { get; }
	    public SqlExpr<SqlBool> Right { get; }

	    public SqlBoolOr(SqlExpr<SqlBool> left, SqlExpr<SqlBool> right)
	    {
		    Left = left;
		    Right = right;
	    }

	    public void Deconstruct(out SqlExpr<SqlBool> left, out SqlExpr<SqlBool> right)
	    {
		    left = Left;
		    right = Right;
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
