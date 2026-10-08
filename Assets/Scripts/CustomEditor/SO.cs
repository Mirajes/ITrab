using System.Collections.Generic;
using UnityEngine;

namespace CE
{
    [CreateAssetMenu(menuName = "SO/Audio")]
    public class SO : ScriptableObject
    {
        [SerializeField] private string _id;

        [SerializeField] private AudioType _audioType = AudioType.Dangerous;
        [SerializeField] private List<AudioExample> _dangerousAudios;
        [SerializeField] private List<AudioExample> _frienlyAudios;
        [SerializeField] private List<AudioExample> _neutralAudios;

        [SerializeField][TextArea(3, 5)] private string _soDescription;

        [SerializeField] private bool _showList = false;
        [SerializeField] private bool _showDescription = false;
    }
}
