using UnityEngine;

public class zerkalo : MonoBehaviour
{
    public bool onKuh = true;
    public camerasFuncs toKuhn;
    public camerasFuncs toKomnt;


    private void OnMouseEnter()
    {
            gameObject.GetComponent<Outline>().OutlineWidth = 7.34f;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Outline>().OutlineWidth = 0f;
    }

    public void OnMouseDown()
    {
        if (onKuh)
        {
            toKomnt.enabled = true;
            toKuhn.enabled = false;
            onKuh = false;
        }
        else
        {
            toKomnt.enabled = false;
            toKuhn.enabled = true;
            onKuh = true;
        }

    }
}
