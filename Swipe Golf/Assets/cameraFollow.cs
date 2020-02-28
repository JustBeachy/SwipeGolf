using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject object2follow;
    public GameObject cam;
    float timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
        
        if (Controller.Start)//don't repeat the cut scene
        {
            object2follow = GameObject.FindGameObjectWithTag("Ball");
            timer = 100; //fix snap stutter bug
        }

            cam.transform.position = object2follow.transform.position; //snap follow object
            cam.transform.position += new Vector3(0, 0, -10);
        

        object2follow = GameObject.FindGameObjectWithTag("Ball");
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < .4f)
            timer += Time.deltaTime;
        else                            //start animation after timer
        {

            if (!Controller.Start)
            {
                Vector3 follow = ((object2follow.transform.position + new Vector3(0, 2, -10)) - cam.transform.position); //camera animation
                follow.Normalize();
                cam.transform.position += follow * Time.deltaTime * 10; //speed

                Vector2 isClose = (object2follow.transform.position - cam.transform.position+ new Vector3(0,2));
                if (isClose.magnitude < .1) //if camera animation finished
                    Controller.Start = true; //start round
            }
            else
            {
                cam.transform.position = object2follow.transform.position; //snap follow object
                cam.transform.position += new Vector3(0, 2, -10);
            }
        }
    }
}
