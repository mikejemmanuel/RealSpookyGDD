using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    #region Position Variables
    [SerializeField]
    [Tooltip("Intended Start Column")]
    private int myColumn;
    [SerializeField]
    [Tooltip("Intended Start Row")]
    private int myRow;
    private int lastColumn;
    private int lastRow;
    [SerializeField]
    [Tooltip("Put the Bedroom GameObject Here")]
    private GameObject myRoom;
    #endregion

    #region Movement Variables 
    private float moveCooldown;
    #endregion

    #region Possession Variables
    private bool isPossessing;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        float colorR = this.GetComponent<SpriteRenderer>().color.r;
        float colorG = this.GetComponent<SpriteRenderer>().color.g;
        float colorB = this.GetComponent<SpriteRenderer>().color.b;
        this.GetComponent<SpriteRenderer>().color = new Color(colorR, colorG, colorB, 0.5f);
        this.transform.position = myRoom.GetComponent<MapHandler>().GetTileCenter(myColumn, myRow);
        moveCooldown = .15f;
        isPossessing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            Debug.Log("Col " + myColumn);
            Debug.Log("Row " + myRow);
        }
        if (isPossessing) {
            if (Input.GetKeyDown(KeyCode.K)) {
                Debug.Log("De-Possessing");
                isPossessing = false;
                float colorR = this.GetComponent<SpriteRenderer>().color.r;
                float colorG = this.GetComponent<SpriteRenderer>().color.g;
                float colorB = this.GetComponent<SpriteRenderer>().color.b;
                this.GetComponent<SpriteRenderer>().color = new Color(colorR, colorG, colorB, 0.5f);
                colorR = myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow).GetComponent<SpriteRenderer>().color.r;
                colorG = myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow).GetComponent<SpriteRenderer>().color.g;
                colorB = myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow).GetComponent<SpriteRenderer>().color.b;
                myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow).GetComponent<SpriteRenderer>().color = new Color(colorR + 100, colorG + 100, colorB);
        }   
        } else {
            if (canPossessTile()) {
                if(Input.GetKeyDown(KeyCode.J)) {
                    Debug.Log("Possessing");
                    isPossessing = true;
                    float colorR = this.GetComponent<SpriteRenderer>().color.r;
                    float colorG = this.GetComponent<SpriteRenderer>().color.g;
                    float colorB = this.GetComponent<SpriteRenderer>().color.b;
                    this.GetComponent<SpriteRenderer>().color = new Color(colorR, colorG, colorB, 0.0f);
                    colorR = myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow).GetComponent<SpriteRenderer>().color.r;
                    colorG = myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow).GetComponent<SpriteRenderer>().color.g;
                    colorB = myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow).GetComponent<SpriteRenderer>().color.b;
                    myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow).GetComponent<SpriteRenderer>().color = new Color(colorR - 100, colorG - 100, colorB);
                }
            }
        }
        if (moveCooldown <= 0) {
            if(Input.GetAxis("Horizontal") > 0) {
                if ((myColumn + 1) > 5) {
                    Debug.Log("OUT OF BOUNDS");
                } else if (isPossessing) {
                    if (myRoom.GetComponent<MapHandler>().GetTileObject(myColumn + 1, myRow) == null) {
                        myRoom.GetComponent<MapHandler>().moveObejct(myColumn, myRow, "right");
                        myColumn += 1;    
                        this.transform.position = myRoom.GetComponent<MapHandler>().GetTileCenter(myColumn, myRow);
                    } else {
                        Debug.Log("NO MORE SPACE RIGHTWARDS");
                    }
                } else if ((myRoom.GetComponent<MapHandler>().GetTileObject(myColumn + 1, myRow) != null) && (myRoom.GetComponent<MapHandler>().GetTileObject(myColumn + 1, myRow).CompareTag("Impassable"))) {
                    Debug.Log("GHOST CANT STAND THERE");
                } else {
                    myColumn += 1;
                    this.transform.position = myRoom.GetComponent<MapHandler>().GetTileCenter(myColumn, myRow);
                }
            } else if(Input.GetAxis("Horizontal") < 0) {
                if ((myColumn - 1) < 1) {
                    Debug.Log("OUT OF BOUNDS");
                } else if (isPossessing) {
                    if (myRoom.GetComponent<MapHandler>().GetTileObject(myColumn - 1, myRow) == null) {
                        myRoom.GetComponent<MapHandler>().moveObejct(myColumn, myRow, "left");
                        myColumn -= 1;    
                        this.transform.position = myRoom.GetComponent<MapHandler>().GetTileCenter(myColumn, myRow);
                    } else {
                        Debug.Log("NO MORE SPACE LEFTWARDS");
                    }
                } else if ((myRoom.GetComponent<MapHandler>().GetTileObject(myColumn - 1, myRow) != null) && (myRoom.GetComponent<MapHandler>().GetTileObject(myColumn - 1, myRow).CompareTag("Impassable"))) {
                    Debug.Log("GHOST CANT STAND THERE");
                } else {
                    myColumn -= 1;
                    this.transform.position = myRoom.GetComponent<MapHandler>().GetTileCenter(myColumn, myRow);
                }
            } else if(Input.GetAxis("Vertical") > 0) {
                if ((myRow + 1) > 5) {
                    Debug.Log("OUT OF BOUNDS");
                } else if (isPossessing) {
                    if (myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow + 1) == null) {
                        myRoom.GetComponent<MapHandler>().moveObejct(myColumn, myRow, "up");
                        myRow += 1;    
                        this.transform.position = myRoom.GetComponent<MapHandler>().GetTileCenter(myColumn, myRow);
                    } else {
                        Debug.Log("NO MORE SPACE UPWARDS");
                    }
                } else if ((myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow + 1) != null) && (myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow + 1).CompareTag("Impassable"))) {
                    Debug.Log("GHOST CANT STAND THERE");
                } else {
                    myRow += 1;
                    this.transform.position = myRoom.GetComponent<MapHandler>().GetTileCenter(myColumn, myRow);
                }
            } else if(Input.GetAxis("Vertical") < 0) {
                if ((myRow - 1) < 1) {
                    Debug.Log("OUT OF BOUNDS");
                } else if (isPossessing) {
                    if (myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow - 1) == null) {
                        myRoom.GetComponent<MapHandler>().moveObejct(myColumn, myRow, "down");
                        myRow -= 1;    
                        this.transform.position = myRoom.GetComponent<MapHandler>().GetTileCenter(myColumn, myRow);
                    } else {
                        Debug.Log("NO MORE SPACE UPWARDS");
                    }
                } else if ((myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow - 1) != null) && (myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow - 1).CompareTag("Impassable"))) {
                    Debug.Log("GHOST CANT STAND THERE");
                } else {
                    myRow -= 1;
                    this.transform.position = myRoom.GetComponent<MapHandler>().GetTileCenter(myColumn, myRow);
                }
            }
            moveCooldown = .15f;
        } else {
            moveCooldown -= Time.deltaTime;
        }         
    }

    public void changeRoom() {

    }
    
    public int getColumn() {
        return myColumn;
    }
    public int getRow() {
        return myRow;
    }

    #region Possession Functions
    private bool canPossessTile() {
        if(myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow) != null) {
            if (myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow).CompareTag("Possessable")) {
                return true;
            }
        }
        return false;
    }
    public void dePossess() {
        Debug.Log("De-Possessing FUNCTION");
        isPossessing = false;
        float colorR = this.GetComponent<SpriteRenderer>().color.r;
        float colorG = this.GetComponent<SpriteRenderer>().color.g;
        float colorB = this.GetComponent<SpriteRenderer>().color.b;
        this.GetComponent<SpriteRenderer>().color = new Color(colorR, colorG, colorB, 0.5f);
        colorR = myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow).GetComponent<SpriteRenderer>().color.r;
        colorG = myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow).GetComponent<SpriteRenderer>().color.g;
        colorB = myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow).GetComponent<SpriteRenderer>().color.b;
        myRoom.GetComponent<MapHandler>().GetTileObject(myColumn, myRow).GetComponent<SpriteRenderer>().color = new Color(colorR + 100, colorG + 100, colorB);
    }
    #endregion
}
