// ����� TutorialEnabled ��� ���������� ������������ �������� ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnabled : MonoBehaviour
{
    static public int TutorialOn = 1; // ����������� ���������� ��� ����������� ��������� �������� ������

    private void Start()
    {
        // ��������� ������� �������� TutorialOnPrefs � ����������� ������ ������
        if (!PlayerPrefs.HasKey("TutorialOnPrefs"))
        {
            TutorialOn = 1; // ������������� �������� TutorialOn �� ���������
        }
        else
        {
            TutorialOn = PlayerPrefs.GetInt("TutorialOnPrefs"); // ����� �������� �������� �� ����������� ������
        }

        // ������������� �� ������� ������ �������� ������, ���� ����� Tutorial
        if (Spawner.scene.name == "Tutorial")
            Tutorial.StateTutorialEvent += NecessaryToTutorial;
    }

    private void OnDestroy()
    {
        // ������������ �� ������� ������ �������� ������ ��� ����������� �������
        if (Spawner.scene.name == "Tutorial")
            Tutorial.StateTutorialEvent -= NecessaryToTutorial;
    }

    public void NecessaryToTutorial()
    {
        // � ���� ������ ����� �������� ������ ����������� ������� ��������� �� ������
        // ��������, �� ��������� �������� ��������� �������� ������ Tutorial.StateTutorial
        // ��� ��������� ���������� � ��������� ������� ������ � PlayerPrefs
        // PlayerPrefs.SetInt("TutorialOnPrefs", 0);
    }
}
