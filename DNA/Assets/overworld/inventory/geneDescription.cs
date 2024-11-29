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

    public static void create(string mode, GameObject button)
    {
        text.transform.position = convertUI(button.transform.position - new Vector3(3, 0, 0));
        box.transform.position = convertUI(button.transform.position - new Vector3(3, 0, 0));
        box.transform.localScale = new Vector3(-1, 1, 0);
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

    public static Vector3 convertUI(Vector3 pos)
    {
        GameObject p = GameObject.FindGameObjectWithTag("player");
        return new Vector3((pos.x - p.transform.position.x) * 960 * 2 / 17.7f + 960, (pos.y - p.transform.position.y) * 540 * 2 / 9.8f + 540, 0);
    }   

    private void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = descriptions[pos];
        if (Input.GetKeyDown(KeyCode.UpArrow) && pos != 0)
        {
            pos += descriptions.Length - 1;
            pos %= descriptions.Length;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && pos != descriptions.Length - 1)
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
