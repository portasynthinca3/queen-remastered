using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySelector : MonoBehaviour
{
    private List<Entity> selectedEntities = new List<Entity>();
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;    
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Entity entity = RaycastForEntity();
            DeselectAll();
            selectedEntities.Add(entity);
        }
    }

    public void DeselectAll()
    {
        selectedEntities.Clear();
    }

    private Entity RaycastForEntity()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if(hit.collider != null)
        {
            Entity entity = hit.collider.gameObject.GetComponent<Entity>();

            if (entity != null) return entity;
        }
        
        return null;
    }
}
