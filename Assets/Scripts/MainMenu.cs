// �������� ����� �������� ����
using System.Collections; // ����������� ������������ ���� ��� ������ � �����������
using System.Collections.Generic; // ����������� ������������ ���� ��� ������ � ����������� �����������
using UnityEngine; // ����������� Unity API
using UnityEngine.SceneManagement; // ����������� ������������ ���� ��� ���������� �������

public class MainMenu : MonoBehaviour // ���������� ������ �������� ����
{
    public void Scene() // ����� ��� �������� ������� �����
    {
        if(TutorialEnabled.TutorialOn == 0) // ���� ���������� ���� ���������� ��������
            SceneManager.LoadScene("Game"); // ��������� ������� �����
        else
            SceneManager.LoadScene("Tutorial"); // ����� ��������� ����� ��������
    }

    public void Exit() // ����� ��� ������ �� ����������
    {
        Debug.Log("Application.Quit()"); // ����� ��������� � �������
        Application.Quit(); // ����� �� ����������
    }

    public void BackToMenu() // ����� ��� ����������� � ������� ����
    {
        SingletoneResourses.Instance.Reset(); // ����� �������� ����� ��������
        SceneManager.LoadScene(0); // �������� ����� � �������� 0 (������� ����)
    }
}