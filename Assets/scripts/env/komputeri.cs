using UnityEngine;
using System.Collections.Generic;


public class komputeri : MonoBehaviour
{
    public GameObject cur1;
    public GameObject cur2;
    public GameObject cur3;
    public GameObject cur4;
    public bool isDone = false;
    public GameObject kartinka;


    private void Update()
    {
        if (cur1.GetComponent<bukva>().cur == "H" && cur2.GetComponent<bukva>().cur == "O" && cur3.GetComponent<bukva>().cur == "P" && cur4.GetComponent<bukva>().cur == "E")
        {
            isDone = true;
            kartinka.SetActive(true);
        }
    }
}
