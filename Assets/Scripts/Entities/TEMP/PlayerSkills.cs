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
    [SerializeField] private GameObject projectile;

    private float damage = 1f;
    private List<Effect> effects;
    public int enemiesAtOnce = 1;
    private int _projectileHits = 0;

    public float _cooldown = 1f;
    private float _lastTick;

    private void Awake()
    {
        _projectileHits = 0;
    }
    private void OnEnable()
    {
        Events.onProjectileHitEnemy.AddListener(ProjectileHit);
    }
    private void OnDisable()
    {
        Events.onProjectileHitEnemy.RemoveListener(ProjectileHit);
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
        //executa os efeitos e triggers de remedios ativos
        CollisonDetection();
    }

    private void CollisonDetection()
    {
        //agora vou testar o melee
        MeleeColDetec();
        ShootProjectile();
    }

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
                temp_enemiesList[i].GetComponent<Virus>().LoseHP();
            }
        }
    }

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
    private void ProjectileHit(Skill_LineProjectile skill_LineProjectile, Virus enemy)
    {
        _projectileHits++;
        //realizar todos os efeitos que acontecem quando atingir um inimigo
        enemy.LoseHP();
        //Limpar da cena caso já tenha atingido o ultimo inimigo possível
        if (_projectileHits >= enemiesAtOnce)
        {
            Destroy(skill_LineProjectile.gameObject);
        }
    }
    #endregion
}

public class Effect { } //TEMPORARIO