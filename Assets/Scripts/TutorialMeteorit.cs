// �����, ���������� �� ��������� ���������� � ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMeteorit : MonoBehaviour
{
    static private int countTutorialMeteorite=1; // ����������� ���������� ��� ������������ ���������� ��������� ����������
    private Rigidbody2D rb; // ��������� Rigidbody ��� ���������� ������� �������
    private float speed = 1f; // �������� ����������� ���������
    [HideInInspector] public bool Zagrugen = false; // ���� ��� ��������, ��� �������� �������
    [HideInInspector] public bool NaStancii = false; // ���� ��� ��������, ��� �������� �� �������

    void Start()
    {
        Vector3 direction = -(transform.position - Vector3.zero).normalized; // ����������� ����������� �������� ��������� � ������
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = Random.Range(-2f, 2f); // ��������� ��������� ������� ��������
        rb.velocity = direction * speed; // ��������� �������� ��������
    }

    // ��������� ������������ ��������� � ������� ���������
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Zagrugen || NaStancii)
            return;

        if (other.TryGetComponent(out Bullet bullet)) // �������� �� ������������ � �����
        {
            Destroy(other.gameObject); // ����������� ����
            WhenDestroy(true); // ����� ������ ��� �����������
        }
        else if (other.TryGetComponent(out Player player)) // �������� �� ������������ � �������
        {
            if (!player.neuyzvimost)
            {
                player.NeuyzvimostFunc(); // ����� ������ ��� ������
            }
            WhenDestroy(); // ����� ������ ��� �����������
        }
        else if (other.TryGetComponent(out SpaceStation spaceStation)) // �������� �� ������������ �� ��������
        {
            spaceStation.Heartint(25); // ����� ������ ���������� ����� �������
            WhenDestroy(); // ����� ������ ��� �����������
        }
    }

    // ����� ��� ��������� ����������� ���������
    public void WhenDestroy(bool isBulletCollision=false)
    {
        if (gameObject.tag != "Friend")
            Spawner.KolvoMeteoritov -= 1;
        else
            Spawner.Friends -= 1;

        Instantiate(SingletoneResourses.Instance.explosion, transform.position, Quaternion.identity); // �������� ������

        Destroy(gameObject); // ����������� �������

        // �������������� ������ ��� ��������� ����������
    }
}