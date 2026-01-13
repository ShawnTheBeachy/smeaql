using System.Text;

namespace Smeaql;

public abstract class SqlStatement
{
    internal abstract void Write(StringBuilder stringBuilder, string? table);
}
