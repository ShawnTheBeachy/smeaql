using System.Text;
using Smeaql.Compilers;

namespace Smeaql;

internal abstract class SqlClause
{
    public abstract void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
        where TCompiler : SqlCompiler<TCompiler>;
}
