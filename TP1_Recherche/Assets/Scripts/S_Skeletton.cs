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
    }

    void Update()
    {
        float distanceJoueur = Vector3.Distance(transform.position, joueur.position);

        if (distanceJoueur < rayonDetection)
        {
            agent.SetDestination(joueur.position);
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= tempsPatrouille)
            {
                Vector3 randomPos = transform.position + Random.insideUnitSphere * rayonPatrouille;
                randomPos.y = transform.position.y;
                if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, rayonPatrouille, NavMesh.AllAreas))
                    agent.SetDestination(hit.position);
                timer = 0;
            }
        }
        animator.SetFloat("Weight", agent.velocity.magnitude);
    }
}