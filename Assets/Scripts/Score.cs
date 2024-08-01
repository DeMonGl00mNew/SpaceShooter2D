//  ласс Score отвечает за хранение и обновление счета игры
// ќн содержит статическое событие StateScoreEvent, которое срабатывает при изменении счета
// —ам счет хранитс€ в приватной переменной _stateScore и доступен через публичное статическое свойство StateScore

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [HideInInspector]public static event Action StateScoreEvent; // —обытие дл€ отслеживани€ изменени€ счета
    static private int _stateScore; // ѕеременна€ дл€ хранени€ счета

    static public int StateScore
    {
        get { return _stateScore; } // ѕолучение текущего счета
        set { _stateScore = value; StateScoreEvent?.Invoke(); } // ”становка нового значени€ счета и вызов событи€
    }

    private void Awake()
    {
        StateScore = 100; // ”становка начального значени€ счета при запуске игры
    }
}
