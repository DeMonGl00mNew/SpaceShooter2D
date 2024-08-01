// Основной класс главного меню
using System.Collections; // Подключение пространства имен для работы с коллекциями
using System.Collections.Generic; // Подключение пространства имен для работы с обобщенными коллекциями
using UnityEngine; // Подключение Unity API
using UnityEngine.SceneManagement; // Подключение пространства имен для управления сценами

public class MainMenu : MonoBehaviour // Объявление класса главного меню
{
    public void Scene() // Метод для загрузки игровой сцены
    {
        if(TutorialEnabled.TutorialOn == 0) // Если установлен флаг отключения обучения
            SceneManager.LoadScene("Game"); // Загрузить игровую сцену
        else
            SceneManager.LoadScene("Tutorial"); // Иначе загрузить сцену обучения
    }

    public void Exit() // Метод для выхода из приложения
    {
        Debug.Log("Application.Quit()"); // Вывод сообщения в консоль
        Application.Quit(); // Выход из приложения
    }

    public void BackToMenu() // Метод для возвращения в главное меню
    {
        SingletoneResourses.Instance.Reset(); // Сброс ресурсов через синглтон
        SceneManager.LoadScene(0); // Загрузка сцены с индексом 0 (главное меню)
    }
}