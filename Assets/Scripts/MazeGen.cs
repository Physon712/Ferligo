using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGen : MonoBehaviour
{
	public GameObject[] tiles;
	public int xSize=8;
	public int ySize=8;
	
	public float xUnit = 9f;
	public float yUnit = 9f;
	
  private GameObject tile;
  private int index;

    // Update is called once per frame
    void Start()
    {
         for (int i = 0; i < xSize; i++)
		 {
			 for (int k = 0; k < ySize; k++)
			{
			index = Random.Range (0, tiles.Length);
			tile = tiles[index];
			Instantiate(tile,new Vector3(i*xUnit, 0f, k*yUnit), transform.rotation);
			}
		 }
    }
}
