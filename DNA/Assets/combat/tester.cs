using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class tester : MonoBehaviour
{
    
    public combatStarter combatStarter;

    public GameObject buttonprefab, textprefab, barprefab;
    public Sprite box, arrow;

    public Sprite mc, friend, testenemy;

    void Start()
    {
        scroller.box = box;
        scroller.arrow = arrow;
        scroller.buttonprefab = buttonprefab;
        scroller.textprefab = textprefab;

        combatController.mcSprite = mc;
        combatController.friendSprite = friend;
        combatController.testenemySprite = testenemy;

        playerData.MCabilities.Add("attack");
        playerData.MCabilities.Add("kick");
        playerData.MCabilities.Add("insult");
        playerData.MCabilities.Add("heal");

        playerData.friendAbilities.Add("attack");
        playerData.friendAbilities.Add("swing");
        playerData.friendAbilities.Add("meditate");
        playerData.friendAbilities.Add("heal");

        playerData.items.Add("potion");
        playerData.items.Add("potion");
        playerData.items.Add("bigpotion");
        playerData.items.Add("molotov");

        playerData.hp = 100;
        playerData.def = 10;

        combatEntity.barPrefab = barprefab;

        combatStarter = new combatStarter(2, 2, new string[] { "testenemy", "testenemy" });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
