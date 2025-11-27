namespace CalculationExtensionLibrary
{
    public static class CollectionExtension
    {
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> collection, Predicate<T> predicate)
        {
            List<T> result = [];
            foreach (var item in collection)
            {
                if (predicate(item))
                    result.Add(item);
            }
            return result;
        }
    }
}
