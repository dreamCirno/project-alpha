using UnityEngine;
using UnityEngine.UI;

namespace ProjectAlpha
{
    public class FrameAnimator : MonoBehaviour
    {
        public Image image;
        public Sprite[] frames;
        public float framesPerSecond = 12.0f;
        public bool isLooping = true;

        private int _currentFrame;
        private float _timer;
        private bool _isPlaying;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        void Start()
        {
            if (frames.Length == 0)
            {
                Debug.LogError("No frames assigned to the animation.");
                enabled = false;
                return;
            }

            _currentFrame = 0;
            _timer = 0.0f;
            _isPlaying = false;
        }

        void Update()
        {
            if (_isPlaying && frames.Length > 1)
            {
                _timer += Time.deltaTime;
                if (_timer >= 1.0f / framesPerSecond)
                {
                    _timer -= 1.0f / framesPerSecond;
                    _currentFrame++;

                    if (_currentFrame >= frames.Length)
                    {
                        if (isLooping)
                        {
                            _currentFrame = 0;
                        }
                        else
                        {
                            _currentFrame = frames.Length - 1;
                            Stop();
                        }
                    }

                    image.sprite = frames[_currentFrame];
                }
            }
        }

        public void Play()
        {
            _isPlaying = true;
        }

        public void Pause()
        {
            _isPlaying = false;
        }

        public void Stop()
        {
            _isPlaying = false;
            _currentFrame = 0;
            _timer = 0.0f;
            if (frames.Length > 0)
            {
                image.sprite = frames[0];
            }
        }

        public void Replay()
        {
            Stop();
            Play();
        }

        public void SetFrames(Sprite[] newFrames)
        {
            frames = newFrames;
            _currentFrame = 0;
            _timer = 0.0f;
            if (frames.Length > 0)
            {
                image.sprite = frames[0];
            }
        }
    }
}