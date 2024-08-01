// Этот скрипт отвечает за поведение обучающего стика и корабля в игре
// Он подписывается и отписывается от событий обучающего уровня

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Импорт библиотеки для работы с вводом
using UnityEngine.UI;

public class StickTutorial : MonoBehaviour
{
    public CanvasGroup AlphaFakeStick; // Прозрачность fake стика
    public GameObject SpaceShipTutorial; // Обучающий корабль
    private PlayerInputMap playerInputMap; // Карта ввода игрока

    void Start()
    {
        playerInputMap = new PlayerInputMap(); // Создаем новую карту ввода
        playerInputMap.Player.Move.started += VisibleStick; // Подписываем метод VisibleStick на событие начала движения
        playerInputMap.Player.Move.canceled += InvisibleStick; // Подписываем метод InvisibleStick на событие завершения движения
        playerInputMap.Player.Enable(); // Включаем карту ввода игрока

        Tutorial.StateTutorialEvent += UnsubscribeStick; // Подписываем метод UnsubscribeStick на событие отмены обучения
    }

    private void OnDestroy()
    {
        playerInputMap.Player.Move.started -= VisibleStick; // Отписываем метод VisibleStick от события начала движения
        playerInputMap.Player.Move.canceled -= InvisibleStick; // Отписываем метод InvisibleStick от события завершения движения
        Tutorial.StateTutorialEvent -= UnsubscribeStick; // Отписываем метод UnsubscribeStick от события отмены обучения
    }

    private void InvisibleStick(InputAction.CallbackContext context)
    {
        AlphaFakeStick.alpha = 1; // Устанавливаем прозрачность fake стика
        SpaceShipTutorial.SetActive(true); // Включаем обучающий корабль
    }

    public void VisibleStick(InputAction.CallbackContext context)
    {
        AlphaFakeStick.alpha = 0; // Устанавливаем прозрачность fake стика
        SpaceShipTutorial.SetActive(false); // Выключаем обучающий корабль
    }

    private void UnsubscribeStick()
    {
        if (Tutorial.StateTutorial != 1)
        {
            AlphaFakeStick.alpha = 0; // Устанавливаем прозрачность fake стика
            playerInputMap.Player.Move.started -= VisibleStick; // Отписываем метод VisibleStick от события начала движения
            playerInputMap.Player.Move.canceled -= InvisibleStick; // Отписываем метод InvisibleStick от события завершения движения
        }
    }
}