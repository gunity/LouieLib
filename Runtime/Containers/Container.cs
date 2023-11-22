namespace LouieLib.Containers
{
    public static class Container
    {
        public static T Get<T>()
        {
            return DataContainer.Instance.Get<T>();
        }

        internal static void Clear()
        {
            DataContainer.Instance?.Clear();
        }
    }
}