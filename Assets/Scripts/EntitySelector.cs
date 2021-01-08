using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySelector : MonoBehaviour
{
    public ClickReceiver clickReceiver;
    public DragReceiver dragReceiver;
    public RectTransform selectionBox;

    private List<Entity> selectedEntities = new List<Entity>();
    private Camera cam;
    private IEnumerator selectionBoxRoutine;

    private void Awake()
    {
        cam = Camera.main;

        clickReceiver.onClick.AddListener(TrySelectSingleEntity);
        dragReceiver.onBeginDrag.AddListener(StartSelectionBoxRoutine);
    }

    public bool IsEntitySelected(Entity entity)
    {
        return selectedEntities.Contains(entity);
    }

    public void DeselectAll()
    {
        foreach (var entity in selectedEntities)
        {
            UIManager.Instance.DestroyEntitySelectionSprite(entity);
        }

        selectedEntities.Clear();
    }

    private void StartSelectionBoxRoutine()
    {
        if(selectionBoxRoutine != null)
        StopCoroutine(selectionBoxRoutine);
        selectionBoxRoutine = SelectionBoxRoutine();
        StartCoroutine(selectionBoxRoutine);
    }

    private IEnumerator SelectionBoxRoutine()
    {
        Vector3 firstPoint = Input.mousePosition;
        selectionBox.transform.position = firstPoint;
        selectionBox.gameObject.SetActive(true);

        while(Input.GetMouseButton(0))
        {
            Vector2 newSize = Input.mousePosition - firstPoint;
            Vector2 newScale = new Vector2(Mathf.Clamp(newSize.x, -1f, 1f), Mathf.Clamp(newSize.y, -1f, 1f));
            newSize = new Vector2(Mathf.Abs(newSize.x), Mathf.Abs(newSize.y));
            
            selectionBox.sizeDelta = newSize;
            selectionBox.localScale = newScale;
                                             
            yield return null;
        }

        var colliders = new Collider2D[];
        Physics2D.OverlapArea(firstPoint, Input.mousePosition, new ContactFilter2D(), colliders);

        selectionBox.gameObject.SetActive(false);
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
