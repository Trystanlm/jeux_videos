using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class S_Player : MonoBehaviour
{

    Vector2 direction = Vector2.zero;
    float vitesse = 0;
    float acceleration = 0.3f;

    Animator animator;
    CharacterController cc;

    [SerializeField]
    S_Controller controller;

    [SerializeField] 
    Transform skeleton;

    int nbKeys = 0;

    [SerializeField]
    private GameObject faisceau;

    [SerializeField]
    private GameObject crypt;

    [SerializeField]
    private Transform respawnPosition;

    [SerializeField]
    private Transform sphereFollow;

    public bool bomb = false;

    private bool ankhCollected = false;


    void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    // Update is called once per frame
    

    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        sphereFollow.position = transform.position + new Vector3(0, 20, -5); // Positionne la sphère de suivi au-dessus du joueur

        if (skeleton != null)
        {
            // Calcul de la distance entre le skelette et le joueur
            float distance = Vector3.Distance(transform.position, skeleton.position);
            if (distance < 0.5f)
            {
                if (!ankhCollected)
                {
                    cc.enabled = false;
                    transform.position = respawnPosition.position;
                    cc.enabled = true;
                }
                else
                {
                    // Le squelette meurt
                    skeleton.GetComponent<Animator>().SetBool("isDead", true);
                    faisceau.SetActive(true);
                    crypt.SetActive(true);
                    skeleton = null; 
                }
            }
        }


        transform.Rotate(new Vector3(0, direction.x * 100f * Time.deltaTime, 0));

        // Si le joueur appuie vers l'avant, on accélère progressivement
        if (direction.y > 0)
        {
            vitesse += acceleration * Time.deltaTime;
        }   
        // Si aucune touche n'est appuyée, on décélère progressivement
        else if (direction.x == 0 && direction.y == 0)
        {
            vitesse -= acceleration * Time.deltaTime;
        }

        // On s'assure que la vitesse reste entre 0 et 1
        vitesse = Mathf.Clamp(vitesse, 0, 1);

        // On met à jour l'animation du personnage selon la vitesse
        animator.SetFloat("Weight", vitesse);

        // On calcule le vecteur de déplacement vers l'avant du personnage
        Vector3 move = transform.forward * vitesse * 1f;

        // On applique la gravité sur l'axe Y pour que le personnage reste au sol
        move.y = -9.81f;

        // On déplace le personnage via le CharacterController qui gère les collisions
        cc.Move(move * Time.deltaTime);
    }


    /*Utilisation de la fonction OnControllerColliderHit pour détecter les collisions avec les objets clés et coffre car nous utilisons un CharacterController pour le mouvement du joueur, 
     * qui ne génère pas de collisions physiques traditionnelles. 
     * Cette fonction est appelée chaque fois que le CharacterController entre en collision avec un objet, 
     * ce qui nous permet de vérifier si cet objet est une clé ou un coffre et d'agir en conséquence.
     * Car avec un simple capsule collider, les collisions ne sont pas détectées de la même manière que les collisions physiques à cause des animations et du mouvement du personnage, 
     * ce qui peut entraîner des problèmes de détection de collision.
    */
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Key"))
        {
            nbKeys++;
            Destroy(hit.gameObject);
            controller.collectKey();
        }
        if (hit.gameObject.CompareTag("Chest") && nbKeys > 0)
        {
            nbKeys--;
            controller.useKey();
        }
        if (hit.gameObject.CompareTag("Ankh"))
        {
            Destroy(hit.gameObject);
            ankhCollected = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BombPosition") && bomb)
        {
            Debug.Log("Bombe utilisée !");
            bomb = false;
            controller.usedBomb();
        }

        if(other.CompareTag("Crypt"))
        {
            controller.EndGame();
        }
    }


}
