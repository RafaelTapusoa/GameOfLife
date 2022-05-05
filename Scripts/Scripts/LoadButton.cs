using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class LoadButton : MonoBehaviour {

   //in reference to our hud script, acquires the script 
    public HUD hud;

    //in reference to our program script -- we will be using variables i alr created in the program script
    public program Program;

    int n;
    public void OnLoadButtonPress()
    {
        //used to test whether the button is actually being clicked or not
        n++;
        Debug.Log("Button clicked " + n + " times.");

        //..im a genius..it actually works hahahha
        //hud.showLoadDialog();
        //Program.simulationEnabled = true;
        hud.isActive = true;
        hud.loadDialog.gameObject.SetActive(true);
    }

    // public void OnPlayButtonPress()
    // {
    //     //used to test whether the button is actually being clicked or not
    //     n++;
    //     Debug.Log("Button clicked " + n + " times.");

    //     //..im a genius..it actually works hahahha
    //     Program.simulationEnabled = true;

    //     //Program.PlaceCells(1);
    // }


    public void OnRandomButtonPress()
    {
        Program.PlaceCells(1);
    }

}


