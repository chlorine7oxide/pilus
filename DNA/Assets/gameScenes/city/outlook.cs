using System.Collections;
using UnityEngine;

public class outlook : overworldInteractable
{
    public GameObject cam, bg, player;
    public float speed, amount;

    public bool on;


    public override void interact()
    {
        if (!on)
        {
            on = true;
            cam.GetComponent<camera>().enabled = false;
            player.GetComponent<movement>().enabled = false;
            StartCoroutine(moveAtSpeed(speed));
        }
        else
        {
            on = false;
            
            StartCoroutine(moveAtSpeed(-speed));
        }
    }

    public IEnumerator moveAtSpeed(float speed)
    {
        for (int i = 0; i < amount / Mathf.Abs(speed);i++)
        {
            bg.transform.Translate(0, speed, 0);
            cam.transform.Translate(0, speed, 0);

            yield return new WaitForSeconds(0.01f);
        }
        if (!on)
        {
            cam.GetComponent<camera>().enabled = true;
            player.GetComponent<movement>().enabled = true;
        }
    }

}
