using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardData
{
    [SerializeField] private EffectData[] m_effects;
    public EffectData[] Effects => m_effects;
    private int m_maxBuffsAmount;
    public int MaxBuffsAmount { set => m_maxBuffsAmount = value; }
    private int m_maxDebuffsAmount;
    public int MaxDebuffsAmount { set => m_maxDebuffsAmount = value; }
    private bool m_hasUnlockedAimbot;
    public bool HasUnlockedAimbot { set => m_hasUnlockedAimbot = value; }


    // Claude helped me here. The old versions caused some bugs and with the fact the 
    // default callback was to just return buff it made things go really wrong.
    // Like "5 bugs at the same time" wrong. I tried and tried but just couldn't.
    // Check Graveyard for past versions. (Modified)
    public void Randomize()
    {
        int buffsAmt = 0;
        int debuffsAmt = 0;

        for (int i = 0; i < m_effects.Length; i++)
        {
            if (!m_effects[i].Enabled) continue;

            bool needBuff = buffsAmt < m_maxBuffsAmount;
            bool needDebuff = debuffsAmt < m_maxDebuffsAmount;

            if (!needBuff && !needDebuff) break;

            FramedAs target = needBuff ? FramedAs.Buff : FramedAs.Debuff;

            int attempts = 0;
            do
            {
                m_effects[i].Randomize();
                attempts++;
                if (attempts > 500)
                {
                    ErrorLogger.LogError($"Randomizer safety engaged on slot {i}, couldn't land a {target}");
                    break;
                }
            } while (m_effects[i].FramedAs != target || m_effects[i].AreBothAmountsZero
            || m_hasUnlockedAimbot && m_effects[i].IsPlayerStat &&
               m_effects[i].PlayerStatEffect == PlayerStat.Aimbot);

            if (m_effects[i].FramedAs == FramedAs.Buff) buffsAmt++;
            else if (m_effects[i].FramedAs == FramedAs.Debuff) debuffsAmt++;
        }
        CheckIfHealthAndHeal();
    }
    //

    // Thanks paint for being really helpful for visualizing the values.
    // I'll save it to be honest lol. And why not, i'll keep it in the project folder.
    // Incase i do more of those in the future. Its suprisingly nice.
    // But be warned, itss a bit messy and was made just so i could rationalize it on my own.
    // Seems to be working but i yet have to find the double heal before two max healths.
    // Its a bit rare (I might have to manually set it somehow).
    private void CheckIfHealthAndHeal()
    {
        bool has = false;
        int healAmt = 0;
        int maxHealthAmt = 0;
        List<int> healPos = new List<int>();
        List<int> maxHealthPos = new List<int>();
        EffectData[] effects = new EffectData[m_effects.Length];
        for (int i = 0; i < m_effects.Length; i++)
        {
            if (!m_effects[i].IsPlayerStat) continue;
            if (m_effects[i].PlayerStatEffect == PlayerStat.MaxHealth)
            { maxHealthAmt++; effects[i] = m_effects[i]; maxHealthPos.Add(i); }
            if (m_effects[i].PlayerStatEffect == PlayerStat.Heal)
            { healAmt++; effects[i] = m_effects[i]; healPos.Add(i); }
        }
        has = healAmt > 0 && maxHealthAmt > 0;
        if (has)
        {
            int lowestHealPos = 999;
            int highestMaxHealthPos = 0;
            int healsBeforeMaxHealth = 0;
            foreach (int pos in healPos)
            {
                if (pos < lowestHealPos) lowestHealPos = pos;
            }
            foreach (int pos in maxHealthPos)
            {
                if (pos > highestMaxHealthPos) highestMaxHealthPos = pos;
                foreach (int pos2 in healPos)
                {
                    if (pos2 < pos) { healsBeforeMaxHealth++; break; }
                }
            }
            bool areHealsAllBeforeMaxHealth = maxHealthAmt == healsBeforeMaxHealth &&
                                              healAmt == healsBeforeMaxHealth;
            if (areHealsAllBeforeMaxHealth && healAmt != 1)
            {
                ErrorLogger.DebugLog($"1 heal pos effect: {m_effects[healPos[0]].PlayerStatEffect}");
                ErrorLogger.DebugLog($"2 heal pos effect: {m_effects[healPos[1]].PlayerStatEffect}");
                ErrorLogger.DebugLog($"1 highest health pos effect: {m_effects[maxHealthPos[0]].PlayerStatEffect}");
                ErrorLogger.DebugLog($"2 highest health pos effect: {m_effects[maxHealthPos[1]].PlayerStatEffect}");

                ErrorLogger.DebugLog($"1  heal pos: {healPos[0]}");
                ErrorLogger.DebugLog($"2 heal pos: {healPos[1]}");
                ErrorLogger.DebugLog($"1 health pos: {maxHealthPos[0]}");
                ErrorLogger.DebugLog($"2 health pos: {maxHealthPos[1]}");


                m_effects[healPos[0]] = effects[maxHealthPos[1]];
                m_effects[maxHealthPos[0]] = effects[healPos[1]];
                m_effects[healPos[1]] = effects[maxHealthPos[0]];
                m_effects[maxHealthPos[1]] = effects[healPos[0]];
                ErrorLogger.DebugLog("Swapped health and heal positions. (2 heals before 2 max healths version)");
                ErrorLogger.LogError("Swapped health and heal positions. (2 heals before 2 max healths version)");
                return;
            }

            if (lowestHealPos < highestMaxHealthPos)
            {
                ErrorLogger.DebugLog($"lowest heal pos effect: {m_effects[lowestHealPos].PlayerStatEffect}");
                ErrorLogger.DebugLog($"highest health pos effect: {m_effects[highestMaxHealthPos].PlayerStatEffect}");

                ErrorLogger.DebugLog($"lowest heal pos: {lowestHealPos}");
                ErrorLogger.DebugLog($"highest health pos: {highestMaxHealthPos}");
                m_effects[lowestHealPos] = effects[highestMaxHealthPos];
                m_effects[highestMaxHealthPos] = effects[lowestHealPos];
                // error logs for spotting easier.
                ErrorLogger.DebugLog("Swapped health and heal positions.");
                ErrorLogger.LogError("Swapped health and heal positions.");

            }
        }
    }
}