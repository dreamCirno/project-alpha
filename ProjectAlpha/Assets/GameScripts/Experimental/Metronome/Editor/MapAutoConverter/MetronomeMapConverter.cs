using System;
using UnityEngine;

namespace ProjectAlpha
{
    public static class MetronomeMapConverter
    {
        public static MetronomeMap ConvertOsu(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                Debug.LogError("Content is null or empty.");
                return null;
            }

            try
            {
                var parser = BeatmapParserFactory.CreateOsuParser();
                return parser.Parse(content);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to convert osu beatmap: {ex.Message}");
                return null;
            }
        }

        public static MetronomeMap ConvertCryptOfTheNecroDancer(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                Debug.LogError("Content is null or empty.");
                return null;
            }

            try
            {
                var parser = BeatmapParserFactory.CreateCryptOfTheNecroDancerParser();
                return parser.Parse(content);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to convert Crypt of the NecroDancer beatmap: {ex.Message}");
                return null;
            }
        }
    }
}