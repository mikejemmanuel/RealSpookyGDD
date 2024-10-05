using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public bool startLight;
    public bool lightOn;
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        lightOn = startLight;
        if (lightOn == true)
        {
            rend.sprite = Resources.Load<Sprite>("Sprites/sprite_lamp1");
        }
        else
        {
            rend.sprite = Resources.Load<Sprite>("Sprites/sprite_lamp0");
        }
    }

    // Update is called once per frame
    void Update()
    {
     

    }

    public void interact()
    {
        lightOn = !lightOn;
        if (lightOn == true)
        {
            rend.sprite = Resources.Load<Sprite>("Sprites/sprite_lamp1");
        }
        else
        {
            rend.sprite = Resources.Load<Sprite>("Sprites/sprite_lamp0");
        }
    }
}
