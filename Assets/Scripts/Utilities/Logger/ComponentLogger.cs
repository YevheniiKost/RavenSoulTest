using UnityEngine;

namespace RavenSoul.Utilities.Logger
{
    [AddComponentMenu("YellowTape/Utilities/Logger/ComponentLogger"), DisallowMultipleComponent]
    public class ComponentLogger : MonoBehaviour, ILogger
    {
        [Header("Settings")] 
        [SerializeField] private bool _showLogs = true;
        [SerializeField] private string _prefix;
        [SerializeField] private Color _prefixColor = Color.white;

        public void Log(string message)
        {
            if (_showLogs)
                Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(_prefixColor)}>{_prefix}</color> {message}");
        }

        public void LogWarning(string message)
        {
            if (_showLogs)
                Debug.LogWarning($"<color=#{ColorUtility.ToHtmlStringRGB(_prefixColor)}>{_prefix}</color> {message}");
        }

        public void LogError(string message)
        {
            if (_showLogs)
                Debug.LogError($"<color=#{ColorUtility.ToHtmlStringRGB(_prefixColor)}>{_prefix}</color> {message}");
        }
    }
}