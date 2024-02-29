using UnityEngine;
using UnityEngine.AI;

namespace NeoFortaleza.Runtime.Systems.Behaviors
{
    public class EnemyAI : MonoBehaviour
    {
        public NavMeshAgent agent;
        public Transform player;
        public LayerMask whatIsGround, whatIsPlayer;
        public Vector3 walkPoint;
        bool walkPointSet;
        public float walkPointRange;
    }
}