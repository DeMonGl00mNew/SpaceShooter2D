// Этот скрипт отвечает за поведение первого метеорита в учебном уровне

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TutorialFirstMeteorite : MonoBehaviour
{
    // Метод вызывается при уничтожении объекта
    private void OnDestroy()
    {
        // Проверяем, находимся ли мы в третьем шаге учебного уровня
        if (Tutorial.StateTutorial == 3)
            // Загружаем уровень учебного режима
            SceneManager.LoadScene("Tutorial");
    }

    void Start()
    {
        // Устанавливаем слой объекта как "Tutorial"
        gameObject.layer = LayerMask.NameToLayer("Tutorial");

        // Получаем половину размера экрана
        Vector2 sizeScreen = SceneColider.Instance.SizeScreen() * 0.5f;

        // Перемещаем объект на указанную позицию с использованием анимации
        transform.DOMove(new Vector3(sizeScreen.x - 2, 0, 0), 2).SetSpeedBased().SetEase(Ease.Linear).OnComplete(OnComplete);
    }

    // Метод вызывается по завершении анимации
    private void OnComplete()
    {
        // Устанавливаем состояние учебного уровня как 2
        Tutorial.StateTutorial = 2;
    }
}