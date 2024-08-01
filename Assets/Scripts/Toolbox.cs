// Класс для объекта "Toolbox"
public class Toolbox : MonoBehaviour
{
    Rigidbody2D rb; // Объявление переменной типа Rigidbody2D

    // Вызывается при запуске приложения
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент Rigidbody2D у объекта
        rb.angularVelocity = Random.Range(-2f, 2f) * 100f; // Задаем случайную угловую скорость
    }

    // Вызывается при столкновении с другим коллайдером
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, есть ли у столкнувшегося объекта компонент Player
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            FindObjectOfType<SpaceStation>().Healing(100000); // Вызываем метод Healing у объекта SpaceStation
            Destroy(gameObject); // Уничтожаем текущий объект
        }
    }
}