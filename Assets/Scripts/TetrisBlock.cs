using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    private float prevTime;
    private float fallTime = 0.6f;

    private void Start()
    {
    }

    private Vector3 nextMove = Vector3.zero;

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
        if (Input.GetButtonDown("West"))
        {
            SetRotationInput(new Vector3(0, 0, -90));
        }
        if (Input.GetButtonDown("East"))
        {
            SetRotationInput(new Vector3(0, 0, 90));
        }
        if (Input.GetButtonDown("South"))
        {
            SetRotationInput(new Vector3(-90, 0, 0));
        }
        if (Input.GetButtonDown("North"))
        {
            SetRotationInput(new Vector3(90, 0, 0));
        }
        if (Input.GetAxis("Dpad Horizontal") > 0)
        {
            nextMove += Vector3.right * 0.05f;
            //SetInput(Vector3.right * 0.05f);
        }
        if (Input.GetAxis("Dpad Horizontal") < 0)
        {
            nextMove += Vector3.left * 0.05f;
            //SetInput(Vector3.left * 0.05f);
        }
        if (Input.GetAxis("Dpad Vertical") < 0)
        {
            nextMove += Vector3.back * 0.05f;
            //SetInput(Vector3.back * 0.05f);
        }
        if (Input.GetAxis("Dpad Vertical") > 0)
        {
            nextMove += Vector3.forward * 0.05f;
            //SetInput(Vector3.forward * 0.05f);
        }

        float x = nextMove.x;
        float z = nextMove.z;
        if (Mathf.Abs(x) >= 1 || Mathf.Abs(z) >= 1)
        {
            x = Mathf.Round(x);
            z = Mathf.Round(z);
            nextMove = new Vector3(x, 0, z);
            SetInput(nextMove);
            nextMove = Vector3.zero;
        }
        else
        {
            SetInput(Vector3.zero);
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
        fallTime = 0.05f;
    }

    //  public void SetHighSpeed()
    // {
    //    activeTetris.setSpeed();
    //   }
}