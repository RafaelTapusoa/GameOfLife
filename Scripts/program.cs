using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class program : MonoBehaviour
{
    //setting static ints to keep screen width/height constant
    private static int SCREEN_WIDTH = 64;  //equivalent to 1024 pixels
    private static int SCREEN_HEIGHT = 48; //equivalent to 768 game pixels

    public float game_speed = 0.1f;

    private float timer = 0;

    public bool simulationEnabled = false;

    cell[,] grid = new cell[SCREEN_WIDTH, SCREEN_HEIGHT];

    //start is called before the first frame update 
    void Start()
    {
        //initially places the cells to fill the entire screen
        //also specifies what "mode" the placed cells will be in
        PlaceCells(2);
    }

    //update is called once per frame
    void Update()
    {

        if (simulationEnabled)
        {
            //creates a float value that can be changed in the unity gui
            if (timer >= game_speed)
            {
                timer = 0f;

                CountNeighbours();

                RunGameRules();
            }
            else
            {
                timer += Time.deltaTime;
            }

        }

        UserInput();
       

        

    }

    public void UserInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            int x = Mathf.RoundToInt(mousePoint.x);
            int y = Mathf.RoundToInt(mousePoint.y);

            //making sure we are inbounds
            if (x >= 0 && y >= 0 && x < SCREEN_WIDTH && y < SCREEN_HEIGHT)
            {
                //-We are in bounds
                //sets cell to alive wherever there is a click
                grid[x, y].SetAlive(!grid[x, y].aliveCell); 

            }
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            //pause simulation
            //simulationEnabled = false;
            if (simulationEnabled == false)
            {
                simulationEnabled = true;
            }
            else 
            {
                simulationEnabled = false;
            }

        }

        // if (Input.GetKeyUp(KeyCode.B))
        // {
        //     //resume simulation
        //     simulationEnabled = true;
        // }
    }

    //method that places our cells in the game
    void PlaceCells (int type) 
    {
        //type 1 - randomly generated cells
        if (type == 1)
        {
            //for every y value there will be the same number of cells on x axis
            for (int y = 0; y < SCREEN_HEIGHT; y++)
            {
                for (int x = 0; x < SCREEN_WIDTH; x++)
                {
                    //generating cells and loading the cell prefab from our Resources folders
                    cell Cells = Instantiate(Resources.Load("Prefabs/cell", typeof(cell)), new Vector2 (x,y), Quaternion.identity) as cell;
                    grid[x, y] = Cells; 
                    //grid[x, y].SetAlive(false);
                    grid[x, y].SetAlive(RandomAliveCell()); //sets a random alive cell depending on the number that is returned
                }
            }

        }


        //type 2 - hidden cells
        if(type == 2)
        {
            //for every y value there will be the same number of cells on x axis
            for (int y = 0; y < SCREEN_HEIGHT; y++)
            {
                //for every x value there will be the same number of cells "dead" cells are still there, just that the graphic is not rendered
                for (int x = 0; x < SCREEN_WIDTH; x++)
                {
                    //generating cells and loading the cell prefab from our Resources folders
                    cell Cells = Instantiate(Resources.Load("Prefabs/cell", typeof(cell)), new Vector2 (x,y), Quaternion.identity) as cell;
                    grid[x, y] = Cells; //places 
                    grid[x, y].SetAlive(false); //the cells are still there the graphic is just not rendered
                }
            }
        }
        
    }

    //checks each direction(N, E, S, W, NE, NW, NE, SW, SE) to check if there are any neighbouring cells
    void CountNeighbours()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                int numNeighbours = 0; 

                //north
                if (y + 1 < SCREEN_HEIGHT) //"< SCREEN_HEIGHT"checks if we are at the highest height
                {
                    //checks if the cell north is alive and adds one to numNeighbours if true
                    if (grid[x, y+1].aliveCell)
                    {
                        numNeighbours++;
                    }
                }

                //east
                if (x + 1 < SCREEN_WIDTH )
                {
                    if (grid[x+1, y].aliveCell)
                    {
                        numNeighbours++;
                    }
                }

                //south
                if (y - 1 >= 0)
                {
                    if (grid[x, y-1].aliveCell)
                    {
                        numNeighbours++;
                    }
                }

                //west
                if (x - 1 >= 0 )
                {
                    if (grid[x-1, y].aliveCell)
                    {
                        numNeighbours++;
                    }
                }

                //north-east
                if (x + 1 < SCREEN_WIDTH && y + 1 < SCREEN_HEIGHT)
                {
                    if (grid[x+1, y+1].aliveCell)
                    {
                        numNeighbours++;
                    }
                }
                
                //north-west
                if (x - 1 >= 0 && y + 1 < SCREEN_HEIGHT)
                {
                    if (grid[x-1, y+1].aliveCell)
                    {
                        numNeighbours++;
                    }
                }

                //south-east
                if (x + 1 < SCREEN_WIDTH && y - 1 >= 0)
                {
                    if (grid[x+1, y-1].aliveCell)
                    {
                        numNeighbours++;
                    }
                }

                //south-west
                if (x - 1 >= 0 && y - 1 >= 0)
                {   
                    if (grid[x-1, y-1].aliveCell)
                    {
                        numNeighbours++;
                    }
                }

                grid[x, y].numNeighbours = numNeighbours;

            }
        }
    }

    void RunGameRules ()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                //- rules
                //any live cell with 2 or 3 live neighbours survives
                //any dead cell with 3 live neighbours becomes a live cell
                //all other live cells die in the next generation and all other dead cells stay dead   

                if (grid[x, y].aliveCell)//cell is currently alive
                {
                    //if cell neighbours != 2 or 3, cell dies
                    if (grid[x,y].numNeighbours != 2 && grid[x,y].numNeighbours != 3)
                    {
                        grid[x,y].SetAlive(false);
                    }
                }
                else//cell is currently dead
                {
    
                    if (grid[x,y].numNeighbours == 3)
                    {
                        grid[x,y].SetAlive(true);
                    }
                }
            }
        }
    }


    //creates a random number generator between 0 - 100, if > 75 returns "true" if else returns "false"
    bool RandomAliveCell ()
    {
        int rand = UnityEngine.Random.Range (0, 100);

        if(rand > 75)
        {
            return true;
        }
        
        return false; 
    }
}
