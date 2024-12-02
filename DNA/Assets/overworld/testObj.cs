using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testObj : overworldInteractable
{
    public Sprite testface;

    public List<GameObject> speakers = new();
    public List<GameObject> faces = new();
    public Queue<msg> msgs = new();

    public GameObject mc;

    public static bool entered = false;

    public override void interact()
    {
        msgs.Enqueue(new msg(testface, () => "interactable object", "", speakers[0]));
        msgs.Enqueue(new msg(testface, enterCombat, "", speakers[0]));

        combatData.scene = SceneManager.GetActiveScene().name;
        
        speakers[0].transform.position = mc.transform.position + new Vector3(-6f, 0, 0);
        msgController.createDialogue(speakers, msgs, faces);

    }

    public string enterCombat()
    {
        if (!entered)
        {
            entered = true;
            combatData.playerPos = GameObject.FindGameObjectWithTag("player").transform.position;
            combatStarter combatStarter = new combatStarter(2, 2, new string[] { "testenemy", "testenemy" }, this.gameObject);
        }
        
        return "oh wow";
    }
}
