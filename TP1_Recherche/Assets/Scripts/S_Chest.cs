using UnityEngine;

public class S_Chest : MonoBehaviour
{
    public Transform couvercle;
    float depart;
    float arrivee = -120f;
    float t = 0;
    float temps = 2;
    bool ouvert = false;

    public GameObject bombe;


    void Start()
    {
        // Récupère la vraie rotation de départ
        depart = couvercle.localEulerAngles.z;
    }

    void Update()
    {
        if (ouvert)
        {
            t += Time.deltaTime;
            float angleZ = Mathf.Lerp(depart, arrivee, t / temps);
            Vector3 rot = couvercle.localEulerAngles;
            rot.z = angleZ;
            couvercle.localEulerAngles = rot;
        }
    }

    public void Ouvrir()
    {

        ouvert = true;
        t = 0;
        bombe.GetComponent<S_Bombe>().Collecter();
    }
}