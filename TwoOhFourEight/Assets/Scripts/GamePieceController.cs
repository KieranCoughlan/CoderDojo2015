using UnityEngine;
using System.Collections;

public class GamePieceController : MonoBehaviour {

  public int Value = 2;
  public Material[] TileMaterials;

  public float MoveSpeed = 1.0f;

  [HideInInspector]
  public bool MergedThisMove = false;

  private int _maxMaterialIndex;
  private int _materialIndex = 0;
  private Renderer _renderer;

  private Vector3 _intendedPosition;

	// Use this for initialization
	void Awake () {
    _maxMaterialIndex = TileMaterials.GetLength (0);
    _renderer = GetComponent<Renderer> ();
    _renderer.material = TileMaterials [0];

    _intendedPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
    if (transform.position != _intendedPosition)
    {
      Vector3 direction = Vector3.Normalize(_intendedPosition - transform.position);
      transform.position = transform.position += (direction * MoveSpeed * Time.deltaTime);
    }
	}

  public bool IncreaseValue()
  {
    if (_materialIndex >= _maxMaterialIndex)
      return false;
     
    Value *= 2;

    _materialIndex++;
    _renderer.material = TileMaterials [_materialIndex];

    return true;
  }

  public void SetGridPosition(int row, int col)
  {
    transform.position = RowColToPos (row, col);
    SetIntendedPosition (row, col); // it's where it should be already
  }

  public void SetIntendedPosition(int row, int col)
  {
    _intendedPosition = RowColToPos (row, col);
  }

  private Vector3 RowColToPos(int row, int col)
  {
    return new Vector3 (col - 1.5f, 0.1f, row - 1.5f);
  }
}
