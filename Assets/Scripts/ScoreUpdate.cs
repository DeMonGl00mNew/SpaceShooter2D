// ��������� ����������� ����������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// ��������� ����� ScoreUpdate
public class ScoreUpdate : MonoBehaviour
{
    // ��������� ���������� ��� ������ � ������
    private TMP_Text ScoreText;

    // �������, ���������� ��� ������� ����
    private void Start()
    {
        // �������� ��������� TMP_Text � �������� �������
        ScoreText = GetComponent<TMP_Text>();
        // ����������� ������� UpdateTextScore �� ������� ��������� �����
        Score.StateScoreEvent += UpdateTextScore;
    }

    // �������, ���������� ��� ���������� �������
    private void OnDisable()
    {
        // ���������� ������� UpdateTextScore �� ������� ��������� �����
        Score.StateScoreEvent -= UpdateTextScore;
    }

    // ������� ���������� ������ � ������
    public void UpdateTextScore()
    {
        // ��������� ����� � ������� ����������� �����
        ScoreText.text = Score.StateScore.ToString();
    }
}