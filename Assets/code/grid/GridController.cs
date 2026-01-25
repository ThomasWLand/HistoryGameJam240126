using UnityEngine;

public class GridController
{
    private GridView _gridView;
    private GridModel _gridModel;

    public GridController(GridView view, GridModel model)
    {
        this._gridView = view;
        this._gridModel = model;
    }

    public void SetDisplayVisible(bool isVisible) => this._gridView.SetDisplayVisible(isVisible);
    public void UpdateDisplay() => this._gridView.UpdateView();
}
