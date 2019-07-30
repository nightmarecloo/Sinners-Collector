using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private KeyCombo falconPunch = new KeyCombo(new string[] { "y", "x", "x" });
    private KeyCombo falconKick = new KeyCombo(new string[] { "x", "y", "x" });

    void Update()
    {
        if (falconPunch.Check())
        {
            // do the falcon punch
            Debug.Log("PUNCH");
        }
        if (falconKick.Check())
        {
            // do the falcon punch
            Debug.Log("KICK");
        }
    }
}
