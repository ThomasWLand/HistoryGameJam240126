using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

    public int width;
    public int height;
    private int health;
    [SerializeField] SpriteRenderer destroyedShipSprite;
    private List<Vector2> _unhitTiles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = width * height;
        destroyedShipSprite.enabled = false;
    }

    public void SetUnhitTiles(List<Vector2> coords)
    {
        this._unhitTiles = coords;
    }

    public void OnTileExploded(Vector2 coord)
    {
        if(!this._unhitTiles.Contains(coord))
        {
            return;            
        }
        health --;
        bool isSunk = health <= 0;
        SoundManager.PlaySound(isSunk ? GameSounds.SHOT_CRITICAL : GameSounds.SHOT_HIT);
        if(isSunk)
        {
            ShipDestroyed();
        }
    }

    public bool GetIsShipAlive() => health > 0;

    public void ShipDestroyed() 
    {
        destroyedShipSprite.enabled = true;
    }
}
