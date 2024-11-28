using UnityEngine;
using TMPro;

public class itemDialogue : MonoBehaviour
{
    public GameObject box;
    public GameObject textObj;
    public GameObject port;
    public bool done = false;

    public static itemDialogue create(string text, Sprite portrait, Vector3 pos)
    {
        GameObject box = new GameObject();
        GameObject textObj = Instantiate(dynamicSelectorText.textPrefabUI, dynamicSelectorText.convertUI(pos), Quaternion.identity);
        textObj.transform.SetParent(dynamicSelectorText.canvas.transform);
        textObj.GetComponent<TextMeshProUGUI>().text = text;
        box.transform.position = pos;
        GameObject port = new();
        port.AddComponent<SpriteRenderer>().sprite = portrait;
        box.AddComponent<itemDialogue>().box = box;
        box.GetComponent<itemDialogue>().textObj = textObj;
        box.GetComponent<itemDialogue>().port = port;

        return box.GetComponent<itemDialogue>();
    }

    public static Vector3 convertUI(Vector3 pos)
    {
        GameObject p = GameObject.FindGameObjectWithTag("player");
        return new Vector3((pos.x - p.transform.position.x) * 960 * 2 / 17.7f + 960, (pos.y - p.transform.position.y) * 540 * 2 / 9.8f + 540, 0);
    }

    public void destroy()
    {
        Destroy(port);
        Destroy(textObj);
        Destroy(box);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            done = true;
        }
    }
}
