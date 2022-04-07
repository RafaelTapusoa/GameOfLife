using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cell : MonoBehaviour
{

    public bool aliveCell = false; //boolean value for whether a cell is alive or not
    public int numNeighbours = 0;   //int value that counts the number of neighbours

    public void SetAlive (bool alive) 
    {
        //checks whether the cell is alive or not using boolean values
        aliveCell = alive;

        //if the cell is alive, we will see the sprite
        //else(if the cell is dead) we won't see it(sprite wont be rendered)
        if (alive)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }  
    }

}
