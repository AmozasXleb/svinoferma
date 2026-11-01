using UnityEngine;

public class plita : MonoBehaviour
{
    public GameObject plitaDver;
    public GameObject tv;
    public GameObject provod;
    public bool isFin = false;

    private void OnMouseEnter()
    {
        if (isFin == false)
        {
            gameObject.GetComponent<Outline>().OutlineWidth = 7.34f;

        }
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Outline>().OutlineWidth = 0f;
    }

    private void OnMouseDown()
    {
        if (isFin == false)
        {
            tv.transform.localPosition = new Vector3(0.791999996f, 0.344999999f, -6.64699984f);
            tv.transform.localRotation = new Quaternion((float)0.695879996, (float)0.125503212, (float)-0.356259912, (float)-0.610801816);
            plitaDver.transform.localPosition = new Vector3(0.730000019f, 1.36000001f, -0.00999999978f);
            plitaDver.transform.localRotation = new Quaternion(0, 0, (float)-0.408346921, (float)0.912826836);
            provod.transform.localPosition = new Vector3(0.819999993f, -2.56999993f, 0.879999995f);
            provod.transform.localRotation = new Quaternion((float)0.862144649, (float)0.472762585, (float)-0.0935734808, (float)-0.156352744);
            isFin = true;

        }
    }
}
