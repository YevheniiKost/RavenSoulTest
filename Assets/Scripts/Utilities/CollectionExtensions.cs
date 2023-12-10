namespace RavenSoul.Utilities
{
    public static class CollectionExtensions
    {
        public static T GetRandomElement<T>(this T[] array)
        {
            return array[UnityEngine.Random.Range(0, array.Length)];
        }
    }
}