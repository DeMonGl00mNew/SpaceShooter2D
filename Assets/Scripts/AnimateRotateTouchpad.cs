// Класс для анимации вращения элемента управления (touchpad)
public class AnimateRotateTouchpad : MonoBehaviour
{
    // Переменная для хранения Tween анимации вращения
    Tween tweenRotate;

    // Start вызывается перед первым кадром
    void Start()
    {
        // Подписка на событие, которое будет вызывать метод Rotate при изменении состояния touchpad
        Player.StateTouchpadTutorialEvent += Rotate;

        // Инициализация анимации вращения: вращение вокруг оси Z на 1 градус за 30 секунд
        tweenRotate = transform.DORotate(new Vector3(0, 0, 1), 30)
            .SetSpeedBased() // Устанавливаем скорость анимации в зависимости от времени
            .SetEase(Ease.Linear) // Устанавливаем линейную интерполяцию
            .SetLoops(-1, LoopType.Incremental) // Зацикливаем анимацию с инкрементальным увеличением
            .Pause(); // Ставим анимацию на паузу, чтобы она не начиналась сразу
    }

    // OnDisable вызывается при отключении объекта
    private void OnDisable()
    {
        // Отписка от события, чтобы избежать утечек памяти
        Player.StateTouchpadTutorialEvent -= Rotate;
    }

    // Метод для обработки вращения в зависимости от значения rotateValue
    private void Rotate(float rotateValue)
    {
        // Проверяем, существует ли tweenRotate
        if (tweenRotate == null)
            return;

        // Если значение rotateValue в пределах от 0 до 0.8, запускаем анимацию
        if (rotateValue < 0.8f && rotateValue > 0)
            tweenRotate.Play();
        else
            // В противном случае ставим анимацию на паузу
            tweenRotate.Pause();
    }
}