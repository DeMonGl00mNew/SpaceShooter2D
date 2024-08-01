// Скрипт POVTurret отвечает за поворот башни по направлению к противнику
// EnemyTransform - текущий цель противника
// EnemyPOVPositions - список позиций противников в области обзора
// minDistanse - минимальное расстояние до цели
// NewInPOV - событие, срабатывающее при появлении нового противника в области обзора

// Метод OnTriggerEnter2D срабатывает при входе в триггер с другим коллайдером
private void OnTriggerEnter2D(Collider2D other)
{
    if (other.TryGetComponent(out Meteorit meteorite))
    {
        meteorite.StateDontShootEvent += DeleteInEnemyPOVPositions;
        if (meteorite.StateDontShoot)
            return;

        if (!EnemyTransform)
            minDistanse = Mathf.Infinity;

        if (!EnemyPOVPositions.Contains(other.transform))
        {
            EnemyPOVPositions.Add(other.transform);
        }

        Targeting();
    }

}

// Метод DeleteInEnemyPOVPositions удаляет противника из обзора башни
private void DeleteInEnemyPOVPositions(Meteorit meteorite)
{
    if (EnemyPOVPositions.Contains(meteorite.transform))
        EnemyPOVPositions.Remove(meteorite.transform);
}

// Метод Targeting выбирает цель с минимальным расстоянием
public void Targeting()
{
    float currentDistance = Mathf.Infinity;
    minDistanse = Mathf.Infinity;
    EnemyTransform = null;
    foreach (var item in EnemyPOVPositions)
    {
        if (!item)
            continue;
        currentDistance = Vector3.Distance(transform.position, item.position);
        if (currentDistance < minDistanse)
        {
            minDistanse = currentDistance;
            EnemyTransform = item;

        }

    }

    if (EnemyTransform)
        NewInPOV?.Invoke();

}

// Метод OnTriggerExit2D срабатывает при выходе из триггера с другим коллайдером
private void OnTriggerExit2D(Collider2D other)
{
    if (other.TryGetComponent(out Meteorit meteorite))
    {
        meteorite.StateDontShootEvent -= DeleteInEnemyPOVPositions;
        if (EnemyPOVPositions.Contains(other.transform))
        {
            EnemyPOVPositions.Remove(other.transform);

        }

        Targeting();
    }
}