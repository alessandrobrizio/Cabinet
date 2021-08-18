using UnityEditor;
using UnityEngine;

namespace AlessandroBrizio.Cabinet.Logging.Samples.Editor
{
    [CustomEditor(typeof(LoggingDemo))]
    public class LoggingDemoEditor : UnityEditor.Editor
    {
        private LoggingDemo _target;

        private void Awake()
        {
            _target = target as LoggingDemo;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Add Log"))
            {
                _target.AddLog();
            }

            if (GUILayout.Button("Get Log Count"))
            {
                _target.GetLogCount();
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
