using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class scrollercontroller : MonoBehaviour
{

    public int center = 0;
    public GameObject[] texts;
    public GameObject[] buttons;
    public string[] data;
    public int numExtras;

    void Start()
    {
        for (int i = numExtras; i < texts.Length; i++)
        {
            texts[i].GetComponent<TextMeshPro>().text = data[i - numExtras];
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (center > 0)
            {
                center--;
                for (int i = 0; i < texts.Length; i++)
                {
                    try
                    {
                        texts[i].GetComponent<TextMeshPro>().text = data[i - numExtras + center];
                    }
                    catch
                    {
                        texts[i].GetComponent<TextMeshPro>().text = "";
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (center < data.Length - 1)
            {
                center++;
                for (int i = 0; i < texts.Length; i++)
                {
                    try
                    {
                        texts[i].GetComponent<TextMeshPro>().text = data[i - numExtras + center];
                    }
                    catch
                    {
                        texts[i].GetComponent<TextMeshPro>().text = "";
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            scroller.result = data[center];
            foreach(GameObject b in buttons)
            {
                Destroy(b);
            }
            foreach(GameObject t in texts)
            {
                Destroy(t);
            }
            Destroy(this.gameObject);
        }
    }
}
