using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A ideia é que o playerSkills seja o lugar com os atributos de combate do jogador.
//Que por padrão ele tenha as informações necessárias para realizar a ação do "rémédio default".
//por hora vou fazer tudo aqui dentro mas talvez eu separe um ScriptableObject
//para cada remédio com suas caracteristicas.
//FAZER DOS DANOS E AREAS DE DANO SPAWNS. ESQUECA OS COLLIDERS!!!!!!

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private GameObject meleeGO;
    [SerializeField] private GameObject areaGO;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject MineAroundGO;

    private float damage = 1f;
    private List<Effect> effects;
    public int enemiesAtOnce = 1;
    private int _projectileHits = 0;
    //info das 'minas' que giram envolta do jogador
    public int numberOfMines = 1;
    [SerializeField] private float mineRadius = 2.5f;
    [SerializeField]//temp
    private List<GameObject> spawnedMines;

    public float _cooldown = 1f;
    private float _lastTick;

    private void Awake()
    {
        _projectileHits = 0;
    }
    private void OnEnable()
    {
        Events.onProjectileHitEnemy.AddListener(ProjectileHit);
        Events.onMineHitEnemy.AddListener(MineAroundHit);
    }
    private void OnDisable()
    {
        Events.onProjectileHitEnemy.RemoveListener(ProjectileHit);
        Events.onMineHitEnemy.RemoveListener(MineAroundHit);
    }

    private void FixedUpdate()
    {
        //a cada X tempos DoAction para que o jogador execute todos os danos e spawns nas áreas detectáveis.
        //
        WaitForTick();
    }

    private void WaitForTick()
    {
        if (Time.time - _lastTick < _cooldown)
        {
            return;
        }
        Debug.Log("Bateu!");
        _lastTick = Time.time;
        DoAction();
    }

    private void DoAction()
    {
        //executa os efeitos, triggers e danos de remedios ativos
        CollisonDetection();
    }

    private void CollisonDetection()
    {
        //agora vou testar o melee
        ShootProjectile();
        AreaDetection();
        MeleeColDetec();
        SpinningAround();
    }
    #region spin around
    private void SpinningAround()
    {
        //se não tiver mais de zero minas pra spawnar ou se ainda tiver minas do outro tick
        if (numberOfMines<=0 || spawnedMines.Count > 0) { return; }
        //instanciar todos em x raio de distância do player distribuido igualmente nos 360º

        Vector3 center = transform.position + new Vector3(0,2.5f,0);//centro do player
        for (int i = 0; i < numberOfMines; i++)
        {
            int angle = 360 / numberOfMines * i;
            Vector3 minePos = RandomCircle(center, mineRadius, angle);
            GameObject mineInst = Instantiate(MineAroundGO, minePos, Quaternion.identity, transform.Find("Pivot").transform);

            spawnedMines.Add(mineInst);
        }

    }
    //Detectar colisão e tal
    //??
    private void MineAroundHit(Skill_SpinningAround spinScript, Enemy enemy)
    {
        //realizar todos os efeitos que acontecem quando atingir um inimigo
        Debug.Log("detonou enemy");
        enemy.LoseHP(damage*20);//sei lá. dano alto
        //limpar da lista de minas ativas e destruir
        spawnedMines.Remove(spinScript.gameObject);
        Destroy(spinScript.gameObject);
    }
    private Vector3 RandomCircle(Vector3 center, float mineRadius, int angle)
    {
        Vector3 pos;
        pos.x = center.x + mineRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.z = center.z + mineRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.y = center.y;
        return pos;
    }
    #endregion
    #region area
    private void AreaDetection()
    {
        if (areaGO.GetComponent<Skill_DetectionTrigger>().enemies.Count == 0)
        {   return; }
        List<GameObject> temp_enemiesList = areaGO.GetComponent<Skill_DetectionTrigger>().enemies;
        int maxOfList;
        if (temp_enemiesList.Count < enemiesAtOnce) {
            maxOfList = temp_enemiesList.Count;
        }
        else {
            maxOfList = enemiesAtOnce;
        }

        for (int i = 0; i < maxOfList; i++)
        {
            if (temp_enemiesList[i] != null)
            {
                //realizar todos os efeitos que acontecem quando atingir um inimigo
                Debug.Log("bateu no inimigo "+(i+1).ToString());
                temp_enemiesList[i].GetComponent<Enemy>().LoseHP(damage);
            }
        }
    }
    #endregion
    #region melee
    private void MeleeColDetec()
    {
        if (meleeGO.GetComponent<Skill_DetectionTrigger>().enemies.Count == 0)
        {
            return;
        }
        List<GameObject> temp_enemiesList = meleeGO.GetComponent<Skill_DetectionTrigger>().enemies;
        int maxOfList;
        if (temp_enemiesList.Count < enemiesAtOnce) {
            maxOfList = temp_enemiesList.Count;
        } else { 
            maxOfList = enemiesAtOnce; 
        }
        
        for (int i = 0; i < maxOfList; i++)
        {
            if (temp_enemiesList[i] != null)
            {
                //realizar todos os efeitos que acontecem quando atingir um inimigo
                temp_enemiesList[i].GetComponent<Enemy>().LoseHP(damage);
            }
        }
    }
    #endregion
    #region Line projectile
    private void ShootProjectile()
    {
        //zerar a contagem
        _projectileHits = 0;
        //instantiate the projectile
        Transform body = transform.Find("Body").transform;
        GameObject projInst = Instantiate(projectile, body.position+(body.up*2)+(body.forward*3), body.rotation);
        Debug.Log("shoot");
    }
    private void ProjectileHit(Skill_LineProjectile skill_LineProjectile, Enemy enemy)
    {
        _projectileHits++;
        //realizar todos os efeitos que acontecem quando atingir um inimigo
        enemy.LoseHP(damage);
        //Limpar da cena caso já tenha atingido o ultimo inimigo possível
        if (_projectileHits >= enemiesAtOnce)
        {
            Destroy(skill_LineProjectile.gameObject);
        }
    }
    #endregion
}

public class Effect { } //TEMPORARIO