using UnityEngine;

public class menuSelector : MonoBehaviour
{
    public int pos = 0;
    public static string response = null;

    void Update()
    {

        switch (pos)
        {
            case 0: this.transform.position = new Vector3(-4.25f, -2f, 0); break;
            case 1: this.transform.position = new Vector3(2.75f, -2f, 0); break;
            case 2: this.transform.position = new Vector3(-4.25f, -4.5f, 0); break;
            case 3: this.transform.position = new Vector3(2.75f, -4.5f, 0); break;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (pos)
            {
                case 0: response = "ability"; break;
                case 1: response = "item"; break;
                case 2: response = "defend"; break;
                case 3: response = "run"; break;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (pos % 2 == 0)
            {
                pos++;
            } else
            {
                pos--;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
           if (pos % 2 == 0)
            {
                pos++;
            }
           else
            {
                pos--;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (pos < 2)
            {
                pos += 2;
            }
            else
            {
                pos -= 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (pos < 2)
            {
                pos += 2;
            }
            else
            {
                pos -= 2;
            }
        }
    }
}
