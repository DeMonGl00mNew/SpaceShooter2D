// ����� Tutorial �������� �� �������� ������ � �������� ������ ��������� ��������� ��������
// �� ���������� ������� StateTutorialEvent ��� ���������� � ��������� ��������� ��������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tutorial : MonoBehaviour
{
    // ����������� �������, ������� ���������� ��� ��������� ��������� ��������
    public static event Action StateTutorialEvent;
    
    // ����������� ���������� ��� �������� �������� ��������� ��������
    static private int _stateTutorial = 0;

    // �������� ��� ������� � �������� ��������� ��������
    static public int StateTutorial 
    { 
        get { return _stateTutorial; } 
        set { _stateTutorial = value; StateTutorialEvent?.Invoke(); } 
    }

    // ������ �� ������� � �����, ������� ����� ����������� � ���� ��������
    public GameObject Magnit;
    public GameObject Fire;
    public GameObject Gamepad;

    private void Start()
    {
        // ������������� �� ������� ��������� ��������� ��������
        Tutorial.StateTutorialEvent += ListOfActionTutrorial;
    }

    private void OnDisable()
    {
        // ������������ �� ������� ��� ���������� �������
        Tutorial.StateTutorialEvent -= ListOfActionTutrorial;
    }

    private void Update()
    {
        // ������� ������� ��������� �������� � ������� ��� �������
        Debug.Log("StateTutorial= " + StateTutorial);
    }

    // �����, ������� ��������� ����������� �������� � ����������� �� ��������� ��������
    private void ListOfActionTutrorial()
    {
        // �������� ������ ��� ���������� ������������� ���������
        if (StateTutorial == 4)
        {
            Magnit.SetActive(true);
        }
        
        // �������� ����� ��� ���������� ������� ���������
        if (StateTutorial == 6)
        {
            Fire.SetActive(true);
        }

        // ������ ������, ����� � ������� ��� ���������� ��������
        if (StateTutorial == 8)
        {
            Magnit.SetActive(false);
            Fire.SetActive(false);
            Gamepad.SetActive(false);
        }
    }
}