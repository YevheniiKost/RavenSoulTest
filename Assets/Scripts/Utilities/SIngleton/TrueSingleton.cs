namespace RavenSoul.Utilities.Singleton
{
    public class TrueSingleton<T> where T : new()
    {
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }
		
        private static T _instance;
    }
}