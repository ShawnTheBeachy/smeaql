using System.Text;
using Smeaql.Compilers;

namespace Smeaql.Limit;

internal sealed class LimitClause : SqlClause
{
    private readonly int _limit,
        _offset;

    public LimitClause(int limit, int offset)
    {
        _limit = limit;
        _offset = offset;
    }

    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
    {
        if (compiler is SqlServerCompiler)
        {
            CompileSqlServer(stringBuilder, parameterFactory);
            return;
        }

        stringBuilder.Append("LIMIT ");
        stringBuilder.Append(parameterFactory.CreateParameter(_limit));
        stringBuilder.Append(" OFFSET ");
        stringBuilder.Append(parameterFactory.CreateParameter(_offset));
    }

    private void CompileSqlServer(StringBuilder stringBuilder, ParameterFactory parameterFactory)
    {
        stringBuilder.Append("OFFSET ");
        stringBuilder.Append(parameterFactory.CreateParameter(_offset));
        stringBuilder.Append(" ROWS FETCH NEXT ");
        stringBuilder.Append(parameterFactory.CreateParameter(_limit));
        stringBuilder.Append(" ROWS ONLY");
    }
}
