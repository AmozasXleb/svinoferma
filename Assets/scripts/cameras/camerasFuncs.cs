using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerasFuncs : MonoBehaviour
{
    [Header("Связи")]
    public GameObject whatCameraAttechedTo;   // камера-приёмник
    public cameraSwitch CameraSwitch;         // менеджер активных камер

    [Header("Камеры-источники (camera3 опционален)")]
    public GameObject camera1;
    public GameObject camera2;
    public GameObject camera3;                // ← опциональный третий источник

    [Header("Подсветка")]
    [SerializeField] private float outlineHoverWidth = 7.34f;

    private Outline outline;
    private readonly List<Camera> sourceCams = new List<Camera>(3);

    // Кэш квадрантов экрана
    private static readonly Rect[] Slots =
    {
        new Rect(0f,   0.5f, 0.5f, 0.5f),
        new Rect(0.5f, 0.5f, 0.5f, 0.5f),
        new Rect(0f,   0f,   0.5f, 0.5f),
        new Rect(0.5f, 0f,   0.5f, 0.5f),
    };

    private void Awake()
    {
        outline = GetComponent<Outline>();
        RebuildSourceCache();
    }

    // Пересобираем кэш источников (можно звать вручную, если меняешь camera1..3 в рантайме)
    public void RebuildSourceCache()
    {
        sourceCams.Clear();
        TryAddSource(camera1);
        TryAddSource(camera2);
        TryAddSource(camera3); // если null/без Camera — просто пропустится
    }

    private void TryAddSource(GameObject go)
    {
        if (go == null) return;
        Camera c = go.GetComponentInChildren<Camera>();
        if (c != null && !sourceCams.Contains(c))
            sourceCams.Add(c);
    }

    // ---------------------------------------------------------------------
    // Наведение / подсветка
    // ---------------------------------------------------------------------
    private void OnMouseEnter()
    {
        if (outline != null) outline.OutlineWidth = outlineHoverWidth;
    }

    private void OnMouseExit()
    {
        if (outline != null) outline.OutlineWidth = 0f;
    }

    // ---------------------------------------------------------------------
    // Клик: проверяем видимость со всех источников, берём ОДИН по приоритету
    // ---------------------------------------------------------------------
    private void OnMouseDown()
    {
        if (CameraSwitch == null || whatCameraAttechedTo == null) return;

        // Если кэш пуст (например, camera1..3 назначены после Awake) — пересобираем
        if (sourceCams.Count == 0) RebuildSourceCache();
        if (sourceCams.Count == 0) return;

        Camera sourceCam = null;
        for (int i = 0; i < sourceCams.Count; i++)
        {
            Camera c = sourceCams[i];
            if (c == null) continue; // камеру могли уничтожить в рантайме
            if (IsVisibleFrom(c, transform))
            {
                sourceCam = c;
                break; // приоритет: camera1 → camera2 → camera3
            }
        }

        if (sourceCam == null) return; // никто не видит

        // Атомарно: один раз обновляем список ДО мутаций
        CameraSwitch.UpdateActives();

        ConnectCamera(sourceCam, whatCameraAttechedTo);
    }

    // ---------------------------------------------------------------------
    // Публичная обёртка (совместимость со старым API)
    // ---------------------------------------------------------------------
    public void CheckCameraRay(Camera cam, GameObject cameraObject)
    {
        if (cam == null || cameraObject == null) return;
        if (CameraSwitch == null || whatCameraAttechedTo == null) return;

        if (!IsVisibleFrom(cam, transform)) return;

        ConnectCamera(cam, whatCameraAttechedTo);
    }

    // ---------------------------------------------------------------------
    // Ядро: подключение приёмника к окну монитора
    // ---------------------------------------------------------------------
    private void ConnectCamera(Camera sourceCam, GameObject receiver)
    {
        if (sourceCam == null || receiver == null) return;

        Camera receiverCam = receiver.GetComponent<Camera>();
        if (receiverCam == null) return;

        sourceCam.clearFlags = CameraClearFlags.SolidColor;
        receiverCam.clearFlags = CameraClearFlags.SolidColor;

        List<GameObject> actives = CameraSwitch.ActiveCameras;
        int duplicateIndex = actives.IndexOf(receiver);

        // 1. Приёмник уже активен → меняемся окнами
        if (duplicateIndex >= 0)
        {
            SwapRects(sourceCam, receiverCam);
            return;
        }

        // 2. Все окна заняты → вытесняем источник
        if (CameraSwitch.fullScreen || actives.Count >= Slots.Length)
        {
            ReplaceSourceSlot(sourceCam, receiver, receiverCam, actives);
            return;
        }

        // 3. Есть свободное окно
        AssignFreeSlot(receiver, receiverCam, actives);
    }

    // ---------------------------------------------------------------------
    // Вытеснение источника
    // ---------------------------------------------------------------------
    private void ReplaceSourceSlot(Camera sourceCam, GameObject receiver, Camera receiverCam, List<GameObject> actives)
    {
        // Ищем в списке именно камеру-источник
        int sourceIndex = -1;
        for (int i = 0; i < actives.Count; i++)
        {
            if (actives[i] == null) continue;
            Camera c = actives[i].GetComponentInChildren<Camera>();
            if (c == sourceCam)
            {
                sourceIndex = i;
                break;
            }
        }

        if (sourceIndex < 0)
        {
            // Источника в списке нет — просто ставим в свободный слот
            AssignFreeSlot(receiver, receiverCam, actives);
            return;
        }

        GameObject replaced = actives[sourceIndex];

        receiverCam.rect = sourceCam.rect;
        receiver.SetActive(true);
        actives[sourceIndex] = receiver;

        if (replaced != null && replaced != receiver)
            replaced.SetActive(false);

        StartCoroutine(SafeRenderNextFrame(receiverCam));
    }

    // ---------------------------------------------------------------------
    // Назначение свободного квадранта
    // ---------------------------------------------------------------------
    private void AssignFreeSlot(GameObject receiver, Camera receiverCam, List<GameObject> actives)
    {
        Rect? freeRect = FindFreeRect(actives);
        if (freeRect == null)
        {
            Debug.LogWarning("camerasFuncs: нет свободных окон, хотя fullScreen == false.");
            return;
        }

        receiverCam.rect = freeRect.Value;
        receiver.SetActive(true);
        actives.Add(receiver);

        if (transform.childCount > 0)
            transform.GetChild(0).gameObject.SetActive(true);

        StartCoroutine(SafeRenderNextFrame(receiverCam));
    }

    // ---------------------------------------------------------------------
    // Утилиты
    // ---------------------------------------------------------------------
    private bool IsVisibleFrom(Camera cam, Transform target)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out RaycastHit hit) && hit.transform == target;
    }

    private static void SwapRects(Camera a, Camera b)
    {
        Rect ra = a.rect;
        a.rect = b.rect;
        b.rect = ra;
    }

    private Rect? FindFreeRect(List<GameObject> actives)
    {
        for (int s = 0; s < Slots.Length; s++)
        {
            Rect slot = Slots[s];
            bool occupied = false;

            for (int i = 0; i < actives.Count; i++)
            {
                if (actives[i] == null) continue;
                Camera c = actives[i].GetComponent<Camera>();
                if (c == null) continue;

                if (RectsApproximatelyEqual(c.rect, slot))
                {
                    occupied = true;
                    break;
                }
            }

            if (!occupied) return slot;
        }
        return null;
    }

    private static bool RectsApproximatelyEqual(Rect a, Rect b)
    {
        const float eps = 0.001f;
        return Mathf.Abs(a.x - b.x) < eps
            && Mathf.Abs(a.y - b.y) < eps
            && Mathf.Abs(a.width - b.width) < eps
            && Mathf.Abs(a.height - b.height) < eps;
    }

    // ---------------------------------------------------------------------
    // Безопасный рендер на следующем кадре
    // ---------------------------------------------------------------------
    private IEnumerator SafeRenderNextFrame(Camera cam)
    {
        yield return new WaitForEndOfFrame();
        if (cam != null) cam.Render();
    }
}