using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class bukva : MonoBehaviour
{
    public List<string> bukvi;

    public TMP_Text l;

    public string cur;
    public int curIn = 0;

    private void Update()
    {
        cur = bukvi[curIn];
        l.text = cur;
    }

    private void OnMouseDown()
    {
        if (curIn < 5)
        {
            curIn++;
        }
        else if (curIn >=5)
        {
            curIn = 0;
        }
    }
}
