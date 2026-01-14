namespace Smeaql.Helpers;

internal static class ListExtensions
{
    public static void Replace<T, TR>(this List<T> collection, TR item)
        where TR : T
    {
        collection.RemoveAll(x => x is TR);
        collection.Add(item);
    }
}
