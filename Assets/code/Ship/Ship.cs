using UnityEngine;

public class Ship : MonoBehaviour
{

    public int width;
    public int height;
    [SerializeField] SpriteRenderer destroyedShipSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destroyedShipSprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShipDestroyed() 
    {
        destroyedShipSprite.enabled = true;
    }
}
