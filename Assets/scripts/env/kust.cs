using UnityEngine;

public class kust : MonoBehaviour
{
    public bool isClicked = false;
    public GameObject camera;
    private void OnMouseEnter()
    {
        if (isClicked == false)
        {
            gameObject.GetComponent<Outline>().OutlineWidth = 7.34f;

        }
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Outline>().OutlineWidth = 0f;
    }

    public void OnMouseDown()
    {

        if (isClicked == false)
        {
            camera.SetActive(true);
            camera.transform.localPosition = new Vector3(-3.8039999f, 1.64999998f, 0.868000031f);
            gameObject.transform.localPosition = new Vector3(-3.6697619f, 0.171000004f, 0.596000016f);
            gameObject.transform.localScale = new Vector3(100f, 100f, 92.4505005f);
            isClicked = true;
        }

    }
}
