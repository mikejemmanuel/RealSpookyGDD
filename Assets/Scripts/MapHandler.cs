using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{   
    #region Tile Variables
    [SerializeField]
    [Tooltip("How many tiles will be in one row?")]
    private int roomSize;
    private GameObject[,] tileOccupancy;
    #endregion

    #region Map Variables 
    private int screenXBound;
    private int screenYBound;
    #endregion

    public GameObject myObject;

    // Start is called before the first frame update
    void Start()
    {
        tileOccupancy = new GameObject[roomSize,roomSize];
        screenXBound = 10;
        screenYBound = 5;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Input a column and row to get the GameObject that is on that tile (1, 1 is the bottom)
    public GameObject GetTileObject(int colummn, int row) {
        return tileOccupancy[colummn, row];
    }
    //Input a columns and row to get the center position of that tile (1, 1 is the bottom)
    public Vector2 GetTileCenter(int colummn, int row) {
        int tileWidth = screenXBound * 2 / roomSize;
        int tileHeight = screenYBound  * 2 / roomSize;
        int xPos = (tileWidth * colummn) - (tileWidth / 2) - screenXBound;
        int yPos = (tileHeight * row) - (tileHeight / 2) - screenYBound;
        return new Vector2(xPos, yPos);
    }
}
