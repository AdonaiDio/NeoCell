using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Events
{
    public static readonly Evt<float> onHpLostCell = new Evt<float>();
    public static readonly Evt<Virus, float> onHpLostVirus = new Evt<Virus, float>();
    public static readonly Evt<float> onXPGained = new Evt<float>();
    public static readonly Evt<Virus> onEnemyDeath = new Evt<Virus>();
    public static readonly Evt<float> onLevelUp = new Evt<float>();

}
public class Evt
{
    private event Action _action = delegate { };

    public void Invoke() => _action.Invoke();
    public void AddListener(Action listener) => _action += listener;
    public void RemoveListener(Action listener) => _action -= listener;
}
public class Evt<T>
{
    private event Action<T> _action = delegate { };

    public void Invoke(T param) => _action.Invoke(param);
    public void AddListener(Action<T> listener) => _action += listener;
    public void RemoveListener(Action<T> listener) => _action -= listener;
}

public class Evt<T0, T1>
{
    private event Action<T0, T1> _action = delegate { };

    public void Invoke(T0 param1, T1 param2) => _action.Invoke(param1, param2);
    public void AddListener(Action<T0, T1> listener) => _action += listener;
    public void RemoveListener(Action<T0, T1> listener) => _action -= listener;
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

