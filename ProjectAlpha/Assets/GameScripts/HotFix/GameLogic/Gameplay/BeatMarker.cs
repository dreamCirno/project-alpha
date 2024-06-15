using UnityEngine;

namespace ProjectAlpha
{
    public class BeatMarker : MonoBehaviour
    {
        public RectTransform LeftRectTransform;
        public RectTransform RightRectTransform;
        public Vector2 LeftStartPoint;
        public Vector2 RightStartPoint;
        public float Time;

        private MarkerSpawner _spawner;

        public void Init(MarkerSpawner spawner, float time)
        {
            _spawner = spawner;
            Time = time;
            LeftStartPoint = new Vector2(-Time, 0);
            RightStartPoint = new Vector2(Time, 0);
        }

        private void Update()
        {
            // 计算当前时间在移动过程中的百分比
            float percent = Mathf.Clamp01(MetronomePlayer.Current.AudioTime / Time);
            MoveTo(LeftRectTransform, LeftStartPoint, percent);
            MoveTo(RightRectTransform, RightStartPoint, percent);

            // 如果已经到达终点，停止更新位置
            if (percent >= 1)
            {
                _spawner.Release(this);
            }
        }

        private void MoveTo(RectTransform point, Vector2 startPos, float percent)
        {
            // 在起点和终点之间插值
            point.localPosition = Vector2.Lerp(startPos, Vector2.zero, percent);
        }
    }
}