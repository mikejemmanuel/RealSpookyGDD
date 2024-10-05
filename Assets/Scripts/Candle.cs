using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    public bool startOn;
    public bool lightOn;
    SpriteRenderer rend;
    Animator anim;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        lightOn = startOn;
        if (lightOn == true)
        {
            anim.SetBool("lightOn", true);
        }
        else
        {
            anim.SetBool("lightOn", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
        {
            Debug.Log("switching states");
            timer = 0;
            interact();
        }

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
