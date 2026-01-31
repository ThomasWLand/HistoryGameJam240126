using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public int width;
    public int height;
    public float spacingX;
    public float spacingY;

    [SerializeField] int numberOfShips;
    [SerializeField] Ship[] ships;
    
    public Transform gridParent;
    private GridController _controller;
    private GridData[,] gridData;
    private PresetPositionManager presetManager;
    private Vector2[] presetPositions; 

    public void Start()
    {
        GenerateGrid();
        presetManager = GetComponent<PresetPositionManager>();

        ManageSpawning();
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
        gridData = _controller.GetModel().GetAllData();

        grid.SetDisplayVisible(true);
    }

    private void ManageSpawning() 
    {
        presetPositions = presetManager.GetChosenPositions();

        for (int i = 0; i < 5; i++)
        {

            int presetX = (int)presetPositions[i].x;
            int presetY = (int)presetPositions[i].y;

            //this is for instantiating the ship as a child of the grid position that is in the middle
            int halfPointX = 0;
            int halfPointY = 0;

            
            int swappedHeight = ships[i].width;
            int swappedWidth = ships[i].height;


            if ((presetX + ships[i].width) < width)
            {
                Debug.Log($"X - {ships[i].name} is occupying position {presetX},{presetY}");
                Debug.Log($"X - {ships[i].name} is {presetX + ships[i].width} compared to limit {width - 1}");
                for (int j = presetX; j < (presetX + ships[i].width); j++)
                {
                    print($"X was used, occupied grid for {ships[i].name} is {j},{presetY}");

                    if (gridData[j, presetY].getIsOccupied() == false)
                    {
                        gridData[j, presetY].setIsOccupied(true);
                    }

                    else 
                    {
                        Debug.LogError("Two ships are overlapping!");
                    }
                }

                halfPointX = presetX + ships[i].width;
                switch (ships[i].width) 
                {
                    case 2:
                        halfPointX -= 2;
                        break;
                    case 3:
                        halfPointX -= 2;
                        break;
                    case 4:
                        halfPointX -= 3;
                        break;
                    case 5:
                        halfPointX -= 3;
                        break;
                }


                halfPointY = presetY;
                Debug.Log($"X - for {ships[i].name} the halfway point is {halfPointX},{halfPointY}");

                Transform parentObject = gridParent.Find($"{halfPointX},{halfPointY}");
                Instantiate(ships[i], parentObject);
            }

            else if ((presetY + swappedHeight) < height)
            {
                Debug.Log($"Y - {ships[i].name} is occupying position {presetX},{presetY}");
                Debug.Log($"Y - {ships[i].name} is {presetY + swappedHeight} compared to limit {height - 1}");

                for (int j = presetY; j < (presetY + swappedHeight); j++)
                {
                    print($"Y was used, placement on grid for {ships[(i)].name} is {presetX}, {j}");
                    if (gridData[presetX, j].getIsOccupied() == false)
                    {
                        gridData[presetX, j].setIsOccupied(true);
                    }

                    else 
                    {
                        Debug.LogError("Two ships are overlapping!");
                    }
                }

                halfPointY = presetY + swappedHeight;
                switch (swappedHeight)
                {
                    case 2:
                        halfPointY -= 2;
                        break;
                    case 3:
                        halfPointY -= 2;
                        break;
                    case 4:
                        halfPointY -= 3;
                        break;
                    case 5:
                        halfPointY -= 3;
                        break;
                }
                halfPointX = presetX;
                Debug.Log($"Y - for {ships[i].name} the halfway point is {halfPointX},{halfPointY}");

                Transform parentObject = gridParent.Find($"{halfPointX},{halfPointY}");
                Instantiate(ships[i], parentObject);
            }

            else 
            {
                Debug.LogError($"The position {presetX},{presetY} is not valid!");
            }
        }
    }    
}
