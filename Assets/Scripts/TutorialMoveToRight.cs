// Комментарий: Этот скрипт отвечает за движение объекта вправо во время учебного уровня
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMoveToRight : MonoBehaviour
{
    public float DebugDIstance = 0; // Задаем переменную для отладочного расстояния
    [SerializeField] private Rigidbody2D _rigidbody2D; // Ссылка на компонент Rigidbody2D
    public ContactFilter2D contactFilter2d; // Фильтр контактов для использования в проверке столкновений

    private readonly RaycastHit2D[] resultRaycast = new RaycastHit2D[1]; // Массив результатов лучевого пуска

    private void Start()
    {
        StartCoroutine(CastForRightCouratine()); // Запуск корутины для проверки столкновений справа
    }
    private void FixedUpdate()
    {

    }
    IEnumerator CastForRightCouratine()
    {
        int collisionCount = 0; // Счетчик столкновений
        while (true)
        {
            if (Tutorial.StateTutorial == 5) // Если уровень учебы равен 5, завершаем корутину
                yield break;
            collisionCount = _rigidbody2D.Cast(transform.up, contactFilter2d, resultRaycast, 10); // Проверяем столкновения впереди объекта
            if (collisionCount != 0)
            {

                if (Tutorial.StateTutorial < 4 ) // Если уровень учебы меньше 4
                    Tutorial.StateTutorial = 3; // Устанавливаем уровень учебы на 3

            }


            yield return new WaitForFixedUpdate(); // Ждем до следующего фиксированного обновления
        }
    }
}