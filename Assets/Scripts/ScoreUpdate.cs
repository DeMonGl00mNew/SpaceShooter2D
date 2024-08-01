// Объявляем необходимые библиотеки
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Объявляем класс ScoreUpdate
public class ScoreUpdate : MonoBehaviour
{
    // Объявляем переменную для текста с очками
    private TMP_Text ScoreText;

    // Функция, вызываемая при запуске игры
    private void Start()
    {
        // Получаем компонент TMP_Text у текущего объекта
        ScoreText = GetComponent<TMP_Text>();
        // Подписываем функцию UpdateTextScore на событие изменения очков
        Score.StateScoreEvent += UpdateTextScore;
    }

    // Функция, вызываемая при отключении объекта
    private void OnDisable()
    {
        // Отписываем функцию UpdateTextScore от события изменения очков
        Score.StateScoreEvent -= UpdateTextScore;
    }

    // Функция обновления текста с очками
    public void UpdateTextScore()
    {
        // Обновляем текст с текущим количеством очков
        ScoreText.text = Score.StateScore.ToString();
    }
}