using UnityEditor;
using UnityEngine;

namespace CE
{
    [CustomEditor(typeof(SO))] // choose script to interact
    [CanEditMultipleObjects] // choosing multiple same objects will work with everything 
    public class SOEditor : Editor
    {
        private SerializedProperty _audioListProp;
        private SerializedProperty _descriptionProp;
        private SerializedProperty _showListProp;
        private SerializedProperty _showDescriptionProp;
        private SerializedProperty _idProp;

        private void OnEnable()
        {
            _audioListProp = serializedObject.FindProperty("_audios");
            _descriptionProp = serializedObject.FindProperty("_soDescription");
            _showListProp = serializedObject.FindProperty("_showList");
            _showDescriptionProp = serializedObject.FindProperty("_showDescription");
            _idProp = serializedObject.FindProperty("_id");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_idProp, true);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Show AudioList"))
            {
                _showListProp.boolValue = true;
                _showDescriptionProp.boolValue = false;
            }

            if (GUILayout.Button("Show Description"))
            {
                _showDescriptionProp.boolValue = true;
                _showListProp.boolValue = false;
            }

            if (GUILayout.Button("Hide everything"))
            {
                _showDescriptionProp.boolValue = false;
                _showListProp.boolValue = false;
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(20);

            if (_showListProp.boolValue)
            {
                GUILayout.Label("Audio List:", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(_audioListProp, true);
            }

            if (_showDescriptionProp.boolValue)
            {
                GUILayout.Label("Description:", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(_descriptionProp, true);
            }

            if (!_showListProp.boolValue && !_showDescriptionProp.boolValue) // why
            {
                EditorGUILayout.HelpBox("Press button pls", MessageType.Info);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
