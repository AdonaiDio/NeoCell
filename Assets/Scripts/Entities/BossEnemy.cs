
    using UnityEngine;
    using UnityEngine.AI;
    using System;
    using Microsoft.Unity.VisualStudio.Editor;
    using UnityEngine.UI;
    using Unity.VisualScripting;



    public class BossEnemy : Enemy
    {
        public static BossEnemy Instance;
        public override void Awake()
        {
            base.Awake();
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
        public override void Start()
        {
            base.Start();
            Events.onBossSpawn.Invoke(this);
        }

        public override void LoseHP(float damage = 1)
        {
            HP -= damage;
            float hpToFillBar = HP / HPMax;
            Events.onHpLostBoss.Invoke(this, hpToFillBar);
        }

        public override void Die()
        {
            GameObject.Destroy(gameObject);
            Events.onBossDeath.Invoke(this);
        }



    }

