namespace SqlDsl.Core
{
    public class SqlIntPlus : SqlUnaryExpr<SqlInt>
    {
        public SqlExpr<SqlInt> Value { get; }

        public SqlIntPlus(SqlExpr<SqlInt> value)
        {
            Value = value;
        }

        public void Deconstruct(out SqlExpr<SqlInt> value)
        {
            value = Value;
        }
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

    public class SqlIntSub : SqlBinExpr<SqlInt>
    {
        public SqlExpr<SqlInt> Left { get; }
        public SqlExpr<SqlInt> Right { get; }
        public SqlIntSub(SqlExpr<SqlInt> left, SqlExpr<SqlInt> right)
        {
            Left = left;
            Right = right;
        }
        public void Deconstruct(out SqlExpr<SqlInt> left, out SqlExpr<SqlInt> right)
        {
            left = Left;
            right = Right;
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

    public class SqlIntAbs : SqlExpr<SqlInt>
    {
        public SqlExpr<SqlInt> Value { get; }

        public SqlIntAbs(SqlExpr<SqlInt> value) =>
            Value = value;

        public void Deconstruct(out SqlExpr<SqlInt> value) =>
            value = Value;
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

 

}
