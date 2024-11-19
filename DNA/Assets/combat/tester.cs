using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class tester : MonoBehaviour
{
    
    public combatStarter combatStarter;

    public GameObject buttonprefab, textprefab;
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

        playerData.MCabilities.AddLast("attack");
        playerData.MCabilities.AddLast("kick");
        playerData.MCabilities.AddLast("insult");
        playerData.MCabilities.AddLast("heal");

        playerData.friendAbilities.AddLast("attack");
        playerData.friendAbilities.AddLast("swing");
        playerData.friendAbilities.AddLast("meditate");
        playerData.friendAbilities.AddLast("heal");

        playerData.items.AddLast("potion");
        playerData.items.AddLast("potion");
        playerData.items.AddLast("bigpotion");
        playerData.items.AddLast("molotov");

        playerData.hp = 100;
        playerData.def = 10;

        combatStarter = new combatStarter(2, 2, new string[] { "testenemy", "testenemy" });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
