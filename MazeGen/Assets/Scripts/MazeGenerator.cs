using UnityEngine;
using System.Collections;

public class MazeGenerator : MonoBehaviour {

  public GameObject MazeElement;
  public GameObject MazeExit;

	// Use this for initialization
	void Start () {
    GenerateMaze (100,100);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void GenerateMaze(int width, int height)
  {
    bool[,] maze = new bool[width, height];
    bool outside = false;
    int x = width / 2; // Start in the middle
    int y = height / 2;

    while (outside == false)
    {
      maze[x, y]  =  true;

      int drn = Random.Range (0, 4);

      if (drn == 0)
        x--;
      else if (drn == 1)
        y++;
      else if (drn == 2)
        x++;
      else
        y--;

      outside = (x < 0 || y < 0 || x >= width || y >= height);
    }

    for (x = 0; x < width; x++)
    {
      for (y = 0; y < width; y++)
      {
        if (maze [x, y] == true)
          continue;

        bool edge = (x == 0 || y == 0 || x == width - 1 || y == height - 1);

        Vector3 pos = new Vector3 (x * 2 - width, 1, y * 2 - height);

        if (edge == true)
          Instantiate (MazeExit, pos, Quaternion.identity);
        else
          Instantiate (MazeElement, pos, Quaternion.identity);
      }
    }
  }
}
