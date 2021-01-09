using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class PushDownButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Color      Color;
    public UnityEvent Event;
    [Range(0.01f, 5f)]
    public float MoveTime = 1f;
    [Range(0f, 5f)]
    public float Offset;

    RectTransform   head;
    Image           selfImage, headImage;
    Button          button;
    IEnumerator     moveRoutine;

    void Awake()
    {
        head      = transform.Find("Head").GetComponent<RectTransform>();
        selfImage = GetComponent<Image>();
        button    = GetComponent<Button>();
        headImage = head.GetComponent<Image>();
    }

    void Update()
    {
        selfImage.color = Color;
        headImage.color = Color;
    }

    void SetY(RectTransform trans, float y)
    {
        var pos = trans.anchoredPosition;
        trans.anchoredPosition = new Vector2(pos.x, y);
    }

    IEnumerator MoveHead(bool up)
    {
        var start = Time.time;
        var pos   = 0f;
        while(Time.time - start <= MoveTime)
        {
            var time = (Time.time - start) / MoveTime;
                pos  = Mathf.SmoothStep(0f, Offset, time);
            if(up) pos = Offset - pos;

            SetY(head, -pos);

            yield return null;
        }

        // Force the head into its final position since Mathf.SmoothStep
        // really hates me today for some reason
        SetY(head, up ? 0f : -Offset);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(moveRoutine != null) StopCoroutine(moveRoutine);
        StartCoroutine(moveRoutine = MoveHead(false));
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if(moveRoutine != null) StopCoroutine(moveRoutine);
        StartCoroutine(moveRoutine = MoveHead(true));

        Event.Invoke();
    }
}