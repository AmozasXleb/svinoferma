using UnityEngine;

public class shitok : MonoBehaviour
{
    public bool isOn = false;
    public GameObject[] teliks;
    private void OnMouseDown()
    {
        if (isOn == false)
        {
            //gameObject.transform.localPosition = new Vector3(-5.07782459f, 1.773f, 0.52700001f);
            gameObject.transform.localRotation = new Quaternion((float)-0.713975847, 0, 0, (float)-0.700170279);
            for (int i = 0; i < teliks.Length; i++)
            {
                teliks[i].SetActive(true);
            }
            isOn = true;
        }
    }
}
