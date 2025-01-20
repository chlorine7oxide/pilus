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
    }

    public combatStarter(int numPlayers, int numEnemies, string[] enemies, GameObject origin, int mcHp, int companionHp)
    {
        combatController = new GameObject();
        combatController.AddComponent<combatController>();
        combatController.GetComponent<combatController>().numPlayers = numPlayers;
        combatController.GetComponent<combatController>().numEnemies = numEnemies;
        combatController.GetComponent<combatController>().enemys = enemies;
        playerData.setAbilities();
        playerData.setFriendAbilities();
        playerData.setStats();
        combatController.GetComponent<combatController>().mcHp = mcHp;
        combatController.GetComponent<combatController>().friendHp = companionHp;
    }
}
