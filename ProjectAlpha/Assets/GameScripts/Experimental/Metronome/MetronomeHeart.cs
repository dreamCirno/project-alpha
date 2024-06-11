using UnityEngine;

namespace ProjectAlpha
{
    public class MetronomeHeart : MonoBehaviour, IMetronomeListener
    {
        public FrameAnimator Animator;

        public void OnHeartbeat()
        {
            Animator.isLooping = false;
            Animator.Replay();
        }

        public void OnPlay()
        {
        }

        public void OnPause()
        {
        }

        public void OnResume()
        {
        }

        public void OnStop()
        {
        }
    }
}