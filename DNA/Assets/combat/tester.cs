using UnityEngine;

public class tester : MonoBehaviour
{
    
    public combatStarter combatStarter;

    void Start()
    {
        combatStarter = new combatStarter(2, 2, new string[] { "testenemy", "testenemy" });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
