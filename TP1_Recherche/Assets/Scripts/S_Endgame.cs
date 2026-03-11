using UnityEngine;
using TMPro;

public class S_Endgame : MonoBehaviour
{
    
        [SerializeField] private TextMeshProUGUI texteDeFin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            texteDeFin.text = "Félicitations vous avez battu le boss !";
        }
    }
}
