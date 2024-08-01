// Скрипт для поднятия бомбы

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBomb : MonoBehaviour
{
    Rigidbody2D rb; // переменная для хранения компонента Rigidbody2D

    private void Start()
    {
        rb = GetComponent<Rigidbody2D(); // получаем компонент Rigidbody2D при старте
        rb.angularVelocity = Random.Range(-2f, 2f)*100f; // устанавливаем случайное вращение бомбы
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player)) // если объект столкнулся с игроком
        {
            Spawner.Instance.DestroyAllMeteorite(); // вызываем метод уничтожения всех метеоритов из класса Spawner
            Destroy(gameObject); // уничтожаем объект
        }
    }
}