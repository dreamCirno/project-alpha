using System;
using Sirenix.OdinInspector.Editor;
using TEngine;
using UnityEngine;
using AudioType = TEngine.AudioType;

namespace ProjectAlpha
{
    public class MetronomePlayer : MonoBehaviour
    {
        public float AudioTime => _metronome.AudioTime;
        public bool IsPlaying => _metronome.IsPlaying;
        public float[] BeatTimes => _metronome.BeatTimes;
        public int CurrentIndex => _metronome.CurrentIndex;

        public static MetronomePlayer Current
        {
            get
            {
                if (!_active)
                {
                    _active = FindObjectOfType<MetronomePlayer>();
                }

                return _active;
            }
        }

        private static MetronomePlayer _active;

        private Metronome _metronome;
        private AudioAgent _audioAgent;
        private OsuParsers.Beatmaps.Objects.Extras[] _audioSamples;

        private void Awake()
        {
            _metronome = new Metronome();
        }

        private void OnEnable()
        {
            _metronome.OnHeartBeat += OnHeartBeat;
        }

        private void OnDisable()
        {
            _metronome.OnHeartBeat -= OnHeartBeat;
        }

        private void Update()
        {
            _metronome.Update();
        }

        private void OnHeartBeat()
        {
            if (_audioSamples != null && !string.IsNullOrEmpty(_audioSamples[CurrentIndex].SampleFileName))
            {
                GameModule.Audio.Play(AudioType.Sound,
                    path: System.IO.Path.GetFileNameWithoutExtension(_audioSamples[CurrentIndex].SampleFileName),
                    volume: _audioSamples[CurrentIndex].Volume);
            }

            GameEvent.Send(GameEventDefine.Heartbeat);
        }

        public void Play()
        {
            if (_metronome.Play())
            {
                if (_audioAgent != null)
                {
                    _audioAgent.UnPause();
                }

                GameEvent.Send(GameEventDefine.Play);
            }
        }

        public void Pause()
        {
            if (_metronome.Pause())
            {
                if (_audioAgent != null)
                {
                    _audioAgent.Pause();
                }

                GameEvent.Send(GameEventDefine.Pause);
            }
        }

        public void Resume()
        {
            if (_metronome.Resume())
            {
                if (_audioAgent != null)
                {
                    _audioAgent.UnPause();
                }

                GameEvent.Send(GameEventDefine.Resume);
            }
        }

        public void Stop()
        {
            _metronome.Stop();
            if (_audioAgent != null)
            {
                _audioAgent.Stop();
                _audioAgent = null;
            }

            GameEvent.Send(GameEventDefine.Stop);
        }

        public void LoadFinixe()
        {
            var metronomeMap = GameModule.Resource.LoadAsset<MetronomeMap>("Finixe_MetronomeMap");
            _metronome.BeatTimes = new float[metronomeMap.BeatObjects.Count];
            for (int i = 0; i < metronomeMap.BeatObjects.Count; i++)
            {
                _metronome.BeatTimes[i] = metronomeMap.BeatObjects[i].StartTime;
            }

            LoadAudio("Assets/AssetRaw/BeatmapRaw/Osu/Finixe/Finixe.mp3");
        }

        public void Loadzone1_1()
        {
            var metronomeMap = GameModule.Resource.LoadAsset<MetronomeMap>("zone1_1_MetronomeMap");
            _metronome.BeatTimes = new float[metronomeMap.BeatObjects.Count];
            for (int i = 0; i < metronomeMap.BeatObjects.Count; i++)
            {
                _metronome.BeatTimes[i] = metronomeMap.BeatObjects[i].StartTime;
            }

            LoadAudio("Assets/AssetRaw/BeatmapRaw/CryptOfTheNecroDancer/zone1_1/zone1_1.ogg");
        }

        public void Loadzone1_2()
        {
            var metronomeMap = GameModule.Resource.LoadAsset<MetronomeMap>("zone1_2_MetronomeMap");
            _metronome.BeatTimes = new float[metronomeMap.BeatObjects.Count];
            for (int i = 0; i < metronomeMap.BeatObjects.Count; i++)
            {
                _metronome.BeatTimes[i] = metronomeMap.BeatObjects[i].StartTime;
            }

            LoadAudio("Assets/AssetRaw/BeatmapRaw/CryptOfTheNecroDancer/zone1_2/zone1_2.ogg");
        }

        public void Loadzone1_3()
        {
            var metronomeMap = GameModule.Resource.LoadAsset<MetronomeMap>("zone1_3_MetronomeMap");
            _metronome.BeatTimes = new float[metronomeMap.BeatObjects.Count];
            for (int i = 0; i < metronomeMap.BeatObjects.Count; i++)
            {
                _metronome.BeatTimes[i] = metronomeMap.BeatObjects[i].StartTime;
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

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(MetronomePlayer))]
    public class MetronomeEditor : OdinEditor
    {
        private MetronomePlayer _metronomePlayer;

        protected override void OnEnable()
        {
            base.OnEnable();
            _metronomePlayer = (MetronomePlayer)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);

            if (GUILayout.Button("LoadFinixe"))
            {
                _metronomePlayer.LoadFinixe();
            }

            if (GUILayout.Button("Loadzone1_1"))
            {
                _metronomePlayer.Loadzone1_1();
            }

            if (GUILayout.Button("Loadzone1_2"))
            {
                _metronomePlayer.Loadzone1_2();
            }

            if (GUILayout.Button("Loadzone1_3"))
            {
                _metronomePlayer.Loadzone1_3();
            }

            if (GUILayout.Button("Play"))
            {
                _metronomePlayer.Play();
            }

            if (GUILayout.Button("Pause"))
            {
                _metronomePlayer.Pause();
            }

            if (GUILayout.Button("Resume"))
            {
                _metronomePlayer.Resume();
            }

            if (GUILayout.Button("Stop"))
            {
                _metronomePlayer.Stop();
            }
        }
    }

#endif
}