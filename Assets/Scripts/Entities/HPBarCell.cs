using System.Collections;
using System.Collections.Generic;
using System;   
using NeoFortaleza.Runtime.Systems.Behaviors;
using UnityEngine;
using UnityEngine.UI;
public class HPBarCell : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image barImage;
    [SerializeField] private Cell player;
            private void OnEnable()
    {
        Events.onHpLostCell.AddListener(loseHP);
        
    }
    private void OnDisable()
    {
        Events.onHpLostCell.RemoveListener(loseHP);
      
    }
    private void Start() {
        
    }
    private void Update(){
        
        transform.rotation = Quaternion.identity;
    }

    private void loseHP(float loseHP){
        
        barImage.fillAmount = loseHP;  //Receive hp normalized to fill bar
    }



}
