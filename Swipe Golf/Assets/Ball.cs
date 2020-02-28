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
    public AudioSource[] hit;
    public AudioSource intheHole;
    public GameObject slash;
    public bool gotHiddenCoin = false;
    GameObject controller;
   

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        gotHiddenCoin = false;
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
        if (Controller.Swipes > 0&&Controller.Start&&canSwipe&&!Controller.noSwipeZone)
        {
            Vector2 velocity = new Vector2(currentX - pastX, currentY - pastY);

            var swipeMark = Instantiate(slash, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10), Quaternion.Euler(0, 0, Mathf.Atan2(currentY - pastY, currentX - pastX) * Mathf.Rad2Deg));
            swipeMark.transform.localScale = new Vector3(velocity.magnitude/14, 1, 1); //make slash mark and scale it

            velocity = velocity / 1.5f;//reduce phyisical swipe sensitivity
            velocity = Vector2.ClampMagnitude(velocity, 12); //limit max speed

            rb.velocity = Vector3.zero;
            rb.AddForce(velocity, ForceMode2D.Impulse);
            isdown = false;
            Controller.Swipes--;

            hit[Random.Range(0,hit.Length)].Play();
            

            
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hole")
        {
            intheHole.Play();
            rb.velocity = Vector2.zero;
            Controller.Start = false;
            
            controller.GetComponent<Controller>().NextLevel(gotHiddenCoin);

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
        if (other.gameObject.tag == "Coin")
        {
            gotHiddenCoin = true;
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
