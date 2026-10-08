using System;
using UnityEngine;

namespace CE
{
    [Serializable]
    public class AudioExample
    {
        [SerializeField] private AudioClip _soundClip;
        [SerializeField][Range(0f, 1f)] private float _volume = 1f;
    }
}
