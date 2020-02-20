using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipeable : MonoBehaviour
{
    public Color cOn, cOff;
    public bool wallOn = true;
    bool alreadyClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        WallUpdate();
    }

    void WallUpdate()
    {
        if (wallOn)
        {
            gameObject.GetComponent<SpriteRenderer>().color = cOn;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = cOff;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            alreadyClicked = false;
        }
        
    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0)&&!alreadyClicked)//&&Controller.Swipes>0)//if swipe count matters, turn on
        {
            wallOn = !wallOn;
            WallUpdate();
            alreadyClicked = true;
        }
    }
}
