using UnityEditor;
using UnityEngine;

namespace AlessandroBrizio.Cabinet.ScoreManagement.Samples.Editor
{
    [CustomEditor(typeof(ScoreManagementDemo))]
    public class ScoreManagementDemoEditor : UnityEditor.Editor
    {
        private ScoreManagementDemo _target;

        private void Awake()
        {
            _target = target as ScoreManagementDemo;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Add Score"))
            {
                _target.AddScore();
            }

            if (GUILayout.Button("Retrieve Top Scores"))
            {
                _target.RetrieveTopScores();
            }

            if (GUILayout.Button("Remove All Scores"))
            {
                _target.RemoveAllScores();
            }

            if (GUILayout.Button("Read Settings"))
            {
                _target.ReadSettings();
            }

            if (GUILayout.Button("Reload Scene"))
            {
                _target.ReloadScene();
            }
        }
    }
}
