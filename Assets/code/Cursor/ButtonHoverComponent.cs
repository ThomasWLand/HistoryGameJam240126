using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverComponent : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.PlaySound(GameSounds.UI_HOVER);
    }
}
