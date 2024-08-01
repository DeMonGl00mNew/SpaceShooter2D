// Класс ClickManager отвечает за обработку кликов игрока

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickManager : MonoBehaviour
{
    private PlayerInputMap playerInputMap; // Переменная для управления вводом игрока
    Camera cam; // Переменная для хранения ссылки на камеру
    Ray ray; // Луч для определения объектов под курсором
    LayerMask layerMask = 1 << 6; // Слой для определения объектов, с которыми можно взаимодействовать

    void Start()
    {
        cam = Camera.main; // Получаем основную камеру
        playerInputMap = new PlayerInputMap(); // Инициализируем управление игрока
        playerInputMap.Enable(); // Включаем управление
        playerInputMap.Player.Click.performed += InputClick; // Подписываемся на событие клика игрока
    }

    private void OnDestroy()
    {
        playerInputMap.Player.Click.performed -= InputClick; // Отписываемся от события клика при уничтожении объекта
    }

    private void InputClick(InputAction.CallbackContext context)
    {
        ray = cam.ScreenPointToRay(playerInputMap.Player.Position.ReadValue<Vector2>()); // Создаем луч из камеры в позицию клика
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 15, layerMask); // Проверяем, попали ли мы на объект на слое layerMask

        if (hit)
        {
            if (hit.collider.TryGetComponent(out TouchMeteorite touchMeteorite)) // Если попали на метеорит с компонентом TouchMeteorite
            {
                touchMeteorite.StateDontShootPropety = true; // Устанавливаем состояние метеорита как "не стрелять"
            }
        }
    }
}