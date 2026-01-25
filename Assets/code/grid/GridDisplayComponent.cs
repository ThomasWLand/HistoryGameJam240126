using UnityEngine;

/* 
This will be a temp way of displaying the grid until we have a better understanding
of how the sprites and stuff will work, for now its just a gameobject with a 
sprite renderer and we mess with the colour based on state 
*/

public class GridDisplayComponent : MonoBehaviour , GridViewDisplay
{
    [SerializeField] SpriteRenderer _rnd;

    static readonly Color hiddenColour = Color.black;
    static readonly Color missedColour = Color.white;
    static readonly Color hitColour = Color.red;

    private int _x, _y;
    private GridView _view;

    public void Init(int x, int y, GridView view)
    {
        this._x = x;
        this._y = y;
        this._view = view;
    }

    public void UpdateDisplay(GridViewState newViewState)
    {
        Color targetColour;
        switch(newViewState)
        {
            case GridViewState.MISSED:
                targetColour = missedColour;
                break;
            case GridViewState.HIT:
                targetColour = hitColour;
                break;
            default:
                targetColour = hiddenColour;
                break;
        }
        this._rnd.color = targetColour;
    }

    public void SetVisible(bool isVisible)
    {
        this._rnd.enabled = isVisible; //temp, could do something nicer
    }

    public void DestroyDisplay()
    {
        Destroy(this.gameObject);
    }

    void OnMouseDown()
    {
        this._view.OnGridDisplayClicked(this._x, this._y);
    }
}