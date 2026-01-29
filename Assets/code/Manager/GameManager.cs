using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int width;
    public int height;
    public float spacingX;
    public float spacingY;
    public Transform gridParent;
    private GridController _controller;
    public void BeginGame()
    {
        GenerateGrid();
    }

    public void ResetGrid()
    {
        this._controller.DestroyGrid();
        GenerateGrid();
    }

    public void DestroyGrid()
    {
        this._controller.DestroyGrid();
    }

    private void GenerateGrid()
    {
        //center the grid
        Vector3 offset = new Vector3(Mathf.Ceil(-width / 2f), Mathf.Ceil(-height / 2f), 0);
        gridParent.position = offset;
        GridController grid = GridFactory.build(width, height, spacingX, spacingY, gridParent);
        this._controller = grid; 

        grid.SetDisplayVisible(true);
        AddShips();
    }

    private void AddShips()
    {
        //stub, add ship placing stuffs here
    }
}
