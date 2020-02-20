using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    float currentX, currentY, pastX, pastY;
    bool isdown = false;
    bool canSwipe = true;
    
       
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isdown = true;
        }
        if(Input.GetMouseButton(0))
        {
            pastX = currentX;
            pastY = currentY;
            currentX = Input.mousePosition.x;
            currentY = Input.mousePosition.y;
        }
        else
        {
            isdown = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            SwipeBall();
        }
    }
  
    private void OnMouseExit()
    {
        if (isdown)
        {
          //  SwipeBall();
        }
    }

    void SwipeBall()
    {
        if (Controller.Swipes > 0&&Controller.Start&&canSwipe)
        {
            Vector2 velocity = new Vector2(currentX - pastX, currentY - pastY);

            velocity = Vector2.ClampMagnitude(velocity, 11); //limit max speed

            rb.velocity = Vector3.zero;
            rb.AddForce(velocity, ForceMode2D.Impulse);
            isdown = false;
            Controller.Swipes--;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hole")
        {
            print("in da hole!");
            rb.velocity = Vector2.zero;
        }
        if (other.gameObject.tag == "Hazard")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (other.gameObject.tag == "Bonus")
        {
            Controller.Swipes++;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "NoSwipe")
        {
            canSwipe = false;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "NoSwipe")
        {
            canSwipe = true;
        }
    }
}
