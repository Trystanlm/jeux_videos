using UnityEngine;
using UnityEngine.AI;

public class S_Animals : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    public float wanderRadius = 15f;
    public float wanderTimer = 4f;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        timer = wanderTimer;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            // Génère une position aléatoire dans le rayon de déplacement
            Vector3 randomPos = transform.position + Random.insideUnitSphere * wanderRadius;
            randomPos.y = transform.position.y;

            // Vérifie que la position est sur le NavMesh avant de s'y déplacer
            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
                agent.SetDestination(hit.position);
            timer = 0;
        }

        // Synchronise les animations avec la vitesse du NavMeshAgent
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Vert", speed);
        animator.SetFloat("State", speed > 0.1f ? 1f : 0f);
    }
}