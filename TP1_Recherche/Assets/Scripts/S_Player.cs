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
        move.y = -9.81f; // gravité
        cc.Move(move * Time.deltaTime);
    }

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
    }
}
