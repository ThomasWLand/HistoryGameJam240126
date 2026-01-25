using UnityEngine;

public class GridController
{
    private GridView _gridView;
    private GridModel _gridModel;

    public GridController(GridView view, GridModel model)
    {
        this._gridView = view;
        this._gridModel = model;

        view.onDisplayComponentClickedEvent.AddListener(this._onDisplayComponentClicked);
    }

    public void SetDisplayVisible(bool isVisible) => this._gridView.SetDisplayVisible(isVisible);
    public void SetGridPointData(int x, int y, GridData newData) => this._gridModel.SetData(x,y,newData);
    public void ResetGrid() => this._gridModel.ResetGrid();

    /*
        This is called whenever any of the grid squares are clicked on
    */
    private void _onDisplayComponentClicked(Vector2Int coord)
    {
        //stub

        GridModel model = this._gridModel;
        GridData newData = model.GetData(coord.x,coord.y);
        bool isOccupied = newData.getIsOccupied();
        newData.setViewState(isOccupied ? GridViewState.HIT : GridViewState.MISSED);
        model.SetData(coord.x,coord.y, newData);
    }

    public void DestroyGrid()
    {
        this._gridView.DestroyView();
    }
}
