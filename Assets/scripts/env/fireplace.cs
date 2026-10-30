using UnityEngine;

public class fireplace : MonoBehaviour
{
    public GameObject col1;
    public GameObject col2;
    public GameObject col3;
    public GameObject col4;
    public GameObject col5;
    public GameObject zubralo;
    public GameObject camera;

    private void Update()
    {
        if (col1.GetComponent<kolishek>().status == 1 && col2.GetComponent<kolishek>().status == 2 && col3.GetComponent<kolishek>().status == 0 && col4.GetComponent<kolishek>().status == 1 && col5.GetComponent<kolishek>().status == 0)
        {
            zubralo.transform.localPosition = new Vector3(0.180000007f, 0.486999989f, 0.992999971f);
            zubralo.transform.localRotation = new Quaternion((float)-0.9091537, (float)-0.0481563583, (float)0.0441921614, (float)0.411299944);
            camera.SetActive(true);

        }
    }
}
