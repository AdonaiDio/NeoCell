    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using System;


    public class HPBarEnemy : MonoBehaviour
    {
        public Image barImage;
        public Canvas HPBarCanvas;
        //private void OnEnable()
        //{
        //    Events.onHpLostEnemy.AddListener(loseHP);
        //}
        //private void OnDisable()
        //{
        //    Events.onHpLostEnemy.RemoveListener(loseHP);
        //}
        private void Update()
        {
            HPBarCanvas.transform.rotation = Quaternion.identity; //Stay in place when virus rotates
        }
        //public void loseHP(Enemy enemy, float hpToFillBar)
        //{
        //    enemy.hpBarImage.fillAmount = hpToFillBar; //calc hp fill
        //}
    }
