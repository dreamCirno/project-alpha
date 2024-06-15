using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace ProjectAlpha
{
    [CustomEditor(typeof(Metronome))]
    public class MetronomeEditor : OdinEditor
    {
        private Metronome metronome;
        private string musicName = "zone1_1"; // 默认音乐名称

        protected override void OnEnable()
        {
            base.OnEnable();
            metronome = (Metronome)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);

            musicName = EditorGUILayout.TextField("Music Name", musicName); // 添加文本框输入音乐名称

            GUILayout.Space(10);

            if (GUILayout.Button("Load"))
            {
                metronome.Load(musicName);
            }

            if (GUILayout.Button("Play"))
            {
                metronome.Play();
            }

            if (GUILayout.Button("Pause"))
            {
                metronome.Pause();
            }

            if (GUILayout.Button("Resume"))
            {
                metronome.Resume();
            }

            if (GUILayout.Button("Stop"))
            {
                metronome.Stop();
            }
        }
    }
}