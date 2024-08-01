// Класс для обработки нажатия на башню
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToTower : MonoBehaviour
{
    // Переменная для доступа к покупке башни
    public int countForAccessBuy=0;
    // Событие для изменения состояния доступа к покупке башни
    [HideInInspector] public static event Action StateCountForAccessBuyEvent;
    // Статическая переменная для хранения состояния доступа к покупке башни
    static private int _stateCountForAccessBuy;

    // Свойство для доступа к состоянию доступа к покупке башни
    static public int StateCountForAccessBuy
    {
        get { return _stateCountForAccessBuy; }
        set { _stateCountForAccessBuy = value; StateCountForAccessBuyEvent?.Invoke(); }
    }

    private void Start()
    {
        // Устанавливаем начальное значение состояния доступа к покупке башни
        StateCountForAccessBuy = countForAccessBuy;
    }

}
// При загрузке игры устанавливаем начальное значение состояния доступа к покупке башни.