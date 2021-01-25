﻿using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Latios.Audio.Authoring
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Latios/Audio/Audio Source")]
    public class LatiosAudioSourceAuthoring : MonoBehaviour
    {
        public AudioClip clip;
        public bool      oneShot;
        public bool      autoDestroyOnFinish;
        public float     volume          = 1f;
        public float     innerRange      = 5f;
        public float     outerRange      = 25f;
        public float     rangeFadeMargin = 1f;
        [Tooltip("The number of unique voices entities instantiated from this converted Game Object may use. Does not apply to OneShot sources.")]
        public int voices;

        [Header("Cone")]
        public bool  useCone;
        public float innerAngle = 30f;
        public float outerAngle = 60f;
        [Range(0f, 1f)]
        public float outerAngleVolume = 0f;
    }
}

