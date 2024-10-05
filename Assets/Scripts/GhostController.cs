using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    #region Position Variables
    [SerializeField]
    [Tooltip("PUT THE EXACT COLUMN YOU PUT IN THE ROOM OBJECT")]
    private int myColumn;
    [SerializeField]
    [Tooltip("PUT THE EXACT COLUMN YOU PUT IN THE ROOM OBJECT")]
    private int myRow;
    [SerializeField]
    [Tooltip("Put the Bedroom GameObject Here")]
    private GameObject myRoom;
    #endregion

    #region Movement Variables 
    private float moveCooldown;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        moveCooldown = .1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCooldown <= 0) {
            if(Input.GetAxis("Horizontal") > 0) {
                if (myRoom.GetComponent<MapHandler>().moveObejct(myColumn, myRow, "right")) {
                    myColumn += 1;
                }
            } else if(Input.GetAxis("Horizontal") < 0) {
                if(myRoom.GetComponent<MapHandler>().moveObejct(myColumn, myRow, "left")){
                    myColumn -= 1;
                }
            } else if(Input.GetAxis("Vertical") > 0) {
                if(myRoom.GetComponent<MapHandler>().moveObejct(myColumn, myRow, "up")){
                    myRow += 1;
                }
            } else if(Input.GetAxis("Vertical") < 0) {
                if(myRoom.GetComponent<MapHandler>().moveObejct(myColumn, myRow, "down")){
                    myRow -= 1;
                }
            }
            moveCooldown = .1f;
        } else {
            moveCooldown -= Time.deltaTime;
        }
         
    }

    public void changeRoom() {

    }
}
