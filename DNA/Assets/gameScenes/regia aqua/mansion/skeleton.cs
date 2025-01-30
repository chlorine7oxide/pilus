using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class skeleton : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public PostProcessProfile scary;

    public bool entered = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            if (!entered)
            {
                entered = true;
                combatData.playerPos = GameObject.FindGameObjectWithTag("player").transform.position;
                combatData.scene = SceneManager.GetActiveScene().name;
                playerData.skeletonFought = true;
                combatStarter combatStarter = new combatStarter(1, 1, new string[] { "skeleton" }, this.gameObject);
            }
        }
    }

    private void Start()
    {
        skeletonEnemy.skeletonPrefab = skeletonPrefab;
        textBox.textPrefab = textPrefab;
        textBox.boxSprite1 = boxSprite1;
        textBox.boxSprite2 = boxSprite2;
        textBox.boxSprite3 = boxSprite3;
        textBox.boxSprite4 = boxSprite4;

        decision.selector = selector;

        if (playerData.skeletonFought && !playerData.skeletonDialogue)
        {
            playerData.skeletonDialogue = true;
            interact();
        }
    }

    public List<GameObject> speakers = new();
    public List<GameObject> faces = new();
    public Queue<msg> msgs = new();

    public Sprite testFace, selector;

    public GameObject textPrefab;
    public Sprite boxSprite1, boxSprite2, boxSprite3, boxSprite4;

    public msgController msgController;

    public Sprite gen2, gen2t, gen2b, gen2j;

    public void interact()
    {
        GameObject p1 = speakers[0];
        GameObject p2 = speakers[1];

        msgs.Enqueue(new msg(testFace, () => "Eeek, a skeleton attacked me!", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "I always knew they were real.", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "But, no thanks to you, I managed to valiantly defend myself.", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "Of course skeletons are real.", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "But, in fact, they are not alive, and it didn't attack you.", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "It simply fell on you when you entered the room, must have been on top of the door.", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "Wait...", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "That's a real skeleton!!!", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "THAT'S WORSE!!!", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "Calm down, I doubt it's real.", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "Judging by its placement, it was placed there as a scare tactic.", "e", p2));
        msgs.Enqueue(new msg(testFace, () => "WELL IT WORKED!", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "At least I got a new gene from it.", "e", p1));
        msgs.Enqueue(new msg(testFace, () => "Oh?", "e", p2));

        playerData.genes.Add(new strongBone(gen2, gen2t, gen2b, gen2j));

        speakers[0].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(-6f, -2.8f, 0);
        speakers[1].transform.position = GameObject.FindGameObjectWithTag("player").transform.position + new Vector3(6f, -2.8f, 0);
        msgController = msgController.createDialogue(speakers, msgs, faces).GetComponent<msgController>();
    }
}
