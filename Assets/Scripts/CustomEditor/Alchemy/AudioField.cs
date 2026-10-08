using System;
using UnityEngine;

namespace CE.Alchemy
{
    [Serializable]
    public class AudioField
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField][Range(0f, 1f)] private float _volume = 1f;
    }
}
