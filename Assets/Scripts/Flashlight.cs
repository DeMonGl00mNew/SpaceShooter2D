// Скрипт для фонарика
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Rigidbody2D rb; // переменная для Rigidbody2D
    public GameObject EffectSelected; // префаб для эффекта

    private void Start() // метод, вызываемый при старте
    {
        rb = GetComponent<Rigidbody2D>(); // получаем компонент Rigidbody2D
        rb.angularVelocity = Random.Range(-2f, 2f) * 100f; // задаем случайное вращение
    }

    private void OnTriggerEnter2D(Collider2D collision) // метод при входе в триггер
    {
        if (collision.gameObject.TryGetComponent(out Player player)) // если объект столкновения имеет компонент Player
        {
            //foreach (var item in Spawner.Instance.FriendsOnScene)
            //{
            //    if (item.WithEffect == false)
            //    {
            //        Instantiate(EffectSelected, item.transform);
            //        item.WithEffect = true;
            //    }
            //}

            Destroy(gameObject); // уничтожаем объект фонарика
        }
    }
}