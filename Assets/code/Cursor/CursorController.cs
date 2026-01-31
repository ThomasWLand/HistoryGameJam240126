using Unity.Burst.Intrinsics;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D menuSprite, gameSprite;
    void Awake()
    {
        SetSprite(true);
    }

    public void SetSprite(bool useMenuSprite)
    {
        Texture2D texture = useMenuSprite ? menuSprite : gameSprite;
        Vector2 centre = useMenuSprite ? Vector2.zero : Vector2.one * 32;
        Cursor.SetCursor(texture, centre, CursorMode.ForceSoftware);
    }
}
