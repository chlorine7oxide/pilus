using Unity.VisualScripting;
using UnityEngine;

public class realcombatController : MonoBehaviour
{
    public int playerHealth;
    public int playerDef;

    public GameObject controller;

    private void Start()
    {
        playerData.setStats();
        playerHealth = playerData.hp;
        playerDef = playerData.def;

        controller = GameObject.FindGameObjectWithTag("boss1Controller");
    }

    public void takeDamage(int damage)
    {
        controller.GetComponent<boss1Controller>().mcHp -= damage - playerDef;
        if (controller.GetComponent<boss1Controller>().mcHp <= 0)
        {
            // Game over
            Debug.Log("dead player");
        }
        print(controller.GetComponent<boss1Controller>().mcHp);
    }
}
