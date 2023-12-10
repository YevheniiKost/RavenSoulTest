namespace RavenSoul.Utilities.Singleton
{
    public class PersistantSingleton<T>  : Singleton<T> where T : Singleton<T>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}