using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventManager
{
    public static readonly OnHPLostCell<float> onHpLostCell = new OnHPLostCell<float>();
        public static readonly OnHPLostVirus<Virus, float> onHpLostVirus = new OnHPLostVirus<Virus, float>();
    public static readonly OnXPGained<float> onXPGained = new OnXPGained<float>();
    public static readonly OnLevelUp<float> onLevelUp = new OnLevelUp<float>();
  public class OnHPLostCell<T> {
    private event Action<T> hpLost = delegate{ };
    public void Invoke(T param) => hpLost.Invoke(param);
    public void AddListener(Action<T> listener) => hpLost += listener;
    public void RemoveListener(Action<T> listener) => hpLost -= listener;
  }
    public class OnHPLostVirus<T0, T1> {
    private event Action<T0, T1> hpLost = delegate{ };
    public void Invoke(T0 param0, T1 param1) => hpLost.Invoke(param0, param1);
    public void AddListener(Action<T0, T1> listener) => hpLost += listener;
    public void RemoveListener(Action<T0, T1> listener) => hpLost -= listener;
  }
    public class OnXPGained<T> {
    private event Action<T> xpGained = delegate{ };
    public void Invoke(T param) => xpGained.Invoke(param);
    public void AddListener(Action<T> listener) => xpGained += listener;
    public void RemoveListener(Action<T> listener) => xpGained -= listener;

  }
    public class OnLevelUp <T>{
    private event Action<T> levelUp = delegate{ };
    public void Invoke(T param) => levelUp.Invoke(param);
    public void AddListener(Action<T> listener) => levelUp += listener;
    public void RemoveListener(Action<T> listener) => levelUp -= listener;
    
  }


   /*/public event EventHandler<OnHPLostEventArgs> OnHPLost; //Send to HP Bar
       
        public class OnHPLostEventArgs : EventArgs{
            public float hpToFillBar;
        }
        public event EventHandler<OnLevelUpEventArgs> OnLevelUp; // Send to Level TextMesh on ExperienceManager
        public class OnLevelUpEventArgs : EventArgs{
            public float currentLevel;
        }
        
        private void Awake(){
        if (Instance != null && Instance != this){
            Destroy(this); 
        }
        else{
            Instance = this;
        } //Applying Singleton
        
    } 
        private void OnEnable(){
        
        ExperienceManager.Instance.OnExperienceChange += HandleXP; //Receive XP change from Experience Manager
    }
        private void OnDisable(){
        
        ExperienceManager.Instance.OnExperienceChange -= HandleXP;
    }
        private void Update(){

        }
        /*/
}
