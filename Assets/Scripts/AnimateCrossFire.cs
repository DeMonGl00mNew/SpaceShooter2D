// Импортируем библиотеку DOTween для использования анимации
// Импортируем пространство имен Unity
using DG.Tweening;
using UnityEngine;

// Определяем класс AnimateCrossFire, который наследует от MonoBehaviour
public class AnimateCrossFire : MonoBehaviour 
{
    Tweener tweenRotate; // Объявляем переменную для хранения Tween-объекта (анимации вращения)
    float rotAngle = 0; // Переменная для хранения угла вращения, инициализируем нулем

    void Start() // Метод, вызываемый при старте скрипта
    {
        rotAngle -= 90; // Уменьшаем угол вращения на 90 градусов (начальное значение для вращения)
    }

    public void Rotate() // Публичный метод для вызова вращения объекта
    {
        // Вращаем объект с помощью DOTween
        // DORotate принимает вектор, определяющий конечное положение (в данном случае - rotAngle % 360)
        // 0.4f - продолжительность анимации в секундах
        // SetLoops(1, LoopType.Incremental) - устанавливаем количество повторений (1) и тип повторения (инкрементальный)
        transform.DORotate(new Vector3(0, 0, rotAngle % 360), 0.4f).SetLoops(1, LoopType.Incremental);

        rotAngle -= 90; // Уменьшаем угол вращения на 90 градусов для следующего вращения
    }
}