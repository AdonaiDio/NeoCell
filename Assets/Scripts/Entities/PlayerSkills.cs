using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A ideia � que o playerSkills seja o lugar com os atributos de combate do jogador.
//Que por padr�o ele tenha as informa��es necess�rias para realizar a a��o do "r�m�dio default".
//por hora vou fazer tudo aqui dentro mas talvez eu separe um ScriptableObject
//para cada rem�dio com suas caracteristicas.
//FAZER DOS DANOS E AREAS DE DANO SPAWNS. ESQUECA OS COLLIDERS!!!!!!

public class PlayerSkills : MonoBehaviour
{
<<<<<<< HEAD:Assets/Scripts/Entities/TEMP/PlayerSkills.cs
=======
    [SerializeField] private List<RemedySO> _remedyList;//Remedios ativos
    private List<RemedySO> _lastRemedyList;//temporario auxiliar
    private List<StatusEffectData> _effects;//recebe os efeitos dos remedios para acesso r�pido

    //colliders
>>>>>>> Adonai:Assets/Scripts/Entities/PlayerSkills.cs
    [SerializeField] private GameObject meleeGO;
    [SerializeField] private GameObject areaGO;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject MineAroundGO;

<<<<<<< HEAD:Assets/Scripts/Entities/TEMP/PlayerSkills.cs
=======
    //FX
    public GameObject fx_melee_prefab;
    public GameObject fx_around_prefab;

    //infos
    public float CollectDropAtDistance = 15f;
    private int enemiesAtOnce = 1;
>>>>>>> Adonai:Assets/Scripts/Entities/PlayerSkills.cs
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

<<<<<<< HEAD:Assets/Scripts/Entities/TEMP/PlayerSkills.cs
=======

    //FMOD
    //[SerializeField] private EventReference sillySound;

>>>>>>> Adonai:Assets/Scripts/Entities/PlayerSkills.cs
    private void Awake()
    {
        _projectileHits = 0;
    }
    private void OnEnable()
    {
        Events.onProjectileHitEnemy.AddListener(ProjectileHit);
        Events.onMineHitEnemy.AddListener(MineAroundHit);
        Events.onRemedyActive.AddListener(AddRemedy);
        Events.onRemedyUpgrade.AddListener(UpgradeRemedy);
    }
    private void OnDisable()
    {
        Events.onProjectileHitEnemy.RemoveListener(ProjectileHit);
        Events.onMineHitEnemy.RemoveListener(MineAroundHit);
        Events.onRemedyActive.RemoveListener(AddRemedy);
        Events.onRemedyUpgrade.RemoveListener(UpgradeRemedy);
    }

    private void FixedUpdate()
    {
        //a cada X tempos DoAction para que o jogador execute todos os danos e spawns nas �reas detect�veis.
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

<<<<<<< HEAD:Assets/Scripts/Entities/TEMP/PlayerSkills.cs
=======
    private void AddRemedy(RemedySO _newRemedySO)
    {
        _remedyList.Add(_newRemedySO);
    }
    private void UpgradeRemedy(RemedySO _newRemedySO)
    {
        foreach (RemedySO remedy in _remedyList)
        {
            if (remedy.GetType() == _newRemedySO.GetType())
            {
                _remedyList.Remove(remedy);
                _remedyList.Add(_newRemedySO);
                return;
            }
        }
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
            //mudou! Ler os rem�dios. Escrever atributos alterados! Quando entra e tal
            foreach (RemedySO r in _remedyList)
            {
                if (r is Remedy_Projectile)
                {
                    //castei o remedio para seu SO correto e usei o auxiliar para pegar os valores espec�ficos.
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

>>>>>>> Adonai:Assets/Scripts/Entities/PlayerSkills.cs
    private void DoAction()
    {
        //executa os efeitos, triggers e danos de remedios ativos
        CollisonDetection();
    }

    private void CollisonDetection()
    {
<<<<<<< HEAD:Assets/Scripts/Entities/TEMP/PlayerSkills.cs
        //agora vou testar o melee
        ShootProjectile();
        AreaDetection();
        MeleeColDetec();
        SpinningAround();
=======
        bool isMeleePointless = false;
        //usar melee se n�o tiver outro dano
        if (_remedyList == null || _remedyList.Count == 0)
        {
            if (!isMeleePointless)
            {
                MeleeColDetec();
                //FX area
                GameObject _fx = Instantiate(fx_melee_prefab, meleeGO.transform);
                //SFX
            }
            return;
        }
        foreach (RemedySO r in _remedyList) {
            //checar se ainda precisa de ataque melee.
            if (r is Remedy_Area || r is Remedy_Projectile ) {
                isMeleePointless = true;
            }
            //checar cada rem�dio com collider
            if (r is Remedy_Projectile)
            {
                ShootProjectile();
                //SFX

            }
            if (r is Remedy_Area)
            {
                AreaDetection();
                //FX area
                GameObject _fx = Instantiate(fx_around_prefab, areaGO.transform);
                float newScaleAxis = areaGO.GetComponent<CapsuleCollider>().radius * 2;
                _fx.transform.localScale = new Vector3(newScaleAxis,newScaleAxis,newScaleAxis);
                //SFX

            }
            if (r is Remedy_Mines)
            {
                SpinningAround();
            }
        }
        if (!isMeleePointless)
        {
            MeleeColDetec();
            //FX area
            GameObject _fx = Instantiate(fx_melee_prefab, meleeGO.transform);
            //SFX

        }
>>>>>>> Adonai:Assets/Scripts/Entities/PlayerSkills.cs
    }
    #region spin around
    private void SpinningAround()
    {
        //se n�o tiver mais de zero minas pra spawnar ou se ainda tiver minas do outro tick
        if (numberOfMines<=0 || spawnedMines.Count > 0) { return; }
        //instanciar todos em x raio de dist�ncia do player distribuido igualmente nos 360�

        Vector3 center = transform.position + new Vector3(0,2.5f,0);//centro do player
        for (int i = 0; i < numberOfMines; i++)
        {
            int angle = 360 / numberOfMines * i;
            Vector3 minePos = RandomCircle(center, mineRadius, angle);
            GameObject mineInst = Instantiate(MineAroundGO, minePos, Quaternion.identity, transform.Find("Pivot").transform);

            spawnedMines.Add(mineInst);
        }

    }
    //Detectar colis�o e tal
    //??
    private void MineAroundHit(Skill_SpinningAround spinScript, Enemy enemy)
    {
        //realizar todos os efeitos que acontecem quando atingir um inimigo
<<<<<<< HEAD:Assets/Scripts/Entities/TEMP/PlayerSkills.cs
        Debug.Log("detonou enemy");
        enemy.LoseHP(damage*20);//sei l�. dano alto
=======
        //Testar a sorte do critico
        float roll = UnityEngine.Random.Range(0f, 100f);
        float criticalMult = 1f;
        bool isCritical = false;
        if (roll <= criticalChance)
        {
            Debug.Log("<color=yellow>!!!! CRITOU !!!!</color>");
            criticalMult = 5f;
            isCritical = true;
        }
        //enemy.LoseHP((damage*_mineDamage)*criticalMult);//sei l�. dano alto
        float damageTotal = (damage*_mineDamage)*criticalMult;//sei l�. dano alto
        //efeito de dano do inimigo. critico ou n�o
        GameObject floatTxt = Instantiate(GetComponent<Player>().floatingDamage, enemy.transform.Find("HPBarUI"));
        floatTxt.GetComponent<DamageIndicator>().damageNumber = damageTotal;
        floatTxt.GetComponent<DamageIndicator>().isCritical = isCritical;
        Events.onDamageEnemy.Invoke(enemy, damageTotal,_effects);
>>>>>>> Adonai:Assets/Scripts/Entities/PlayerSkills.cs
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
<<<<<<< HEAD:Assets/Scripts/Entities/TEMP/PlayerSkills.cs
                temp_enemiesList[i].GetComponent<Enemy>().LoseHP(damage);
=======
                //Testar a sorte do critico
                float roll = UnityEngine.Random.Range(0f, 100f);
                float criticalMult = 1f;
                bool isCritical = false;
                if (roll <= criticalChance)
                {
                    Debug.Log("<color=yellow>!!!! CRITOU !!!!</color>");
                    criticalMult = 5f;
                    isCritical = true;
                }
                Enemy enemy = temp_enemiesList[i].GetComponent<Enemy>();
                float damageTotal = (damage*criticalMult);
                //efeito de dano do inimigo. critico ou n�o
                GameObject floatTxt = Instantiate(GetComponent<Player>().floatingDamage, enemy.transform.Find("HPBarUI"));
                floatTxt.GetComponent<DamageIndicator>().damageNumber = damageTotal;
                floatTxt.GetComponent<DamageIndicator>().isCritical = isCritical;
                Events.onDamageEnemy.Invoke(enemy, damageTotal, _effects);

>>>>>>> Adonai:Assets/Scripts/Entities/PlayerSkills.cs
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
<<<<<<< HEAD:Assets/Scripts/Entities/TEMP/PlayerSkills.cs
                temp_enemiesList[i].GetComponent<Enemy>().LoseHP(damage);
=======
                //Testar a sorte do critico
                float roll = UnityEngine.Random.Range(0f, 100f);
                float criticalMult = 1f;
                bool isCritical = false;
                if (roll <= criticalChance)
                {
                    Debug.Log("<color=yellow>!!!! CRITOU !!!!</color>");
                    criticalMult = 5f;
                    isCritical = true;
                }
                Enemy enemy = temp_enemiesList[i].GetComponent<Enemy>();
                float damageTotal = (damage * criticalMult);
                //efeito de dano do inimigo. critico ou n�o
                GameObject floatTxt = Instantiate(GetComponent<Player>().floatingDamage, enemy.transform.Find("HPBarUI"));
                floatTxt.GetComponent<DamageIndicator>().damageNumber = damageTotal;
                floatTxt.GetComponent<DamageIndicator>().isCritical = isCritical;
                Events.onDamageEnemy.Invoke(enemy, damageTotal, _effects);
>>>>>>> Adonai:Assets/Scripts/Entities/PlayerSkills.cs
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
<<<<<<< HEAD:Assets/Scripts/Entities/TEMP/PlayerSkills.cs
        enemy.LoseHP(damage);
=======
        //Testar a sorte do critico
        float roll = UnityEngine.Random.Range(0f, 100f);
        float criticalMult = 1f;
        bool isCritical = false;
        if (roll <= criticalChance)
        {
            Debug.Log("<color=yellow>!!!! CRITOU !!!!</color>");
            criticalMult = 5f;
            isCritical = true;
        }
        float damageTotal = (damage * criticalMult);
        GameObject floatTxt = Instantiate(GetComponent<Player>().floatingDamage, enemy.transform.Find("HPBarUI"));
        floatTxt.GetComponent<DamageIndicator>().damageNumber = damageTotal;
        floatTxt.GetComponent<DamageIndicator>().isCritical = isCritical;
        Events.onDamageEnemy.Invoke(enemy, damageTotal, _effects);
        //efeito de dano do inimigo. critico ou n�o
>>>>>>> Adonai:Assets/Scripts/Entities/PlayerSkills.cs
        //Limpar da cena caso j� tenha atingido o ultimo inimigo poss�vel
        if (_projectileHits >= enemiesAtOnce)
        {
            Destroy(skill_LineProjectile.gameObject);
        }
    }
    #endregion
}

public class Effect { } //TEMPORARIO