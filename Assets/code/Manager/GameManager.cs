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
        GridController grid = GridMVCFactory.build(width, height, spacingX, spacingY, gridParent);

        grid.UpdateDisplay();
        grid.SetDisplayVisible(true);
    }
}
