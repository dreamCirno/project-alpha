using System;
using UnityEngine;

namespace ProjectAlpha
{
    [Serializable]
    public class BeatObject
    {
        public Vector2 Position;
        public int StartTime;
        public int EndTime;
        public bool IsNewCombo;
        public int ComboOffset;

        public BeatObject()
        {
        }

        public BeatObject(Vector2 position, int startTime, int endTime, bool isNewCombo, int comboOffset)
        {
            this.Position = position;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.IsNewCombo = isNewCombo;
            this.ComboOffset = comboOffset;
        }

        public float DistanceFrom(BeatObject otherObject) => Vector2.Distance(this.Position, otherObject.Position);
    }
}