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

            //set 
            //gridData[presetX, presetY].setIsOccupied(true);

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

                        if (Math.Round((float)ships[i].width / 2f, MidpointRounding.AwayFromZero) == (j - presetX))
                        {
                            //this should on paper mean that middle is selected
                            halfPointX = (int)Math.Round((float)ships[i].width / 2f, MidpointRounding.AwayFromZero);
                            halfPointY = presetY;

                            Debug.Log($"X - for {ships[i].name} the halfway point is {halfPointX},{halfPointY}");
                        }
                    }

                    else 
                    {
                        Debug.LogError("Two ships are overlapping!");
                    }
                }

                Transform parentObject = gridParent.Find($"{presetX},{presetY}");
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

                        if (Math.Round((float)swappedHeight / 2f, MidpointRounding.AwayFromZero) == (j - presetY))
                        {
                            //this should on paper mean that middle is selected
                            halfPointX = presetX;
                            halfPointY = (int)Math.Round((float)swappedHeight / 2f, MidpointRounding.AwayFromZero);

                            Debug.Log($"Y - for {ships[i].name} the halfway point is {halfPointX},{halfPointY}");
                        }
                    }

                    else 
                    {
                        Debug.LogError("Two ships are overlapping!");
                    }
                }

                
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
