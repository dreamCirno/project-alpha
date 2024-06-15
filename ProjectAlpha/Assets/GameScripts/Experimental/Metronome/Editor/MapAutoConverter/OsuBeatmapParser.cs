using System;
using System.Linq;
using OsuParsers.Decoders;
using UnityEngine;

namespace ProjectAlpha
{
    public class OsuBeatmapParser : IBeatmapParser
    {
        public MetronomeMap Parse(string content)
        {
            var beatmap =
                BeatmapDecoder.Decode(content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));

            var metronomeMap = ScriptableObject.CreateInstance<MetronomeMap>();
            foreach (var o in from hitObject in beatmap.HitObjects
                     where hitObject.TotalTimeSpan.Milliseconds == 0
                     select new BeatObject
                     {
                         StartTime = hitObject.StartTime,
                         EndTime = hitObject.EndTime,
                     })
            {
                metronomeMap.BeatObjects.Add(o);
            }

            return metronomeMap;
        }
    }
}