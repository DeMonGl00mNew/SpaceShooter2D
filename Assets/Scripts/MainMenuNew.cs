// ����� MainMenuNew �������� �� �������� ���� ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNew : MonoBehaviour
{
    public GameObject OptionsMenu; // ������ �� ������ ���� �����

    // ����� ������� ����
    public void PlayGame()
    {
        SceneManager.LoadScene("Game"); // ��������� ����� "Game"
    }

    // ����� ������ �� ����
    public void ExitGame()
    {
        Application.Quit(); // ��������� ����������
    }

    // ����� ����������� ���� �����
    public void OnPauseMenuClick()
    {
        OptionsMenu.SetActive(true); // �������� ���� �����
    }

    // ����� ������� ���� �����
    public void OffPauseMenuClick()
    {
        OptionsMenu.SetActive(false); // ��������� ���� �����
    }
}