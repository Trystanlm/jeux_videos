using UnityEngine;
using TMPro;
using System.Collections;
public class S_Controller : MonoBehaviour
{

    private int keyCount = 0;

    private int nbKeys = 0;

    private int numberKey = 4;

    private int usedKey = 0;

    [SerializeField]
    private TMP_Text keyText;

    [SerializeField]
    private TMP_Text chestText;

    [SerializeField]
    private TMP_Text gateText;

    [SerializeField]
    S_Chest chestScript;

    [SerializeField]
    private Transform bombPosition;

    [SerializeField]
    private GameObject bombPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chestText.text = "Chest: Locked! \n Find all keys.";
        
    }

    // Update is called once per frame
    void Update()
    {
        keyText.text = "Keys: " + nbKeys + "/" + numberKey;
        if (usedKey >= numberKey)
        {
            chestText.text = "Chest Unlocked !";
            chestScript.Ouvrir();
        }
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

    public void openGate()
    {
        if (usedKey >= numberKey)
        {
            gateText.text = "Gate Unlocked !";
        }
    }

    public void usedBomb()
    {
        StartCoroutine(OpenGate());
    }

    IEnumerator OpenGate()
    {
        for (int i = 0; i < 3; i++)
        {
            gateText.text = i.ToString()+"...!";
            yield return new WaitForSeconds(1f);
            
        }
        gateText.text = "Gate Unlocked !";
    }

}
