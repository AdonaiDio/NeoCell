    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    [CreateAssetMenu(fileName = "New Enemy", menuName = "EnemySO")]
    public class EnemySO : ScriptableObject
    {
        public string enemyName; //virus, bacteria, fungus
        public string enemyType; //weak, strong and boss
        public float health;
        public float damage;
        public Material material; //eventually going to be model/animator, just for test


    }
