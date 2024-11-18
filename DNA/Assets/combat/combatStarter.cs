using UnityEngine;

public class combatStarter
{
    protected GameObject combatController;

    public combatStarter(int numPlayers, int numEnemies)
    {
        combatController = new GameObject();
        combatController.AddComponent<combatController>();
        combatController.GetComponent<combatController>().numPlayers = numPlayers;
        combatController.GetComponent<combatController>().numEnemies = numEnemies;
    }
}
