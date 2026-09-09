using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnlockByWave : MonoBehaviour
{
    [SerializeField] private GameObject[] m_objs;
    [SerializeField] private int m_waveCheck = 10;
    [SerializeField] private int m_waveCheckAddition = 10;
    private List<GameObject> m_activatedObjs = new(0);

    private void Start()
    {
        EnemyHandler.OnWaveStartWithNumber += CheckIfUnlocked;
    }

    private void OnDestroy()
    {
        EnemyHandler.OnWaveStartWithNumber -= CheckIfUnlocked; 
    }

    private void CheckIfUnlocked(int wave)
    {
        int waveCheck = m_waveCheck;
        for(int i = 0; i < m_objs.Length; i++)
        {
            print($"temp waveCheck: {waveCheck}. I am: {gameObject.name}");
            if (m_activatedObjs != null && m_activatedObjs.Contains(m_objs[i]))
            { waveCheck += m_waveCheckAddition; continue; }
            if (m_objs[i].activeSelf) continue;
            if (waveCheck <= wave) Unlock(i);
            waveCheck += m_waveCheckAddition;
        }
    }

    protected virtual void Unlock(int i)
    {
        m_objs[i].SetActive(true);
        m_activatedObjs.Add(m_objs[i]);
    }
}