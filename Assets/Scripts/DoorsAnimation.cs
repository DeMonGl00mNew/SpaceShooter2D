// Класс для анимации дверей
public class DoorsAnimation : MonoBehaviour
{
    static public DoorsAnimation Instance { get; private set; } // Статическое свойство для доступа к экземпляру класса

    public GameObject[] Doors; // Массив объектов дверей
    Tween closeTween = null; // Анимация закрытия двери

    private void Awake()
    {
        if (Instance == null) // Если экземпляр не создан
        {
            Instance = this; // Создать экземпляр
        }
        else if (Instance != this) // Если экземпляр уже существует
        {
            Destroy(gameObject); // Уничтожить текущий объект
        }
    }

    public void MoveAnimation()
    {
        float PosX; // Позиция по X
        float ScaleX; // Масштаб по X

        if (!closeTween.IsActive()) // Если анимация закрытия не активна
        {
            foreach (var door in Doors) // Для каждой двери из массива
            {
                PosX = door.transform.localPosition.x; // Получить текущую позицию по X
                ScaleX = door.transform.localScale.x; // Получить текущий масштаб по X
                door.transform.DOLocalMoveX(PosX - Mathf.Sign(ScaleX) * 1.133f, 2); // Анимировать движение двери влево
                closeTween = door.transform.DOLocalMoveX(PosX, 2).SetDelay(3); // Закрыть дверь через 3 секунды
            }
        }
    }
}