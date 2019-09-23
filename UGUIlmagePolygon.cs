using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UGUIlmagePolygon : Image
{
    PolygonCollider2D imageCollider;

    protected override void Awake()
    {
        base.Awake();
        imageCollider = GetComponent<PolygonCollider2D>();
    }

    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        var _hit = imageCollider.OverlapPoint(eventCamera.ScreenToWorldPoint(screenPoint));
        return _hit;
    }
}
