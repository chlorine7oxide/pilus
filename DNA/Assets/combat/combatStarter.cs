using UnityEngine;

public class combatStarter
{
    protected GameObject combatController;
    public GameObject origin;

    public combatStarter(int numPlayers, int numEnemies, string[] enemies, GameObject origin)
    {
        combatController = new GameObject();
        combatController.AddComponent<combatController>();
        combatController.GetComponent<combatController>().numPlayers = numPlayers;
        combatController.GetComponent<combatController>().numEnemies = numEnemies;
        combatController.GetComponent<combatController>().enemys = enemies;
        playerData.setAbilities();
        playerData.setFriendAbilities();
        playerData.setStats();
        Debug.Log(origin.name);
    }
}
