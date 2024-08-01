// Класс TutorialEnabled для управления отображением учебного режима

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnabled : MonoBehaviour
{
    static public int TutorialOn = 1; // Статическая переменная для определения состояния учебного режима

    private void Start()
    {
        // Проверяем наличие значения TutorialOnPrefs в сохраненных данных игрока
        if (!PlayerPrefs.HasKey("TutorialOnPrefs"))
        {
            TutorialOn = 1; // Устанавливаем значение TutorialOn по умолчанию
        }
        else
        {
            TutorialOn = PlayerPrefs.GetInt("TutorialOnPrefs"); // Иначе получаем значение из сохраненных данных
        }

        // Подписываемся на событие начала учебного режима, если сцена Tutorial
        if (Spawner.scene.name == "Tutorial")
            Tutorial.StateTutorialEvent += NecessaryToTutorial;
    }

    private void OnDestroy()
    {
        // Отписываемся от события начала учебного режима при уничтожении объекта
        if (Spawner.scene.name == "Tutorial")
            Tutorial.StateTutorialEvent -= NecessaryToTutorial;
    }

    public void NecessaryToTutorial()
    {
        // В этом методе можно добавить логику отображения учебных подсказок на экране
        // Например, на основании текущего состояния учебного режима Tutorial.StateTutorial
        // Или сохранить информацию о пройденом учебном режиме в PlayerPrefs
        // PlayerPrefs.SetInt("TutorialOnPrefs", 0);
    }
}
