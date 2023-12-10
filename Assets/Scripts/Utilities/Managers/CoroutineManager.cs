using System.Collections;
using RavenSoul.Utilities.Singleton;
using UnityEngine;


namespace RavenSoul.Utilities.Managers
{
    public class CoroutineManager : PersistantSingleton<CoroutineManager>
    {
        
        public static void CreateInstance()
        {
            if (Instance == null)
            {
                var go = new GameObject("CoroutineManager");
                DontDestroyOnLoad(go);
                go.AddComponent<CoroutineManager>();
            }
        }
        
        public static void DestroyInstance()
        {
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }
        }

        public static void StartCoroutineMethod(IEnumerator coroutine)
        {
            Instance.StartCoroutine(coroutine);
        }

        public static void StopCoroutineMethod(IEnumerator coroutine)
        {
            Instance.StopCoroutine(coroutine);
        }

        private IEnumerator DelayedActionInner(float delay, System.Action action)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }

        public static void DelayedAction(float delay, System.Action action)
        {
            Instance.StartCoroutine(Instance.DelayedActionInner(delay, action));
        }
    }
}