using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    private float prevTime;
    private float fallTime = 3f;

    private void Start()
    {
    }

    private void Update()
    {
        if (Time.time - prevTime > fallTime)
        {
            transform.position += Vector3.down;
            if (!CheckVaildMove())
            {
                transform.position += Vector3.up;
                //delete layer id possible
                enabled = false;
                //create a new tetris block
            }
            else
            {
                //update the grid
            }

            prevTime = Time.time;
        }
    }

    private bool CheckVaildMove()
    {
        foreach (Transform child in transform)
        {
            Vector3 pos = Playfield.instance.Round(child.position);
            if (!Playfield.instance.CheckInsideGrid(pos))
            {
                return false;
            }
        }

        return true;
    }
}