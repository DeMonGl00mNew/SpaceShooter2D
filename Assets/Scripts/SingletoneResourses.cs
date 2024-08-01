// ����� ��� ���������� ��������� � ����, ��������� ������� Singleton
// Singleton ��������� ����� ������ ���� ��������� ������

using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletoneResourses : MonoBehaviour
{
    public static SingletoneResourses Instance; // ����������� ���������� ��� ������� � ���������� ������
    public GameObject explosion; // ������ ������
    public SoundsManager SoundsManager; // �������� ������

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // ���� ��������� ��� �� ������, ������� ���
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // ���� ��������� ��� ����������, ���������� ������� ������
        }
    }

    // ����� ��� ������ ������� ����������
    public void Reset()
    {
        Spawner.scene = SceneManager.GetActiveScene(); // �������� �������� �����
        Scanner.isEmpty = true; // �������������, ��� ������ ����
        Spawner.KolvoMeteoritov = 0; // ���������� ���������� ����������
        Spawner.Friends = 0; // ���������� ���������� ������
        Tutorial.StateTutorial = 0; // ���������� ��������� ��������
    }
}