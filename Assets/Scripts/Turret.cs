// ����� Turret �������� �� �������� �� �����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Turret : MonoBehaviour
{
    public float speed = 1.0f; // �������� �������� �����
    public GameObject BulletTurret; // ������ ����
    public Transform FirePlace; // ������� ��������
    public POVTurret POVTurret; // ������ ��� ������ �����
    private Vector3 targetDirection;
    private bool isReloading = false;
    private Tweener rotTO = null; // Tween ��� �������� ��������
    private LayerMask layerMask = 1 << 6; // ������� ���� ��� ����������� ������

    private void OnEnable()
    {
        POVTurret.NewInPOV += TargetDetected; // ������������� �� ������� ����������� ����
        POVTurret.Targeting(); // ��������� ����� 
    }

    private void OnDisable()
    {
        POVTurret.NewInPOV -= TargetDetected; // ������������ �� ������� ����������� ����
        isReloading = false;
        StopAllCoroutines();
    }

    private void TargetDetected()
    {
        StartCoroutine(Targeting(speed)); // ��������� �������� �����������
    }

    IEnumerator Targeting(float speed)
    {
        while (true)
        {
            if (!POVTurret.EnemyTransform) // ���� ���� �� ����������
            {
                if (rotTO != null)
                {
                    rotTO.Kill(); // ��������� Tween ��������
                    rotTO = null;
                }
                yield break;
            }

            targetDirection = POVTurret.EnemyTransform.position - transform.position;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            float angleToFire = Vector3.Angle(targetDirection, transform.right);

            if (rotTO == null)
                rotTO = transform.DORotate(new Vector3(0, 0, angle), speed).SetSpeedBased().SetEase(Ease.Linear).SetAutoKill(false);
            else
            {
                rotTO.ChangeEndValue(new Vector3(0, 0, angle), true).Restart(); // ��������� ���� ��������
            }

            if (!isReloading && angleToFire < 10)
                if (!AimToFrendly())
                    StartCoroutine(Fire()); // ���������� �������

            yield return null;
        }
    }

    private bool AimToFrendly()
    {
        Ray ray = new Ray(transform.position, transform.right); // ��� � ����������� ������� �����

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10, layerMask);
        if (hit)
        {
            if (hit.collider.TryGetComponent(out TouchMeteorite touchMeteorite))
            {
                if (touchMeteorite.StateDontShootPropety) // ���������, ����� �� �� �������� � ������ ������
                    return true;
            }
        }
        return false;
    }

    private IEnumerator Fire()
    {
        Instantiate(BulletTurret, FirePlace.position, FirePlace.rotation); // ������� ����
        isReloading = true; // ������������� ��������� �����������
        yield return new WaitForSeconds(1f); // ���� �������
        isReloading = false; // ��������� �����������
    }
}