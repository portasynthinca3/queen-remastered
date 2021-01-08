using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Sprite entitySelectedSprite;
    
    private Dictionary<Entity, GameObject> selectionSprites = new Dictionary<Entity, GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public void CreateEntitySelectedSprite(Entity entity)
    {
        var collider = entity.GetComponent<Collider2D>();
        
        var spriteObject = new GameObject("Selection sprite");
        spriteObject.transform.parent = entity.transform;
        spriteObject.transform.localPosition = Vector3.zero;

        var sprite = spriteObject.AddComponent<SpriteRenderer>();
        sprite.sprite = entitySelectedSprite;
        var bounds = collider.bounds;
        spriteObject.transform.localScale = bounds.size;

        selectionSprites.Add(entity, spriteObject);
    }

    public void DestroyEntitySelectionSprite(Entity entity)
    {
        GameObject selectionSprite;
        if(!selectionSprites.TryGetValue(entity, out selectionSprite)) return;
        Destroy(selectionSprite);
        selectionSprites.Remove(entity);
    }
}
