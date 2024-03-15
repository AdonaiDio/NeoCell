
using UnityEngine;
using UnityEngine.AI;
using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using Unity.VisualScripting;



public class BossEnemy : Enemy
    {
     public static BossEnemy Instance;
     private void Awake(){
         if (Instance != null && Instance != this){
            Destroy(this); 
        }
        else{
            Instance = this;
        }
     }
    private void Start(){
        Events.onBossSpawn.Invoke(this);
    }

    public override void LoseHP()
        {
        HP--;
        float hpToFillBar = HP / HPMax;
        Events.onHpLostBoss.Invoke(this, hpToFillBar);
        //float hpToFillBar = HP/HPMax;
        //hpBarImage.fillAmount = hpToFillBar;

        //EventManager.Instance.OnHPLost?.Invoke(this, new OnHPLostEventArgs{
        //hpToFillBar = HP/HPMax //calc hp bar fill 
        //});

        }

    public override void Die()
    {
        GameObject.Destroy(gameObject);    
        Events.onBossDeath.Invoke(this);
    }



}
        
