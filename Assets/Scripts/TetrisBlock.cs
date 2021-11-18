using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    private float prevTime;
    private float fallTime = 1f;

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
                Playfield.instance.SpawnNewBlock();
            }
            else
            {
                //update the grid
                Playfield.instance.UpdatedGrid(this);
            }

            prevTime = Time.time;
        }

        // LEFT RIGHT FORWARD BACK
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetInput(Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetInput(Vector3.right);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetInput(Vector3.forward);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetInput(Vector3.back);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SetRotationInput(new Vector3(90, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetRotationInput(new Vector3(-90, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetRotationInput(new Vector3(0, 0, 90));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SetRotationInput(new Vector3(0, 0, -90));
        }
    }

    public void SetInput(Vector3 direction)
    {
        transform.position += direction;
        if (!CheckVaildMove())
        {
            transform.position -= direction;
        }
        else
        {
            Playfield.instance.UpdatedGrid(this);
        }
    }

    //rotation
    public void SetRotationInput(Vector3 rotation)
    {
        transform.Rotate(rotation, Space.World);
        if (!CheckVaildMove())
        {
            transform.Rotate(-rotation, Space.World);
        }
        else
        {
            Playfield.instance.UpdatedGrid(this);
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

        foreach (Transform child in transform)
        {
            Vector3 pos = Playfield.instance.Round(child.position);
            Transform t = Playfield.instance.GetTransformOnGridPos(pos);
            if (t != null && t.parent != transform)
            {
                return false;
            }
        }

        return true;
    }

    public void SetSpeed()
    {
        fallTime = 0.1f;
    }
}