using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{
    public static Playfield instance;

    public int gridSizeX, gridSizeY, gridSizeZ;

    [Header("Blocks")]
    public GameObject[] blocklist;

    public GameObject[] ghostlist;

    [Header("Playfield Visuals")]
    public GameObject bottomPlane;

    public GameObject N, S, W, E;

    public Transform[,,] theGrid;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        theGrid = new Transform[gridSizeX, gridSizeY, gridSizeZ];
        SpawnNewBlock();
    }

    //rounding function
    public Vector3 Round(Vector3 vec)
    {
        return new Vector3(Mathf.RoundToInt(vec.x),
                             Mathf.RoundToInt(vec.y),
                             Mathf.RoundToInt(vec.z));
    }

    public bool CheckInsideGrid(Vector3 pos)

    {
        return ((int)pos.x >= 0 && (int)pos.x < gridSizeX &&
                 (int)pos.z >= 0 && (int)pos.z < gridSizeZ &&
                 (int)pos.y >= 0);
    }

    public void UpdatedGrid(TetrisBlock block)
    {
        //delete possible parent objects
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    if (theGrid[x, y, z] != null)
                    {
                        if (theGrid[x, y, z].parent == block.transform)
                        {
                            theGrid[x, y, z] = null;
                        }
                    }
                }
            }
        }
        //fill fin all child objects
        foreach (Transform child in block.transform)
        {
            Vector3 pos = Round(child.position);
            if (pos.y < gridSizeY)
            {
                theGrid[(int)pos.x, (int)pos.y, (int)pos.z] = child;
            }
        }
    }

    public Transform GetTransformOnGridPos(Vector3 pos)
    {
        if (pos.y > gridSizeY - 1)
        {
            return null;
        }
        else
        {
            return theGrid[(int)pos.x, (int)pos.y, (int)pos.z];
        }
    }

    public void SpawnNewBlock()
    {
        Vector3 spawnPoint = new Vector3((int)(transform.position.x + (float)gridSizeX / 2),
                                             (int)transform.position.y + gridSizeY,
                                             (int)(transform.position.z + (float)gridSizeZ / 2));
        int randomIndex = Random.Range(0, blocklist.Length);

        //SPAWN THE BLOCK /show new block
        GameObject newBlock = Instantiate(blocklist[randomIndex], spawnPoint, Quaternion.identity) as GameObject;
        //GHOST
        GameObject newGhost = Instantiate(ghostlist[randomIndex], spawnPoint, Quaternion.identity) as GameObject;
        newGhost.GetComponent<GhostBlock>().SetParent(newBlock);
        //SET INPUTS
    }

    private void OnDrawGizmos()
    {
        if (bottomPlane != null)
        {
            //RESIZE BOTTOM PLANE
            Vector3 scaler = new Vector3((float)gridSizeX / 10, 1, (float)gridSizeZ / 10);
            bottomPlane.transform.localScale = scaler;

            //REPOSITION / ? WHY NOT -
            bottomPlane.transform.position = new Vector3(transform.position.x + (float)gridSizeX / 2,
                                                              transform.position.y,
                                                              transform.position.z + (float)gridSizeZ / 2);

            //RETILE MATERIAL  /  ? why = vector2
            bottomPlane.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeX, gridSizeZ);
        }

        if (N != null)
        {
            //RESIZE BOTTOM PLANE / ? why Y not in the second position
            Vector3 scaler = new Vector3((float)gridSizeX / 10, 1, (float)gridSizeY / 10);
            N.transform.localScale = scaler;

            //REPOSITION / ? WHY NOT -
            N.transform.position = new Vector3(transform.position.x + (float)gridSizeX / 2,
                                                  transform.position.y + (float)gridSizeY / 2,
                                                  transform.position.z + gridSizeZ);

            //RETILE MATERIAL  /  ? why = vector2
            N.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeX, gridSizeY);
        }

        if (S != null)
        {
            //RESIZE BOTTOM PLANE /
            Vector3 scaler = new Vector3((float)gridSizeX / 10, 1, (float)gridSizeY / 10);
            S.transform.localScale = scaler;

            //REPOSITION /
            S.transform.position = new Vector3(transform.position.x + (float)gridSizeX / 2,
                                                 transform.position.y + (float)gridSizeY / 2,
                                                 transform.position.z);

            //RETILE MATERIAL  /
            //S.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeX, gridSizeY);
        }

        if (W != null)
        {
            //RESIZE BOTTOM PLANE /
            Vector3 scaler = new Vector3((float)gridSizeZ / 10, 1, (float)gridSizeY / 10);
            W.transform.localScale = scaler;

            //REPOSITION /
            W.transform.position = new Vector3(transform.position.x,
                                                 transform.position.y + (float)gridSizeY / 2,
                                                 transform.position.z + (float)gridSizeZ / 2);

            //RETILE MATERIAL  /
            W.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeZ, gridSizeY);
        }

        if (E != null)
        {
            //RESIZE BOTTOM PLANE / ? why gridsize Z
            Vector3 scaler = new Vector3((float)gridSizeZ / 10, 1, (float)gridSizeY / 10);
            E.transform.localScale = scaler;

            //REPOSITION /
            E.transform.position = new Vector3(transform.position.x + gridSizeX,
                                                  transform.position.y + (float)gridSizeY / 2,
                                                  transform.position.z + (float)gridSizeZ / 2);

            //RETILE MATERIAL  /
            //S.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeX, gridSizeY);
        }
    }
}