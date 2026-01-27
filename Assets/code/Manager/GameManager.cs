using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int width;
    public int height;
    public float spacingX;
    public float spacingY;
    [SerializeField] int numberOfShips;
    [SerializeField] List<Ship> ships;
    public Transform gridParent;
    private GridController _controller;
    public void Start()
    {
        GenerateGrid();
    }

    public void ResetGrid()
    {
        this._controller.DestroyGrid();
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        //center the grid
        Vector3 offset = new Vector3(Mathf.Ceil(-width / 2f), Mathf.Ceil(-height / 2f), 0);
        gridParent.position = offset;
        GridController grid = GridFactory.build(width, height, spacingX, spacingY, gridParent);
        this._controller = grid; 

        grid.SetDisplayVisible(true);

        for (int i = 0; i < numberOfShips; i++) 
        {
            AddShips(i);
        }
    }

    private void AddShips(int index)
    {
        //stub, add ship placing stuffs here
        GridData[,] _grid = _controller.GetModel().GetAllData();

        int _placementWidthLocation = UnityEngine.Random.Range(0, width);
        int _placementHeightLocation = UnityEngine.Random.Range(0, height);


        GridData _singleGrid = _grid[_placementWidthLocation, _placementHeightLocation];
        GridModel model = _controller.GetModel();
        print($"{_placementWidthLocation}" + " " + $"{_placementHeightLocation}");

        for (int i = _placementWidthLocation; i < width; i++) 
        {
            // can it fit?
            if ((i + ships[index].width) < width)
            {
                _grid[i, _placementHeightLocation].setIsOccupied(true);
                print($"{ships[index].name}: " + $"{i}" + " " + $"{_placementHeightLocation}");
            }

            else 
            {
                print($"{ships[index].name}: can't fit");
            }

            for (int j = _placementHeightLocation; j < height; j++)
            {

            }
        }
    }
}
