using Smeaql.Compilers;

namespace Smeaql;

internal abstract class SqlClause
{
    public abstract void Compile<TCompiler>(TCompiler compiler, CompiledSqlQuery compiledQuery)
        where TCompiler : SqlCompiler<TCompiler>;
}
