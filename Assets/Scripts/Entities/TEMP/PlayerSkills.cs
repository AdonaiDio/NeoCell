using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

//A ideia é que o playerSkills seja o lugar com os atributos de combate do jogador.
//Que por padrão ele tenha as informações necessárias para realizar a ação do "rémédio default".
//por hora vou fazer tudo aqui dentro mas talvez eu separe um ScriptableObject
//para cada remédio com suas caracteristicas.
//FAZER DOS DANOS E AREAS DE DANO SPAWNS. ESQUECA OS COLLIDERS!!!!!!

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private List<RemedySO> _remedyList;//Remedios ativos
    private List<RemedySO> _lastRemedyList;//temporario auxiliar
    private List<StatusEffectData> _effects;//recebe os efeitos dos os remedios para acesso rápido

    //colliders
    [SerializeField] private GameObject meleeGO;
    [SerializeField] private GameObject areaGO;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject MineAroundGO;
    //infos
    private int enemiesAtOnce = 1;
    private float damage = 1f;
    private float criticalChance = 0f;
    //projetil
    private int _projectileHits = 0;
    private float projectileThickness = 0f;
    //AreaDamage
    //private float areaRadius = 0f; //alterar radius do capsule collider(areaGO)
    //Mines
    private int numberOfMines = 1;
    private float mineRadius = 2.5f;
    private float _mineDamage = 20f;
    private List<GameObject> spawnedMines;
    //controle do tick
    public float _cooldown = 1f;
    private float _lastTick;

    //FMOD
    [SerializeField] private EventReference sillySound;

    private void Awake()
    {
        _projectileHits = 0;
        _remedyList = new List<RemedySO>();
        _lastRemedyList = new List<RemedySO>();
        _effects = new List<StatusEffectData>();
        spawnedMines = new List<GameObject>();
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
        _lastTick = Time.time;
        CheckForRemedyChange();
        DoAction();
    }

    private void CheckForRemedyChange()
    {
        bool isChanged = false;
        //verificar se mudou
        foreach (RemedySO r in _remedyList)
        {
            if(_remedyList.Count != _lastRemedyList.Count)
            {
                isChanged = true;
                break;
            }
            if (r != _lastRemedyList[_remedyList.IndexOf(r)])
            {
                isChanged = true;
                break;
            }
        }
        if (isChanged)
        {
            //resetar lista auxiliar
            _lastRemedyList.Clear();
            _effects.Clear();
            //mudou! Ler os remédios. Escrever atributos alterados! Quando entra e tal
            foreach (RemedySO r in _remedyList)
            {
                if (r is Remedy_Projectile)
                {
                    //castei o remedio para seu SO correto e usei o auxiliar para pegar os valores específicos.
                    Remedy_Projectile aux_r = (Remedy_Projectile)r;
                    projectileThickness = aux_r._projectileThickness;
                }
                else if (r is Remedy_Area)
                {
                    //'castei' o remedio para seu SO corr...
                    Remedy_Area aux_r = (Remedy_Area)r;
                    areaGO.GetComponent<CapsuleCollider>().radius = aux_r._areaRadius;
                }
                else if (r is Remedy_Mines)
                {
                    //'castei'...
                    Remedy_Mines aux_r = (Remedy_Mines)r;
                    numberOfMines = aux_r._numberOfMines;
                    mineRadius = aux_r._mineRadius;
                }
                else if (r is Remedy_Quantity)
                {
                    //'castei'...
                    Remedy_Quantity aux_r = (Remedy_Quantity)r;
                    enemiesAtOnce = aux_r._enemiesAtOnce;
                }
                else if (r is Remedy_Multiplicator)
                {
                    //'castei'...
                    Remedy_Multiplicator aux_r = (Remedy_Multiplicator)r;
                    damage = aux_r._damage;
                }
                else if (r is Remedy_Critical)
                {
                    //'castei'...
                    Remedy_Critical aux_r = (Remedy_Critical)r;
                    criticalChance = aux_r._criticalChance;
                }
                else if (r is Remedy_Decay)
                {
                    //'castei'...
                    Remedy_Decay aux_r = (Remedy_Decay)r;
                    _effects.Add(aux_r._effect);
                }
                else if (r is Remedy_Explosion)
                {
                    //'castei'...
                    Remedy_Explosion aux_r = (Remedy_Explosion)r;
                    _effects.Add(aux_r._effect);
                }
                else if (r is Remedy_Slowdown)
                {
                    //'castei'...
                    Remedy_Slowdown aux_r = (Remedy_Slowdown)r;
                    _effects.Add(aux_r._effect);
                }
                //guardar a copia da lista na lista auxiliar
                _lastRemedyList.Add(r);
            }
        }
    }

    private void DoAction()
    {
        //executa os efeitos, triggers e danos de remedios ativos
        CollisonDetection();
        //AudioManager.instance.PlayOneShot(sillySound, transform.position); ASSIM QUE USA
    }


    private void CollisonDetection()
    {
        bool isMeleePointless = false;
        //usar melee se não tiver outro dano
        if (_remedyList == null || _remedyList.Count == 0)
        {
            MeleeColDetec();
            return;
        }
        foreach (RemedySO r in _remedyList) {
            //checar se ainda precisa de ataque melee.
            if (r is Remedy_Area || r is Remedy_Projectile ) {
                isMeleePointless = true;
            }
            //checar cada remédio com collider
            if (r is Remedy_Projectile)
            {
                ShootProjectile();
            }
            if (r is Remedy_Area)
            {
                AreaDetection();
            }
            if (r is Remedy_Mines)
            {
                SpinningAround();
            }
        }
        if (!isMeleePointless)
        {
            MeleeColDetec();
        }
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
        //Testar a sorte do critico
        float roll = UnityEngine.Random.Range(0f, 100f);
        float criticalMult = 1f;
        if (roll <= criticalChance)
        {
            Debug.Log("!!!! CRITOU !!!!<color=yellow>");
            criticalMult = 5f;
        }
        //enemy.LoseHP((damage*_mineDamage)*criticalMult);//sei lá. dano alto
        float damageTotal = (damage*_mineDamage)*criticalMult;//sei lá. dano alto
        Events.onDamageEnemy.Invoke(enemy, damageTotal,_effects);
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
                //Testar a sorte do critico
                float roll = UnityEngine.Random.Range(0f, 100f);
                float criticalMult = 1f;
                if (roll <= criticalChance)
                {
                    Debug.Log("!!!! CRITOU !!!!<color=yellow>");
                    criticalMult = 5f;
                }
                Enemy enemy = temp_enemiesList[i].GetComponent<Enemy>();
                float damageTotal = (damage*criticalMult);
                Events.onDamageEnemy.Invoke(enemy, damageTotal, _effects);
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
                //Testar a sorte do critico
                float roll = UnityEngine.Random.Range(0f, 100f);
                float criticalMult = 1f;
                if (roll <= criticalChance)
                {
                    Debug.Log("!!!! CRITOU !!!!<color=yellow>");
                    criticalMult = 5f;
                }
                Enemy enemy = temp_enemiesList[i].GetComponent<Enemy>();
                float damageTotal = (damage * criticalMult);
                Events.onDamageEnemy.Invoke(enemy, damageTotal, _effects);
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
        //adjust thickness
        projInst.transform.localScale = new Vector3(projectileThickness, 0.5f, 0.5f);
    }
    private void ProjectileHit(Skill_LineProjectile skill_LineProjectile, Enemy enemy)
    {
        _projectileHits++;
        //realizar todos os efeitos que acontecem quando atingir um inimigo
        //Testar a sorte do critico
        float roll = UnityEngine.Random.Range(0f, 100f);
        float criticalMult = 1f;
        if (roll <= criticalChance)
        {
            Debug.Log("<color=yellow>!!!! CRITOU !!!!</color>");
            criticalMult = 5f;
        }
        float damageTotal = (damage * criticalMult);
        Events.onDamageEnemy.Invoke(enemy, damageTotal, _effects);
        //Limpar da cena caso já tenha atingido o ultimo inimigo possível
        if (_projectileHits >= enemiesAtOnce)
        {
            Destroy(skill_LineProjectile.gameObject);
        }
    }
    #endregion
}