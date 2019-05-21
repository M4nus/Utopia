using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;

public class MapGeneratorScript : MonoBehaviour
{
    public List<RoomInstance> unvisitedRooms = new List<RoomInstance>();
    public List<RoomInstance> visitedRooms = new List<RoomInstance>();
    public List<RoomInstance> currentRoomGrid = new List<RoomInstance>();
    public int unvisitedRoomsNumber;
    public int visitedRoomsNumber;
    public int currentRoomGridNumber;

    public struct RoomInstance
    {
        public RoomInstance(int roomIndex)
        {
            this.roomIndex = roomIndex;
            isVisited = false;
            roomStateDescription = "missingRoomStateDescription";
        }
        public int roomIndex;
        public bool isVisited;
        public string roomStateDescription;
    }
    // Start is called before the first frame update
    void Start()
    {
        Object.DontDestroyOnLoad(this.gameObject);
        InitStartingRoomSetup(); //Make RoomInstacnes according to the anount of Rooms in assets
        ChooseStartingGrid();//Choose 9 random rooms to make starting location
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadScene(currentRoomGrid[4].roomIndex); //Load scene from RoomInstance at middle position (index 4)
        }
        if (Input.GetKeyDown(KeyCode.U))//Go up one room
        {
            LoadScene(currentRoomGrid[7].roomIndex); //Load scene from RoomInstance at top position (index 7)
        }
        if (Input.GetKeyDown(KeyCode.J))//Go down one room
        {
            LoadScene(currentRoomGrid[1].roomIndex); //Load scene from RoomInstance at down position (index 1)
        }
        if (Input.GetKeyDown(KeyCode.H))//Go left one room
        {
            LoadScene(currentRoomGrid[3].roomIndex); //Load scene from RoomInstance at left position (index 3)
        }
        if (Input.GetKeyDown(KeyCode.K))//Go right one room
        {
            LoadScene(currentRoomGrid[5].roomIndex); //Load scene from RoomInstance at right position (index 5)
        }


        unvisitedRoomsNumber = unvisitedRooms.Count;
        visitedRoomsNumber = visitedRooms.Count;
        currentRoomGridNumber = currentRoomGrid.Count;
      
    }
    void InitStartingRoomSetup()
    {
        int numberOfRoomsInTheData = Directory.GetFiles(Application.dataPath + "\\SCENES\\Rooms").Length;  //Count files in "Rooms" directory
        numberOfRoomsInTheData /= 2;        //Divide by 2 because each room has its metadata
        Debug.Log(numberOfRoomsInTheData);
        for(int i = 0; i < numberOfRoomsInTheData; i++)
        {
            unvisitedRooms.Add(new RoomInstance(i + 1));        //Index from 1 because The name of the first room is "Room1"
        }

    }
    void ChooseStartingGrid()
    {
        if (unvisitedRooms.Count < 9)
        {
            Debug.Log("Not enough rooms to generate starting grid");
            return;
        }
        for(int i = 0; i < 9; i++)
        {
            int tempRand = Random.Range(0, unvisitedRooms.Count);
            currentRoomGrid.Add(unvisitedRooms[tempRand]);
            unvisitedRooms.RemoveAt(tempRand);
        }
    }
    void LoadScene(int gridIndex)
    {
        if (gridIndex < 0)
        {
            Debug.Log("Incorect grid index");
            return;
        }
        SceneManager.LoadScene(gridIndex);
    }
}
