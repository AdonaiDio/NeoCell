
using UnityEngine;
using UnityEngine.AI;
using System;



    public class Virus : MonoBehaviour
    {
        [SerializeField] private float HPMax = 3; //Max HP
        private float HP; //Used to store current HP


        [SerializeField] private LayerMask collisionMask;
        [SerializeField] private NavMeshAgent virusAgent;


        [SerializeField] private Transform playerTarget; 
        [SerializeField] private float interactDistance;
  
     
        [SerializeField] private float damageCooldown; //Damage cooldown
        private float lastDamage; //Store last damage Time.time
        [SerializeField] private GameObject body;
        
        public event EventHandler<OnHPLostEventArgs> OnHPLost; 
        [SerializeField] float expAmount = 1; 
        private Rigidbody rb;
       
        public class OnHPLostEventArgs : EventArgs{
            public float hpToFillBar; //Variable used to calculate fill on HP Bar
        } 

        private void Awake()
        {
            playerTarget = GameObject.Find("Cell").transform; //Find Cell target to follow
            HP = HPMax; //Start with max HP
            rb = GetComponent<Rigidbody>();

        }
        void Update()
        {
              if (HP <= 0) //Die
            {
                //FlyweightFactory.ReturnToPool(this);
                GameObject.Destroy(gameObject); 
                ExperienceManager.Instance.AddExperience(expAmount); //send XP after death, maybe convert to dna drop later
            }
            ChasePlayer(); //follow player
            CheckCollisions(interactDistance);//check collisions to do damage
            
        }
        void ChasePlayer()
        {
            virusAgent.SetDestination(playerTarget.position);
        }
        public void LoseHP()
        {
        HP--;
        OnHPLost?.Invoke(this, new OnHPLostEventArgs{
        hpToFillBar = HP/HPMax //calc hp bar fill 
        });

        }
        void CheckCollisions(float interactDistance)
        {

            Ray ray = new Ray(body.transform.position, body.transform.forward); 
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDistance, collisionMask, QueryTriggerInteraction.Collide))
            {
                OnHitObject(hit);


            }

        }

        void OnHitObject(RaycastHit hit)
        {
            if (hit.collider.GetComponent<Cell>())
            {
                if (Time.time - lastDamage < damageCooldown ){ //Calc damage cooldown
              
                }
                else{
                hit.collider.GetComponent<Cell>().LoseHP(); //Damage player
                lastDamage = Time.time;
                }
            }
        }

    }
        
