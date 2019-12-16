using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
   
    public int Width, Heigth;

    public InputField WidthInput, HeightInput;
    //Depending on the Cell Type we Define the walls around it
    public enum CellType
    {
        LColumnCell,
        LRowCell,
        InnerCell
            
    }
    
    //This is the Cell Class witch consists of the walls and the visited 
    //var in order to use it our maze generation algorithm
    public class Cell
    {
        private bool right, left, up, down,visited;
        private CellType cellType;   
       
        
        public Cell(CellType ct)
        {
            visited = false;
            switch (ct)
            {
                case (CellType.InnerCell):

                    break;
                case (CellType.LColumnCell):

                    break;
                case (CellType.LRowCell):
                    
                    break;
                default:
                    Debug.Log("Error in Cell Initialization");
                    break;
                
            }
            
            
        }
    }

    public void SetDimensions()
    {
        this.Heigth = Int32.Parse(HeightInput.text);
        this.Width = Int32.Parse(WidthInput.text);;
        Debug.Log("Got dimensions : "+Width+" "+Heigth);
    }

   

    //Create Maze
    public void CreateMaze_Prims()
    {
        
    }

    public void DrawMaze()
    {
        
    }


    public void InitialiseMaze()
    {
        SetDimensions();
        CreateMaze_Prims();
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
