// Импортируем библиотеку для работы с анимациями
// Импортируем необходимые библиотеки
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Объявляем класс для анимации движения Touchpad
public class AnimateMovingTouchpad : MonoBehaviour
{
    Tween tweenRotate; // Объявляем переменную для анимации вращения

    // Функция вызывается при старте
    void Start()
    {
        // Подписываемся на событие изменения состояния Touchpad
        Player.StateTouchpadTutorialEvent += Rotate;
        
        // Создаем и настраиваем анимацию вращения
        tweenRotate = transform.DORotate(new Vector3(0, 0, -1), 30).SetSpeedBased().SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental).Pause();
    }
    
    // Функция вызывается при выключении объекта
    private void OnDisable()
    {
        // Отписываемся от события изменения состояния Touchpad
        Player.StateTouchpadTutorialEvent -= Rotate;
    }
    
    // Функция для вращения объекта
    private void Rotate(float rotateValue)
    {
        // Проверяем, существует ли анимация
        if (tweenRotate == null)
            return;
        
        // Включаем или отключаем анимацию в зависимости от значения вращения
        if (rotateValue >= 0.8f)
            tweenRotate.Play();
        else
            tweenRotate.Pause();
    }
}