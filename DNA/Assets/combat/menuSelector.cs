using UnityEngine;

public class menuSelector : MonoBehaviour
{
    public int pos = 0;
    public static string response = null;

    void Update()
    {
        if (pos == 0)
        {
            this.transform.position = new Vector3(-4.25f, -3.25f, 0);
        } 
        else
        {
            this.transform.position = new Vector3(2.75f, -3.25f, 0);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            response = pos == 0 ? "ability" : "item";
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pos = 0;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            pos = 1;
        }
    }
}
