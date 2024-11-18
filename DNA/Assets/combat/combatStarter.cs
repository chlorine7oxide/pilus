using UnityEngine;

public class combatStarter
{
    protected GameObject combatController;

    public combatStarter(int numPlayers, int numEnemies, string[] enemies)
    {
        combatController = new GameObject();
        combatController.AddComponent<combatController>();
        combatController.GetComponent<combatController>().numPlayers = numPlayers;
        combatController.GetComponent<combatController>().numEnemies = numEnemies;
        combatController.GetComponent<combatController>().enemys = enemies;
    }
}
