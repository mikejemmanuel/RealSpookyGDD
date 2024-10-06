using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReset : MonoBehaviour
{
    [SerializeField]
    [Tooltip("PUT COLUMN THAT IS IN BEDROOM")]
    private int ogColumn;
    [SerializeField]
    [Tooltip("PUT ROW THAT IS IN BEDROOM")]
    private int ogRow;
    [SerializeField]
    [Tooltip("Put the Bedroom Object here")]
    private GameObject myRoom;
    [SerializeField]
    [Tooltip("Put in top right wall Objects here")]
    private GameObject[] wallObjects;
    private int currentColumn;
    private int currentRow;

    // Start is called before the first frame update
    void Start()
    {
        currentColumn = ogColumn;
        currentRow = ogRow;   
    }

    // Update is called once per frame
    void Update()
    {
        bool reset = false;
        foreach(GameObject wall in wallObjects) {
            if (wall.GetComponent<WallReactor>().needToReset()) {
                reset = true;
            }
        }
        if (reset) {
            GameObject.Find("Ghost").GetComponent<GhostController>().dePossess();
            this.transform.position = myRoom.GetComponent<MapHandler>().GetTileCenter(ogColumn, ogRow);
            myRoom.GetComponent<MapHandler>().deleteOccupancy(currentColumn, currentRow);
            myRoom.GetComponent<MapHandler>().fillOccupancy(ogColumn, ogRow, this.gameObject);
            currentColumn = ogColumn;
            currentRow = ogRow;
            myRoom.GetComponent<MapHandler>().resetObjectsAppend();
        }
    }

    public void Up() {
        currentRow += 1;
    }
    public void Down() {
        currentRow -= 1;
    }
    public void Left() {
        currentColumn -= 1;
    }
    public void Right() {
        currentColumn += 1;
    }
    
    public void resetMachine() {
        foreach (GameObject wall in wallObjects) {
            wall.GetComponent<WallReactor>().resetReset(); 
        }   
    }
}
