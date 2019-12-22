using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/*Recursive backtracker

Given a current cell as a parameter,
    Mark the current cell as visited
    While the current cell has any unvisited neighbour cells
        Chose one of the unvisited neighbours
        Remove the wall between the current cell and the chosen cell
Invoke the routine recursively for a chosen cell
which is invoked once for any initial cell in the area.

Note : implemented without recursion , but with 2 Main Functions using loops
 * 
 */
public class MazeGenerator : MonoBehaviour
{
   
    public int Width, Heigth;

    public InputField WidthInput, HeightInput;

    public Cell[,] Maze;
    //3D reconstruction Variables
    public GameObject wall;
    public float size = 2f;
    
    //Pseudo-Recursive Backtracing variables 
    private int currRow, currCol=0;
    private bool Completed = false;


    //This is the Cell Class whitch consists of the walls and the visited 
    //var in order to use it our maze generation algorithm
    public class Cell
    {
        //I would usually use private but theI would have to 
        //write extra methods.For the purposes of this
        //test I use public variables
        public bool right, left, up, down,visited;
        public Cell()
        {
            this.down = this.left = this.up = this.right = true;
            visited = false;
        }
    }

    //Get UI dimensions and Use them for maze
    public void SetDimensions()
    {
        this.Heigth = Int32.Parse(HeightInput.text);
        this.Width = Int32.Parse(WidthInput.text);;
        Debug.Log("Got dimensions : "+Width+" "+Heigth);
    }
    
    //Checking Neighbours If they are Vistided
    private bool RouteExists(int row, int col) {
        int availableRoutes = 0;
        
        if (row > 0 && !Maze[row-1,col].visited) {
            availableRoutes++;
        }

        if (row < Heigth - 1 && !Maze [row + 1, col].visited) {
            availableRoutes++;
        }

        if (col > 0 && !Maze[row,col-1].visited) {
            availableRoutes++;
        }

        if (col < Width-1 && !Maze[row,col+1].visited) {
            availableRoutes++;
        }
        
        return availableRoutes > 0;
    }
    
    //Help function for shorter "IFs"
    private bool isValidCell(int row, int col)
    {
        if ( row>=0 && col>=0  && row < Heigth  && col < Width && ! Maze[row, col].visited  ) {
            return true;
        } else {
            return false;
        }
    }
    
    private void DestroyCurrent()
    {
        //If there is no Route available 
        while (RouteExists( currRow, currCol))
        {
            int direction = Random.Range(1, 5);
            
            //Accoding to chosen direction of the random number we
            //delete the wall and move up down left or right
            
          
            if (direction == 1 && isValidCell(currRow-1,currCol))
            {//up
                Maze[currRow, currCol].up = false;
                Maze[currRow - 1, currCol].down = false;
                currRow--;
            }else if (direction == 2 && isValidCell(currRow + 1, currCol))
            {//down
                Maze[currRow + 1, currCol].up = false;
                Maze[currRow, currCol].down = false;
                currRow++;
            }else if (direction == 3 && isValidCell(currRow, currCol + 1))
            {//right
                Maze[currRow, currCol].right = false;
                Maze[currRow, currCol + 1].left = false;
                currCol++;
            }else if (direction==4 && isValidCell(currRow, currCol - 1))
            {//left
                Maze[currRow, currCol].left = false;
                Maze[currRow, currCol - 1].right = false;
                currCol--;
            } 
            Maze[currRow, currCol].visited = true;

        }
    }

    
    //When we hit dead end and we cant visit other cells 
    //through our root and first 3 steps
    //we go to last step of algorithm and search for unvisited
    //cells in our maze and repeat the procedure if we find one
    private void SearchNew()
    {

        Completed = true;
        for (int i = 0; i < Heigth; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (!Maze[i, j].visited)
                {
                    Completed = false;
                    currCol = j;
                    currRow = i;
                    DestroyAdjacent(i,j);;
                    
                    Maze[i, j].visited = true;

                    return;
                    
                }
            }
        }

    }

    private void DestroyAdjacent(int row, int column)
    {

        while (true)
        {
            int direction = Random.Range(1, 5);
            //int direction = ProceduralNumberGenerator.GetNextNumber ();

            if (direction == 1 && row > 0 && Maze[row - 1, column].visited)
            {
                Maze[row, column].up = false;
                Maze[row - 1, column].down = false;
                break;
            }
            else if (direction == 2 && row < (Heigth - 2) && Maze[row + 1, column].visited)
            {
                Maze[row, column].down = false;
                Maze[row + 1, column].up = false;
                break;
            }
            else if (direction == 3 && column > 0 && Maze[row, column - 1].visited)
            {
                Maze[row, column].left = false;
                Maze[row, column - 1].right = false;
                break;
            }
            else if (direction == 4 && column < (Width - 2) && Maze[row, column + 1].visited)
            {
                Maze[row, column].right = false;
                Maze[row, column + 1].left = false;
                break;
            }
        }
    }



    //Create Maze
    public void CreateMaze()
    {
      //Initialize
      Maze = new Cell[Heigth,Width];
      for (int i = 0; i < Heigth; i++)
      {
          for (int j = 0; j < Width; j++)
          {
              Maze[i, j] = new Cell();
          }
      }
      
      //Set first cell visited
      Maze[0, 0].visited = true;
      while ( !Completed)
      {
          DestroyCurrent();//Keeps walking till a dead end
          SearchNew();//Search for the the next unvisited cell with an adjacent visited cell.If there isnt any one the we are done
      }

    }

   

    public void DrawMaze()
    {
        GameObject container = GameObject.Find("Table/MazePivot");
        //We get our pivot point to start drawing in Word Cordinates
        float px = container.transform.position.x;
        float pz = container.transform.position.z;
        
        for (int i = 0; i < Heigth; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                
                //For every cell we check our values and if true we spawn the desired walls
                //We use our pivot values as offset for starting point
                if (j == 0) {
                    if(Maze[i,j].left){
                    GameObject Wall = Instantiate (wall, new Vector3 (px+i*size, 0, pz+(j*size) - (size/2f)), Quaternion.identity) as GameObject;
                    Wall.transform.parent = container.transform;
                    Wall.name = "LeftWall " + i + "," + j;
                    }
                }

                if (Maze[i, j].right)
                {
                    GameObject Wall = Instantiate(wall, new Vector3(px+i * size, 0, pz+(j * size) + (size / 2f)),
                        Quaternion.identity) as GameObject;
                    
                    Wall.name = "RightWall " + i + "," + j;
                    Wall.transform.parent = container.transform;
                }

                if (i == 0) {
                    if(Maze[i,j].up){
                    GameObject Wall = Instantiate (wall, new Vector3 (px+(i*size) - (size/2f), 0, pz+j*size), Quaternion.identity) as GameObject;
                    Wall.name = "UpperWall " + i + "," + j;
                    Wall.transform.parent = container.transform; 
                    Wall.transform.Rotate (Vector3.up * 90f);
                    }
                }

                if (Maze[i, j].down)
                {
                    GameObject Wall = Instantiate(wall, new Vector3(px+(i * size) + (size / 2f), 0, pz+j * size),
                        Quaternion.identity) as GameObject;
                    Wall.transform.parent = container.transform;
                    Wall.name = "BottomWall " + i + "," + j;
                    Wall.transform.Rotate(Vector3.up * 90f);
                }
            }
        }
        //Then we scale our object base on our Width and Height Dimensions
        
      
    }

    //Gets called when Generate Maze button is hit 
    public void InitialiseMaze()
    {
        SetDimensions();
        CreateMaze();
        DrawMaze();
    }
    
    // Start is called before the first frame update
    void Start()
    {
    
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
