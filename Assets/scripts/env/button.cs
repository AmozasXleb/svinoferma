using TMPro;
using UnityEngine;

public class button : MonoBehaviour
{
    public pinkod pinkod;
    public TMP_Text text;

    private void OnMouseDown()
    {
        if (pinkod == null || text == null)
        {
            return;
        }

        pinkod.AddDigit(text.text);
    }
}
