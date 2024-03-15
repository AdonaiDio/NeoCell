using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] Enemy enemy;
    [SerializeField] Enemy boss;
    [SerializeField] Vector3 spawnArea;
    [SerializeField] float spawnTimer;
    [SerializeField] Player player;
    
    [SerializeField] float spawnDistance;
    [SerializeField] float maxEnemies;
    private int enemiesPoolSize = 3;
    [SerializeField] private float waitSpawnStrongTimer;
    [SerializeField] private float waitSpawnTimerDecrease;
    [SerializeField] private float waitSpawnTimerBoss;
     [SerializeField] private float spawnTimerDecreaseRate;
    
    List<Enemy> enemies = new List<Enemy>();
    
    [SerializeField] private int strongEnemySpawnChance = 0;
    
    [SerializeField] List<EnemySO> weakEnemiesPool = new List<EnemySO>();
    [SerializeField] List<EnemySO> strongEnemiesPool = new List<EnemySO>();
    [SerializeField] List<EnemySO> bossEnemiesPool = new List<EnemySO>();
    [SerializeField] List<Transform> bossSpawnPoints = new List<Transform>();
    private bool hasSpawned = false;
    

     private void OnEnable()
    {
        Events.onEnemyDeath.AddListener(removeEnemy);
        StartCoroutine(StrongerEnemiesCoroutine());   
        StartCoroutine(SpawnIncrease());   
        StartCoroutine(SpawnBoss());   
        
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
        Vector3 pos = RandomCircle (center, spawnDistance, a);
      
        int enemySOIndex = UnityEngine.Random.Range(0, enemiesPoolSize);
          if (UnityEngine.Random.Range(0,100) <= strongEnemySpawnChance ){
            enemy.enemySO = strongEnemiesPool[enemySOIndex];
        }
        else{
        enemy.enemySO = weakEnemiesPool[enemySOIndex];
        } 
        Enemy newEnemy = Instantiate (enemy, pos, Quaternion.identity);
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
    private void removeEnemy(Enemy removedEnemy){        
        enemies.Remove(removedEnemy);
    }
    IEnumerator StrongerEnemiesCoroutine(){
        while (strongEnemySpawnChance < 100){
        yield return new WaitForSeconds(waitSpawnStrongTimer);
       
        strongEnemySpawnChance += 10;
        }
        
    }
     IEnumerator SpawnIncrease(){
        while (true){
            yield return new WaitForSeconds(waitSpawnTimerDecrease);
            spawnTimer = spawnTimer * spawnTimerDecreaseRate;

        }
        
        
    }
        IEnumerator SpawnBoss(){
       
        while (true && hasSpawned == false){
            yield return new WaitForSeconds(waitSpawnTimerBoss);
            int i = 0;
            
            while (i < bossSpawnPoints.Count && hasSpawned == false){
                if (Vector3.Distance(bossSpawnPoints[i].position, player.transform.position) > spawnDistance){
                    int bossSOIndex = UnityEngine.Random.Range(0, enemiesPoolSize);
                    
                    boss.enemySO = bossEnemiesPool[bossSOIndex];
                    
                   
                    
                    Instantiate(boss, bossSpawnPoints[i].position, Quaternion.identity);
                    hasSpawned = true;
                }
                else{
                    i++;
                }
            }

        }
        
        
    }


}
