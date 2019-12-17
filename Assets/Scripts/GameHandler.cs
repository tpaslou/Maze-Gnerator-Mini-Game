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
    
    public enum Directions
    {
        Up,
        Down,
        Right,
        Left
            
    }
    
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
    
    private void SearchNext()
    {
        while (RouteStillAvailable( currRow, currCol))
        {
            int direction = Random.Range(1, 5);


        }
    }

    private void BreakWalls()
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
          BreakWalls();//Keeps walking till a dead end
          SearchNext();//Search for the the next unvisited cell with an adjacent visited cell.If there isnt any one the we are done
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
