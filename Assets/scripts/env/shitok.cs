using UnityEngine;

public class shitok : MonoBehaviour
{
    public bool isOn = false;
    private void OnMouseDown()
    {
        if (isOn == false)
        {
            gameObject.transform.localPosition = new Vector3(-5.07782459f, 1.773f, 0.52700001f);
            gameObject.transform.localRotation = new Quaternion((float)-0.716387093, 0, 0, (float)-0.697703123);
            isOn = true;
        }
    }
}
