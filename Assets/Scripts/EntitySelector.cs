using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySelector : MonoBehaviour
{
    public ClickReceiver clickReceiver;

    private List<Entity> selectedEntities = new List<Entity>();
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;

        clickReceiver.onClick.AddListener(TrySelectSingleEntity);
    }

    public bool IsEntitySelected(Entity entity)
    {
        foreach (var entity1 in selectedEntities)
        {
            if (entity == entity1) return true;
        }
        return false;
    }

    public void DeselectAll()
    {
        foreach (var entity in selectedEntities)
        {
            UIManager.Instance.DestroyEntitySelectionSprite(entity);
        }

        selectedEntities.Clear();
    }

    private void TrySelectSingleEntity(Vector2 screenPosition)
    {
        Entity entity = RaycastForEntity(screenPosition);
        DeselectAll();

        if (entity != null)
        {
            selectedEntities.Add(entity);

            UIManager.Instance.CreateEntitySelectedSprite(entity);
        }
    }

    private Entity RaycastForEntity(Vector2 screenPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(screenPosition), Vector2.zero);

        if (hit.collider != null)
        {
            Entity entity = hit.collider.gameObject.GetComponent<Entity>();

            if (entity != null) return entity;
        }

        return null;
    }
}
