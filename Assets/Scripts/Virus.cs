
using UnityEngine;
using UnityEngine.AI;
using System;



    public class Virus : MonoBehaviour
    {
        [SerializeField] private float HPMax = 3;
        private float HP;


        [SerializeField] private LayerMask collisionMask;
        [SerializeField] private NavMeshAgent virusAgent;


        [SerializeField] private Transform playerTarget;
        [SerializeField] private float interactDistance;
        private UnityEngine.Vector3 FollowPos;
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private float damageCooldown;
        [SerializeField] private GameObject body;
        
        public event EventHandler<OnHPLostEventArgs> OnHPLost;
        [SerializeField] float expAmount = 1;
        private Rigidbody rb;
        public class OnHPLostEventArgs : EventArgs{
            public float hpNormalized;
        } 

        private void Awake()
        {
            playerTarget = GameObject.Find("Cell").transform;
            HP = HPMax;
            rb = GetComponent<Rigidbody>();

        }
        void Update()
        {
              if (HP <= 0)
            {
                //FlyweightFactory.ReturnToPool(this);
                GameObject.Destroy(gameObject);
                ExperienceManager.Instance.AddExperience(expAmount);
            }
            ChasePlayer();
            CheckCollisions(interactDistance);
            
        }
        void ChasePlayer()
        {
            virusAgent.SetDestination(playerTarget.position);
        }
        public void LoseHP()
        {
            HP--;
        OnHPLost?.Invoke(this, new OnHPLostEventArgs{
        hpNormalized = HP/HPMax
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

                hit.collider.GetComponent<Cell>().LoseHP();
            }
        }

    }
        
