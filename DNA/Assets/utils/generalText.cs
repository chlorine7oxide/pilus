using UnityEngine;
using TMPro;

public class generalText : MonoBehaviour
{
    public static GameObject textPrefabUI;
    public static GameObject canvas;
    public static Sprite box;

    public static generalText createAt(string text, Sprite portrait, Sprite face, Vector3 pos)
    {
        GameObject textBox = new();
        textBox.transform.position = GameObject.FindGameObjectWithTag("MainCamera").transform.position + pos;

        textBox.AddComponent<generalText>().textBox = textBox;

        GameObject port = new();
        port.transform.position = GameObject.FindGameObjectWithTag("MainCamera").transform.position + pos + new Vector3(-2f, 0, 0);
        port.AddComponent<SpriteRenderer>().sprite = portrait;
        port.GetComponent<SpriteRenderer>().sortingOrder = 22;
        textBox.GetComponent<generalText>().portrait = port;
        port.transform.localScale = new Vector3(0.3f, 0.3f, 1);

        GameObject faceObj = new();
        faceObj.transform.position = pos + new Vector3(-2f, 0, 0);
        faceObj.AddComponent<SpriteRenderer>().sprite = face;
        faceObj.GetComponent<SpriteRenderer>().sortingOrder = 23;
        textBox.GetComponent<generalText>().face = faceObj;

        GameObject t = Instantiate(textPrefabUI, convertUI(GameObject.FindGameObjectWithTag("MainCamera").transform.position + pos), Quaternion.identity);
        t.transform.SetParent(canvas.transform);
        t.GetComponent<TextMeshProUGUI>().text = text;
        textBox.GetComponent<generalText>().text = t;
        t.transform.Translate(new Vector3(100f, 0, 0));

        textBox.AddComponent<SpriteRenderer>().sprite = box;
        textBox.GetComponent<SpriteRenderer>().sortingOrder = 21;

        textBox.GetComponent<generalText>().offset = pos;

        return textBox.GetComponent<generalText>();
    }

    public static generalText createTimed(string text, Sprite portrait, Sprite face, Vector3 pos, float time)
    {
        generalText t = createAt(text, portrait, face, pos);
        t.timeEnd = time;
        return t;
    }

    public static generalText create(string text, Sprite portrait, Sprite face)
    {
        generalText t = createAt(text, portrait, face, new Vector3(-5f, -4f, 0));
        return t;
    }

    public static Vector3 convertUI(Vector3 pos)
    {
        GameObject p = GameObject.FindGameObjectWithTag("MainCamera");
        return new Vector3((pos.x - p.transform.position.x) * 960 * 2 / 17.7f + 960, (pos.y - p.transform.position.y) * 540 * 2 / 9.8f + 540, 0);
    }

    public GameObject textBox;
    public GameObject portrait;
    public GameObject face;
    public GameObject text;
    public Vector3 offset;

    public float timeEnd = 9999;
    public float time = 0;

    public bool done = false;

    public void destroy()
    {
        Destroy(portrait);
        Destroy(face);
        Destroy(text);
        Destroy(textBox);
    }

    public void Update()
    {
        offset.z = 10;
        if (Input.GetKeyDown(KeyCode.Z) || time > timeEnd || Input.GetKeyDown(KeyCode.C))
        {
            done = true;
        }
        textBox.transform.position = GameObject.FindGameObjectWithTag("MainCamera").transform.position + offset;
        portrait.transform.position = GameObject.FindGameObjectWithTag("MainCamera").transform.position + offset + new Vector3(-2f, 0, 0);
        face.transform.position = GameObject.FindGameObjectWithTag("MainCamera").transform.position + offset + new Vector3(-2f, 0, 0);
        time += Time.deltaTime;
    }
}
