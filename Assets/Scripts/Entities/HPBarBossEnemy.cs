using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HPBarBossEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected Image barImage;
    [SerializeField] protected Canvas HPBarCanvas;
    private void Awake()
    {
        HPBarCanvas.enabled = false;
    }
    private void OnEnable()
    {
        //Events.onHpLostVirus.AddListener(loseHP);
        Events.onBossSpawn.AddListener(onBossSpawn);
        Events.onHpLostBoss.AddListener(OnHPLostBoss);
        Events.onBossDeath.AddListener(onBossDeath);

    }
    private void OnDisable()
    {
        //Events.onHpLostVirus.RemoveListener(loseHP);
        Events.onBossSpawn.RemoveListener(onBossSpawn);
        Events.onHpLostBoss.RemoveListener(OnHPLostBoss);
        Events.onBossDeath.RemoveListener(onBossDeath);
    }



    private void Update()
    {

    }
    private void onBossSpawn(BossEnemy boss)
    {
        HPBarCanvas.enabled = true;

    }
    private void onBossDeath(BossEnemy enemy)
    {
        HPBarCanvas.enabled = false;
    }
    private void OnHPLostBoss (BossEnemy enemy, float hpToFillBar){
        barImage.fillAmount = hpToFillBar;
    }
}
