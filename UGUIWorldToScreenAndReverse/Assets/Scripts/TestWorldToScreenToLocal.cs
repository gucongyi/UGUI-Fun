using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestWorldToScreenToLocal : MonoBehaviour
{
    public Canvas canvas;
    public Image image2;
    public Image image;
    public Image image3;
    // Start is called before the first frame update
    void Start()
    {
        
        Vector2 image2Pos=RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, image2.transform.position);
        Debug.Log($"image2.transform.position:{image2.transform.position} image2Pos:{image2Pos}");
        Vector3 worldPoint = Vector3.zero;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.GetComponent<RectTransform>(), image2Pos, canvas.worldCamera, out worldPoint);
        Debug.Log($"worldPoint:{worldPoint}");
        image.transform.position = worldPoint;
        image3.transform.position = worldPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
