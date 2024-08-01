// ������ ��� �������� �����

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBomb : MonoBehaviour
{
    Rigidbody2D rb; // ���������� ��� �������� ���������� Rigidbody2D

    private void Start()
    {
        rb = GetComponent<Rigidbody2D(); // �������� ��������� Rigidbody2D ��� ������
        rb.angularVelocity = Random.Range(-2f, 2f)*100f; // ������������� ��������� �������� �����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player)) // ���� ������ ���������� � �������
        {
            Spawner.Instance.DestroyAllMeteorite(); // �������� ����� ����������� ���� ���������� �� ������ Spawner
            Destroy(gameObject); // ���������� ������
        }
    }
}