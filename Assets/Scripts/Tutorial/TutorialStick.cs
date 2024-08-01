// Класс TutorialStick отвечает за отображение подсказки в виде палки
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Импорт библиотеки для работы с анимациями
using UnityEngine.UI;
using System;

public class TutorialStick : MonoBehaviour
{
    public Transform FakeStick; // Префаб для отображения "фальшивой" палки
    public Image Stick; // Изображение реальной палки
    Sequence s; // Последовательность для анимации элементов

    void Start()
    {
        Tutorial.StateTutorialEvent += MoveFakeStick; // Подписываем метод MoveFakeStick на событие StateTutorialEvent

        //Stick.color = Vector4.Scale(Stick.color, new Color(1, 1, 1, 0)); 
        // Изменение цвета палки на прозрачный (закомментировано, возможно не используется)
    }

    private void OnDisable()
    {
        Tutorial.StateTutorialEvent -= MoveFakeStick; // Отписываем метод MoveFakeStick от события StateTutorialEvent
    }

    private void MoveFakeStick()
    {
        s = DOTween.Sequence(); // Создаем новую последовательность анимации с помощью DOTween
        if (Tutorial.StateTutorial == 2) // Если текущее состояние туториала равно 2
        {
            s.Append(FakeStick.DOLocalMoveY(-130, 2)); // Двигаем "фальшивую" палку вниз за 2 секунды
            s.AppendInterval(0.25f); // Делаем паузу на 0.25 секунды
            s.SetLoops(-1, LoopType.Restart); // Устанавливаем зацикливание анимации
        }
        if (Tutorial.StateTutorial == 3 && s != null) // Если текущее состояние туториала равно 3 и последовательность анимации существует
        {
            s.Kill(); // Прерываем анимацию
            FakeStick.gameObject.SetActive(false); // Скрываем "фальшивую" палку
        }
    }
}