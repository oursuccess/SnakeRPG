using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int horizontal = 0;
    private int vertical = 0;
    void Update()
    {
        horizontal = (int)Input.GetAxisRaw("Horizontal");
        if(horizontal == 0)
        {
            vertical = (int)Input.GetAxisRaw("Vertical");
        }

        if(horizontal != 0 || vertical != 0)
        {

        }
    }
}

