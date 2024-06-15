using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace ProjectAlpha
{
    [CustomEditor(typeof(Metronome))]
    public class MetronomeEditor : OdinEditor
    {
        private Metronome metronome;

        protected override void OnEnable()
        {
            base.OnEnable();
            metronome = (Metronome)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);

            if (GUILayout.Button("LoadFinixe"))
            {
                metronome.LoadFinixe();
            }

            if (GUILayout.Button("Loadzone1_1"))
            {
                metronome.Loadzone1_1();
            }

            if (GUILayout.Button("Loadzone1_2"))
            {
                metronome.Loadzone1_2();
            }

            if (GUILayout.Button("Loadzone1_3"))
            {
                metronome.Loadzone1_3();
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