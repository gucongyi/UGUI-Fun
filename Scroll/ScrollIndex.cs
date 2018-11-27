using System;
using UnityEngine;

public class ScrollIndex : MonoBehaviour
{
    public Action<int> IndexAction;
    int index;

    public void ScrollCellIndex(int idx)
    {
        index = idx;
        IndexAction?.Invoke(index);
    }



}
