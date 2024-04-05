
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;



public class Enemy : MonoBehaviour
{
    public EnemySO enemySO;
    [SerializeField] protected string enemyName;
    [SerializeField] protected string type;

    [SerializeField] protected float HPMax; //Max HP
    [SerializeField] protected float HP; //Used to store current HP
    protected HPBarEnemy hpBarEnemy;
    [SerializeField] protected float damage;
    protected float speed = 3f;

    protected LayerMask collisionMask;
    protected Transform playerTarget;


    [SerializeField] protected float damageCooldown; //Damage cooldown
    protected float LastDamageTime; //Store last damage Time.time
    protected GameObject body;
    [SerializeField] protected GameObject dnaDrop;
    [SerializeField] protected GameObject medicineDrop;
    [SerializeField] private float medicineDropRate = 40f;///temp

    private List<StatusEffectData> effects;//recebe os efeitos do golpe do player
    public GameObject explosionGO;
    public GameObject explosionIcon_prefab;
    private GameObject explosionIconGO;

    protected Rigidbody rb;

    private bool doingTask = false;
    public virtual void Awake()
    {
        playerTarget = FindFirstObjectByType<Player>().transform; //Find player target to follow
        body = transform.Find("Body").gameObject;
        collisionMask = (1 << 3); //player layer is 3
        rb = GetComponent<Rigidbody>();
        hpBarEnemy = transform.Find("HPBarUI").GetComponent<HPBarEnemy>();
        effects = new List<StatusEffectData>();
    }

    public void OnEnable()
    {
        Events.onDamageEnemy.AddListener(ReceveDamage);
    }
    public void OnDisable()
    {
        Events.onDamageEnemy.RemoveListener(ReceveDamage);
    }


    public virtual void Start()
    {
        enemyName = enemySO.enemyName;
        type = enemySO.enemyType;
        HPMax = enemySO.health;
        HP = HPMax; //Start with max HP
        damage = enemySO.damage;
        speed = 6f;
        GetComponent<NavMeshAgent>().speed = speed;

        //body.GetComponent<Renderer>().material = enemySO.material;

        scaleWithType();
        doingTask = false;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.sfx_gameplay_enemy_spawn, transform.position);
    }

    private void Update()
    {
        ChasePlayer(); //follow player
        //CheckCollisions(interactDistance);//check collisions to do damage
                                          //hpBarEnemy.transform.rotation = Quaternion.identity;
    }
    #region Relacionados a Efeitos
    private void FixedUpdate()
    {
        if (!doingTask) WaitForTicks();
    }

    private float _firstTick_Slow = 0;
    //private float _lastTick_Slow = 0;

    private float _firstTick_Decay = 0;//usar para checar o Lifetime
    private float _lastTick_Decay = 0;//usar para cada tick

    private float _firstTick_Explosion = 0;
    //private float _lastTick_Explosion = 0;
    //private float _lastTick_Heal = 0;
    private void WaitForTicks()
    {
        doingTask = true;
        //DO
        if (effects.Count == 0)
        {
            doingTask = false;
            return;
        }

        bool hasToRemoveSlow = false;
        bool hasToRemoveDecay = false;
        bool hasToRemoveExplosion = false;

        foreach (StatusEffectData fx in effects)
        {
            if (fx.Type == StatusEffectType.Speed)
            {
                //tempo de vida do efeito
                if (Time.time - _firstTick_Slow > fx.Lifetime)
                {
                    //retornar a velocidade normal
                    GetComponent<NavMeshAgent>().speed = speed;
                    //preciso saber quando e quem remover da lista fora do for
                    hasToRemoveSlow = true;
                }
            }
            else if (fx.Type == StatusEffectType.DamageOverTime)
            {
                //tempo de vida do efeito
                if (Time.time - _firstTick_Decay <= fx.Lifetime)
                {
                    //age conforme o proprio tickSpeed
                    if (Time.time - _lastTick_Decay >= fx.TickSpeed)
                    {
                        _lastTick_Decay = Time.time;
                        //causar dano (com efeito?)
                        LoseHP(fx.Amount);
                    }
                }
                else
                {
                    //preciso saber quando e quem remover da lista fora do for
                    hasToRemoveDecay = true;
                }
            }
            else if (fx.Type == StatusEffectType.Explosion)
            {
                //tempo de vida do efeito
                if (Time.time - _firstTick_Explosion > fx.Lifetime)
                {
                    //n�o faz nada durante os ticks s� espera acabar para desabilitar o efeito
                    hasToRemoveExplosion = true;
                    Destroy(explosionIconGO);
                }
            }
            //if (fx.Type == StatusEffectType.Heal)
            //{

            //}
        }
        //Hora de remover de fato o efeito que viveu todo o lifetime da lista
        for (int i = effects.Count - 1; i >= 0; i--)
        {
            if (effects[i].Type == StatusEffectType.Speed && hasToRemoveSlow)
            {
                effects.Remove(effects[i]);
            }
            else if (effects[i].Type == StatusEffectType.DamageOverTime && hasToRemoveDecay)
            {
                effects.Remove(effects[i]);
            }
            else if (effects[i].Type == StatusEffectType.Explosion && hasToRemoveExplosion)
            {
                effects.Remove(effects[i]);
            }
        }
        doingTask = false;
    }
    public void ReceveDamage(Enemy _enemy, float _damage, List<StatusEffectData> _effects)
    {
        if (_enemy != this)
        {
            return;
        }
        LoseHP(_damage);
        //se puder, mudar os efeitos.
        if (_effects.Count > 0)
        {
            //checar se mudou os efeitos
            bool isEffectsChanged = false;
            if (_effects.Count != effects.Count)
            {
                isEffectsChanged = true;
            }
            else
            {
                for (int i = 0; i < effects.Count; i++)
                {
                    if (effects[i] != _effects[i])
                    {
                        isEffectsChanged = true;
                        break;
                    }
                }
            }
            if (!isEffectsChanged)
            {
                return;
            }
            //n�o tem jeito, mudou mesmo. Logo:
            effects.Clear();
            foreach (StatusEffectData fx in _effects)
            {
                //add a nova lista de efeitos
                effects.Add(fx);
                //iniciar o timer do Lifetime deste efeito
                if (fx.Type == StatusEffectType.Speed)
                {
                    _firstTick_Slow = Time.time;
                    //alterar a velocidade do enemy
                    GetComponent<NavMeshAgent>().speed = speed * fx.Amount;
                }
                else if (fx.Type == StatusEffectType.DamageOverTime)
                {
                    _firstTick_Decay = Time.time;
                    //n�o faz nada ao iniciar efeito, s� durante os ticks
                }
                else if (fx.Type == StatusEffectType.Explosion)
                {
                    _firstTick_Explosion = Time.time;
                    //nao faz nada ao iniciar efeito no maximo algum indicativo visual
                    explosionIconGO = Instantiate(explosionIcon_prefab, hpBarEnemy.transform);
                }
            }
        }
    }
    #endregion

    public void scaleWithType()
    {
        if (type == "Strong")
        {
            float size = transform.localScale.x * 1.25f;
            transform.localScale += new Vector3(size, size, size);
        }
    }
    public void ChasePlayer()
    {
        //persegue jogador enquanto a distancia dele para o jogador for
        //maior que a soma do tamanho de ambos colliders
        float desiredDistance = playerTarget.GetComponent<CapsuleCollider>().radius 
            * playerTarget.transform.localScale.x 
            + (GetComponent<CapsuleCollider>().radius * transform.localScale.x);

        if (Vector3.Distance(transform.position, playerTarget.position) > desiredDistance) {
            GetComponent<NavMeshAgent>().SetDestination(playerTarget.position);
        }
        else {
            GetComponent<NavMeshAgent>().SetDestination(transform.position);
        }
    }

    //public void CheckCollisions(float interactDistance)
    //{
      
    //    Ray ray = new Ray(body.transform.position, body.transform.forward);
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit, interactDistance, collisionMask, QueryTriggerInteraction.Collide))
    //    {
    //        OnHitObject(hit);
    //    }
    //}
    void OnTriggerStay(Collider col)
    {
        if(col.GetComponent<Player>())
        {
            if (Time.time - LastDamageTime > damageCooldown) { 
                col.GetComponent<Player>().LoseHP(damage); //Damage player
                LastDamageTime = Time.time;
            }
        }
    }

    //public void OnHitObject(Collider hit)
    //{
    //    if (hit.collider.GetComponent<Player>())
    //    {
    //        if (Time.time - LastDamageTime < damageCooldown)
    //        { //Calc damage cooldown

    //        }
    //        else
    //        {
    //            hit.collider.GetComponent<Player>().LoseHP(damage); //Damage player
    //            LastDamageTime = Time.time;
    //        }
    //    }
    //}
    public virtual void LoseHP(float damage = 1)
    {
        HP -= damage;
        float hpToFillBar = HP / HPMax;
        hpBarEnemy.barImage.fillAmount = hpToFillBar;

        if (HP <= 0) //Die
        {
            if (effects.Count != 0)
            {
                //antes de morrer ver se vai morrer com efeito de explos�o
                foreach (StatusEffectData fx in effects)
                {
                    if (fx.Type == StatusEffectType.Explosion)
                    {
                        //hora de fazer a explos�o!!!
                        GameObject explo = Instantiate(explosionGO);
                        explo.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                        explo.GetComponent<ExplosionScript>().damage = damage;
                        explo.GetComponent<ExplosionScript>().finalSize = fx.Amount;
                        //KABUM!
                        AudioManager.instance.PlayOneShot(FMODEvents.instance.sfx_gameplay_atack_explose, transform.position);
                    }
                }
            }
            Die();
        }
    }
    public virtual void Die()
    {
        float rand = UnityEngine.Random.Range(-1f, 1f);
        float rand3 = UnityEngine.Random.Range(-1f, 1f);
        Vector3 lootSpawnPoint = new Vector3(rand+transform.position.x, 
                                                1f, 
                                                rand3 + transform.position.z);
        //lootSpawnPoint.y = 1f;

        Instantiate(dnaDrop, lootSpawnPoint, dnaDrop.transform.rotation);

        rand = UnityEngine.Random.Range(-1f, 1f);
        rand3 = UnityEngine.Random.Range(-1f, 1f);
        lootSpawnPoint = new Vector3(rand + transform.position.x,
                                                1f,
                                                rand3 + transform.position.z);

        if (medicineDropRate >= UnityEngine.Random.Range(1f,100f) && medicineDropRate != 0 
            && InventoryManager.Instance.HasRemedyPool())
        {
            Instantiate(medicineDrop, lootSpawnPoint, medicineDrop.transform.rotation);
        }

        Events.onEnemyDeath.Invoke(this);

        Destroy(gameObject);
    }



}

