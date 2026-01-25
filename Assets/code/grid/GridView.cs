using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public interface GridViewDisplay
{
    public void Init(int x, int y, GridView view);
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
    private Transform _parentTransform;

    public readonly UnityEvent<Vector2Int> onDisplayComponentClickedEvent = new UnityEvent<Vector2Int>(); //contains coord

    public GridView(GridModel model, int gridWidth, int gridHeight, 
        float spacingX, float spacingY, Transform displaysParent)
    {
        this._model = model;
        int width = this._width = gridWidth;
        int height = this._height= gridHeight;
        this._parentTransform = displaysParent;
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
                    displayComponent.Init(i,j,this);
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

    public void SetGridPointVisible(int x, int y, bool visible)
    {
        this._displays[x,y].SetVisible(visible);
    }

    public void SetDisplayVisible(bool isVisible)
    {
        foreach(GridViewDisplay display in this._displays)
        {
            display.SetVisible(isVisible);
        }
    }

    public void OnGridDisplayClicked(int x, int y)
    {
        this.onDisplayComponentClickedEvent.Invoke(new Vector2Int(x,y));
    }
}

