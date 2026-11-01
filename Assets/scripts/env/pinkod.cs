using TMPro;
using UnityEngine;

public class pinkod : MonoBehaviour
{
    public string pin = "";
    public TMP_Text nadpic;
    public GameObject dver;
    public bool isOver = false;

    public void AddDigit(string digit)
    {
        if (isOver) return;

        digit = digit.Trim().Replace(" ", "");

        pin += digit;

        if (nadpic != null)
        {
            nadpic.text = pin;
        }

        if (pin.Length == 3)
        {
            if (pin == "729")
            {
                OpenDoor();
            }
            else
            {
                ResetPin();
            }
        }
    }

    private void OpenDoor()
    {
        dver.transform.localPosition = new Vector3(0.897000015f, -1.13199997f, -11.4040003f);
        dver.transform.localRotation = new Quaternion(-0.533542752f, -0.464038938f, -0.464038938f, 0.533542752f);
        isOver = true;
    }

    private void ResetPin()
    {
        pin = "";
        if (nadpic != null) nadpic.text = "";
    }
}
