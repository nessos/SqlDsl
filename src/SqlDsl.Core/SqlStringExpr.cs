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
    }

    public class SqlStringToUpper : SqlExpr<SqlString>
    {
        public SqlExpr<SqlString> Value { get; }
        public SqlStringToUpper(SqlExpr<SqlString> value)
        {
            this.Value = value;
        }

        public void Deconstruct(out SqlExpr<SqlString> value)
        {
            value = this.Value;
        }
    }

    public class SqlStringToLower : SqlExpr<SqlString>
    {
        public SqlExpr<SqlString> Value { get; }
        public SqlStringToLower(SqlExpr<SqlString> value)
        {
            this.Value = value;
        }

        public void Deconstruct(out SqlExpr<SqlString> value)
        {
            value = this.Value;
        }
    }

}
