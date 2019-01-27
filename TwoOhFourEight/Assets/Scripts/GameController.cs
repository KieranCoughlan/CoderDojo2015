using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
  // Consts
  private const int Rows = 4;
  private const int Cols = 4;

  // Enums
  private enum MoveDir
  {
    none,
    left,
    right,
    up,
    down}

  ;

  // Public members
  public GameObject TilePrefab;
  public float ChanceOfFour = 0.25f;
  public Text scoreText;

  public bool ShowMeTheBrokenMovement = false;

  // Private members
  private GamePieceController[,] _tiles = new GamePieceController[Rows, Cols];
  private int _numTiles = 0;
  private int _score = 0;
  private float timeSinceLastInstruction = 0.0f;
  private MoveDir lastReqDrn = MoveDir.none;
  private bool gameOver = false;

  // Use this for initialization
  void Start ()
  {
    for (int i = 0; i < 2; i++)
      AddRandomTile ();

    UpdateScoreText ();
  }
	
  // Update is called once per frame
  void Update ()
  {
    bool movedSomething = false;

    if (Input.GetKeyDown (KeyCode.LeftArrow))
      movedSomething = MoveLeft ();
    else if (Input.GetKeyDown (KeyCode.RightArrow))
      movedSomething = MoveRight ();
    else if (Input.GetKeyDown (KeyCode.DownArrow))
      movedSomething = MoveDown ();
    else if (Input.GetKeyDown (KeyCode.UpArrow))
      movedSomething = MoveUp ();
 
    if (movedSomething)
    {
      AddRandomTile ();
      gameOver = GameOver ();
    }
  }

  private bool MoveUp ()
  {
    ResetMergedFlags ();

    bool movedSomething = false;

    // Go column by column
    for (int col = 0; col < Cols; col++)
    {
      // Go from the top down
      for (int row = Rows - 1; row >= 0; row--)
      {
        if (SlideTileUp (row, col))
          movedSomething = true;
      }
    }

    return movedSomething;
  }

  private bool MoveDown ()
  {
    ResetMergedFlags ();

    bool movedSomething = false;

    // Go column by column
    for (int col = 0; col < Cols; col++)
    {
      // Go from the bottom up 
      for (int row = 0; row < Rows; row++)
      {
        if (SlideTileDown (row, col))
          movedSomething = true;
      }
    }

    return movedSomething;
  }

  private bool MoveLeft ()
  {
    ResetMergedFlags ();

    bool movedSomething = false;

    // Go row by row
    for (int row = 0; row < Rows; row++)
    {
      // Go from the left 
      for (int col = 0; col < Cols; col++)
      {
        if (SlideTileLeft (row, col))
          movedSomething = true;
      }
    }

    return movedSomething;
  }

  private bool MoveRight ()
  {
    ResetMergedFlags ();

    bool movedSomething = false;

    // Go row by row
    for (int row = 0; row < Rows; row++)
    {
      // Go from the left 
      for (int col = Cols - 1; col >= 0; col--)
      {
        if( SlideTileRight (row, col) )
          movedSomething = true;
      }
    }

    return movedSomething;
  }

  private bool AddRandomTile ()
  {
    if (_numTiles >= Rows * Cols)
      return false;

    bool addedSomething = false;

    while (addedSomething == false)
    {
      int row = Random.Range (0, Rows);
      int col = Random.Range (0, Cols);

      if (_tiles [row, col] == null)
      { 
        GameObject newGO = Instantiate (TilePrefab) as GameObject;
        GamePieceController gpc = newGO.GetComponent<GamePieceController> ();

        // Promote to a four sometimes
        if (Random.value < ChanceOfFour)
          gpc.IncreaseValue ();

        gpc.SetGridPosition (row, col);

        _tiles [row, col] = gpc;
        _numTiles++;

        addedSomething = true;
      }
    }

    return true;
  }

  private void RemoveTile (int row, int col)
  {
    if (_tiles [row, col] == null)
      return;
    
    GamePieceController gpc = _tiles [row, col];
    _tiles [row, col] = null;
    _numTiles--;

    Destroy (gpc.gameObject);
  }

  private bool MoveTile (int row0, int col0, MoveDir drn)
  {
    if (_tiles [row0, col0] == null || drn == MoveDir.none)
      return false;

    int row1 = row0;
    int col1 = col0;

    if (drn == MoveDir.left)
      col1--;
    else if (drn == MoveDir.right)
      col1++;
    else if (drn == MoveDir.up)
      row1++;
    else // MoveDir.down
      row1--;

    // Bound check
    if (col0 < 0 || col0 >= Cols || row0 < 0 || row0 >= Rows ||
        col1 < 0 || col1 >= Cols || row1 < 0 || row1 >= Rows)
      return false;

    // Nothing there? Just move
    if (_tiles [row1, col1] == null)
    {
      _tiles [row1, col1] = _tiles [row0, col0];
      _tiles [row0, col0] = null;

      if(ShowMeTheBrokenMovement)
        _tiles [row1, col1].SetIntendedPosition (row1, col1);
      else 
        _tiles [row1, col1].SetGridPosition (row1, col1);

      return true;
    } 
    else if ((_tiles [row0, col0].Value == _tiles [row1, col1].Value) &&
             _tiles [row1, col1].MergedThisMove == false)
    {
      RemoveTile (row1, col1);

      _tiles [row1, col1] = _tiles [row0, col0];
      _tiles [row0, col0] = null;

      if(ShowMeTheBrokenMovement)
        _tiles [row1, col1].SetIntendedPosition (row1, col1);
      else
        _tiles [row1, col1].SetGridPosition (row1, col1);
      
      _tiles [row1, col1].IncreaseValue ();
      _tiles [row1, col1].MergedThisMove = true;

      _score += _tiles [row1, col1].Value;

      UpdateScoreText ();

      return true;
    }

    return false;
  }
 
  // Slides a tile as far left as it can
  private bool SlideTileLeft (int row, int col)
  {
    if (_tiles [row, col] == null)
      return false;

    bool moved = false;

    while (col >= 0)
    {
      if (MoveTile (row, col, MoveDir.left) == false)
        break;

      moved = true;
      col--;
    }

    return moved;
  }

  // Slides a tile as far right as it can
  private bool SlideTileRight (int row, int col)
  {
    if (_tiles [row, col] == null)
      return false;

    bool moved = false;

    while (col < Cols)
    {
      if (MoveTile (row, col, MoveDir.right) == false)
        break;

      moved = true;
      col++;
    }

    return moved;
  }


  // Slides a tile as far down as it can
  private bool SlideTileDown (int row, int col)
  {
    if (_tiles [row, col] == null)
      return false;

    bool moved = false;

    while (row >= 0)
    {
      if (MoveTile (row, col, MoveDir.down) == false)
        break;

      moved = true;
      row--;
    }

    return moved;
  }

  // Slides a tile as far up as it can
  private bool SlideTileUp (int row, int col)
  {
    if (_tiles [row, col] == null)
      return false;

    bool moved = false;

    while (row <  Rows)
    {
      if (MoveTile (row, col, MoveDir.up) == false)
        break;

      moved = true;
      row++;
    }

    return moved;
  }

  // Clear the flags marking any tiles as merged this move
  private void ResetMergedFlags ()
  {
    for (int row = 0; row < Rows; row++)
    {
      for (int col = 0; col < Cols; col++)
      {
        if (_tiles [row, col] != null)
          _tiles [row, col].MergedThisMove = false;
      }
    }
  }

  // Check for game over
  private bool GameOver ()
  {
    // Any free spaces? Not over
    if (_numTiles < Rows * Cols)
      return false;

    // Full, but any merges possible?
    for (int col = 0; col < Cols; col++)
    {
      for (int row = 0; row < Rows; row++)
      {
        // Compare with the tile to the right and below to see if we can merge

        // Tile to the right?
        if (row + 1 < Rows && _tiles [row, col].Value == _tiles [row + 1, col].Value)
          return false;

        // Tile below?
        if (col + 1 < Cols && _tiles [row, col].Value == _tiles [row, col + 1].Value)
          return false;
      }
    }

    // Get here? Game over
    return true;
  }

  private void UpdateScoreText()
  {
    scoreText.text = string.Format ("Score: {0}", _score);
  }
}
