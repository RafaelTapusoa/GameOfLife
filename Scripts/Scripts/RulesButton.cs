using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesButton : MonoBehaviour
{
    
    //this script will allow the user to hover over the "rules" button, showing the rules of the game

    public HUD hud; //call method to our HUD script
    public GameObject rulesText; //public game object that will be our Rules Text

    // Start is called before the first frame update
    void Start()
    {
        rulesText.SetActive(false); //will initially hide the rules
    }

    //will show the rules text when the mouse hovers over it    
    public void OnButtonPress()
    {
        hud.isActive = true; //hud is disables so user does not automatically click the background
        rulesText.SetActive(true);
    }

    public void quitDialog()
    {
        hud.isActive = false;
        rulesText.SetActive(false);
    }

    //will hide the rules text again once the mouse stops hovering over the game object
    // public void OnMouseExit()
    // {
    //     rulesText.SetActive(false);
    // }


}
