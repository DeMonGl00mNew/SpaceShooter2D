// Класс для управления ресурсами в игре, используя паттерн Singleton
// Singleton позволяет иметь только один экземпляр класса

using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletoneResourses : MonoBehaviour
{
    public static SingletoneResourses Instance; // Статическая переменная для доступа к экземпляру класса
    public GameObject explosion; // Объект взрыва
    public SoundsManager SoundsManager; // Менеджер звуков

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Если экземпляр еще не создан, создаем его
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Если экземпляр уже существует, уничтожаем текущий объект
        }
    }

    // Метод для сброса игровых параметров
    public void Reset()
    {
        Spawner.scene = SceneManager.GetActiveScene(); // Получаем активную сцену
        Scanner.isEmpty = true; // Устанавливаем, что сканер пуст
        Spawner.KolvoMeteoritov = 0; // Сбрасываем количество метеоритов
        Spawner.Friends = 0; // Сбрасываем количество друзей
        Tutorial.StateTutorial = 0; // Сбрасываем состояние обучения
    }
}