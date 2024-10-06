using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("PUT EXACT COLUMN THAT IS IN BEDROOM")]
    private int myColumn;
    [SerializeField]
    [Tooltip("PUT EXACT ROW THAT IS IN BEDROOM")]
    private int myRow;
    [SerializeField]
    [Tooltip("Put the Bedroom Object here")]
    private GameObject myRoom;
    [SerializeField]
    [Tooltip("Put the Ghost Object here")]
    private GameObject ghost;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = myRoom.GetComponent<MapHandler>().GetTileCenter(myColumn, myRow);
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow) != null) {
            active = true;
            Debug.Log("ACTIVE");
        } else if ((ghost.GetComponent<GhostController>().getColumn() == myColumn) && (ghost.GetComponent<GhostController>().getRow() == myRow)) {
            active = true;
            Debug.Log("ACTIVE");
        } else {
            active = false;
            Debug.Log("INACTIVE");
        }
    }

    public bool isActive() {
        return active;
    }
}
