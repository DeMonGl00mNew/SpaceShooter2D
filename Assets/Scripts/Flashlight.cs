// ������ ��� ��������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Rigidbody2D rb; // ���������� ��� Rigidbody2D
    public GameObject EffectSelected; // ������ ��� �������

    private void Start() // �����, ���������� ��� ������
    {
        rb = GetComponent<Rigidbody2D>(); // �������� ��������� Rigidbody2D
        rb.angularVelocity = Random.Range(-2f, 2f) * 100f; // ������ ��������� ��������
    }

    private void OnTriggerEnter2D(Collider2D collision) // ����� ��� ����� � �������
    {
        if (collision.gameObject.TryGetComponent(out Player player)) // ���� ������ ������������ ����� ��������� Player
        {
            //foreach (var item in Spawner.Instance.FriendsOnScene)
            //{
            //    if (item.WithEffect == false)
            //    {
            //        Instantiate(EffectSelected, item.transform);
            //        item.WithEffect = true;
            //    }
            //}

            Destroy(gameObject); // ���������� ������ ��������
        }
    }
}