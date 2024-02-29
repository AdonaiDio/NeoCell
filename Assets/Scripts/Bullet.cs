using UnityEngine;



    public class Bullet : MonoBehaviour
    {
        [SerializeField] public LayerMask collisionMask;
        [SerializeField] private float speed = 10f;
        private Camera mainCamera;

        public void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }

        void Start()
        {
            mainCamera = Camera.main;
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
