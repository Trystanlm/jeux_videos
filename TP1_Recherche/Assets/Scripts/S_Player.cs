using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class S_Player : MonoBehaviour
{

    Vector2 direction = Vector2.zero;
    float vitesse = 0;
    float acceleration = 0.3f;

    Animator animator;


    int nbKeys = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, direction.x * acceleration, 0));

        vitesse += direction.y * acceleration * Time.deltaTime;
        vitesse = Mathf.Clamp(vitesse, 0, 1);

        animator.SetFloat("Weight", vitesse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            nbKeys++;
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Chest") && nbKeys > 0)
        {
            nbKeys--;
            Destroy(collision.gameObject);
        }
    }
}
