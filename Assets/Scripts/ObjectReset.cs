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
        if (wallObjects[0].GetComponent<WallReactor>().needToReset()) {
            this.transform.position = myRoom.GetComponent<MapHandler>().GetTileCenter(ogColumn, ogRow);
            myRoom.GetComponent<MapHandler>().deleteOccupancy(currentColumn, currentRow);
            myRoom.GetComponent<MapHandler>().fillOccupancy(ogColumn, ogRow, this.gameObject);
            if (wallObjects.Length > 1) {
                foreach (GameObject wall in wallObjects) {
                    wall.GetComponent<WallReactor>().resetReset(); 
                }
            }
            currentColumn = ogColumn;
            currentRow = ogRow;
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
}
