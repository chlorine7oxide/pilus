using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.SceneManagement;

public class lockDoor1 : overworldInteractable
{
    public Sprite portrait;

    public static bool lockActive = false;

    public int password;
    public int lockNum;

    public override void interact()
    {
        if (playerData.lockFlags[lockNum - 1])
        {
            switch (lockNum) {
                case 1:
                    {
                        SceneManager.LoadScene("basement2"); 
                        break;
                    }
                case 2:
                    {
                        SceneManager.LoadScene("dock");
                        break;
                    }
            }
        }
        else if (!lockActive)
        {
            StartCoroutine(startlock());
        }
        

    }

    public GameObject lockObj, lockPrefab;

    public IEnumerator startlock()
    {
        lockActive = true;
        lockObj = Instantiate(lockPrefab, GameObject.FindGameObjectWithTag("player").transform.position, Quaternion.identity);

        yield return new WaitForEndOfFrame();

        int position = 0;
        int[] code = { 0, 0, 0, 0 };

        while (!Input.GetKeyDown(KeyCode.Z))
        {
            setNumbers(code);
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => Input.anyKeyDown);

            if (Input.GetKey(KeyCode.Z))
            {
                break;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (position > 0)
                {
                    position--;
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (position < 3)
                {
                    position++;
                }
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                code[position]++;
                code[position] %= 10;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                code[position] += 9;
                code[position] %= 10;
            }

        }

        if (code[0] == (password / 1000) % 10 && code[1] == (password / 100) % 10 && code[2] == (password / 10) % 10 && code[3] == password % 10)
        {
            print("Unlocked");
            yield return new WaitForEndOfFrame();
            playerData.lockFlags[lockNum - 1] = true;
        }
        else
        {
            print("incorrent");
        }

        lockActive = false;
        lockObj.transform.Translate(new Vector3(10000, 0, 0));
        Destroy(lockObj);
        yield return new WaitForEndOfFrame();

    }

    public void setNumbers(int[] code)
    {
        try
        {
            lockObj.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = code[0].ToString();
            lockObj.transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text = code[1].ToString();
            lockObj.transform.GetChild(0).GetChild(7).GetComponent<TextMeshProUGUI>().text = code[2].ToString();
            lockObj.transform.GetChild(0).GetChild(10).GetComponent<TextMeshProUGUI>().text = code[3].ToString();

            lockObj.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = ((code[0] + 9) % 10).ToString();
            lockObj.transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = ((code[1] + 9) % 10).ToString();
            lockObj.transform.GetChild(0).GetChild(6).GetComponent<TextMeshProUGUI>().text = ((code[2] + 9) % 10).ToString();
            lockObj.transform.GetChild(0).GetChild(9).GetComponent<TextMeshProUGUI>().text = ((code[3] + 9) % 10).ToString();

            lockObj.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = ((code[0] + 1) % 10).ToString();
            lockObj.transform.GetChild(0).GetChild(5).GetComponent<TextMeshProUGUI>().text = ((code[1] + 1) % 10).ToString();
            lockObj.transform.GetChild(0).GetChild(8).GetComponent<TextMeshProUGUI>().text = ((code[2] + 1) % 10).ToString();
            lockObj.transform.GetChild(0).GetChild(11).GetComponent<TextMeshProUGUI>().text = ((code[3] + 1) % 10).ToString();
        }
        catch { }

        
    }
}
