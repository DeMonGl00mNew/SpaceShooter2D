// Скрипт для работы с меню паузы
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnPauseMenu : MonoBehaviour
{
    // Объект меню паузы
    public GameObject PauseMenu;

    // Метод для отображения меню паузы
    public void OnPauseMenuClick()
    {
        // Включаем меню паузы
        PauseMenu.SetActive(true);

        // Останавливаем время в игре
        Time.timeScale = 0;
    }

    // Метод для загрузки главного меню
    public void LoadStartMenu()
    {
        // Загружаем сцену главного меню
        SceneManager.LoadScene("StartMenu");
    }
}