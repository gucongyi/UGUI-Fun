using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHitZone : MonoBehaviour
{

    public float width=50;
    public float height=50;

    public class EmptyGraphic : Graphic
    {
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }

    void OnValidate()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            width = Mathf.Max(width, rectTransform.sizeDelta.x);
            height = Mathf.Max(height, rectTransform.sizeDelta.y);
        }
    }

    void Awake()
    {
        createHitZone();
    }

    void createHitZone()
    {
        // create child object
        GameObject gobj = new GameObject("Button Hit Zone");
        RectTransform hitzoneRectTransform = gobj.AddComponent<RectTransform>();
        hitzoneRectTransform.SetParent(transform);
        hitzoneRectTransform.localPosition = Vector3.zero;
        hitzoneRectTransform.localScale = Vector3.one;
        hitzoneRectTransform.sizeDelta = new Vector2(width, height);

        // create transparent graphic
        // Image image = gobj.AddComponent<Image>();
        // image.color = new Color(0, 0, 0, 0);
        gobj.AddComponent<EmptyGraphic>();

        // delegate events
        EventTrigger eventTrigger = gobj.AddComponent<EventTrigger>();
        // pointer up
        addEventTriggerListener(eventTrigger, EventTriggerType.PointerDown,
            (BaseEventData data) => {
                ExecuteEvents.Execute(gameObject, data,
                   ExecuteEvents.pointerDownHandler);
            });
        // pointer down
        addEventTriggerListener(eventTrigger, EventTriggerType.PointerUp,
            (BaseEventData data) => {
                ExecuteEvents.Execute(gameObject, data,
                   ExecuteEvents.pointerUpHandler);
            });
        // pointer click
        addEventTriggerListener(eventTrigger, EventTriggerType.PointerClick,
            (BaseEventData data) => {
                ExecuteEvents.Execute(gameObject, data,
                   ExecuteEvents.pointerClickHandler);
            });
    }

    static void addEventTriggerListener(EventTrigger trigger, EventTriggerType eventType,
                                         System.Action<BaseEventData> method)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(method));
        trigger.triggers.Add(entry);
    }
}