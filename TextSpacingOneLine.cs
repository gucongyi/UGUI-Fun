using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextSpacingOneLine : MonoBehaviour, IMeshModifier
{
    private Graphic refGraphics = null;
    private Graphic graphics
    {
        get
        {
            if (refGraphics == null)
            {
                refGraphics = GetComponent<Graphic>();
            }
            return refGraphics;
        }
    }

    private Text refText = null;
    private Text text
    {
        get
        {
            if (refText == null)
            {
                refText = GetComponent<Text>();
            }
            return refText;
        }
    }

    [SerializeField]
    private float textSpacing = 0;

    private const int CountQuadVertices = 6;

    void OnValidate()
    {
        if (graphics != null)
        {
            graphics.SetVerticesDirty();
        }
    }

    public void ModifyMesh(VertexHelper verts)
    {
        List<UIVertex> vertices = new List<UIVertex>();
        verts.GetUIVertexStream(vertices);

        ModifyTextSpacing(ref vertices);

        verts.Clear();
        verts.AddUIVertexTriangleStream(vertices);
    }

    private void ModifyTextSpacing(ref List<UIVertex> vertices)
    {
        if (text != null)
        {
            var textWidth = CalcTextWidth(vertices);
            switch (text.alignment)
            {
                case TextAnchor.LowerLeft:
                case TextAnchor.MiddleLeft:
                case TextAnchor.UpperLeft:
                    ModifyTextSpacingAlign(ref vertices, textWidth, TextAlignment.Left);
                    break;

                case TextAnchor.LowerCenter:
                case TextAnchor.MiddleCenter:
                case TextAnchor.UpperCenter:
                    ModifyTextSpacingAlign(ref vertices, textWidth, TextAlignment.Center);
                    break;

                case TextAnchor.LowerRight:
                case TextAnchor.MiddleRight:
                case TextAnchor.UpperRight:
                    ModifyTextSpacingAlign(ref vertices, textWidth, TextAlignment.Right);
                    break;
            }
        }
    }

    private float CalcTextWidth(List<UIVertex> vertices)
    {
        float width = 0;
        int vcount = vertices.Count;
        for (int v = 0; v < vcount; v += CountQuadVertices)
        {
            width += (vertices[v + 1].position.x - vertices[v].position.x);
        }
        return width;
    }

    private void ModifyTextSpacingAlign(ref List<UIVertex> vertices, float textWidth, TextAlignment align)
    {
        int vcount = vertices.Count;
        if (vcount <= 0)
        {
            return;
        }

        float xshift = 0;
        var size = text.rectTransform.sizeDelta;
        var w = textWidth + (vcount / CountQuadVertices - 1) * textSpacing;
        if (align == TextAlignment.Center)
        {
            xshift = -w * 0.5f;
        }
        else
        {
            if (align == TextAlignment.Right)
            {
                xshift = size.x * 0.5f - w;
            }
            else
            {
                xshift = -size.x * 0.5f;
            }
        }
        xshift = xshift - vertices[0].position.x;

        Vector3 pos;
        Vector3 prev = vertices[0].position;
        float xgap = 0;
        for (int v = 0; v < vcount; v += CountQuadVertices)
        {
            pos = vertices[v].position;
            xgap = (prev.x - pos.x) + xshift;
            for (int i = 0; i < CountQuadVertices; i++)
            {
                var vertex = vertices[v + i];
                vertex.position.x += xgap;
                vertices[v + i] = vertex;
            }
            prev = vertices[v + 1].position;
            xshift = textSpacing;
        }
    }

    public void ModifyMesh(Mesh mesh) { }
}