using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using System.Linq;
using UnityEditor;

public class dynamicSelectorText : MonoBehaviour
{
    public static GameObject textPrefabUI;
    public static GameObject canvas;

    public GameObject[] texts = new GameObject[5];
    public string[] options = new string[5];
    public int pos = 0;
    public bool done = false;
    public int result;

    public int nums;

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
        if (positions.Length != 1)
        {
            g.transform.position = positions[2];
        }
        else
        {
            g.transform.position = positions[0];
        }
        g.GetComponent<dynamicSelectorText>().nums = positions.Count();
        

        g.GetComponent<dynamicSelectorText>().options = texts;
        g.GetComponent<SpriteRenderer>().sortingOrder = 21;

        if (positions.Length == 1)
        {
            g.gameObject.transform.position = positions[0];
        }
        else
        {
            g.gameObject.transform.position = positions[1];
        }
        

        return g.GetComponent<dynamicSelectorText>();
    }

    public static dynamicSelectorText create(Vector3[] positions, string[] texts, Sprite sel, Vector3 offset)
    {
        GameObject g = new GameObject();
        g.AddComponent<dynamicSelectorText>();
        g.GetComponent<dynamicSelectorText>().nums = positions.Count();
        
        for (int i = 0; i < positions.Length; i++)
        {
            GameObject t = Instantiate(textPrefabUI, convertUI(positions[i]), Quaternion.identity);
            t.transform.SetParent(canvas.transform);
            g.GetComponent<dynamicSelectorText>().texts[i] = t;
        }
        g.AddComponent<SpriteRenderer>().sprite = sel;

        if (positions.Length == 1)
        {
            g.transform.position = positions[0] + offset;
        }
        else
        {
            g.transform.position = positions[2] + offset;
        }

        

        g.GetComponent<dynamicSelectorText>().options = texts;
        g.GetComponent<SpriteRenderer>().sortingOrder = 21;

        return g.GetComponent<dynamicSelectorText>();
    }

    public static Vector3 convertUI(Vector3 pos)
    {
        /*
        GameObject p = GameObject.FindGameObjectWithTag("camera");
        if (p == null)
        {
            return new Vector3((pos.x) * 1920 / 19.2f + 960, (pos.y) * 1080 / 10.8f + 540, 0);
        }
        return new Vector3((pos.x - p.transform.position.x) * 1920 / 19.2f + 960, (pos.y - p.transform.position.y) * 1080 / 10.8f + 540, 0);
        */

        return GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(pos);
    }

    public void destroy()
    {
        foreach (GameObject t in texts)
        {
            Destroy(t);
        }
        if (this.gameObject is not null)
        {
            Destroy(this.gameObject);
        }
        
    }

    public void updateTexts()
    {

        if (nums == 1)
        {
            texts[0].GetComponent<TextMeshProUGUI>().text = options[pos];
            return;
        }
        
        for (int i = 0; i < 5; i++)
        {
            if (texts[i] != null)
            {

                try
                {
                    texts[i].GetComponent<TextMeshProUGUI>().text = options[i - 2 + pos];
                }
                catch (IndexOutOfRangeException)
                {
                    texts[i].GetComponent<TextMeshProUGUI>().text = "";
                }
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            destroy();
        }

        updateTexts();
    }
}
