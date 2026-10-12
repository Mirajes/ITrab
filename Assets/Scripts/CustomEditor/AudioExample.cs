using System;
using UnityEngine;

namespace CE
{
    [Serializable]
    public class AudioExample
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField][Range(0f, 1f)] private float _volume = 1f;

        public AudioClip AudioClip => _audioClip;
        public float Volume => _volume;
    }
}
