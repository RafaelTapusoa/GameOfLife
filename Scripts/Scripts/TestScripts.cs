using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripts : MonoBehaviour
{
    
    int n;
    public void OnButtonPress()
    {
        //used to test whether the button is actually being clicked or not
        n++;
        Debug.Log("Button clicked " + n + " times.");

        //..im a genius..it actually works hahahha
        //Program.simulationEnabled = true;
    }

}
