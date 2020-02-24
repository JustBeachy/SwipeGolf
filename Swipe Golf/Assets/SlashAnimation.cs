using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAnimation : MonoBehaviour
{
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color newColor = gameObject.GetComponent<SpriteRenderer>().color;
        newColor.a -= 5 * Time.deltaTime;
        gameObject.GetComponent<SpriteRenderer>().color = newColor;

        timer += Time.deltaTime;

        if (timer >= .2)
            Destroy(gameObject);
    }
}
