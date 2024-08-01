// Класс TutorialSpaceship отвечает за управление космическим кораблем в рамках обучающего уровня
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class TutorialSpaceship : MonoBehaviour
{
    public Transform Player; // Ссылка на игрока
    public SpriteRenderer spriteRenderer; // Компонент отображения спрайта космического корабля
    public SpriteRenderer meteoSprite; // Компонент отображения спрайта метеорита
    Sequence s ; // Последовательность анимаций

    // Подписываемся на события обучающего уровня при запуске
    void Start()
    {
        Tutorial.StateTutorialEvent += MoveToMeteorite; // Движение к метеориту
        Tutorial.StateTutorialEvent += MoveToStation; // Движение к станции
    }

    // Отписываемся от событий при отключении скрипта
    private void OnDisable()
    {
        Tutorial.StateTutorialEvent -= MoveToMeteorite;
        Tutorial.StateTutorialEvent -= MoveToStation;
    }

    // Движение к метеориту в зависимости от состояния обучающего уровня
    private void MoveToMeteorite()
    {
        if (Tutorial.StateTutorial == 2)
        {
            s = DOTween.Sequence();
            s.Append(transform.DORotate(new Vector3(0, 0, -90), 0.5f)); // Поворот космического корабля
            s.Append(transform.DOMove(new Vector3(5.5f, 0, 0), 1).SetSpeedBased().SetEase(Ease.Linear)); // Движение к метеориту
            s.AppendInterval(0.5f);
            s.SetLoops(-1, LoopType.Restart); // Повторять анимацию бесконечно
        }
        if (Tutorial.StateTutorial == 4)
        {
            s.Kill(); // Прервать выполнение анимации
            spriteRenderer.enabled = false; // Скрыть спрайт космического корабля
        }
    }

    // Движение к станции в зависимости от состояния обучающего уровня
    private void MoveToStation()
    {
        if (Tutorial.StateTutorial == 5)
        {
            TutorialFirstMeteorite tutorialFirstMeteorite = Player.GetComponentInChildren<TutorialFirstMeteorite>(); // Получаем ссылку на метеорит из игрока
            
            if (tutorialFirstMeteorite)
            {
                meteoSprite.sprite = tutorialFirstMeteorite.gameObject.GetComponent<SpriteRenderer>().sprite; // Обновляем спрайт для отображения
            }
                
            spriteRenderer.enabled = true; // Показать спрайт космического корабля

            transform.position = Player.position; // Переместить космический корабль к игроку
            transform.rotation = Player.rotation; // Повернуть космический корабль в соответствии с игроком
            s = DOTween.Sequence();
            Vector3 direction = Player.position - transform.position;
            s.Append(GoToPoint(new Vector3(0,0,0))); // Направить корабль к точке (0, 0, 0)
            s.Append(transform.DOMove(new Vector3(0, 0, 0), 1).SetSpeedBased().SetEase(Ease.Linear)); // Движение к точке (0, 0, 0)
            s.AppendInterval(0.5f);
            s.SetLoops(-1, LoopType.Restart); // Повторять анимацию бесконечно
        }
        if (Tutorial.StateTutorial == 6) 
        {
            SpriteRenderer[] spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
            s.Kill(); // Прервать выполнение анимации
            foreach (var sr in spriteRenderer)
            {
                sr.enabled = false; // Скрыть все спрайты космического корабля
            }
        }
    }

    // Анимация направления движения к точке
    public Tweener GoToPoint(Vector3 targetPos)
    {
        var dir = targetPos - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var rot = Quaternion.AngleAxis(angle-90f, Vector3.forward);

        return transform.DORotate(rot.eulerAngles, 0.5f).SetEase(Ease.Linear).SetSpeedBased(); // Вращение космического корабля к целевой точке
    }
}