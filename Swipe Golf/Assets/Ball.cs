using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    float currentX, currentY, pastX, pastY,firstX,firstY;
    bool isdown = false;
    bool canSwipe = true;
    public AudioSource[] hit;
    public AudioSource intheHole;
    public GameObject slash;
    public bool gotHiddenCoin = false;
    GameObject controller;
    float holeTimer = 0;
    bool inHole = false;
   

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        gotHiddenCoin = false;
        GetComponent<LineRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inHole)
        {
            holeTimer += Time.deltaTime;
            if(holeTimer>.5)
                controller.GetComponent<Controller>().NextLevel(gotHiddenCoin);

        }

        if (Input.GetMouseButtonDown(0))
        {
            isdown = true;
            firstX = Input.mousePosition.x;
            firstY = Input.mousePosition.y;

            GetComponent<LineRenderer>().enabled = true;
            GetComponent<LineRenderer>().SetPosition(0, Camera.main.ScreenToWorldPoint(new Vector3(firstX, firstY, 1)));
            GetComponent<LineRenderer>().SetPosition(1, Camera.main.ScreenToWorldPoint(new Vector3(firstX, firstY, 1))); //reset line renderer
        }
        if(Input.GetMouseButton(0)&&Controller.Swipes>0&&canSwipe)
        {
            pastX = currentX;
            pastY = currentY;
            currentX = Input.mousePosition.x;
            currentY = Input.mousePosition.y;

            GetComponent<LineRenderer>().SetPosition(1, Camera.main.ScreenToWorldPoint(new Vector3(currentX, currentY, 1)));//stretch and color line
            ColorLine();
            
        }
        else
        {
            isdown = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            GetComponent<LineRenderer>().enabled = false;
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
        if (Controller.Swipes > 0&&Controller.start&&canSwipe&&!Controller.noSwipeZone)
        {
            Vector2 velocity = new Vector2(currentX - firstX, currentY - firstY);

           // var swipeMark = Instantiate(slash, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10), Quaternion.Euler(0, 0, Mathf.Atan2(currentY - pastY, currentX - pastX) * Mathf.Rad2Deg));
           // swipeMark.transform.localScale = new Vector3(velocity.magnitude/14, 1, 1); //make slash mark and scale it

            velocity = velocity / (Screen.height/25);//reduce phyisical swipe sensitivity
            velocity = Vector2.ClampMagnitude(velocity, 12); //limit max speed

            rb.velocity = Vector3.zero;
            rb.AddForce(velocity, ForceMode2D.Impulse);
            isdown = false;
            Controller.Swipes--;

            hit[Random.Range(0,hit.Length)].Play();
            

            
        }
    }

    void ColorLine()
    {
        Vector2 line = new Vector2(currentX - firstX, currentY - firstY);
        if (line.magnitude <= Screen.height / 4)
        {
            GetComponent<LineRenderer>().endColor = Color.green;
            GetComponent<LineRenderer>().startColor = Color.green;
        }
        if (line.magnitude > Screen.height / 4&& line.magnitude < Screen.height / 2)
        {
            GetComponent<LineRenderer>().endColor = Color.yellow;
            GetComponent<LineRenderer>().startColor = Color.yellow;
        }
        if (line.magnitude >= Screen.height / 2)
        {
            GetComponent<LineRenderer>().endColor = Color.red;
            GetComponent<LineRenderer>().startColor = Color.red;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hole")
        {
            intheHole.Play();
            rb.velocity = Vector2.zero;
            Controller.start = false;
            inHole = true;

            if (gotHiddenCoin)
                Controller.HiddenCoins[SceneManager.GetActiveScene().buildIndex-1] = true;
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
            other.gameObject.GetComponent<AudioSource>().Play();
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
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
