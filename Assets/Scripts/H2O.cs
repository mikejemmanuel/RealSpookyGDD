using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H2O : MonoBehaviour
{
    public bool startLiquid;
    public bool liquid;
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting");
        rend = GetComponent<SpriteRenderer>();
        liquid = startLiquid;
        if (liquid == true)
        {
            rend.sprite = Resources.Load<Sprite>("Sprites/puddle1");
        } else
        {
            rend.sprite = Resources.Load<Sprite>("Sprites/puddle0");
        }
        Debug.Log("Starting ended");
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void melt()
    {
        liquid = true;
        rend.sprite = Resources.Load<Sprite>("Sprites/puddle1");

    }
        }
