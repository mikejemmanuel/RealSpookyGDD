using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallReactor : MonoBehaviour
{
    [SerializeField]
    [Tooltip("PUT EXACT COLUMN THAT IS IN BEDROOM")]
    private int myColumn;
    [SerializeField]
    [Tooltip("PUT EXACT ROW THAT IS IN BEDROOM")]
    private int myRow;
    [SerializeField]
    [Tooltip("Put the Pressure Plate Object here")]
    private GameObject pressurePlate;
    [SerializeField]
    [Tooltip("Put the Bedroom Object here")]
    private GameObject myRoom;
    private bool hasntOpened;
    private bool needRoomReset;

    // Start is called before the first frame update
    void Start()
    {
        hasntOpened = false;
        needRoomReset = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pressurePlate.GetComponent<PlateScript>().isActive()) {
            if (hasntOpened) {
                Debug.Log("OPENING");
                Open();
                hasntOpened = false;
            }
        } else {
            if (myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow) != this.gameObject) {
                Debug.Log("CLOSING");
                Close();
            }
            hasntOpened = true;
        }
    }

    //Make walls disappear or reappear from view and room 2D array.
    public void Open() {
        this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0f);
        myRoom.GetComponent<MapHandler>().deleteOccupancy(myColumn, myRow);
    }
    public void Close() {
        this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
        if (myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow) != null) {
            needRoomReset = true;
        }
        myRoom.GetComponent<MapHandler>().fillOccupancy(myColumn, myRow, this.gameObject);
    }

    //Others can check if the room neeeds to be reset to avoid bugs/softlocks
    public bool needToReset() {
        return needRoomReset;
    }

    //Others can call for reset variable to reset
    public void resetReset() {
        needRoomReset = false;
    }
}
