using UnityEngine;



    public class Bullet : MonoBehaviour
    {
        [SerializeField] public LayerMask collisionMask; //enemy collision mask
        private float speed = 0;//going to receive speed from weapon 
       

        public void SetSpeed(float newSpeed)
        {
            speed = newSpeed;//receive speed from weapon
        }

        void Start()
        {
      
        }

        void Update()
        {

            transform.Translate(Vector3.forward * speed * Time.deltaTime);


            CheckCollisions();
        }

        void CheckCollisions()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, speed * Time.deltaTime, collisionMask, QueryTriggerInteraction.Collide))
            {
                OnHitObject(hit);
            }
        }

        void OnHitObject(RaycastHit hit)
        {
            if (hit.collider.GetComponent<Virus>())
            {
                hit.collider.GetComponent<Virus>().LoseHP();
            }
            Destroy(gameObject);
        }
    }
