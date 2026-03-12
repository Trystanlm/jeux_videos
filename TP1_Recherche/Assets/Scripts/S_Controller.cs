using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class S_Controller : MonoBehaviour
{
    private int keyCount = 0;
    private int nbKeys = 0;
    private int numberKey = 4;
    private int usedKey = 0;
    [SerializeField] private TMP_Text keyText;
    [SerializeField] private TMP_Text chestText;
    [SerializeField] private TMP_Text gateText;
    [SerializeField] private S_Player playerScript;
    [SerializeField] S_Chest chestScript;
    [SerializeField] private Transform bombPosition;
    [SerializeField] private GameObject bombPrefab;
    private bool chestOpened = false;
    [SerializeField] private GameObject gate;
    [SerializeField] private GameObject bombExplosion;

    void Start()
    {
        chestText.text = "Chest: Locked! \n Find all keys.";
    }

    void Update()
    {
        keyText.text = "Keys: " + nbKeys + "/" + numberKey;

        // Une fois toutes les clés utilisées, on ouvre le coffre une seule fois
        if (usedKey >= numberKey && !chestOpened)
        {
            chestText.text = "Chest Unlocked !";
            chestScript.Ouvrir();
            chestOpened = true;
        }
    }

    private bool hasBombe = false;

    public void collectBombe()
    {
        hasBombe = true;
        chestText.text = "Bombe collectée !";
        playerScript.bomb = true;
    }

    public void collectKey()
    {
        keyCount++;
        nbKeys++;
    }

    public void useKey()
    {
        if (keyCount > 0)
        {
            usedKey += keyCount;
            keyCount = 0;
            chestText.text = nbKeys + "/" + numberKey;
        }
    }

    public void usedBomb()
    {
        StartCoroutine(OpenGate());
    }

    IEnumerator OpenGate()
    {
        // Instancie la bombe au point d'explosion
        GameObject bombeInstance = Instantiate(bombPrefab, bombPosition.position, Quaternion.identity);

        // Compte à rebours avant l'explosion
        for (int i = 3; i > 0; i--)
        {
            gateText.text = i.ToString() + "...!";
            yield return new WaitForSeconds(1f);
        }

        bombExplosion.SetActive(true); 
        Destroy(bombeInstance);
        Destroy(gate); // Détruit la porte
        gateText.text = "La légende raconte qu'il y a un item sacré caché dans le cimetière permettant de vaincre le squelette.\n Et permet d'ouvrir la crypte une fois tué.";
    }

    public void EndGame()
    {
        // Charge la scène de fin
        SceneManager.LoadScene("EndScene");
    }
}