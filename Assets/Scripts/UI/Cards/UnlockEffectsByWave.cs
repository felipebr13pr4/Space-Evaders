using UnityEngine;

public class UnlockEffectsByWave : UnlockByWave
{
    [SerializeField] private Card m_data;

    protected override void Unlock(int i)
    {
        base.Unlock(i);
        m_data.CardData.Effects[i].Enabled = true;
    }
}