using UnityEngine;
using UnityEngine.UI;

public class scrollerTester : MonoBehaviour
{
    public scroller s;
    public string[] data = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
    public GameObject boxprefab, textprefab;

    public Sprite box, arrow;

    void Start()
    {
        scroller.buttonprefab = boxprefab;
        scroller.textprefab = textprefab;
        scroller.box = box;
        scroller.arrow = arrow;
        scroller.result = null;
        s = new scroller(5f, 5f, data, 3);
    }

    void Update()
    {
        if (scroller.result != null)
        {
            Debug.Log(scroller.result);
            Destroy(s.controller);
            foreach(GameObject g in s.texts)
            {
                Destroy(g);
            }
            foreach(GameObject g in s.buttons)
            {
                Destroy(g);
            }
        }
    }
}
