// Импорт библиотек
using UnityEngine;
using DG.Tweening;

// Объявление класса AnimateMagnit, наследуемого от MonoBehaviour
public class AnimateMagnit : MonoBehaviour
{
    // Переменные для настройки анимации
    public Ease ease; // тип анимации
    public Vector3 punch = new Vector3(0.1f, 0.1f, 0.1f); // величина эффекта
    public float duration = 0.5f; // продолжительность анимации
    public int vibrato = 1; // вибрация
    public float elasticity = 0.5f; // эластичность
    private Tweener tweener=null; // анимационный объект
    
    // Метод Start вызывается при запуске сцены
    void Start()
    {
        // Создание анимации и задание ее параметров
        tweener = transform.DOPunchScale(punch, duration, vibrato, elasticity).SetAutoKill(false).Pause();
        // Вызов метода Animate() для проигрывания анимации
        Animate();
    }

    // Публичный метод для анимации
    public void Animate()
    {
        // Проверка, что анимация не проигрывается в данный момент
        if (!tweener.IsPlaying())
            // Проигрывание анимации с возможностью перезапуска
            tweener.Play().Restart(true);
    }
}
