// Класс для задания анимации с помощью библиотеки DOTween
// Подключение необходимых библиотек
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoploTween : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Tweener tweener;
    public Ease ease; // Параметр для задания типа анимации
    public Vector3 punch = new Vector3(0.1f, 0.1f, 0.1f); // Параметр для задания перемещения объекта
    public float duration= 0.5f; // Продолжительность анимации
    public int vibrato=1; // Количество "вибраций"
    public float elasticity = 0.5f; // Эластичность анимации

    // Функция вызывается при старте объекта
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer();
        spriteRenderer.DOColor(new Color(0.5424528f, 0.5992324f, 1, 1), 0.5f)
            .SetLoops(-1, LoopType.Yoyo) // Установка повторения анимации
            .SetEase(ease); // Установка типа анимации
        spriteRenderer.DOFade(0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo); // Анимация затухания спрайта
        transform.DOPunchScale(punch, duration, vibrato, elasticity).SetLoops(-1, LoopType.Restart); // Анимация изменения размера объекта
    }

    // Функция вызывается каждый кадр
    void Update()
    {
        
    }
}