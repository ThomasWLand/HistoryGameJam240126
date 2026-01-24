using UnityEngine;

public struct GridData
{
    private bool _isOccupied;
    private GridViewState _viewState;

    public GridData(bool occupied = false, GridViewState viewState = GridViewState.HIDDEN)
    {
        this._isOccupied = occupied;
        this._viewState = viewState;
    }

    public GridViewState getViewState() => this._viewState;
    public bool getIsOccupied() => this._isOccupied;
}

public enum GridViewState
{
    HIDDEN,
    MISSED,
    HIT
}

public class GridModel
{
    private GridData[,] _data;
    private int _width, _height;
    public GridModel(int gridWidth, int gridHeight)
    {
        this._width = gridWidth;
        this._height = gridHeight;

        GridData[,] data = this._data = new GridData[gridWidth, gridHeight];
        for(int i = 0; i < gridWidth; i++)
        {
            for(int j = 0; j < gridHeight; j++)
            {
                data[i,j] = new GridData();
            }
        }
    }

    public GridData[,] GetAllData() => this._data;
    public GridData GetData(int xPos, int yPos)
    {
        bool isOutOfBounds = this._getIsOutOfBounds(xPos, yPos);
        
        if(isOutOfBounds)
        {
            Debug.LogError("Error: Tried to read out of bounds grid data at: " + xPos + "," + yPos);
            return new GridData();
        }
        
        return this._data[xPos, yPos];
    }

    public void SetData(int xPos, int yPos, GridData newData)
    {
        bool isOutOfBounds = this._getIsOutOfBounds(xPos, yPos);
        if(isOutOfBounds)
        {
            Debug.LogError("Error: Tried to write out of bounds grid data at: " + xPos + "," + yPos);
            return;
        }

        this._data[xPos,yPos] = newData;
    }

    private bool _getIsOutOfBounds(int xPos, int yPos)
    {
        return ( 
        xPos < 0 ||
        xPos >= this._width ||
        yPos < 0 ||
        yPos >= this._height);
    }
}

