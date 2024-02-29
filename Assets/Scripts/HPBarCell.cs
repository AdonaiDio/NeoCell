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
    private void Start() {
        player.OnHPLost += Cell_OnHPLost;
    }

    private void Cell_OnHPLost(object sender, Cell.OnHPLostEventArgs e){
        barImage.fillAmount = e.hpNormalized; 
    }



}
