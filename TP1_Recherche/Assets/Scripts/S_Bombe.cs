using UnityEngine;
using System.Collections;

public class S_Bombe : MonoBehaviour
{
    [SerializeField] Transform joueur;
    [SerializeField] S_Controller controller;

    public void Collecter()
    {
        StartCoroutine(AnimationCollecte());
    }

    IEnumerator AnimationCollecte()
    {
        float t = 0;
        float temps = 1f;
        Vector3 depart = transform.position;

        while (t < temps)
        {
            t += Time.deltaTime; // On incrémente t à chaque frame pour faire avancer l'animation

            // Lerp déplace la bombe progressivement de sa position de départ vers le joueur
            // t/temps va de 0 à 1
            transform.position = Vector3.Lerp(depart, joueur.position + Vector3.up, t / temps);

            // LerpAngle réduit le scale de 1 à 0, la bombe rapetisse jusqu'à disparaître
            float scale = Mathf.LerpAngle(1f, 0f, t / temps);
            transform.localScale = Vector3.one * scale;

            yield return null;
        }

        controller.collectBombe();
        Destroy(gameObject);
    }
}