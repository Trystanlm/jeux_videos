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
        depart = couvercle.localEulerAngles.z; // On sauvegarde la rotation de départ du couvercle
    }

    void Update()
    {
        if (ouvert)
        {
            t += Time.deltaTime; // On incrémente t à chaque frame pour faire avancer l'animation

            // t/temps va de 0 à 1, ce qui fait progresser l'ouverture
            float angleZ = Mathf.Lerp(depart, arrivee, t / temps);
            Vector3 rot = couvercle.localEulerAngles;
            rot.z = angleZ;
            couvercle.localEulerAngles = rot;
        }
    }

    public void Ouvrir()
    {
        ouvert = true;
        t = 0; // On remet t à 0 pour que l'animation repart du début
        bombe.GetComponent<S_Bombe>().Collecter();
    }
}