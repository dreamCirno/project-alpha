using UnityEngine;

namespace ProjectAlpha
{
    [CreateAssetMenu(fileName = "NewBeatmap", menuName = "ProjectAlpha/Beatmap")]
    public class Beatmap : ScriptableObject
    {
        [Tooltip("Text file containing the beatmap data")]
        public TextAsset beatmapText;
        
        [Tooltip("Audio file associated with the beatmap")]
        public AudioClip beatmapAudio;
    }
}