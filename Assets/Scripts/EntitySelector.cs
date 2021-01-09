using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySelector : MonoBehaviour
{
    public static EntitySelector Instance { get; private set; }

    public ClickReceiver clickReceiver;
    public DragReceiver dragReceiver;
    public RectTransform selectionBox;

    private List<Entity> selectedEntities = new List<Entity>();
    private Camera cam;
    private IEnumerator selectionBoxRoutine;
    private bool unitsSelected;
    private bool draggingSelectionBox;

    private void Awake()
    {
        Instance = this;
        cam = Camera.main;

        clickReceiver.onLeftClick.AddListener(TrySelectSingleEntity);
        dragReceiver.onBeginDrag.AddListener(StartSelectionBoxRoutine);
    }

    public bool IsEntitySelected(Entity entity)
    {
        return selectedEntities.Contains(entity);
    }

    public bool UnitsSelected()
    {
        return unitsSelected;
    }

    public void DeselectAll()
    {
        foreach (var entity in selectedEntities)
        {
            UIManager.Instance.DestroyEntitySelectionSprite(entity);
        }

        unitsSelected = false;
        selectedEntities.Clear();
    }

    private void StartSelectionBoxRoutine()
    {
        if (selectionBoxRoutine != null)
            StopCoroutine(selectionBoxRoutine);
        selectionBoxRoutine = SelectionBoxRoutine();
        StartCoroutine(selectionBoxRoutine);
    }

    private Vector2 Vector2Abs(Vector2 val)
    {
        return new Vector2(Mathf.Abs(val.x), Mathf.Abs(val.y));
    }

    private IEnumerator SelectionBoxRoutine()
    {
        Vector3 firstPoint = Input.mousePosition;
        selectionBox.transform.position = firstPoint;
        selectionBox.gameObject.SetActive(true);
        draggingSelectionBox = true;

        while (Input.GetMouseButton(0))
        {
            DeselectAll();

            // Draw selection box.
            var newSize = Input.mousePosition - firstPoint;
            var newScale = new Vector2(Mathf.Clamp(newSize.x, -1f, 1f), Mathf.Clamp(newSize.y, -1f, 1f));
            newSize = new Vector2(Mathf.Abs(newSize.x), Mathf.Abs(newSize.y));

            selectionBox.sizeDelta = newSize;
            selectionBox.localScale = newScale;

            // Look for units in the area.
            var point2 = cam.ScreenToWorldPoint(Input.mousePosition);
            var point1 = cam.ScreenToWorldPoint(firstPoint);
            var selectionSize = Vector2Abs(point2 - point1);
            var position = (point1 + point2) / 2;

            var colliders = Physics2D.OverlapBoxAll(position, selectionSize, 0f);
            foreach (var collider in colliders)
            {
                var unit = collider.GetComponent<Unit>();

                if (unit != null)
                {
                    SelectEntity(unit);
                }
            }
            yield return null;
        }

        draggingSelectionBox = false;
        if (selectedEntities.Count > 0) unitsSelected = true;

        selectionBox.gameObject.SetActive(false);
    }

    private void SelectEntity(Entity entity)
    {
        selectedEntities.Add(entity);

        unitsSelected = entity.gameObject.GetComponent<Unit>();

        UIManager.Instance.CreateEntitySelectedSprite(entity);
    }

    private void TrySelectSingleEntity(Vector2 screenPosition)
    {
        if(draggingSelectionBox) return;

        Entity entity = RaycastForEntity(screenPosition);
        DeselectAll();

        if (entity != null)
        {
            SelectEntity(entity);
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
