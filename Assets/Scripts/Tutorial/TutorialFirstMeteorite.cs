// ���� ������ �������� �� ��������� ������� ��������� � ������� ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TutorialFirstMeteorite : MonoBehaviour
{
    // ����� ���������� ��� ����������� �������
    private void OnDestroy()
    {
        // ���������, ��������� �� �� � ������� ���� �������� ������
        if (Tutorial.StateTutorial == 3)
            // ��������� ������� �������� ������
            SceneManager.LoadScene("Tutorial");
    }

    void Start()
    {
        // ������������� ���� ������� ��� "Tutorial"
        gameObject.layer = LayerMask.NameToLayer("Tutorial");

        // �������� �������� ������� ������
        Vector2 sizeScreen = SceneColider.Instance.SizeScreen() * 0.5f;

        // ���������� ������ �� ��������� ������� � �������������� ��������
        transform.DOMove(new Vector3(sizeScreen.x - 2, 0, 0), 2).SetSpeedBased().SetEase(Ease.Linear).OnComplete(OnComplete);
    }

    // ����� ���������� �� ���������� ��������
    private void OnComplete()
    {
        // ������������� ��������� �������� ������ ��� 2
        Tutorial.StateTutorial = 2;
    }
}