using TEngine;
using UnityEngine;

namespace ProjectAlpha
{
    public class HeartbeatListener : MonoBehaviour
    {
        private IHeartbeatListener _heartbeatListener;

        private void Awake()
        {
            _heartbeatListener = GetComponent<IHeartbeatListener>();
        }

        protected void OnEnable()
        {
            GameEvent.AddEventListener(nameof(Metronome), OnHeartbeat);
        }

        protected void OnDisable()
        {
            GameEvent.RemoveEventListener(nameof(Metronome), OnHeartbeat);
        }

        private void OnHeartbeat()
        {
            if (_heartbeatListener != null)
            {
                _heartbeatListener.OnHeartbeat();
            }
        }
    }
}