// ����� TutorialSpaceship �������� �� ���������� ����������� �������� � ������ ���������� ������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class TutorialSpaceship : MonoBehaviour
{
    public Transform Player; // ������ �� ������
    public SpriteRenderer spriteRenderer; // ��������� ����������� ������� ������������ �������
    public SpriteRenderer meteoSprite; // ��������� ����������� ������� ���������
    Sequence s ; // ������������������ ��������

    // ������������� �� ������� ���������� ������ ��� �������
    void Start()
    {
        Tutorial.StateTutorialEvent += MoveToMeteorite; // �������� � ���������
        Tutorial.StateTutorialEvent += MoveToStation; // �������� � �������
    }

    // ������������ �� ������� ��� ���������� �������
    private void OnDisable()
    {
        Tutorial.StateTutorialEvent -= MoveToMeteorite;
        Tutorial.StateTutorialEvent -= MoveToStation;
    }

    // �������� � ��������� � ����������� �� ��������� ���������� ������
    private void MoveToMeteorite()
    {
        if (Tutorial.StateTutorial == 2)
        {
            s = DOTween.Sequence();
            s.Append(transform.DORotate(new Vector3(0, 0, -90), 0.5f)); // ������� ������������ �������
            s.Append(transform.DOMove(new Vector3(5.5f, 0, 0), 1).SetSpeedBased().SetEase(Ease.Linear)); // �������� � ���������
            s.AppendInterval(0.5f);
            s.SetLoops(-1, LoopType.Restart); // ��������� �������� ����������
        }
        if (Tutorial.StateTutorial == 4)
        {
            s.Kill(); // �������� ���������� ��������
            spriteRenderer.enabled = false; // ������ ������ ������������ �������
        }
    }

    // �������� � ������� � ����������� �� ��������� ���������� ������
    private void MoveToStation()
    {
        if (Tutorial.StateTutorial == 5)
        {
            TutorialFirstMeteorite tutorialFirstMeteorite = Player.GetComponentInChildren<TutorialFirstMeteorite>(); // �������� ������ �� �������� �� ������
            
            if (tutorialFirstMeteorite)
            {
                meteoSprite.sprite = tutorialFirstMeteorite.gameObject.GetComponent<SpriteRenderer>().sprite; // ��������� ������ ��� �����������
            }
                
            spriteRenderer.enabled = true; // �������� ������ ������������ �������

            transform.position = Player.position; // ����������� ����������� ������� � ������
            transform.rotation = Player.rotation; // ��������� ����������� ������� � ������������ � �������
            s = DOTween.Sequence();
            Vector3 direction = Player.position - transform.position;
            s.Append(GoToPoint(new Vector3(0,0,0))); // ��������� ������� � ����� (0, 0, 0)
            s.Append(transform.DOMove(new Vector3(0, 0, 0), 1).SetSpeedBased().SetEase(Ease.Linear)); // �������� � ����� (0, 0, 0)
            s.AppendInterval(0.5f);
            s.SetLoops(-1, LoopType.Restart); // ��������� �������� ����������
        }
        if (Tutorial.StateTutorial == 6) 
        {
            SpriteRenderer[] spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
            s.Kill(); // �������� ���������� ��������
            foreach (var sr in spriteRenderer)
            {
                sr.enabled = false; // ������ ��� ������� ������������ �������
            }
        }
    }

    // �������� ����������� �������� � �����
    public Tweener GoToPoint(Vector3 targetPos)
    {
        var dir = targetPos - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var rot = Quaternion.AngleAxis(angle-90f, Vector3.forward);

        return transform.DORotate(rot.eulerAngles, 0.5f).SetEase(Ease.Linear).SetSpeedBased(); // �������� ������������ ������� � ������� �����
    }
}