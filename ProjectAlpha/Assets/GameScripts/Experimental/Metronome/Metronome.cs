using System;
using UnityEngine;

namespace ProjectAlpha
{
    public class Metronome
    {
        public float AudioTime => _timer;
        public bool IsPlaying => _currentMetronomePlayState == MetronomePlayState.Playing;
        public float[] BeatTimes;
        public MetronomePlayState CurrentMetronomePlayState => _currentMetronomePlayState;
        public int CurrentIndex => _currentIndex;

        public event Action OnPlay;
        public event Action OnPause;
        public event Action OnResume;
        public event Action OnStop;
        public event Action OnHeartBeat;

        private float _timer = 0.0f;
        private int _currentIndex = 0;
        private MetronomePlayState _currentMetronomePlayState = MetronomePlayState.Stopped;

        // 播放状态枚举
        public enum MetronomePlayState
        {
            Stopped,
            Playing,
            Paused
        }

        public void Update()
        {
            if (_currentMetronomePlayState != MetronomePlayState.Playing || _currentIndex >= BeatTimes.Length) return;

            _timer += Time.deltaTime * 1000; // 转换为毫秒

            // 检查是否到达当前心跳时间点
            if (_timer >= BeatTimes[_currentIndex])
            {
                Heartbeat();
                _currentIndex++;
            }
        }

        public bool Play()
        {
            if (_currentMetronomePlayState == MetronomePlayState.Playing)
                return false;
            _currentMetronomePlayState = MetronomePlayState.Playing;
            OnPlay?.Invoke();
            return true;
        }

        public bool Pause()
        {
            if (_currentMetronomePlayState != MetronomePlayState.Playing) return false;
            _currentMetronomePlayState = MetronomePlayState.Paused;
            OnPause?.Invoke();
            return true;
        }

        public bool Resume()
        {
            if (_currentMetronomePlayState != MetronomePlayState.Paused) return false;
            _currentMetronomePlayState = MetronomePlayState.Playing;
            OnResume?.Invoke();
            return true;
        }

        public void Stop()
        {
            _currentMetronomePlayState = MetronomePlayState.Stopped;
            ResetTimerAndIndex();
            OnStop?.Invoke();
        }

        private void Heartbeat()
        {
            OnHeartBeat?.Invoke();
        }

        private void ResetTimerAndIndex()
        {
            _timer = 0.0f;
            _currentIndex = 0;
        }
    }
}