using UnityEngine;

namespace ProjectAlpha
{
    public class MetronomeHeart : MonoBehaviour
    {
        public FrameAnimator Animator;

        public void Heartbeat()
        {
            Animator.isLooping = false;
            Animator.Replay();
        }
    }
}