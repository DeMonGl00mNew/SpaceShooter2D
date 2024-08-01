// Класс Tutorial отвечает за обучение игрока и содержит логику изменения состояния обучения
// Он использует событие StateTutorialEvent для оповещения о изменении состояния обучения

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tutorial : MonoBehaviour
{
    // Статическое событие, которое вызывается при изменении состояния обучения
    public static event Action StateTutorialEvent;
    
    // Статическая переменная для хранения текущего состояния обучения
    static private int _stateTutorial = 0;

    // Свойство для доступа к текущему состоянию обучения
    static public int StateTutorial 
    { 
        get { return _stateTutorial; } 
        set { _stateTutorial = value; StateTutorialEvent?.Invoke(); } 
    }

    // Ссылки на объекты в сцене, которые будут управляться в ходе обучения
    public GameObject Magnit;
    public GameObject Fire;
    public GameObject Gamepad;

    private void Start()
    {
        // Подписываемся на событие изменения состояния обучения
        Tutorial.StateTutorialEvent += ListOfActionTutrorial;
    }

    private void OnDisable()
    {
        // Отписываемся от события при выключении объекта
        Tutorial.StateTutorialEvent -= ListOfActionTutrorial;
    }

    private void Update()
    {
        // Выводим текущее состояние обучения в консоль для отладки
        Debug.Log("StateTutorial= " + StateTutorial);
    }

    // Метод, который выполняет необходимые действия в зависимости от состояния обучения
    private void ListOfActionTutrorial()
    {
        // Показать магнит при достижении определенного состояния
        if (StateTutorial == 4)
        {
            Magnit.SetActive(true);
        }
        
        // Показать огонь при достижении другого состояния
        if (StateTutorial == 6)
        {
            Fire.SetActive(true);
        }

        // Скрыть магнит, огонь и геймпад при завершении обучения
        if (StateTutorial == 8)
        {
            Magnit.SetActive(false);
            Fire.SetActive(false);
            Gamepad.SetActive(false);
        }
    }
}