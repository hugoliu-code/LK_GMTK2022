using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;

    private float defTime = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            waitTime = defTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = defTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            effector.rotationalOffset = 0f;
        }
    }
}

