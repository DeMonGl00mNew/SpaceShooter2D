// Класс для управления интерфейсом башни
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfaceForTower : MonoBehaviour
{

    public TMP_Text CostsText; // Текстовое поле для отображения стоимости
    public GameObject CloseTower; // Игровой объект для закрытия башни

    // Метод вызывается при старте игры
    private void Start()
    {
        // Подписываемся на событие обновления стоимости покупки башни
        ClickToTower.StateCountForAccessBuyEvent += UpdateCost;
        // Показываем стоимость
        VisibleCost();
    }

    // Метод вызывается при уничтожении объекта
    private void OnDestroy()
    {
        // Отписываемся от события обновления стоимости
        ClickToTower.StateCountForAccessBuyEvent -= UpdateCost;
    }

    // Метод для обновления стоимости
    private void UpdateCost()
    {
        if(CostsText.gameObject.activeSelf)
            CostsText.text = ClickToTower.StateCountForAccessBuy.ToString();
    }

    // Метод для отображения стоимости
    public void VisibleCost()
    {
        if (CostsText.gameObject.activeSelf)
            return;
        CostsText.gameObject.SetActive(true);
        CostsText.text = ClickToTower.StateCountForAccessBuy.ToString();
    }

    // Метод для отображения кнопки закрытия башни
    public void VisibleClose()
    {
        if (CloseTower.activeSelf)
            return;
        CloseTower.gameObject.SetActive(true);
    }

    // Метод для закрытия башни
    public void CloseTurret(GameObject turret)
    {
        if (turret.activeSelf)
        {
            turret.SetActive(false);
            CloseTower.SetActive(false);
            ClickToTower.StateCountForAccessBuy -= 2;
            VisibleCost();
        }
    }

    // Метод для установки башни на пустую платформу
    public void ClickToEmptyPlatform(GameObject currentTower)
    {
        if (Score.StateScore >= ClickToTower.StateCountForAccessBuy && !currentTower.activeSelf)
        {
            currentTower.SetActive(true);
            VisibleClose();
            CostsText.gameObject.SetActive(false);
            ClickToTower.StateCountForAccessBuy += 2;
        }
    }
}