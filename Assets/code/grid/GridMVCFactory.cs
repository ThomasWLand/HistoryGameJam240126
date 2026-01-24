using UnityEngine;

public class GridMVC
{
    public readonly GridView gridView;
    public GridMVC(GridView view)
    {
        gridView = view;
    }
}

public static class GridMVCFactory
{
    public static GridMVC build(int gridWidth, int gridHeight, float spacingx, 
        float spacingY, Transform parent)
    {
        GridModel model = new GridModel(gridWidth, gridHeight);
        GridView view = new GridView(model, gridWidth, gridHeight, spacingx, spacingY, parent);

        return new GridMVC(view);
    }
}
