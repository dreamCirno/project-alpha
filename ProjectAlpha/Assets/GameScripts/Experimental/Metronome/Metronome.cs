using System;
using System.Linq;
using OsuParsers.Beatmaps.Objects.Mania;
using TEngine;
using UnityEngine;
using AudioType = TEngine.AudioType;

namespace ProjectAlpha
{
    public class Metronome : MonoBehaviour
    {
        public float AudioTime => _timer;
        public bool IsPlaying => _currentPlayState == PlayState.Playing;
        public float[] BeatTimes => _beatTimes;

        public static Metronome Current
        {
            get
            {
                if (!_active)
                {
                    _active = FindObjectOfType<Metronome>();
                }

                return _active;
            }
        }

        private static Metronome _active;

        private AudioAgent _audioAgent;
        private float _timer = 0.0f;
        private int _currentIndex = 0;
        private float[] _beatTimes;
        private OsuParsers.Beatmaps.Objects.Extras[] _audioSamples;

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
            // Load("zone1_3");
            Loadzone1_1();
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

                if (_audioSamples != null && !string.IsNullOrEmpty(_audioSamples[_currentIndex].SampleFileName))
                {
                    GameModule.Audio.Play(AudioType.Sound,
                        path: System.IO.Path.GetFileNameWithoutExtension(_audioSamples[_currentIndex].SampleFileName),
                        volume: _audioSamples[_currentIndex].Volume);
                }

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

            GameEvent.Send(GameEventDefine.Play);
        }

        public void Pause()
        {
            if (_currentPlayState != PlayState.Playing) return;
            if (_audioAgent != null)
            {
                _audioAgent.Pause();
                _currentPlayState = PlayState.Paused;
            }

            GameEvent.Send(GameEventDefine.Pause);
        }

        public void Resume()
        {
            if (_currentPlayState != PlayState.Paused) return;
            if (_audioAgent != null)
            {
                _audioAgent.UnPause();
                _currentPlayState = PlayState.Playing;
            }

            GameEvent.Send(GameEventDefine.Resume);
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

            GameEvent.Send(GameEventDefine.Stop);
        }

        private void Heartbeat()
        {
            GameEvent.Send(GameEventDefine.Heartbeat);
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
                var cleanedText = beatmapText.text.Replace("\r", ",").Replace("\n", ",");
                _beatTimes = cleanedText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(timeString => float.Parse(timeString))
                    .ToArray();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load beatmap: {ex.Message}");
                _beatTimes = Array.Empty<float>();
            }

            LoadAudio(map);
        }

        private void LoadWithOsu(string map)
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
                var lines = beatmapText.text
                    .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .AsEnumerable();
                var beatmap = OsuParsers.Decoders.BeatmapDecoder.Decode(lines);
                // 提取 StartTime 并转换为数组
                _beatTimes = beatmap.HitObjects
                    .Where(hitObject => hitObject.TotalTimeSpan.Seconds == 0)
                    .Select(hitObject => (float)hitObject.StartTime)
                    .ToArray();
                _audioSamples = beatmap.HitObjects
                    .Where(hitObject => hitObject.TotalTimeSpan.Seconds == 0)
                    .Select(hitObject => hitObject.Extras).ToArray();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load beatmap: {ex.Message}");
                _beatTimes = new float[0];
            }

            LoadAudio(map);
        }

        public void LoadFinixe()
        {
            var metronomeMap = GameModule.Resource.LoadAsset<MetronomeMap>("Finixe_MetronomeMap");
            _beatTimes = new float[metronomeMap.BeatObjects.Count];
            for (int i = 0; i < metronomeMap.BeatObjects.Count; i++)
            {
                _beatTimes[i] = metronomeMap.BeatObjects[i].StartTime;
            }

            LoadAudio("Assets/AssetRaw/BeatmapRaw/Osu/Finixe/Finixe.mp3");
        }

        public void Loadzone1_1()
        {
            var metronomeMap = GameModule.Resource.LoadAsset<MetronomeMap>("zone1_1_MetronomeMap");
            _beatTimes = new float[metronomeMap.BeatObjects.Count];
            for (int i = 0; i < metronomeMap.BeatObjects.Count; i++)
            {
                _beatTimes[i] = metronomeMap.BeatObjects[i].StartTime;
            }

            LoadAudio("Assets/AssetRaw/BeatmapRaw/CryptOfTheNecroDancer/zone1_1/zone1_1.ogg");
        }

        public void Loadzone1_2()
        {
            var metronomeMap = GameModule.Resource.LoadAsset<MetronomeMap>("zone1_2_MetronomeMap");
            _beatTimes = new float[metronomeMap.BeatObjects.Count];
            for (int i = 0; i < metronomeMap.BeatObjects.Count; i++)
            {
                _beatTimes[i] = metronomeMap.BeatObjects[i].StartTime;
            }

            LoadAudio("Assets/AssetRaw/BeatmapRaw/CryptOfTheNecroDancer/zone1_2/zone1_2.ogg");
        }

        public void Loadzone1_3()
        {
            var metronomeMap = GameModule.Resource.LoadAsset<MetronomeMap>("zone1_3_MetronomeMap");
            _beatTimes = new float[metronomeMap.BeatObjects.Count];
            for (int i = 0; i < metronomeMap.BeatObjects.Count; i++)
            {
                _beatTimes[i] = metronomeMap.BeatObjects[i].StartTime;
            }

            LoadAudio("Assets/AssetRaw/BeatmapRaw/CryptOfTheNecroDancer/zone1_3/zone1_3.ogg");
        }

        private void LoadAudio(string audioName)
        {
            var path = $"{audioName}";
            if (_audioAgent == null)
            {
                _audioAgent = GameModule.Audio.Play(AudioType.Music, path);
            }
            else
            {
                _audioAgent.Load(path, false);
            }

            _audioAgent.Pause();
        }
    }
}