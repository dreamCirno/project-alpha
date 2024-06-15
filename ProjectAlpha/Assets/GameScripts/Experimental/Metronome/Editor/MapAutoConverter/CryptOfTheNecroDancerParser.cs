using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAlpha
{
    public class CryptOfTheNecroDancerParser : IBeatmapParser
    {
        public MetronomeMap Parse(string content)
        {
            var beatmap = ScriptableObject.CreateInstance<MetronomeMap>();
            var beatObjects = new List<BeatObject>();

            try
            {
                string[] tokens = content.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string token in tokens)
                {
                    if (int.TryParse(token, out int time))
                    {
                        // 假设每个时间点是一个新的 BeatObject
                        var beatObject = new BeatObject
                        {
                            Position = Vector2.zero,  // 默认位置，可以根据需要调整
                            StartTime = time,
                            EndTime = time,
                            IsNewCombo = false,
                            ComboOffset = 0
                        };
                        beatObjects.Add(beatObject);
                    }
                }

                beatmap.BeatObjects = beatObjects;
                return beatmap;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to parse Crypt of the NecroDancer beatmap: {ex.Message}");
                return null;
            }
        }
    }
}