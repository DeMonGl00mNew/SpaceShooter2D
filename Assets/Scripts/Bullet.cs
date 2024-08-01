using UnityEngine;

public class Bullet : MonoBehaviour
{
    // �������� �������� ����. �������� ����� �������� � ����������.
    public float Speed = 1;

    private void Update()
    {
        // ���������� ���� ����� �� ������ �������� �������� � �������, ���������� � ���������� �����.
        // Vector3.up ��������� ����������� �������� (�����).
        transform.Translate(Vector3.up * Speed * Time.deltaTime);
    }

    // ���� ����� ����������, ����� ������ ���������� ��������� ��� ������.
    void OnBecameInvisible()
    {
        // ���������� ������ ����, ����� �������� ������ ������ � ������ �������� �� �������.
        Destroy(gameObject);
    }
}