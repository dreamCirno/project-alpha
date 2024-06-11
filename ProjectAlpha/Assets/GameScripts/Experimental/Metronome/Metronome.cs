using System;
using System.Linq;
using TEngine;
using UnityEngine;

namespace ProjectAlpha
{
    public class Metronome : MonoBehaviour
    {
        private AudioAgent _audioAgent;
        private float _timer = 0.0f;
        private int _currentIndex = 0;
        private int[] _beatTimes;

        // 播放状态枚举
        private enum PlayState
        {
            Stopped,
            Playing,
            Paused
        }

        private PlayState _currentPlayState = PlayState.Stopped;

        private void Start()
        {
            Load("zone1_1");
            Play();
        }

        private void Update()
        {
            if (_currentPlayState != PlayState.Playing || _currentIndex >= _beatTimes.Length) return;

            _timer += Time.deltaTime * 1000; // 转换为毫秒

            // 检查是否到达当前心跳时间点
            if (_timer >= _beatTimes[_currentIndex])
            {
                Heartbeat();
                _currentIndex++;
            }
        }

        public void Play()
        {
            if (_currentPlayState == PlayState.Playing) return;
            if (_audioAgent != null)
            {
                _audioAgent.UnPause();
                _currentPlayState = PlayState.Playing;
            }
        }

        public void Pause()
        {
            if (_currentPlayState != PlayState.Playing) return;
            if (_audioAgent != null)
            {
                _audioAgent.Pause();
                _currentPlayState = PlayState.Paused;
            }
        }

        public void Resume()
        {
            if (_currentPlayState != PlayState.Paused) return;
            if (_audioAgent != null)
            {
                _audioAgent.UnPause();
                _currentPlayState = PlayState.Playing;
            }
        }

        public void Stop()
        {
            _currentPlayState = PlayState.Stopped;
            ResetTimerAndIndex();
            if (_audioAgent != null)
            {
                _audioAgent.Stop();
                _audioAgent = null;
            }
        }

        private void Heartbeat()
        {
            GameEvent.Send(nameof(Metronome));
        }

        private void ResetTimerAndIndex()
        {
            _timer = 0.0f;
            _currentIndex = 0;
        }

        public void Load(string map)
        {
            Stop();

            var beatmapText = GameModule.Resource.LoadAsset<TextAsset>($"Beatmap_{map}");
            if (beatmapText == null)
            {
                Debug.LogError("Beatmap text is null.");
                return;
            }

            try
            {
                _beatTimes = beatmapText.text.Split(',')
                    .Select(int.Parse)
                    .ToArray();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load beatmap: {ex.Message}");
                _beatTimes = new int[0];
            }

            LoadAudio(map);
        }

        private void LoadAudio(string audioName)
        {
            var path = $"{audioName}";
            if (_audioAgent == null)
            {
                _audioAgent = GameModule.Audio.Play(TEngine.AudioType.Music, path);
            }
            else
            {
                _audioAgent.Load(path, false);
            }

            _audioAgent.Pause();
        }
    }
}