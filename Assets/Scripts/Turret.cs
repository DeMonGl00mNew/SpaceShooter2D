// Класс Turret отвечает за стрельбу из башни
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Turret : MonoBehaviour
{
    public float speed = 1.0f; // Скорость вращения башни
    public GameObject BulletTurret; // Префаб пули
    public Transform FirePlace; // Позиция выстрела
    public POVTurret POVTurret; // скрипт для обзора башни
    private Vector3 targetDirection;
    private bool isReloading = false;
    private Tweener rotTO = null; // Tween для плавного вращения
    private LayerMask layerMask = 1 << 6; // Игровой слой для обнаружения врагов

    private void OnEnable()
    {
        POVTurret.NewInPOV += TargetDetected; // Подписываемся на событие обнаружения цели
        POVTurret.Targeting(); // Запускаем обзор 
    }

    private void OnDisable()
    {
        POVTurret.NewInPOV -= TargetDetected; // Отписываемся от события обнаружения цели
        isReloading = false;
        StopAllCoroutines();
    }

    private void TargetDetected()
    {
        StartCoroutine(Targeting(speed)); // Запускаем корутину целирования
    }

    IEnumerator Targeting(float speed)
    {
        while (true)
        {
            if (!POVTurret.EnemyTransform) // Если цель не обнаружена
            {
                if (rotTO != null)
                {
                    rotTO.Kill(); // Прерываем Tween вращения
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
                rotTO.ChangeEndValue(new Vector3(0, 0, angle), true).Restart(); // Изменение угла вращения
            }

            if (!isReloading && angleToFire < 10)
                if (!AimToFrendly())
                    StartCoroutine(Fire()); // Производим выстрел

            yield return null;
        }
    }

    private bool AimToFrendly()
    {
        Ray ray = new Ray(transform.position, transform.right); // Луч в направлении впереди башни

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10, layerMask);
        if (hit)
        {
            if (hit.collider.TryGetComponent(out TouchMeteorite touchMeteorite))
            {
                if (touchMeteorite.StateDontShootPropety) // Проверяем, можем ли мы стрелять в данный объект
                    return true;
            }
        }
        return false;
    }

    private IEnumerator Fire()
    {
        Instantiate(BulletTurret, FirePlace.position, FirePlace.rotation); // Создаем пулю
        isReloading = true; // Устанавливаем состояние перезарядки
        yield return new WaitForSeconds(1f); // Ждем секунду
        isReloading = false; // Закончили перезарядку
    }
}