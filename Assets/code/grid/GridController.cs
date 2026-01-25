using Unity.Collections;
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
    public void UpdateDisplay() => this._gridView.UpdateView();


    /*
        This is called whenever any of the grid squares are clicked on
    */
    private void _onDisplayComponentClicked(Vector2Int coord)
    {
        Debug.Log("display " + coord.x + "," + coord.y + " clicked");
    }
}
