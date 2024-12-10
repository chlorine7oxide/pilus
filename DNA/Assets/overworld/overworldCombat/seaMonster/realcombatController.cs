using Unity.VisualScripting;
using UnityEngine;

public class realcombatController : MonoBehaviour
{
    public int playerHealth;
    public int playerDef;

    private void Start()
    {
        playerData.setStats();
        playerHealth = playerData.hp;
        playerDef = playerData.def;
    }

    public void takeDamage(int damage)
    {
        playerHealth -= damage - playerDef;
        if (playerHealth <= 0)
        {
            // Game over
            Debug.Log("dead player");
        }
    }
}
