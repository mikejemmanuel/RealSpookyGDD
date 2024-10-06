using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{   
    /*
    The intention for this script is to be put on a GameObject representing a room
    For example, the Kitchen and Bedroom would be GameObjects and have this script
    This script will keep all of their necesarry information whil also giving 
        functions to change their information
    Using the inspector's serialized values for this script, it will handle placing
        all starting objects
    Please have one of each item in the hierachy so they can be used in the serialized 
        fields. It is important to have the instantiated version of them.
    */

    #region Tile Variables
    [SerializeField]
    [Tooltip("How many tiles will be in one row?")]
    private int roomSize;
    [SerializeField]
    [Tooltip("Items that will need to be in this room on start.")]
    private GameObject[] roomStarterObjects;
    [SerializeField]
    [Tooltip("1 to 1 column and row of each starter object.")]
    private Vector2[] starterObjectPositions;
    private GameObject[,] tileOccupancy;
    #endregion

    #region Map Variables 
    private int screenXBound;
    private int screenYBound;
    [SerializeField]
    [Tooltip("Kitchen, Bedroom, Bathroom, or LivingRoom EXACTLY")]
    private string roomName;
    [SerializeField]
    [Tooltip("Put the two Hole objects here")]
    private GameObject[] holes;
    private int resetObjects;
    #endregion

    [SerializeField]
    [Tooltip("Put the fixed Mirror Sprite here")]
    private Sprite fixedMirror;
    void Awake() 
    {
        //tileOccupancy = new GameObject[roomSize,roomSize];
        //Hard Coded Bounds for Vector2 usage
        screenXBound = 20;
        screenYBound = 10;

        //Loop to fill the tileOccupancy 2D array with values from the inspector
        //All values (objects) from the inspector are put in their grid position
        tileOccupancy = new GameObject[roomSize,roomSize];
        for (int i = 0; i < roomStarterObjects.Length; i++){
            int column = (int)starterObjectPositions[i].x;
            int row = (int)starterObjectPositions[i].y;
            tileOccupancy[column - 1, row - 1] = roomStarterObjects[i];
            roomStarterObjects[i].transform.position = GetTileCenter(column, row);
        }

        //Place the holes onto the map
        holes[0].transform.position = GetTileCenter(3, 5);
        holes[1].transform.position = GetTileCenter(1, 1);

        //Reset Onbject Helper
        resetObjects = 0;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (resetObjects == 3) {
            Debug.Log(resetObjects);
            roomStarterObjects[0].GetComponent<ObjectReset>().resetMachine();
            resetObjects = 0;
            Debug.Log(resetObjects);
        }
    }

    //Input a column and row to get the GameObject that is on that tile (1, 1 is the bottom)
    public GameObject GetTileObject(int column, int row) {
        return tileOccupancy[column - 1, row - 1];
    }
    //Input a columns and row to get the center position of that tile (1, 1 is the bottom)
    public Vector2 GetTileCenter(int column, int row) {
        int tileWidth = screenXBound / 2 / roomSize;
        int tileHeight = screenYBound / roomSize;
        int xPos = (tileWidth * column) - (tileWidth / 2) - (screenXBound / 4);
        int yPos = (tileHeight * row) - (tileHeight / 2) - (screenYBound / 2);
        
        int xOffset = 0;
        int yOffset = 0;
        if (roomName == "Bedroom") {
            xOffset = 0;
            yOffset = 0;
        } else if (roomName == "LivingRoom") {
            xOffset = -10;
            yOffset = 0;
        } else if (roomName == "Kitchen") {
            xOffset = -10;
            yOffset = -10;
        } else if (roomName == "Bathroom") {
            xOffset = 0;
            yOffset = -10;
        }
        return new Vector2(xPos + xOffset, yPos + yOffset);
    }
    //Give column and row of tile to move its object in direction 
    //tileOccupancy is updated as a move occurs
    public bool moveObejct(int column, int row, string direction) {
        GameObject objectToMove = GetTileObject(column, row);
        if (direction == "up") {
            if ((row + 1) > 5) {
                Debug.Log("OUT OF BOUNDS");
            } else if (GetTileObject(column, row + 1) == null){
                if (objectToMove.GetComponent<ObjectReset>() != null) {
                    objectToMove.GetComponent<ObjectReset>().Up();
                }
                objectToMove.transform.position = GetTileCenter(column, row + 1);
                tileOccupancy[column - 1, row - 1] = null;
                tileOccupancy[column - 1, row] = objectToMove;
                return true;
            } else {
                Debug.Log("NO MORE SPACE UPWARDS");
            }
        } else if (direction == "down") {
            if ((row - 1) < 1) {
                Debug.Log("OUT OF BOUNDS");
            } else if (GetTileObject(column, row - 1) == null){
                if (objectToMove.GetComponent<ObjectReset>() != null) {
                    objectToMove.GetComponent<ObjectReset>().Down();
                }
                objectToMove.transform.position = GetTileCenter(column, row - 1);
                tileOccupancy[column - 1, row - 1] = null;
                tileOccupancy[column - 1, row - 2] = objectToMove;
                return true;
            } else {
                Debug.Log("NO MORE SPACE DOWNWARDS");
            }
        } else if (direction == "left") {
            if ((column - 1) < 1) {
                Debug.Log("OUT OF BOUNDS");
            } else if (GetTileObject(column - 1, row) == null){
                if (objectToMove.GetComponent<ObjectReset>() != null) {
                    objectToMove.GetComponent<ObjectReset>().Left();
                }
                objectToMove.transform.position = GetTileCenter(column - 1, row);
                tileOccupancy[column - 1, row - 1] = null;
                tileOccupancy[column - 2, row - 1] = objectToMove;
                return true;
            } else {
                Debug.Log("NO MORE SPACE LEFTWARDS");
            }
        } else if (direction == "right") {
            if ((column + 1) > 5) {
                Debug.Log("OUT OF BOUNDS");
            } else if (GetTileObject(column + 1, row) == null){
                if (objectToMove.GetComponent<ObjectReset>() != null) {
                    objectToMove.GetComponent<ObjectReset>().Right();
                }
                objectToMove.transform.position = GetTileCenter(column + 1, row);
                tileOccupancy[column - 1, row - 1] = null;
                tileOccupancy[column, row - 1] = objectToMove;
                return true;
            } else {
                Debug.Log("NO MORE SPACE RIGHTWARDS");
            }
        }
        return false;
    }

    public void deleteOccupancy(int column, int row) {
        tileOccupancy[column - 1, row - 1] = null;
        return;
    }
    public void fillOccupancy(int column, int row, GameObject fillerObject) {
        tileOccupancy[column - 1, row - 1] = fillerObject;
        return;
    }

    public void resetObjectsAppend() {
        resetObjects += 1;
    }
    public int getResetObjects() {
        return resetObjects;
    }

    public void fixMirror() {
        roomStarterObjects[5].GetComponent<SpriteRenderer>().sprite = fixedMirror;
        roomStarterObjects[5].tag = "Possessable";
    }
}
