using System.Collections.Generic;
using UnityEngine;

namespace CE
{
    [CreateAssetMenu(menuName = "SO/Audio")]
    public class SO : ScriptableObject
    {

        [SerializeField] private List<AudioExample> _audios = new();
        [SerializeField] private string _id;
        [SerializeField][TextArea(3, 5)] private string _soDescription;

        [SerializeField] private bool _showList = false;
        [SerializeField] private bool _showDescription = false;

    }
}
