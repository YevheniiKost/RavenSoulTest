using System;
using System.Collections;
using RavenSoul.Utilities.Managers;
using UnityEngine;

namespace RavenSoul.Utilities.Timer
{
    public class Timer : ITimer
    {
        public event Action<float> OnTimerUpdateEvent;
        public event Action OnTimerEndEvent;
        public event Action<bool> OnTimerPauseEvent;
        
        private Action _onTimerEnd;
        private Action<float> _onUpdate;

        public bool IsRunning => _isTimerStarted;
        public long StartTime => _startTime;
        public long EndTime => _endTime;
        public float Progress => _current / _duration;
        public float Current => _current;
        public float Duration => _duration;

        private float _duration;
        private float _current;
        private float _timeCounter;
        private float _speed;

        private long _startTime;
        private long _endTime;

        private TimerDirection _direction;

        private bool _isTimerStarted;
        private bool _isPaused;
        private bool _isStopped;

        public Timer(float duration, TimerDirection direction, Action<float> onUpdate, Action onTimerEnd, float speed)
        {
            _duration = duration;
            _direction = direction;
            _onUpdate = onUpdate;
            _onTimerEnd = onTimerEnd;
            _speed = speed;
        }

        public static Timer Empty()
        {
            return new Timer(0, TimerDirection.Backward, null, null, 1);
        }

        public void SetupNewTime(float duration, TimerDirection direction, Action<float> onUpdate, Action onTimerEnd, float speed = 1)
        {
            _duration = duration;
            _direction = direction;
            _onUpdate = onUpdate;
            _onTimerEnd = onTimerEnd;
            _speed = speed;
        }

        public void Pause()
        {
            _isPaused = true;
            OnTimerPauseEvent?.Invoke(_isPaused);
        }

        public void Start()
        {
            SetupStartData();
            CoroutineManager.StartCoroutineMethod(TimerUpdate());
        }

        public void Stop()
        {
            _isStopped = true;
            _isTimerStarted = false;
            CoroutineManager.StopCoroutineMethod(TimerUpdate());
        }

        public void UnPause()
        {
            _isPaused = false;
            OnTimerPauseEvent?.Invoke(_isPaused);
        }

        private void SetupStartData()
        {
            _timeCounter = 0;
            _startTime = DateTime.Now.Ticks;
            _endTime = DateTime.Now.AddSeconds(_duration).Ticks;
        }

        private IEnumerator TimerUpdate()
        {
            if (_isTimerStarted)
            {
                Debug.LogError("Timer is started!");
                yield return null;
            }

            _isTimerStarted = true;
            while (_timeCounter < _duration)
            {
                if (_isStopped)
                    yield break;

                if (_isPaused)
                {
                    yield return null;
                }
                else
                {
                    if (_direction == TimerDirection.Forward)
                    {
                        _current = _timeCounter;
                    }
                    else
                    {
                        _current = _duration - _timeCounter;
                    }
                    _timeCounter += UnityEngine.Time.deltaTime * _speed;
                    _onUpdate?.Invoke(_current);
                    OnTimerUpdateEvent?.Invoke(_current);
                }
                yield return null;
            }

            _onTimerEnd?.Invoke();
            OnTimerEndEvent?.Invoke();
            _isTimerStarted = false;
        }
    }

    public interface ITimerInfo
    {
        bool IsRunning { get; }
        long StartTime { get; }
        long EndTime { get; }
        float Progress { get; }
        float Current { get; }
        float Duration { get; }

        event Action<float> OnTimerUpdateEvent;
        event Action OnTimerEndEvent;
        event Action<bool> OnTimerPauseEvent;
    }

    public interface ITimer : ITimerInfo
    {
        void Pause();
        void UnPause();
        void Stop();
        void Start();
        void SetupNewTime(float duration, TimerDirection direction, Action<float> onUpdate, Action onTimerEnd, float speed = 1);
    }

    public enum TimerDirection
    {
        Forward = 0,
        Backward
    }
}