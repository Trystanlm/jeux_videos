using UnityEngine;
using TMPro;
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
    S_Chest chestScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chestText.text = "Chest: Locked! Find all keys.";
        
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

}
