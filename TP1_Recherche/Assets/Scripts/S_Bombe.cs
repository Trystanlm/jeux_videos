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
            t += Time.deltaTime;

            transform.position = Vector3.Lerp(depart, joueur.position + Vector3.up, t / temps);

            float scale = Mathf.LerpAngle(1f, 0f, t / temps);
            transform.localScale = Vector3.one * scale;
            yield return null;
        }

        controller.collectBombe();
        Destroy(gameObject);
    }
}