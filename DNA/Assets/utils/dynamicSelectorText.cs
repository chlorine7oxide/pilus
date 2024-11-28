using UnityEngine;
using TMPro;
using System;

public class dynamicSelectorText : MonoBehaviour
{
    public static GameObject textPrefabUI;
    public static GameObject canvas;

    public GameObject[] texts = new GameObject[5];
    public string[] options = new string[5];
    public int pos = 0;
    public bool done = false;
    public int result;

    public static dynamicSelectorText create(Vector3[] positions, string[] texts, Sprite sel)
    {
        GameObject g = new GameObject();
        g.AddComponent<dynamicSelectorText>();

        for (int i = 0; i < positions.Length; i++)
        {
            GameObject t = Instantiate(textPrefabUI, convertUI(positions[i]), Quaternion.identity);
            t.transform.SetParent(canvas.transform);
            g.GetComponent<dynamicSelectorText>().texts[i] = t;
        }
        g.AddComponent<SpriteRenderer>().sprite = sel;
        g.transform.position = positions[2];

        g.GetComponent<dynamicSelectorText>().options = texts;
        g.GetComponent<SpriteRenderer>().sortingOrder = 21;

        return g.GetComponent<dynamicSelectorText>();
    }

    public static Vector3 convertUI(Vector3 pos)
    {
        GameObject p = GameObject.FindGameObjectWithTag("player");
        return new Vector3((pos.x - p.transform.position.x) * 960 * 2 / 17.7f + 960, (pos.y - p.transform.position.y) * 540 * 2 / 9.8f + 540, 0);
    }

    public void destroy()
    {
        foreach(GameObject t in texts)
        {
            Destroy(t);
        }
        Destroy(this.gameObject);
    }

    public void updateTexts()
    {
        for (int i = 0; i < 5; i++)
        {
            try
            {
                texts[i].GetComponent<TextMeshProUGUI>().text = options[i - 2 + pos];
            } catch (IndexOutOfRangeException)
            {
                texts[i].GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && pos != 0)
        {
            pos += options.Length - 1;
            pos %= options.Length;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && pos != options.Length - 1)
        {
            pos++;
            pos %= options.Length;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            result = pos;
            done = true;
        }

        updateTexts();
    }
}
