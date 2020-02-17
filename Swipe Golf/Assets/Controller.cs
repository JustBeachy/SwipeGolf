using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public int swipes, cameraZoom;
    public static int Swipes, CameraZoom;
    public static bool Start=false;
    public Text swipesText;
    // Start is called before the first frame update
    void Awake()
    {
        Swipes = swipes;
        CameraZoom = cameraZoom;
    }

    // Update is called once per frame
    void Update()
    {
        swipesText.text = Swipes.ToString();
    }

    public void RestartRoom()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
