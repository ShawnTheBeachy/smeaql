using System.Text;
using Microsoft.Extensions.ObjectPool;

namespace Smeaql;

internal static class ObjectPools
{
    public static readonly ObjectPool<StringBuilder> StringBuilders =
        new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy());
}
