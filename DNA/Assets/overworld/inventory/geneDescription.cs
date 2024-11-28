using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class geneDescription : MonoBehaviour
{
    public static Sprite s;
    public static GameObject text;
    public static GameObject box;
    public string[] descriptions;

    public int pos;

    public static void create(string mode, Vector3 pos)
    {
        text.transform.position = new Vector3(960, 540, 0);
        box.transform.position = new Vector3(960, 540, 0);
        text.AddComponent<geneDescription>();
        geneDescription d = text.GetComponent<geneDescription>();
        switch (mode)
        {
            case "gene":
                d.descriptions = new string[playerData.genes.Count];
                for (int i = 0; i < d.descriptions.Length; i++)
                {
                    d.descriptions[i] = playerData.genes[i].description;
                }
                break;
            case "eye":
                d.descriptions = new string[playerData.eyes.Count]; 
                for(int i = 0; i < d.descriptions.Length; i++)
                {
                    d.descriptions[i] = playerData.eyes[i].description;
                }
                break;
            case "arm":
                d.descriptions = new string[playerData.arms.Count]; 
                for(int i = 0; i < d.descriptions.Length; i++)
                {
                    d.descriptions[i] = playerData.arms[i].description;
                }
                break;
        }
    }

    private void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = descriptions[pos];
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pos += descriptions.Length - 1;
            pos %= descriptions.Length;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            pos++;
            pos %= descriptions.Length;
        }
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
        {
            text.transform.Translate(Vector3.down * 10000);
            box.transform.Translate(Vector3.down * 10000);
            Destroy(this);
        }
    }
}
