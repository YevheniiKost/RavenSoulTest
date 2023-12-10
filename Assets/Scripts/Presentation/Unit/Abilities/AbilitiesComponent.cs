using System;
using System.Collections.Generic;
using RavenSoul.Presentation.Interactors;
using RavenSoul.Utilities.Timer;
using UnityEngine;

namespace RavenSoul.Presentation.Unit
{
    public class AbilitiesComponent : MonoBehaviour, IAbilitiesComponent
    {
        [SerializeField] private float _attackCircleRadius;
        [SerializeField] private float _attackDistance;
        [SerializeField] private Transform _attackStartPoint;

        private ITimer _attackTimer;
        private Action _onAttackEnd;
        
        private bool _attacking;
        private Vector2 _attackDirection;

        public void Attack(Vector2 direction, float attackSpeed, HitParams hitParams, Action onAttackEnd)
        {
            if (_attackTimer != null && _attackTimer.IsRunning)
            {
                _attackTimer.Stop();
            }

            _attacking = true;
            _attackDirection = direction;
            _onAttackEnd = onAttackEnd;
            _attackTimer = TimerService.CreateTimer(attackSpeed, TimerDirection.Backward, null, () =>
            {
                ProcessHit(direction, hitParams);
                OnAttackEnd();
                _attacking = false;
            });
            _attackTimer.Start();
        }

        public void SetAttackRadius(float attackSpeed)
        {
            _attackDistance = attackSpeed;
        }

        private void OnAttackEnd()
        {
            _attackTimer.Stop();
            _attackTimer = null;
            _onAttackEnd?.Invoke();
        }

        private void ProcessHit(Vector2 direction, HitParams hitParams)
        {
            RaycastHit2D[] results = new RaycastHit2D[10];

            Physics2D.CircleCastNonAlloc(_attackStartPoint.position, _attackCircleRadius, direction, results, _attackDistance);

            HashSet<IHit> hitObjects = new HashSet<IHit>();
            foreach (var result in results)
            {
                if (result.collider != null && result.collider.gameObject && !result.collider.transform.IsChildOf(transform))
                {
                    var hitObject = result.collider.GetComponent<IHit>();

                    if (hitObject != null && !hitObjects.Contains(hitObject))
                    {
                        hitObject.Hit(hitParams);
                        hitObjects.Add(hitObject);
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_attackStartPoint.position, _attackCircleRadius);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_attackStartPoint.position, _attackDistance);
            
            if (_attacking)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(_attackStartPoint.position, _attackStartPoint.position + (Vector3)_attackDirection.normalized * _attackDistance);
            }
        }
    }
}