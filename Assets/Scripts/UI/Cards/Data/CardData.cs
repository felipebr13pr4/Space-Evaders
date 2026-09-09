using System;
using UnityEngine;

[Serializable]
public class CardData
{
    [SerializeField] private EffectData[] m_effects;
    public EffectData[] Effects => m_effects;
}