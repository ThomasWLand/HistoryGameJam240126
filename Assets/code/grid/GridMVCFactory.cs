using UnityEngine;

public static class GridMVCFactory
{
    public static GridController build(int gridWidth, int gridHeight, float spacingx, 
        float spacingY, Transform parent)
    {
        GridModel model = new GridModel(gridWidth, gridHeight);
        GridView view = new GridView(model, gridWidth, gridHeight, spacingx, spacingY, parent);
        GridController controller = new GridController(view, model);
        return controller;
    }
}
