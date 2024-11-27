using UnityEngine;

public class inventoryTester : MonoBehaviour
{
    public Sprite empty, s;
    public Sprite eye1, eye2, arm1, arm2, gen1, gen2;
    public GameObject textPrefab;
    public GameObject box, text;

    void Start()
    {
        playerData.eyes.Add(new eye(eye1));
        playerData.eyes.Add(new watchfulEye(eye2));
        playerData.arms.Add(new arm(arm1));
        playerData.arms.Add(new mechanicalArm(arm2));
        playerData.genes.Add(new courage(gen1));
        playerData.genes.Add(new strongBone(gen2));
        dynamicSelector.empty = empty;

        playerData.equiped[0] = playerData.eyes[0];
        playerData.equiped[2] = playerData.arms[0];
        playerData.equiped[3] = playerData.arms[0];
        playerData.equiped[4] = playerData.genes[0];

        text.transform.position = new Vector3(10000, 0, 0);
        box.transform.position = new Vector3(10000, 0, 0);

        geneDescription.text = text;
        geneDescription.box = box;
        geneDescription.s = s;
    }
}
