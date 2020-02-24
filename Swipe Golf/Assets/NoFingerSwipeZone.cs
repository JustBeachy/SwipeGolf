using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoFingerSwipeZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Controller.noSwipeZone = false;
    }

    
    private void OnMouseDrag()
    {
        Controller.noSwipeZone = true;
    }
 
    
}
