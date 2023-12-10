using System;
using UnityEngine;
using RavenSoul.Utilities.Singleton;

namespace RavenSoul.Utilities.Managers
{
    public delegate void OnUpdate(float deltaTime);
    
    public class PersistantBehaviour : PersistantSingleton<PersistantBehaviour>
    {
        public static event Action OnAwakeEvent;
        public static event Action OnStartEvent;
        public static event Action OnApplicationQuitEvent;
        public static event Action<bool> OnApplicationPauseEvent;
        public static event Action<bool> OnApplicationFocusEvent;
        public static event Action OnEverySecondEvent;
        
        public static event OnUpdate OnUpdateEvent;
        public static event OnUpdate OnFixedUpdateEvent;
        public static event OnUpdate OnLateUpdateEvent;
        
        public static void CreateInstance()
        {
            if (Instance == null)
            {
                var go = new GameObject("PersistantBehaviour");
                DontDestroyOnLoad(go);
                go.AddComponent<PersistantBehaviour>();
            }
        }
        
        public static void DestroyInstance()
        {
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }
        }

        private float _timeCounter;
        
        protected override void Awake()
        {
            base.Awake();
            OnAwakeEvent?.Invoke();
        }
        
        private void Start()
        {
            OnStartEvent?.Invoke();
        }
        
        private void Update()
        {
            OnUpdateEvent?.Invoke(Time.deltaTime);
            
            _timeCounter += Time.deltaTime;
            if (_timeCounter >= 1f)
            {
                OnEverySecondEvent?.Invoke();
                _timeCounter = 0f;
            }
        }
        
        private void FixedUpdate()
        {
            OnFixedUpdateEvent?.Invoke(Time.fixedDeltaTime);
        }
        
        private void LateUpdate()
        {
            OnLateUpdateEvent?.Invoke(Time.deltaTime);
        }
        
        private void OnApplicationQuit()
        {
            OnApplicationQuitEvent?.Invoke();
        }
        
        private void OnApplicationPause(bool pauseStatus)
        {
            OnApplicationPauseEvent?.Invoke(pauseStatus);
        }
        
        private void OnApplicationFocus(bool hasFocus)
        {
            OnApplicationFocusEvent?.Invoke(hasFocus);
        }
    }
}