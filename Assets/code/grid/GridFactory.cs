using UnityEngine;

/*
    Factory that builds the MVC pattern and spits out the controller for the Game Manager to use
*/
public static class GridFactory
{
    public static GridController build(int gridWidth, int gridHeight, float spacingx, 
        float spacingY, Transform parent)
    {
        GridModel model = new GridModel(gridWidth, gridHeight);
        GridView view = new GridView(model, gridWidth, gridHeight, spacingx, spacingY, parent);
        GridController controller = new GridController(view, model);

        model.ResetGrid();
        return controller;
    }
}
