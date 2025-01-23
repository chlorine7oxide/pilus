using System.Collections;
using UnityEngine;

public class inventoryTester : MonoBehaviour
{
    public Sprite empty, s;
    public Sprite eye1, eye2, arm1, arm2, gen1, gen2;
    public Sprite eye1t, eye1j, eye1b, eye2t, eye2j, eye2b, arm1t, arm1j, arm1b, arm2t, arm2j, arm2b, gen1t, gen1j, gen1b, gen2t, gen2j, gen2b;
    public GameObject textPrefab;
    public GameObject box, text;
    public GameObject canvas;
    public GameObject money;

    public static Sprite port, giantRock;

    public Sprite boxSprite, ports, giantRocks;

    void Start()
    {
        giantRock = giantRocks;
        inventoryController.money = money;

        if (playerData.genes.Count == 0)
        {
            playerData.eyes.Add(new eye(eye1, eye1t, eye1b, eye1j));
            //playerData.eyes.Add(new watchfulEye(eye2, eye2t, eye2b, eye2j));
            playerData.arms.Add(new arm(arm1, arm1t, arm1b, arm1j));
            //playerData.arms.Add(new mechanicalArm(arm2, arm2t, arm2b, arm2j));
            playerData.genes.Add(new courage(gen1, gen1t, gen1b, gen1j));
            //playerData.genes.Add(new strongBone(gen2, gen2t, gen2b, gen2j));
        }

        dynamicSelector.empty = empty;

        playerData.equiped[0] = playerData.eyes[0];
        playerData.equiped[1] = playerData.eyes[0];
        playerData.equiped[2] = playerData.arms[0];
        playerData.equiped[3] = playerData.arms[0];
        playerData.equiped[4] = playerData.genes[0];

        text.transform.position = new Vector3(10000, 0, 0);
        box.transform.position = new Vector3(10000, 0, 0);

        geneDescription.text = text;
        geneDescription.box = box;
        geneDescription.s = s;

        //playerData.items.Add("Giant Rock");
        //playerData.items.Add("item2");
        //playerData.items.Add("item3");
        //playerData.items.Add("item4");
        //playerData.items.Add("item5");
        //playerData.items.Add("item6");

        dynamicSelectorText.textPrefabUI = textPrefab;
        dynamicSelectorText.canvas = canvas;

        generalText.textPrefabUI = textPrefab;
        generalText.canvas = canvas;
        generalText.box = boxSprite;

        itemDialogue.boxSprite = boxSprite;


        port = ports;
    }

}
