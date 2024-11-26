using Unity.VisualScripting;
using UnityEngine;

public class inventoryController : MonoBehaviour
{
    public string mode = "base"; // base, gene, item
    public GameObject selector;

    public GameObject[] eyes = new GameObject[3];
    public GameObject[] arms = new GameObject[3];
    public GameObject[] others = new GameObject[3];

    public GameObject[] equiped = new GameObject[8]; // 0, 1 eyes , 2, 3 arms, 4, 5, 6, 7 others

    public int equipPos = 1;
    public int selectNum = 1;

    public int mainPos = 0;
    public GameObject[] baseObj = new GameObject[3];
    
    void Update()
    {
        if (mode == "base")
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                switch (mainPos)
                {
                    case 0:
                        enterGene();
                        break;
                    case 1:
                        enterItem();
                        break;
                    case 2:
                        break;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                mainPos += 2;
                mainPos %= 3;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                mainPos++;
                mainPos %= 3;
            }
            updateBase();
        }
        /*
        if (mode == "gene")
        {
            if (equipPos > 0)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    equipPos += 6;
                    equipPos %= 8;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    equipPos += 2;
                    equipPos %= 8;
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    equipPos += 7;
                    equipPos %= 8;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    equipPos ++;
                    equipPos %= 8;
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    equipPos = -equipPos;
                }
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                returnToBase();
            }

            if (equipPos < 0)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    selectNum = Mathf.Max(0, selectNum--);
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Mathf.Min(
                }

            }

            updateGene();       
        }
        */
    }

    public void enterGene()
    {
        GameObject.FindGameObjectWithTag("inventory").transform.Translate(new Vector3(20, 0, 0));
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("gene"))
        {
            g.transform.Translate(new Vector3(-20, 0, 0));
        }
        mode = "gene";
    }

    public void enterItem()
    {

    }

    public void returnToBase()
    {

    }

    public void updateBase()
    {
        selector.transform.position = baseObj[mainPos].transform.position;
    }

    public void updateGene()
    {

    }

    public static void openInventory()
    {
        GameObject g = new();
        g.AddComponent<inventoryController>();
        g.tag = "inventoryController";

        GameObject.FindGameObjectWithTag("inventory").transform.position = GameObject.FindGameObjectWithTag("player").transform.position;
    }

    public static void closeInventory()
    {
        GameObject.FindGameObjectWithTag("inventory").transform.position = new Vector3(1000, 1000, 1000);
        GameObject g = GameObject.FindGameObjectWithTag("inventoryController");
        Destroy(g);
        
    }
}
