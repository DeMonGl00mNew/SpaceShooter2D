// �����������: ���� ������ �������� �� �������� ������� ������ �� ����� �������� ������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMoveToRight : MonoBehaviour
{
    public float DebugDIstance = 0; // ������ ���������� ��� ����������� ����������
    [SerializeField] private Rigidbody2D _rigidbody2D; // ������ �� ��������� Rigidbody2D
    public ContactFilter2D contactFilter2d; // ������ ��������� ��� ������������� � �������� ������������

    private readonly RaycastHit2D[] resultRaycast = new RaycastHit2D[1]; // ������ ����������� �������� �����

    private void Start()
    {
        StartCoroutine(CastForRightCouratine()); // ������ �������� ��� �������� ������������ ������
    }
    private void FixedUpdate()
    {

    }
    IEnumerator CastForRightCouratine()
    {
        int collisionCount = 0; // ������� ������������
        while (true)
        {
            if (Tutorial.StateTutorial == 5) // ���� ������� ����� ����� 5, ��������� ��������
                yield break;
            collisionCount = _rigidbody2D.Cast(transform.up, contactFilter2d, resultRaycast, 10); // ��������� ������������ ������� �������
            if (collisionCount != 0)
            {

                if (Tutorial.StateTutorial < 4 ) // ���� ������� ����� ������ 4
                    Tutorial.StateTutorial = 3; // ������������� ������� ����� �� 3

            }


            yield return new WaitForFixedUpdate(); // ���� �� ���������� �������������� ����������
        }
    }
}