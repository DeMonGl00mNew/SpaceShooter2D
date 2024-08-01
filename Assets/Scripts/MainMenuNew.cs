// Класс MainMenuNew отвечает за основное меню игры
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNew : MonoBehaviour
{
    public GameObject OptionsMenu; // Ссылка на объект меню опций

    // Метод запуска игры
    public void PlayGame()
    {
        SceneManager.LoadScene("Game"); // Загружаем сцену "Game"
    }

    // Метод выхода из игры
    public void ExitGame()
    {
        Application.Quit(); // Завершаем приложение
    }

    // Метод отображения меню опций
    public void OnPauseMenuClick()
    {
        OptionsMenu.SetActive(true); // Включаем меню опций
    }

    // Метод скрытия меню опций
    public void OffPauseMenuClick()
    {
        OptionsMenu.SetActive(false); // Выключаем меню опций
    }
}