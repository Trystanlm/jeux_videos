using UnityEngine;
using UnityEngine.AI;

public class S_Skeletton : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    public Transform joueur;
    public float rayonDetection = 8f;
    public float rayonPatrouille = 10f;
    public float tempsPatrouille = 4f;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        timer = tempsPatrouille;
        agent.speed = 1f;
    }

    void Update()
    {
        if (animator.GetBool("isDead")) return;

        float distanceJoueur = Vector3.Distance(transform.position, joueur.position);

        if (distanceJoueur < rayonDetection)
        {
            // Si le joueur est dans le rayon de détection, le squelette le suit
            agent.SetDestination(joueur.position);
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= tempsPatrouille)
            {
                // Génère une position aléatoire dans le rayon de patrouille
                Vector3 randomPos = transform.position + Random.insideUnitSphere * rayonPatrouille;
                randomPos.y = transform.position.y;

                // Vérifie que la position est bien sur le NavMesh avant de s'y déplacer
                if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, rayonPatrouille, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                }
                timer = 0;
            }
        }

        // Synchronise l'animation avec la vitesse du NavMeshAgent
        animator.SetFloat("Weight", agent.velocity.magnitude);
    }
}