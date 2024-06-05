using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hornController : MonoBehaviour
{
    public float speed = 0.8f;
    public float leftBound;
    public float rightBound;
    private bool movingLeft = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftBound)
            {
                transform.Translate(speed * Time.deltaTime * Vector2.left);
            }
            else
            {
                movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightBound)
            {
                transform.Translate(speed * Time.deltaTime * Vector2.right);
            }
            else
            {
                movingLeft = true;
            }
        }
    }

}
