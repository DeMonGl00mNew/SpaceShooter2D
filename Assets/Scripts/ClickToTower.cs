// ����� ��� ��������� ������� �� �����
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToTower : MonoBehaviour
{
    // ���������� ��� ������� � ������� �����
    public int countForAccessBuy=0;
    // ������� ��� ��������� ��������� ������� � ������� �����
    [HideInInspector] public static event Action StateCountForAccessBuyEvent;
    // ����������� ���������� ��� �������� ��������� ������� � ������� �����
    static private int _stateCountForAccessBuy;

    // �������� ��� ������� � ��������� ������� � ������� �����
    static public int StateCountForAccessBuy
    {
        get { return _stateCountForAccessBuy; }
        set { _stateCountForAccessBuy = value; StateCountForAccessBuyEvent?.Invoke(); }
    }

    private void Start()
    {
        // ������������� ��������� �������� ��������� ������� � ������� �����
        StateCountForAccessBuy = countForAccessBuy;
    }

}
// ��� �������� ���� ������������� ��������� �������� ��������� ������� � ������� �����.