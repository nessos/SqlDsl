using System;

namespace SqlDsl.Core
{
    public interface SqlType { }
    public interface SqlString : SqlType { }
    public interface SqlInt : SqlType { }

	public interface SqlBool : SqlType { }
}
