using UnityEngine;

public class Ship : MonoBehaviour
{
    public int width { get; private set; }
    public int height { get; private set; }
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

    public void Hit() 
    {
        destroyedShipSprite.enabled = true;
    }
}
