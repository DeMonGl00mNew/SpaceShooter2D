// Класс SceneColider отвечает за установку размеров BoxCollider2D сцены на основе размеров экрана
// и предотвращает создание дубликатов объекта с таким компонентом
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneColider : MonoBehaviour
{
    static public SceneColider Instance { get; private set; } // Статическое свойство для доступа к единственному экземпляру класса
    BoxCollider2D collider; // Поле для хранения BoxCollider2D

    private void Awake()
    {
        if (Instance == null) // Если экземпляр класса еще не был создан
        {
            Instance = this; // Устанавливаем текущий объект как единственный экземпляр
        }
        else if (Instance != this) // Если уже есть экземпляр класса
        {
            Destroy(gameObject); // Уничтожаем текущий объект
        }
    }

    void Start()
    {       
        collider = GetComponent<BoxCollider2D(); // Получаем компонент BoxCollider2D
        collider.size = SizeScreen(); // Устанавливаем размеры BoxCollider2D на основе размеров экрана
    }

    public Vector2 SizeScreen()
    {
        // Рассчитываем пропорции для корректного отображения на разных экранах
        float ratio = (float)Screen.width / Screen.height;
        float camHeight = 2.0f * Camera.main.orthographicSize;
        float camWidth = ratio * camHeight;
        return new Vector2(camWidth, camHeight); // Возвращаем размеры экрана
    }
}