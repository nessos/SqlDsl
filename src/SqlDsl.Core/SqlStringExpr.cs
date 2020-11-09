namespace SqlDsl.Core
{
    public class SqlStringValue : SqlExpr<SqlString>
    {
        public string Value { get; private set; }
        public SqlStringValue(string v)
        {
            this.Value = v;
        }
        public void Deconstruct(out string value)
        {
            value = this.Value;
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
        public void Deconstruct(out SqlExpr<SqlString> left, out SqlExpr<SqlString> right)
        {
            left = this.Left;
            right = this.Right;
        }
    }
}
