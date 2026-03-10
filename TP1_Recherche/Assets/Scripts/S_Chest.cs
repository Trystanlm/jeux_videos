using UnityEngine;

public class S_Chest : MonoBehaviour
{
    public Transform couvercle;
    float depart = 0f;
    float arrivee = -120f;
    float t = 0;
    float temps = 2;
    public bool ouvert = false;

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
    }
}