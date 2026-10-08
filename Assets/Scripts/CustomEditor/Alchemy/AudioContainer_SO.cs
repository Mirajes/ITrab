using Alchemy.Inspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CE.Alchemy
{
    [CreateAssetMenu(menuName = "Alchemy")]
    public class AudioContainer_SO : ScriptableObject
    {
        [Header("Settings")]
        [LabelText("ID")]
        [SerializeField] private string _id;

        [ShowIf(nameof(_isAudioAndDescriptionDisabled))]
        [HelpBox("Press button", HelpBoxMessageType.Info)]
        [ReadOnly] [SerializeField] private string _message = "cool";

        [LabelText("Description")]
        [ShowIf(nameof(_isDescriptionEnabled))]
        [SerializeField] [TextArea(3, 5)] private string _description;

        [HorizontalGroup("Buttons")]
        [Button] public void ShowAudios()
        {
            _isAudiosEnabled = true;
            _isDescriptionEnabled = false;
        }

        [HorizontalGroup("Buttons")]
        [Button] public void ShowDescription()
        {
            _isDescriptionEnabled = true;
            _isAudiosEnabled = false;
        }

        [HorizontalGroup("Buttons")]
        [Button] public void HideEverything()
        {
            _isAudiosEnabled = false;
            _isDescriptionEnabled = false;
        }


        [Header("AudioData")]
        [ShowIf(nameof(_isAudiosEnabled))]
        [TabGroup("Audios", "Dangerous")]
        [SerializeField] private List<AudioField> _dangerousAudiosList;

        [ShowIf(nameof(_isAudiosEnabled))]
        [TabGroup("Audios", "Friendly")]
        [SerializeField] private List<AudioField> _friendlyAudiosList;

        [ShowIf(nameof(_isAudiosEnabled))]
        [TabGroup("Audios", "Neutral")]
        [SerializeField] private List<AudioField> _neutralAudiosList;

        [HideInInspector] [SerializeField] private bool _isAudiosEnabled = false;
        [HideInInspector][SerializeField] private bool _isDescriptionEnabled = false;

        private bool _isAudioAndDescriptionDisabled => !_isAudiosEnabled && !_isDescriptionEnabled;
    }

    /* // был бы один можно было бы сделать через VerticalGroup("AudioSection")
     * [ShowIf("AudioSection", nameof(_isAudiosEnabled))]
     * "AudioSection/Audios" для TabGroup
        [System.Serializable]
    public struct AudioTabsGroup
    {
        [TabGroup("Audios", "Dangerous")]
        public List<AudioField> dangerousList;

        [TabGroup("Audios", "Friendly")]
        public List<AudioField> friendlyList;

        [TabGroup("Audios", "Neutral")]
        public List<AudioField> neutralList;
    }
    */
}
