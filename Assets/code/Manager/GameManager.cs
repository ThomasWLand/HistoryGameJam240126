using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int width;
    public int height;
    public float spacingX;
    public float spacingY;
    public Transform gridParent;
    public void Start()
    {
        GridMVC grid = GridMVCFactory.build(width, height, spacingX, spacingY, gridParent);
        GridView view = grid.gridView;

        view.UpdateView();
    }
}
