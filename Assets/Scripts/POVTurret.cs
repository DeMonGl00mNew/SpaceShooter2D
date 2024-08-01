// ������ POVTurret �������� �� ������� ����� �� ����������� � ����������
// EnemyTransform - ������� ���� ����������
// EnemyPOVPositions - ������ ������� ����������� � ������� ������
// minDistanse - ����������� ���������� �� ����
// NewInPOV - �������, ������������� ��� ��������� ������ ���������� � ������� ������

// ����� OnTriggerEnter2D ����������� ��� ����� � ������� � ������ �����������
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

// ����� DeleteInEnemyPOVPositions ������� ���������� �� ������ �����
private void DeleteInEnemyPOVPositions(Meteorit meteorite)
{
    if (EnemyPOVPositions.Contains(meteorite.transform))
        EnemyPOVPositions.Remove(meteorite.transform);
}

// ����� Targeting �������� ���� � ����������� �����������
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

// ����� OnTriggerExit2D ����������� ��� ������ �� �������� � ������ �����������
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