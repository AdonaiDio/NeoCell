using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] Virus enemy;
    [SerializeField] Vector3 spawnArea;
    [SerializeField] float spawnTimer;
    [SerializeField] Player player;
    [SerializeField] float minDistance;
    [SerializeField] float maxDistance;
    [SerializeField] float maxEnemies;
    List<Virus> enemies = new List<Virus>();    
     private void OnEnable()
    {
        Events.onEnemyDeath.AddListener(removeEnemy);
        
        
    }
    private void OnDisable()
    {
        Events.onEnemyDeath.RemoveListener(removeEnemy);

        
      
    }
    float timer;

    private void Update(){
        timer -= Time.deltaTime;
        if (timer < 0f && enemies.Count < maxEnemies){
            SpawnEnemy();
            timer = spawnTimer;
        }
    }

    
    private void SpawnEnemy()
    {   
         

        Vector3 center = player.transform.position;
        int a = UnityEngine.Random.Range(1, 360);
        center.y = 0;
        Vector3 pos = RandomCircle (center, maxDistance, a);
        Virus newEnemy = Instantiate (enemy, pos, Quaternion.identity);
        enemies.Add(newEnemy);
        
    }
    Vector3 RandomCircle(Vector3 center, float radius, int a){
        float ang = a;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin (ang * Mathf.Deg2Rad);
        pos.z = center.z + radius * Mathf.Cos (ang * Mathf.Deg2Rad);
        pos.y = center.y;
        return pos;

    }
    private void removeEnemy(Virus removedEnemy){        
        enemies.Remove(removedEnemy);
    }
    


}
