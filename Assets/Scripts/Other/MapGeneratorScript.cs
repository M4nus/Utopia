using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;

public class MapGeneratorScript : MonoBehaviour
{
    [SerializeField] Transform _camera;
    [SerializeField] Transform _player;
    public List<RoomInstance> unvisitedRooms = new List<RoomInstance>();
    public List<RoomInstance> visitedRooms = new List<RoomInstance>();
    public List<RoomInstance> currentRoomGrid = new List<RoomInstance>();
   [SerializeField] public GameObject []RoomSprites = new GameObject[9];
    public int unvisitedRoomsNumber;
    public int visitedRoomsNumber;
    public int currentRoomGridNumber;
    public int room0;
    public int room1;
    public int room2;
    public int room3;
    public int room4;
    public int room5;
    public int room6;
    public int room7;
    public int room8;
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
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _player = GameObject.Find("[ANTONI]").transform;

    }

    // Update is called once per frame
    void Update()
    {
        room0 = currentRoomGrid[0].roomIndex;
        room1 = currentRoomGrid[1].roomIndex;
        room2 = currentRoomGrid[2].roomIndex;
        room3 = currentRoomGrid[3].roomIndex;
        room4 = currentRoomGrid[4].roomIndex;
        room5 = currentRoomGrid[5].roomIndex;
        room6 = currentRoomGrid[6].roomIndex;
        room7 = currentRoomGrid[7].roomIndex;
        room8 = currentRoomGrid[8].roomIndex;
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
        if (_camera == null || _player == null)
        {
            _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
            _player = GameObject.Find("[ANTONI]").transform;
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
        LoadRoomSprites();
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _player = GameObject.Find("[ANTONI]").transform;

    }

    void ChangeRoomToLeft()
    {
        SaveCurrentRoom();
        StartCoroutine(MoveCameraToNextScene("x", -1, 5.5f, 3)); //Load scene from RoomInstance at left position (index 3)
    }
    void ChangeGridLeft()
    {
        UnloadScene(8);
        UnloadScene(5);
        UnloadScene(2);
        currentRoomGrid[8] = currentRoomGrid[7];
        currentRoomGrid[5] = currentRoomGrid[4];
        currentRoomGrid[2] = currentRoomGrid[1];
        currentRoomGrid[7] = currentRoomGrid[6];
        currentRoomGrid[4] = currentRoomGrid[3];
        currentRoomGrid[1] = currentRoomGrid[0];
        for (int i = 0; i < 7; i += 3)
        {
            currentRoomGrid[i] = GetRoomInstanceToLoad();
        }
        LoadRoomSprites();
    }

    void ChangeRoomToRight()
    {
        SaveCurrentRoom();
        StartCoroutine(MoveCameraToNextScene("x", 1, 5.5f, 5)); //Load scene from RoomInstance at right position (index 5)
    }
    void ChangeGridRight()
    {
        UnloadScene(0);
        UnloadScene(3);
        UnloadScene(6);
        currentRoomGrid[0] = currentRoomGrid[1];
        currentRoomGrid[3] = currentRoomGrid[4];
        currentRoomGrid[6] = currentRoomGrid[7];
        currentRoomGrid[1] = currentRoomGrid[2];
        currentRoomGrid[4] = currentRoomGrid[5];
        currentRoomGrid[7] = currentRoomGrid[8];
        for (int i = 2; i < 9; i += 3)
        {
            currentRoomGrid[i] = GetRoomInstanceToLoad();
        }
        LoadRoomSprites();
    }

    void ChangeRoomToUp()
    {
        SaveCurrentRoom();
        StartCoroutine(MoveCameraToNextScene("y", 1, 5.5f, 7)); //Load scene from RoomInstance at top position (index 7)
    }
    void ChangeGridUp()
    {
        UnloadScene(0);
        UnloadScene(1);
        UnloadScene(2);
        currentRoomGrid[0] = currentRoomGrid[3];
        currentRoomGrid[1] = currentRoomGrid[4];
        currentRoomGrid[2] = currentRoomGrid[5];
        currentRoomGrid[3] = currentRoomGrid[6];
        currentRoomGrid[4] = currentRoomGrid[7];
        currentRoomGrid[5] = currentRoomGrid[8];
        for (int i = 6; i < 9; i += 1)
        {
            currentRoomGrid[i] = GetRoomInstanceToLoad();
        }
        LoadRoomSprites();
    }

    void ChangeRoomToDown()
    {
        SaveCurrentRoom();
        StartCoroutine(MoveCameraToNextScene("y", -1, 5.5f, 1)); //Load scene from RoomInstance at down position (index 1)
    }
    void ChangeGridDown()
    {
        UnloadScene(6);
        UnloadScene(7);
        UnloadScene(8);
        currentRoomGrid[6] = currentRoomGrid[3];
        currentRoomGrid[7] = currentRoomGrid[4];
        currentRoomGrid[8] = currentRoomGrid[5];
        currentRoomGrid[3] = currentRoomGrid[0];
        currentRoomGrid[4] = currentRoomGrid[1];
        currentRoomGrid[5] = currentRoomGrid[2];
        for (int i = 0; i < 3; i += 1)
        {
            currentRoomGrid[i] = GetRoomInstanceToLoad();
        }
        LoadRoomSprites();
    }
    void UnloadScene(int index)
    {
        if(index < 0 || index > 8)
        {
            Debug.Log("Something tried to unload scene with bad index: Index" + index);
        }
        RoomInstance SceneToUnload = currentRoomGrid[index];

        if (SceneToUnload.isVisited == true)
        {
            visitedRooms.Add(SceneToUnload);
        }
        else
        {
            unvisitedRooms.Add(SceneToUnload);
        }
    }

    RoomInstance GetRoomInstanceToLoad()
    {
        RoomInstance roomInstance;
        if (unvisitedRooms.Count > 0)
        {
            int choosenRoomIndex = (int)Random.Range(0, unvisitedRooms.Count - 1);
            roomInstance = unvisitedRooms[choosenRoomIndex];
            unvisitedRooms.RemoveAt(choosenRoomIndex);
        }
        else if (visitedRooms.Count > 0)
        {
            int choosenRoomIndex = (int)Random.Range(0, visitedRooms.Count - 1);
            roomInstance = visitedRooms[choosenRoomIndex];
            visitedRooms.RemoveAt(choosenRoomIndex);
        }
        else
        {
            Debug.Log("There are no more room instances to load");
            roomInstance = new RoomInstance(0);
        }
        return roomInstance;
    }

    void SaveCurrentRoom()
    {
        RoomInstance tempRoomInstance = currentRoomGrid[4];
        tempRoomInstance.isVisited = true;
        tempRoomInstance.roomStateDescription = "RoomHasBeenVisited";
        currentRoomGrid[4] = tempRoomInstance;
    }

    public IEnumerator MoveCameraToNextScene(string axis,int sign, float distToMove, int nextSceneIndex)
    {
        float pos = 0; // Camera move increaser  
        _player.transform.gameObject.SetActive(false); 
        if(axis == "x")
        {
            distToMove = 5.5f;
        }
        else
        {
            distToMove = 3f;
        }
        while (distToMove > Mathf.Abs(pos))
        {
            Vector3 move = (axis == "x") ? new Vector3(sign * pos, 0f, -5f) : new Vector3(0f, sign * pos, -5f);
            _camera.position = move;
            pos += Time.deltaTime;
            yield return null;
        }
        if (nextSceneIndex == 1)
        {
            ChangeGridDown();
        }
        else if(nextSceneIndex == 3)
        {
            ChangeGridLeft();
        }
        else if(nextSceneIndex == 5)
        {
            ChangeGridRight();
        }
        else
        {
            ChangeGridUp();
        }
        LoadScene(currentRoomGrid[4].roomIndex);

    }
    void LoadRoomSprites()
    {
        for (int i = 0; i < 9; i++)
        {
            if (i == 4) { continue; }
            RoomSprites[i] = Resources.Load<GameObject>("RoomSprites/OneRoom/Room" + currentRoomGrid[i].roomIndex);

        }
    }





}
