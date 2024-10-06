using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleBurn : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Put the bedroom here")]
    private GameObject myRoom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int column = this.GetComponent<ObjectReset>().getColumn();
        int row = this.GetComponent<ObjectReset>().getRow();
        if (column + 1 <= 5) {
            if (myRoom.GetComponent<MapHandler>().GetTileObject(column + 1, row) != null) {
                Debug.Log("Checking For Webs RIGHT");
                if (myRoom.GetComponent<MapHandler>().GetTileObject(column + 1, row).transform.parent.gameObject.name == "Webs") {
                    Debug.Log("Webs FOUND");
                    myRoom.GetComponent<MapHandler>().GetTileObject(column + 1, row).SetActive(false);
                    myRoom.GetComponent<MapHandler>().deleteOccupancy(column + 1, row);
                }
            }
        } 
        if (column - 1 >= 1) {
            if (myRoom.GetComponent<MapHandler>().GetTileObject(column - 1, row) != null) {
                Debug.Log("Checking For Webs LEFT");
                if (myRoom.GetComponent<MapHandler>().GetTileObject(column - 1, row).transform.parent.gameObject.name == "Webs") {
                    Debug.Log("Webs FOUND");
                    myRoom.GetComponent<MapHandler>().GetTileObject(column - 1, row).SetActive(false);
                    myRoom.GetComponent<MapHandler>().deleteOccupancy(column - 1, row);
                }
            }
        }
        if (row + 1 <= 5) {
            if (myRoom.GetComponent<MapHandler>().GetTileObject(column, row + 1) != null) {
                Debug.Log("Checking For Webs UP");
                if (myRoom.GetComponent<MapHandler>().GetTileObject(column, row + 1).transform.parent.gameObject.name == "Webs") {
                    Debug.Log("Webs FOUND");
                    myRoom.GetComponent<MapHandler>().GetTileObject(column, row + 1).SetActive(false); 
                    myRoom.GetComponent<MapHandler>().deleteOccupancy(column, row + 1);
            }
            
            }
        }
        if (row - 1 <= 5) {
            if (myRoom.GetComponent<MapHandler>().GetTileObject(column, row - 1) != null) {
                Debug.Log("Checking For Webs DOWN");
                if (myRoom.GetComponent<MapHandler>().GetTileObject(column, row - 1).transform.parent.gameObject.name == "Webs") {
                    Debug.Log("Webs FOUND");
                    myRoom.GetComponent<MapHandler>().GetTileObject(column, row - 1).SetActive(false);
                    myRoom.GetComponent<MapHandler>().deleteOccupancy(column, row - 1);
                }
            }
        }
    }
}
