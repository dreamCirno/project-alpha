using UnityEngine;

namespace ProjectAlpha
{
    public class MetronomeHeart : MonoBehaviour, IHeartbeatListener
    {
        public FrameAnimator Animator;

        public void OnHeartbeat()
        {
            Animator.isLooping = false;
            Animator.Replay();
        }
    }
}