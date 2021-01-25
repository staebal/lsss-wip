﻿using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Latios.Audio
{
    internal struct ClipFrameLookup : IEquatable<ClipFrameLookup>
    {
        public BlobAssetReference<AudioClipBlob> clip;
        public int                               spawnFrameOrOffsetIndex;

        public unsafe bool Equals(ClipFrameLookup other)
        {
            return ((ulong)clip.GetUnsafePtr()).Equals((ulong)other.clip.GetUnsafePtr()) && spawnFrameOrOffsetIndex == other.spawnFrameOrOffsetIndex;
        }

        public unsafe override int GetHashCode()
        {
            return new int2((int)((ulong)clip.GetUnsafePtr() >> 4), spawnFrameOrOffsetIndex).GetHashCode();
        }
    }

    internal struct Weights
    {
        public FixedListFloat512 channelWeights;
        public FixedListFloat128 itdWeights;

        public static Weights operator + (Weights a, Weights b)
        {
            Weights result = a;
            for (int i = 0; i < a.channelWeights.Length; i++)
            {
                result.channelWeights[i] += b.channelWeights[i];
            }
            for (int i = 0; i < a.itdWeights.Length; i++)
            {
                result.itdWeights[i] += b.itdWeights[i];
            }
            return result;
        }
    }

    internal struct ListenerBufferParameters
    {
        public int bufferStart;
        public int leftChannelsCount;
        public int samplesPerChannel;
        public int subFramesPerFrame;
    }

    internal struct ListenerWithTransform
    {
        public AudioListener  listener;
        public RigidTransform transform;
    }

    internal struct OneshotEmitter
    {
        public AudioSourceOneShot     source;
        public RigidTransform         transform;
        public AudioSourceEmitterCone cone;
        public bool                   useCone;
    }

    internal struct LoopedEmitter
    {
        public AudioSourceLooped      source;
        public RigidTransform         transform;
        public AudioSourceEmitterCone cone;
        public bool                   useCone;
    }
}

