using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SaveButton : MonoBehaviour {

   //in reference to our hud script, acquires the script 
    public HUD hud;

    //in reference to our program script -- we will be using variables i alr created in the program script
    public program Program;

    int n;
    public void OnButtonPress()
    {
        //used to test whether the button is actually being clicked or not
        n++;
        Debug.Log("Button clicked " + n + " times.");

        //..im a genius..it actually works hahahha
        //hud.showLoadDialog();
        //Program.simulationEnabled = true;
        hud.isActive = true; //hud is active so the user will not be able to click on cells
        hud.saveDialog.gameObject.SetActive(true);
    }

}


