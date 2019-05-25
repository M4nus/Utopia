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
            LoadPlayerIntoMap();

        }
        if (Input.GetKeyDown(KeyCode.U))//Go up one room
        {
            ChangeRoomToUp();

        }
        if (Input.GetKeyDown(KeyCode.J))//Go down one room
        {
            ChangeRoomToDown();
        }
        if (Input.GetKeyDown(KeyCode.H))//Go left one room
        {
            ChangeRoomToLeft();
        }
        if (Input.GetKeyDown(KeyCode.K))//Go right one room
        {
            ChangeRoomToRight();
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

    void LoadPlayerIntoMap()
    {
        LoadScene(currentRoomGrid[4].roomIndex); //Load scene from RoomInstance at middle position (index 4)
    }

    void ChangeRoomToLeft()
    {
        LoadScene(currentRoomGrid[3].roomIndex); //Load scene from RoomInstance at left position (index 3)
        UnloadScene(8);
        UnloadScene(5);
        UnloadScene(2);
        currentRoomGrid[6] = currentRoomGrid[7];
        currentRoomGrid[3] = currentRoomGrid[4];
        currentRoomGrid[0] = currentRoomGrid[1];
        currentRoomGrid[7] = currentRoomGrid[8];
        currentRoomGrid[4] = currentRoomGrid[5];
        currentRoomGrid[1] = currentRoomGrid[2];
    }

    void ChangeRoomToRight()
    {
        LoadScene(currentRoomGrid[5].roomIndex); //Load scene from RoomInstance at right position (index 5)
        UnloadScene(6);
        UnloadScene(3);
        UnloadScene(0);
        currentRoomGrid[8] = currentRoomGrid[7];
        currentRoomGrid[5] = currentRoomGrid[4];
        currentRoomGrid[2] = currentRoomGrid[1];
        currentRoomGrid[7] = currentRoomGrid[6];
        currentRoomGrid[4] = currentRoomGrid[3];
        currentRoomGrid[1] = currentRoomGrid[0];
    }

    void ChangeRoomToUp()
    {
        LoadScene(currentRoomGrid[7].roomIndex); //Load scene from RoomInstance at top position (index 7)
        UnloadScene(0);
        UnloadScene(1);
        UnloadScene(2);
        currentRoomGrid[6] = currentRoomGrid[3];
        currentRoomGrid[7] = currentRoomGrid[4];
        currentRoomGrid[8] = currentRoomGrid[5];
        currentRoomGrid[3] = currentRoomGrid[0];
        currentRoomGrid[4] = currentRoomGrid[1];
        currentRoomGrid[5] = currentRoomGrid[2];
    }

    void ChangeRoomToDown()
    {
        LoadScene(currentRoomGrid[1].roomIndex); //Load scene from RoomInstance at down position (index 1)
        UnloadScene(6);
        UnloadScene(7);
        UnloadScene(8);
        currentRoomGrid[0] = currentRoomGrid[3];
        currentRoomGrid[1] = currentRoomGrid[4];
        currentRoomGrid[2] = currentRoomGrid[5];
        currentRoomGrid[3] = currentRoomGrid[6];
        currentRoomGrid[4] = currentRoomGrid[7];
        currentRoomGrid[5] = currentRoomGrid[8];
    }

    void UnloadScene(int index)
    {
        if(index < 0 || index > 8)
        {
            Debug.Log("Something tried to unload scene with bad index: Index" + index);
        }
        RoomInstance SceneToUnload = currentRoomGrid[index];
        currentRoomGrid.RemoveAt(index);

        if (SceneToUnload.isVisited == true)
        {
            visitedRooms.Add(SceneToUnload);
        }
        else
        {
            currentRoomGrid.RemoveAt(index);
        }
    }
}
