using NUnit.Framework.Constraints;
using Unity.Mathematics;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Video;

public interface GridViewDisplay
{
    public void UpdateDisplay(GridViewState newViewState);
    public void SetVisible(bool isVisible);
    public void DestroyDisplay();
}

public class GridView
{
    private GridModel _model;
    private GridViewDisplay[,] _displays;
    static readonly string displayPrefabPath = "GridDisplayPrefab";
    private int _width, _height;

    public GridView(GridModel model, int gridWidth, int gridHeight, 
        float spacingX, float spacingY, Transform displaysParent)
    {
        this._model = model;
        int width = this._width = gridWidth;
        int height = this._height= gridHeight;
        GridViewDisplay[,] displays = this._displays = new GridViewDisplay[width, height];

        GameObject prefab = Resources.Load(displayPrefabPath) as GameObject;
        Vector3 rootPosition = displaysParent.position;

        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                Vector3 offset = new Vector3(i * spacingX, j * spacingY, 0);
                Vector3 position = rootPosition + offset;
                GameObject newDisplay = GameObject.Instantiate(prefab, position, quaternion.identity);
                newDisplay.name = i + "," + j;
                newDisplay.transform.parent = displaysParent;

                if(newDisplay.TryGetComponent<GridViewDisplay>(out GridViewDisplay displayComponent))
                {
                    displays[i,j] = displayComponent;
                }
            }
        }
    }

    public void UpdateView()
    {
        GridData[,] modelData = this._model.GetAllData();
        int width = this._width;
        int height = this._height;
        GridViewDisplay[,] displays = this._displays;

        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                GridData data = modelData[i, j];
                GridViewState state = data.getViewState();
                displays[i,j].UpdateDisplay(state);
            }
        }
    }

    public void DestroyView()
    {
        GridViewDisplay[,] displays = this._displays;
        foreach(GridViewDisplay display in displays)
        {
            display.DestroyDisplay();
        }
    }

    public void SetVisible(int x, int y, bool visible)
    {
        this._displays[x,y].SetVisible(visible);
    }
}

