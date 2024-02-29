using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using NeoFortaleza.Runtime.Systems.Behaviors;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance;
    public delegate void ExperienceChangeHandler (float amount);
    public event ExperienceChangeHandler OnExperienceChange;
    public delegate void LevelChangeHandler (float level);
    public event LevelChangeHandler OnLevelUp;
    [SerializeField] TextMeshProUGUI levelText;

    
    [SerializeField] private Cell player;
    [SerializeField] UnityEngine.UI.Image barImage;
    private void Awake(){
        if (Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
        }
        
    }
    private void Update(){
        player.OnLevelUp += Player_OnLevelUp;
    }
    public void AddExperience(float amount){
        OnExperienceChange?.Invoke(amount);
        barImage.fillAmount = player.experienceNormalized;
    }
    public void Player_OnLevelUp(object sender, Cell.OnLevelUpEventArgs e){
        levelText.text = e.currentLevel.ToString();
    }
}
