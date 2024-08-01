// ����� Score �������� �� �������� � ���������� ����� ����
// �� �������� ����������� ������� StateScoreEvent, ������� ����������� ��� ��������� �����
// ��� ���� �������� � ��������� ���������� _stateScore � �������� ����� ��������� ����������� �������� StateScore

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [HideInInspector]public static event Action StateScoreEvent; // ������� ��� ������������ ��������� �����
    static private int _stateScore; // ���������� ��� �������� �����

    static public int StateScore
    {
        get { return _stateScore; } // ��������� �������� �����
        set { _stateScore = value; StateScoreEvent?.Invoke(); } // ��������� ������ �������� ����� � ����� �������
    }

    private void Awake()
    {
        StateScore = 100; // ��������� ���������� �������� ����� ��� ������� ����
    }
}
