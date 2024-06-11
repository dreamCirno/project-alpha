using TEngine;
using UnityEngine;

namespace ProjectAlpha
{
    public class MetronomeListener : MonoBehaviour
    {
        private IMetronomeListener _metronomeListener;

        private void Awake()
        {
            _metronomeListener = GetComponent<IMetronomeListener>();
        }

        protected void OnEnable()
        {
            GameEvent.AddEventListener(GameEventDefine.Heartbeat, OnHeartbeat);
            GameEvent.AddEventListener(GameEventDefine.Play, OnPlay);
            GameEvent.AddEventListener(GameEventDefine.Pause, OnPause);
            GameEvent.AddEventListener(GameEventDefine.Resume, OnResume);
            GameEvent.AddEventListener(GameEventDefine.Stop, OnStop);
        }

        protected void OnDisable()
        {
            GameEvent.RemoveEventListener(GameEventDefine.Heartbeat, OnHeartbeat);
            GameEvent.RemoveEventListener(GameEventDefine.Play, OnPlay);
            GameEvent.RemoveEventListener(GameEventDefine.Pause, OnPause);
            GameEvent.RemoveEventListener(GameEventDefine.Resume, OnResume);
            GameEvent.RemoveEventListener(GameEventDefine.Stop, OnStop);
        }

        private void OnHeartbeat()
        {
            _metronomeListener?.OnHeartbeat();
        }

        private void OnPlay()
        {
            _metronomeListener?.OnPlay();
        }

        private void OnPause()
        {
            _metronomeListener?.OnPause();
        }

        private void OnResume()
        {
            _metronomeListener?.OnResume();
        }

        private void OnStop()
        {
            _metronomeListener?.OnStop();
        }
    }
}