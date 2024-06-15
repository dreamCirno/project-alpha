using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ProjectAlpha
{
    public class MarkerSpawner : MonoBehaviour, IMetronomeListener
    {
        public BeatMarker BeatMarkerPrefab;
        public float PreGenerateMiliseconds = 3000;

        private Queue<float> _beatTimesQueue;
        private List<BeatMarker> _beatMarkersUseList;
        private ObjectPool<BeatMarker> _pool;

        private void Awake()
        {
            _beatMarkersUseList = new List<BeatMarker>();
            _pool = new ObjectPool<BeatMarker>(
                createFunc: () =>
                {
                    var spawned = Instantiate(BeatMarkerPrefab, transform);
                    spawned.transform.localPosition = Vector3.zero;
                    return spawned;
                }
                , actionOnGet: marker => { marker.gameObject.SetActive(true); }
                , actionOnRelease: marker => { marker.gameObject.SetActive(false); });
        }

        private void Update()
        {
            while (MetronomePlayer.Current.IsPlaying && _beatTimesQueue.Count > 0 &&
                   _beatTimesQueue.Peek() <= MetronomePlayer.Current.AudioTime + PreGenerateMiliseconds)
            {
                float beatTime = _beatTimesQueue.Dequeue();
                if (beatTime >= MetronomePlayer.Current.AudioTime)
                {
                    _beatMarkersUseList.Add(Spawn(beatTime));
                }
            }
        }

        private BeatMarker Spawn(float time)
        {
            var beatMarker = _pool.Get();
            beatMarker.Init(this, time);
            return beatMarker;
        }

        public void Release(BeatMarker beatMarker)
        {
            _pool.Release(beatMarker);
            _beatMarkersUseList.Remove(beatMarker);
        }

        public void OnHeartbeat()
        {
        }

        public void OnPlay()
        {
            _beatTimesQueue = new Queue<float>(MetronomePlayer.Current.BeatTimes);
        }

        public void OnPause()
        {
        }

        public void OnResume()
        {
        }

        public void OnStop()
        {
            if (_beatTimesQueue != null)
            {
                _beatTimesQueue.Clear();
            }

            for (int index = _beatMarkersUseList.Count - 1; index >= 0; index--)
            {
                Release(_beatMarkersUseList[index]);
            }

            _beatMarkersUseList.Clear();
        }
    }
}