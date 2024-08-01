// Класс, отвечающий за поведение метеоритов в игре
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMeteorit : MonoBehaviour
{
    static private int countTutorialMeteorite=1; // Статическая переменная для отслеживания количества обучающих метеоритов
    private Rigidbody2D rb; // Компонент Rigidbody для управления физикой объекта
    private float speed = 1f; // Скорость перемещения метеорита
    [HideInInspector] public bool Zagrugen = false; // Флаг для указания, что метеорит заряжен
    [HideInInspector] public bool NaStancii = false; // Флаг для указания, что метеорит на станции

    void Start()
    {
        Vector3 direction = -(transform.position - Vector3.zero).normalized; // Определение направления движения метеорита к центру
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = Random.Range(-2f, 2f); // Установка случайной угловой скорости
        rb.velocity = direction * speed; // Установка скорости движения
    }

    // Обработка столкновений метеорита с другими объектами
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Zagrugen || NaStancii)
            return;

        if (other.TryGetComponent(out Bullet bullet)) // Проверка на столкновение с пулей
        {
            Destroy(other.gameObject); // Уничтожение пули
            WhenDestroy(true); // Вызов метода при уничтожении
        }
        else if (other.TryGetComponent(out Player player)) // Проверка на столкновение с игроком
        {
            if (!player.neuyzvimost)
            {
                player.NeuyzvimostFunc(); // Вызов метода для игрока
            }
            WhenDestroy(); // Вызов метода при уничтожении
        }
        else if (other.TryGetComponent(out SpaceStation spaceStation)) // Проверка на столкновение со станцией
        {
            spaceStation.Heartint(25); // Вызов метода увеличения жизни станции
            WhenDestroy(); // Вызов метода при уничтожении
        }
    }

    // Метод для обработки уничтожения метеорита
    public void WhenDestroy(bool isBulletCollision=false)
    {
        if (gameObject.tag != "Friend")
            Spawner.KolvoMeteoritov -= 1;
        else
            Spawner.Friends -= 1;

        Instantiate(SingletoneResourses.Instance.explosion, transform.position, Quaternion.identity); // Создание взрыва

        Destroy(gameObject); // Уничтожение объекта

        // Дополнительная логика для обучающих метеоритов
    }
}