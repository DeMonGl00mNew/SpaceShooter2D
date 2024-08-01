// ����� TutorialStick �������� �� ����������� ��������� � ���� �����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // ������ ���������� ��� ������ � ����������
using UnityEngine.UI;
using System;

public class TutorialStick : MonoBehaviour
{
    public Transform FakeStick; // ������ ��� ����������� "���������" �����
    public Image Stick; // ����������� �������� �����
    Sequence s; // ������������������ ��� �������� ���������

    void Start()
    {
        Tutorial.StateTutorialEvent += MoveFakeStick; // ����������� ����� MoveFakeStick �� ������� StateTutorialEvent

        //Stick.color = Vector4.Scale(Stick.color, new Color(1, 1, 1, 0)); 
        // ��������� ����� ����� �� ���������� (����������������, �������� �� ������������)
    }

    private void OnDisable()
    {
        Tutorial.StateTutorialEvent -= MoveFakeStick; // ���������� ����� MoveFakeStick �� ������� StateTutorialEvent
    }

    private void MoveFakeStick()
    {
        s = DOTween.Sequence(); // ������� ����� ������������������ �������� � ������� DOTween
        if (Tutorial.StateTutorial == 2) // ���� ������� ��������� ��������� ����� 2
        {
            s.Append(FakeStick.DOLocalMoveY(-130, 2)); // ������� "���������" ����� ���� �� 2 �������
            s.AppendInterval(0.25f); // ������ ����� �� 0.25 �������
            s.SetLoops(-1, LoopType.Restart); // ������������� ������������ ��������
        }
        if (Tutorial.StateTutorial == 3 && s != null) // ���� ������� ��������� ��������� ����� 3 � ������������������ �������� ����������
        {
            s.Kill(); // ��������� ��������
            FakeStick.gameObject.SetActive(false); // �������� "���������" �����
        }
    }
}