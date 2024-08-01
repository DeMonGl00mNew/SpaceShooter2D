// ����� SceneColider �������� �� ��������� �������� BoxCollider2D ����� �� ������ �������� ������
// � ������������� �������� ���������� ������� � ����� �����������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneColider : MonoBehaviour
{
    static public SceneColider Instance { get; private set; } // ����������� �������� ��� ������� � ������������� ���������� ������
    BoxCollider2D collider; // ���� ��� �������� BoxCollider2D

    private void Awake()
    {
        if (Instance == null) // ���� ��������� ������ ��� �� ��� ������
        {
            Instance = this; // ������������� ������� ������ ��� ������������ ���������
        }
        else if (Instance != this) // ���� ��� ���� ��������� ������
        {
            Destroy(gameObject); // ���������� ������� ������
        }
    }

    void Start()
    {       
        collider = GetComponent<BoxCollider2D(); // �������� ��������� BoxCollider2D
        collider.size = SizeScreen(); // ������������� ������� BoxCollider2D �� ������ �������� ������
    }

    public Vector2 SizeScreen()
    {
        // ������������ ��������� ��� ����������� ����������� �� ������ �������
        float ratio = (float)Screen.width / Screen.height;
        float camHeight = 2.0f * Camera.main.orthographicSize;
        float camWidth = ratio * camHeight;
        return new Vector2(camWidth, camHeight); // ���������� ������� ������
    }
}