using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public int width;
    public int height;
    public float spacingX;
    public float spacingY;

    [Range(0,1)]public float shipScale = 0.45f;

    [SerializeField] int numberOfShips;
    [SerializeField] Ship[] ships;
    
    public Transform gridParent;
    private GridController _controller;
    private GridData[,] gridData;
    private PresetPositionManager presetManager;
    private Vector2[] presetPositions;
    private List<Ship> activeShips = new List<Ship>();

    public int totalBombs = 40;
    private int _bombsRemaining;
    public GamePlayState playState;

    public TextMeshProUGUI bombsRemainingText;

    bool _isPlaying = false;

    public void BeginGame()
    {
        this._isPlaying = true;
        this._bombsRemaining = totalBombs;
        bombsRemainingText.text = "Bombs Remaining: " + totalBombs;
        GenerateGrid();
        presetManager = GetComponent<PresetPositionManager>();

        ManageSpawning();
    }

    private void _onBombDropped(Vector2Int coord)
    {
        if(!this._isPlaying || this._bombsRemaining <= 0)
        {
            return;
        }
        GridModel model = this._controller.GetModel();
        GridData newData = model.GetData(coord.x,coord.y);
        bool isHidden = newData.getViewState() == GridViewState.HIDDEN;
        bool isOccupied = newData.getIsOccupied();
        if(!isHidden)
        {
            return;
        }

        this._bombsRemaining--;
        this._controller.onDisplayComponentClicked(isOccupied, coord);
        if(isOccupied)
        {
            this._updateShipHits(coord);
        }
        else
        {
            SoundManager.PlaySound(GameSounds.SHOT_MISS);
        }
        bombsRemainingText.text = "Bombs Remaining: " + this._bombsRemaining;
        bool didWin = this._getAreAllShipsSunk();
        if(this._bombsRemaining == 0 || didWin)
        {
            this._onGameFinished(didWin);
        }
    }

    private void _updateShipHits(Vector2 coord)
    {
        foreach(Ship currShip in activeShips)
        {
            currShip.OnTileExploded(coord);
        }
    }

    private bool _getAreAllShipsSunk()
    {
        foreach(Ship currShip in activeShips)
        {
            if(currShip.GetIsShipAlive())
            {
                return false;
            }
        }
        return true;
    }

    private void _onGameFinished(bool won)
    {
        this.playState.onGameComplete(won);
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
        gridData = _controller.GetModel().GetAllData();

        GridView view = grid.GetView();
        view.onDisplayComponentClickedEvent.AddListener(this._onBombDropped);
        grid.SetDisplayVisible(true);
    }
    public void SetIsPlaying(bool isPlaying) => this._isPlaying = isPlaying;

    private void ManageSpawning() 
    {
        activeShips.Clear();
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

            List<Vector2> shipCoords = new List<Vector2>();
            if ((presetX + ships[i].width) < width)
            {
                Debug.Log($"X - {ships[i].name} is occupying position {presetX},{presetY}");
                Debug.Log($"X - {ships[i].name} is {presetX + ships[i].width} compared to limit {width - 1}");
                for (int j = presetX; j < (presetX + ships[i].width); j++)
                {
                    shipCoords.Add(new Vector2(j, presetY));
                    print($"X was used, occupied grid for {ships[i].name} is {j},{presetY}");

                    if (gridData[j, presetY].getIsOccupied() == false)
                    {
                        gridData[j, presetY].setIsOccupied(true);
                    }

                    else 
                    {
                        //Debug.LogError("Two ships are overlapping!");
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
                Ship newShip = Instantiate(ships[i], parentObject);
                newShip.SetUnhitTiles(shipCoords);
                activeShips.Add(newShip);
                newShip.transform.localScale = Vector3.one * shipScale;
                newShip.transform.eulerAngles = new Vector3(0, 0, 90);
            }

            else if ((presetY + swappedHeight) < height)
            {
                Debug.Log($"Y - {ships[i].name} is occupying position {presetX},{presetY}");
                Debug.Log($"Y - {ships[i].name} is {presetY + swappedHeight} compared to limit {height - 1}");

                for (int j = presetY; j < (presetY + swappedHeight); j++)
                {
                    shipCoords.Add(new Vector2(presetX, j));
                    print($"Y was used, placement on grid for {ships[(i)].name} is {presetX}, {j}");
                    if (gridData[presetX, j].getIsOccupied() == false)
                    {
                        gridData[presetX, j].setIsOccupied(true);
                    }

                    else 
                    {
                        //Debug.LogError("Two ships are overlapping!");
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
                Ship newShip = Instantiate(ships[i], parentObject);
                newShip.SetUnhitTiles(shipCoords);
                activeShips.Add(newShip);
                newShip.transform.localScale = Vector3.one * shipScale;
                newShip.transform.eulerAngles = new Vector3(0, 0, 0);
            }

            else 
            {
                //Debug.LogError($"The position {presetX},{presetY} is not valid!");
            }
        }
    }    
}
