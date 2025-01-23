using UnityEngine;

public class dynamicSelector : MonoBehaviour
{
    public GameObject[] buttons;
    public Sprite[] options;
    public Sprite[] tops;
    public Sprite[] bottoms;

    public static Sprite empty;

    public int pos = 0;
    public int result;
    public bool done = false;

    public bool e = false;

    public static dynamicSelector create(GameObject[] buttons, Sprite[] options, Sprite sel, Sprite[] tops, Sprite[] bottoms)
    {
        GameObject g = new();
        g.AddComponent<dynamicSelector>();
        dynamicSelector d = g.GetComponent<dynamicSelector>();
        d.transform.position = buttons[1].transform.position;
        g.AddComponent<SpriteRenderer>().sprite = sel;
        d.buttons = buttons;
        d.options = options;
        d.GetComponent<SpriteRenderer>().sortingOrder = 20;
        g.GetComponent<dynamicSelector>().tops = tops;
        g.GetComponent<dynamicSelector>().bottoms = bottoms;

        return d;
    }
    public static dynamicSelector create(GameObject[] buttons, Sprite[] options, Sprite sel, Sprite[] tops, Sprite[] bottoms, Vector3 offset)
    {
        GameObject g = new();
        g.AddComponent<dynamicSelector>();
        dynamicSelector d = g.GetComponent<dynamicSelector>();
        d.transform.position = buttons[1].transform.position + offset;
        g.AddComponent<SpriteRenderer>().sprite = sel;
        d.buttons = buttons;
        d.options = options;
        d.GetComponent<SpriteRenderer>().sortingOrder = 20;
        g.GetComponent<dynamicSelector>().tops = tops;
        g.GetComponent<dynamicSelector>().bottoms = bottoms;
        g.transform.Rotate(new Vector3(0, 0, 90));
        d.updateSprites();

        return d;
    }

    public void destroy()
    {
        pos = 0;
        updateSprites();
        Destroy(this.gameObject);
    }

    public void updateSprites()
    {
        if (pos - 1 >= 0)
        {
            buttons[0].GetComponent<SpriteRenderer>().sprite = bottoms[pos - 1];
        }
        else
        {
            buttons[0].GetComponent<SpriteRenderer>().sprite = empty;
        }
        buttons[1].GetComponent<SpriteRenderer>().sprite = options[pos];
        
        if (pos + 1 < options.Length)
        {
            buttons[2].GetComponent<SpriteRenderer>().sprite = tops[pos + 1];
        }
        else
        {
            buttons[2].GetComponent<SpriteRenderer>().sprite = empty;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pos--;
            pos = Mathf.Max(0, pos);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            pos++;
            pos = Mathf.Min(options.Length - 1, pos);
        }
        if (Input.GetKeyDown(KeyCode.Z) && e)
        {
            Debug.Log(pos);
            result = pos;
            done = true;
        }
        updateSprites();
        e = true;
    }
}
