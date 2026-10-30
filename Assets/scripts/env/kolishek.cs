using UnityEngine;

public class kolishek : MonoBehaviour
{
    public int status = 1;
    public void OnMouseDown()
    {
        status++;
        if (status > 2)
        {
            status = 0;
        }

        if (status == 2)
        {
            //0.101
            //Vector3 pos = new Vector3(0f, 0.3f, 0f);
            transform.localPosition = new Vector3(transform.localPosition.x, 0.099f, transform.localPosition.z);
        }
        if (status == 1)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 0.02699999f, transform.localPosition.z);
        }
        if (status == 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -0.044f, transform.localPosition.z);
        }
    }

    private void OnMouseEnter()
    {

        gameObject.GetComponent<Outline>().OutlineWidth = 7.34f;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Outline>().OutlineWidth = 0f;
    }
}
