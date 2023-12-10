using System;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;

using RavenSoul.Utilities.Logger;
using Object = UnityEngine.Object;

namespace RavenSoul.Utilities.Managers
{
    public static class ScenesManager
    {
        public static async void LoadSceneWithObject<T>(string sceneName, Action<T> objectToGet) where T : MonoBehaviour
        {
            await LoadSceneAsync(sceneName);
            
            var obj = Object.FindObjectOfType<T>();
            if (obj == null)
            {
                MyLogger.LogError($"Object of type {typeof(T)} not found in scene {sceneName}");
            }
            
            objectToGet?.Invoke(obj);
        }
        
        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        
        public static void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        
        public static async Task LoadSceneAsync(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            while (!operation.isDone)
            {
                await Task.Yield();
            }
        }
    }
}