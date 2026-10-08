using UnityEditor;
using UnityEngine;

namespace CE
{
    [CustomEditor(typeof(SO))] // choose script to interact
    [CanEditMultipleObjects] // choosing multiple same objects will work with everything 
    public class SOEditor : Editor
    {
        private SerializedProperty _idProp;

        private SerializedProperty _audiosTypeProp;
        private SerializedProperty _dangerousAudiosListProp;
        private SerializedProperty _friendlyAudiosListProp;
        private SerializedProperty _neutralAudiosListProp;

        private SerializedProperty _descriptionProp;
        private SerializedProperty _showListProp;
        private SerializedProperty _showDescriptionProp;

        private void OnEnable()
        {
            _idProp = serializedObject.FindProperty("_id");
            _audiosTypeProp = serializedObject.FindProperty("_audioType");

            _dangerousAudiosListProp = serializedObject.FindProperty("_dangerousAudios");
            _friendlyAudiosListProp = serializedObject.FindProperty("_frienlyAudios");
            _neutralAudiosListProp = serializedObject.FindProperty("_neutralAudios");

            _descriptionProp = serializedObject.FindProperty("_soDescription");
            _showListProp = serializedObject.FindProperty("_showList");
            _showDescriptionProp = serializedObject.FindProperty("_showDescription");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_idProp, true);
            EditorGUILayout.PropertyField(_audiosTypeProp, true);

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
                AudioType audioType = (AudioType)_audiosTypeProp.enumValueIndex;

                switch (audioType)
                {
                    case AudioType.Dangerous:
                        GUILayout.Label("Dangerous Audios List:", EditorStyles.boldLabel);
                        EditorGUILayout.PropertyField(_dangerousAudiosListProp, true);
                        break;
                    case AudioType.Friendly:
                        GUILayout.Label("Friendly Audios List:", EditorStyles.boldLabel);
                        EditorGUILayout.PropertyField(_friendlyAudiosListProp, true);
                        break;
                    case AudioType.Neutral:
                        GUILayout.Label("Neutral Audios List:", EditorStyles.boldLabel);
                        EditorGUILayout.PropertyField(_neutralAudiosListProp, true);
                        break;
                    default:
                        Debug.LogWarning("[SOEditor] - no enum");
                        return;
                }
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
