using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine;

public class camerasFuncs : MonoBehaviour
{
    public GameObject whatCameraAttechedTo;
    public cameraSwitch CameraSwitch;
    public GameObject camera1; // камера, с которой идет сигнал
    public GameObject camera2;

    private void OnMouseDown()
    {
        //7.34

        CheckCameraRay(camera1.GetComponentInChildren<Camera>(), camera1); // проверка, идет ли сигнал от камеры с которой мы смотрим
        CheckCameraRay(camera2.GetComponentInChildren<Camera>(), camera2);
    }

    private void OnMouseEnter()
    {

        gameObject.GetComponent<Outline>().OutlineWidth = 7.34f;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Outline>().OutlineWidth = 0f;
    }

    //алгоритм проверки камер вообще:
    //    тыкается камера, активируется этот скрипт
    //        проверяются все возможные камеры, с которых может прийти сигнал в радиусе видимости этой камеры
    //        если сигнал есть, записываем данные (рект для камеры, активность, саму камеру)
    //        просчитываем сколько камер на данный момент активно (чекаем массив активных камер на наличие активных первых 4 камер для фулл комплекта я ебала)
    //        если у нас не активно 4 камеры - запускаем по порядку камеры (просто запускаем камеру, которая была тыкнута)
    //        если у нас активны все 4 камеры:
    //            проверка на то какие камеры есть
    //            проверка на то, какая камера ща готовится врубиться 
    //            если есть дубликат той камеры, которая готовится врубится:
    //                получаем данные рект камеры дубликата
    //                получаем данные рект камеры которая будет активироваться
    //                шафлим между собой
    //            если дубликата нет:
    //                берем рект камеры которая будет заменяться
    //                заменяем рект камеры которая будет включена на рект который только что получили
    //                вырубаем активность камеры которую заменили от греха подальше
    //                врубаем камеру которая врубается

    void CheckCameraRay(Camera cam, GameObject cameraObject)
    {
        if (cam == null && cameraObject == null) return; //на случай если развилки нет

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == this.transform)
            {
                CameraSwitch.UpdateActives();

                if (CameraSwitch.fullScreen == true) // если все 4 камеры открыты
                {
                    if (CameraSwitch.ActiveCameras.Contains<GameObject>(whatCameraAttechedTo)) // если есть дубликат
                    {
                        Debug.Log("дубликат есть");
                        Rect storeRect = cam.rect;

                        for (int i = 0; i < CameraSwitch.ActiveCameras.Count; i++)
                        {
                            if (CameraSwitch.ActiveCameras[i] == whatCameraAttechedTo)
                            {
                                cam.rect = whatCameraAttechedTo.GetComponent<Camera>().rect;
                                whatCameraAttechedTo.GetComponent<Camera>().rect = storeRect;
                            }
                        }
                    }
                    else // если нет дубликата
                    {
                        Debug.Log("дубликата нет");

                        whatCameraAttechedTo.SetActive(true);
                        whatCameraAttechedTo.GetComponent<Camera>().rect = cam.rect;
                        for (int i = 0; i < CameraSwitch.ActiveCameras.Count; i++)
                        {
                            if (CameraSwitch.ActiveCameras[i] == cameraObject.transform.GetChild(0).gameObject)
                            {
                                CameraSwitch.ActiveCameras[i] = whatCameraAttechedTo;
                                cameraObject.transform.GetChild(0).gameObject.SetActive(false);
                            }
                        }
                    }
                }
                else
                {
                    if (CameraSwitch.ActiveCameras.Contains<GameObject>(whatCameraAttechedTo)) // если есть дубликат
                    {
                        Debug.Log("дубликат есть - но окна не все");
                        Rect storeRect = cam.rect;

                        for (int i = 0; i < CameraSwitch.ActiveCameras.Count; i++)
                        {
                            if (CameraSwitch.ActiveCameras[i] == whatCameraAttechedTo)
                            {
                                cam.rect = whatCameraAttechedTo.GetComponent<Camera>().rect;
                                whatCameraAttechedTo.GetComponent<Camera>().rect = storeRect;
                            }
                        }
                    }
                    else // если нет дубликата
                    {
                        Debug.Log("дубликата нет - но окна не все");
                        CameraSwitch.ActiveCameras.Add(whatCameraAttechedTo);
                        if (CameraSwitch.ActiveCameras[1] == whatCameraAttechedTo)
                        {
                            whatCameraAttechedTo.GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                            gameObject.transform.GetChild(0).gameObject.SetActive(true);
                        }
                        else if (CameraSwitch.ActiveCameras[2] == whatCameraAttechedTo)
                        {
                            whatCameraAttechedTo.GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 0.5f);
                            gameObject.transform.GetChild(0).gameObject.SetActive(true);
                        }
                        else if (CameraSwitch.ActiveCameras[3] == whatCameraAttechedTo)
                        {
                            whatCameraAttechedTo.GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                            gameObject.transform.GetChild(0).gameObject.SetActive(true);

                        }
                    }

                    

                }

            }
        }
    }
}
