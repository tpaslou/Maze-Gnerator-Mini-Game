using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public Material startMat, endMat, PlayerMat;

    public MazeGenerator scr;
    public bool Started=false;
   
    private VerySimpleMove Pscr;
    private GameObject player;
    public void StartGame()
    {
        
        if(!Started){
        string pos;
        int width, height;
        GameObject container = GameObject.Find("Table/MazePivot");
        //We get our pivot point to start drawing in Word Cordinates
        float px = container.transform.position.x;
        float pz = container.transform.position.z;
        //I spawned the maze rotated, so in order not to make
        //many changes in code i changed width and height in 
        //UI only , and the application works
        height = scr.Width-1;
        width = scr.Heigth-1;
        pos = " " + width.ToString() + "," + height.ToString();
        //Start
        if(GameObject.Find("MazePivot/LeftWall 0,0")!=null) GameObject.Find("MazePivot/LeftWall 0,0").GetComponent<MeshRenderer>().material = startMat;
        if(GameObject.Find("MazePivot/UpperWall 0,0")!=null)GameObject.Find("MazePivot/UpperWall 0,0").GetComponent<MeshRenderer>().material = startMat;
        if(GameObject.Find("MazePivot/BottomWall 0,0")!=null)GameObject.Find("MazePivot/BottomWall 0,0").GetComponent<MeshRenderer>().material = startMat;
        if(GameObject.Find("MazePivot/RightWall 0,0")!=null)GameObject.Find("MazePivot/RightWall 0,0").GetComponent<MeshRenderer>().material = startMat;

        //End
        if (GameObject.Find("MazePivot/BottomWall" + pos) != null)
        {
            GameObject wall = GameObject.Find("MazePivot/BottomWall" + pos);
            wall.GetComponent<MeshRenderer>().material = endMat;
            wall.tag = "GameEnd";
            wall.GetComponent<BoxCollider>().isTrigger = true;
        }

        if (GameObject.Find("MazePivot/UpperWall" + pos) != null)
        {
            GameObject wall = GameObject.Find("MazePivot/UpperWall" + pos); 
            wall.GetComponent<MeshRenderer>().material = endMat;
            wall.tag = "GameEnd";
            wall.GetComponent<BoxCollider>().isTrigger = true;
        }

        if (GameObject.Find("MazePivot/LeftWall" + pos) != null)
        {
            GameObject wall = GameObject.Find("MazePivot/LeftWall" + pos); 
            wall.GetComponent<MeshRenderer>().material = endMat;
            wall.tag = "GameEnd";
            wall.GetComponent<BoxCollider>().isTrigger = true;

        }

        if (GameObject.Find("MazePivot/RightWall" + pos) != null)
        {
            GameObject wall = GameObject.Find("MazePivot/RightWall" + pos); 
            wall.GetComponent<MeshRenderer>().material = endMat;
            wall.tag = "GameEnd";
            wall.GetComponent<BoxCollider>().isTrigger = true;

        }
        
        
        //Create Player and add the needed Attributes
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(px, 1.5f, pz);
        Rigidbody gameObjectsRigidBody = sphere.AddComponent<Rigidbody>(); 
        gameObjectsRigidBody.mass = 5;
        sphere.AddComponent<VerySimpleMove>();
        TrailRenderer tr = sphere.AddComponent<TrailRenderer>();
        tr.time = 1000;//So the line doesnt dissapear
        tr.material = PlayerMat;
        sphere.name = "Player";
        Pscr = sphere.GetComponent<VerySimpleMove>();
        player = sphere;
        Started = true;
            
        }


    }

    // Update is called once per frame
    void Update()
    {
        if ( Pscr!=null &&  Pscr.GameEnd )
        {
            Debug.Log("You WON !");
            Destroy(player);
        }
        
    }
}
