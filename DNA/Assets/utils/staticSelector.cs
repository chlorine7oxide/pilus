using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class staticSelector : MonoBehaviour
{
    public int result;
    public bool done = false;

    public GameObject[] objs;
    public int pos = 0;
    public int prow;

    public Vector3 offset;

    public static staticSelector create(GameObject[] objs, int pRow, Sprite sel)
    {
        GameObject g = new GameObject();
        g.AddComponent<staticSelector>();
        g.GetComponent<staticSelector>().objs = objs;
        g.GetComponent<staticSelector>().prow = pRow;
        g.AddComponent<SpriteRenderer>().sprite = sel;
        g.GetComponent<SpriteRenderer>().sortingOrder = 20;
        g.GetComponent<staticSelector>().offset = new Vector3(0, 0, 0);

        return g.GetComponent<staticSelector>();
    }

    public static staticSelector create(GameObject[] objs, int pRow, Sprite sel, Vector3 offset)
    {
        GameObject g = new GameObject();
        g.AddComponent<staticSelector>();
        g.GetComponent<staticSelector>().objs = objs;
        g.GetComponent<staticSelector>().prow = pRow;
        g.GetComponent<staticSelector>().offset = offset;
        g.AddComponent<SpriteRenderer>().sprite = sel;
        g.GetComponent<SpriteRenderer>().sortingOrder = 20;

        return g.GetComponent<staticSelector>();
    } 

    public void destroy()
    {
        Destroy(this.gameObject);
    }

    public void updatePos()
    {
        if (!done)
        {
            this.gameObject.transform.position = objs[pos].transform.position + offset;
        }
        else
        {
            this.gameObject.transform.position = new Vector3(1000, 0, 0);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pos += objs.Length - prow;
            pos %= objs.Length;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            pos += prow;
            pos %= objs.Length;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pos += objs.Length - 1;
            pos %= objs.Length;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            pos++;
            pos %= objs.Length;
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
        updatePos();
    }


}
