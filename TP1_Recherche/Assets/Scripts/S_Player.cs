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

    int nbKeys = 0;

    [SerializeField]
    private Transform respawnPosition;

    public bool bomb = false;

    private bool ankhCollected = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


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
        transform.Rotate(new Vector3(0, direction.x * 100f * Time.deltaTime, 0));

        if (direction.y > 0)
            vitesse += acceleration * Time.deltaTime;
        else if (direction.x == 0 && direction.y == 0)
            vitesse -= acceleration * Time.deltaTime;

        vitesse = Mathf.Clamp(vitesse, 0, 1);
        animator.SetFloat("Weight", vitesse);


        Vector3 move = transform.forward * vitesse * 1f;
        move.y = -9.81f; 
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
        if (hit.gameObject.CompareTag("Skeleton") && !ankhCollected)
        {
            Debug.Log("Vous êtes mort !");
            transform.position = respawnPosition.position;
        }
        else if(hit.gameObject.CompareTag("Skeleton") && ankhCollected)
        {
            hit.gameObject.GetComponent<Animator>().SetBool("isDead", true);
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
    }


}
