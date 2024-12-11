using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class tester : MonoBehaviour
{
    
    public combatStarter combatStarter;

    public GameObject buttonprefab, textprefab, barprefab;
    public Sprite box, arrow;

    public Sprite mc, friend, testenemy;

    public GameObject canvas;

    public GameObject[] buttons;

    public GameObject tentaclePrefab;

    void Start()
    {
        tentacleEnemy.tentaclePrefab = tentaclePrefab;

        scroller.box = box;
        scroller.arrow = arrow;
        scroller.buttonprefab = buttonprefab;
        scroller.textprefab = textprefab;

        combatController.mcSprite = mc;
        combatController.friendSprite = friend;
        combatController.testenemySprite = testenemy;

        combatEntity.barPrefab = barprefab;

        dynamicSelectorText.textPrefabUI = textprefab;
        dynamicSelectorText.canvas = canvas;

        combatController.buttons = buttons;
    }
}
