// ������ ��� ������ � ���� �����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnPauseMenu : MonoBehaviour
{
    // ������ ���� �����
    public GameObject PauseMenu;

    // ����� ��� ����������� ���� �����
    public void OnPauseMenuClick()
    {
        // �������� ���� �����
        PauseMenu.SetActive(true);

        // ������������� ����� � ����
        Time.timeScale = 0;
    }

    // ����� ��� �������� �������� ����
    public void LoadStartMenu()
    {
        // ��������� ����� �������� ����
        SceneManager.LoadScene("StartMenu");
    }
}