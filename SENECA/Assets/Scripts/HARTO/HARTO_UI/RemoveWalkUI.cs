using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWalkUI : MonoBehaviour
{

    public bool horizontalInput;
    public bool verticalInput;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            verticalInput = true;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            horizontalInput = true;
        }

        if (verticalInput && horizontalInput)
        {
            Destroy(this.gameObject);
        }
    }
}
