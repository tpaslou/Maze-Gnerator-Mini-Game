using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class GameHandler : MonoBehaviour
{
   
    public int Width, Heigth;

    public InputField WidthInput, HeightInput;

    public Cell[,] Maze;

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
    private bool RouteStillAvailable(int row, int column) {
        int availableRoutes = 0;
        
        if (row > 0 && !Maze[row-1,column].visited) {
            availableRoutes++;
        }

        if (row < Heigth - 1 && !Maze [row + 1, column].visited) {
            availableRoutes++;
        }

        if (column > 0 && !Maze[row,column-1].visited) {
            availableRoutes++;
        }

        if (column < Width-1 && !Maze[row,column+1].visited) {
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
    
    private void DestroyAdjacent()
    {
        //If there is no Route available 
        while (RouteStillAvailable( currRow, currCol))
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
                Maze[currRow, currRow].down = false;
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
            Maze[currCol, currCol].visited = true;

        }
    }

    

    private void SearchNew()
    {
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
          DestroyAdjacent();//Keeps walking till a dead end
          SearchNew();//Search for the the next unvisited cell with an adjacent visited cell.If there isnt any one the we are done
      }

    }

   

    public void DrawMaze()
    {
        
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
