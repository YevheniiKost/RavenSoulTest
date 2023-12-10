using Cinemachine;
using UnityEngine;

namespace RavenSoul.Presentation
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class PlayerCamera : MonoBehaviour
    {
        private CinemachineVirtualCamera _virtualCamera;

        private void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }
        
        public void SetFollowTarget(Transform target)
        {
            _virtualCamera.Follow = target;
        }
    }
}