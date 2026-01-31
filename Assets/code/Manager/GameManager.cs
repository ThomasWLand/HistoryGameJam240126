using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int width;
    public int height;
    public float spacingX;
    public float spacingY;
    public Transform gridParent;
    private GridController _controller;

    public int totalBombs = 10;
    private int _bombsRemaining = 10;
    public GamePlayState playState;

    public TextMeshProUGUI bombsRemainingText;

    bool _isPlaying = false;

    public void BeginGame()
    {
        this._isPlaying = true;
        this._bombsRemaining = totalBombs;
        bombsRemainingText.text = "Bombs Remaining: 10";
        GenerateGrid();
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
        bombsRemainingText.text = "Bombs Remaining: " + this._bombsRemaining;
        if(this._bombsRemaining == 0)
        {
            this._onGameFinished();
        }
    }

    private void _onGameFinished()
    {
        bool didWin = false;
        this.playState.onGameComplete(didWin);
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
        GridView view = grid.GetView();
        view.onDisplayComponentClickedEvent.AddListener(this._onBombDropped);
        grid.SetDisplayVisible(true);
        AddShips();
    }

    private void AddShips()
    {
        //stub, add ship placing stuffs here
    }
}
