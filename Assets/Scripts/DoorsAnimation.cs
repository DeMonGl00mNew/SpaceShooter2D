// ����� ��� �������� ������
public class DoorsAnimation : MonoBehaviour
{
    static public DoorsAnimation Instance { get; private set; } // ����������� �������� ��� ������� � ���������� ������

    public GameObject[] Doors; // ������ �������� ������
    Tween closeTween = null; // �������� �������� �����

    private void Awake()
    {
        if (Instance == null) // ���� ��������� �� ������
        {
            Instance = this; // ������� ���������
        }
        else if (Instance != this) // ���� ��������� ��� ����������
        {
            Destroy(gameObject); // ���������� ������� ������
        }
    }

    public void MoveAnimation()
    {
        float PosX; // ������� �� X
        float ScaleX; // ������� �� X

        if (!closeTween.IsActive()) // ���� �������� �������� �� �������
        {
            foreach (var door in Doors) // ��� ������ ����� �� �������
            {
                PosX = door.transform.localPosition.x; // �������� ������� ������� �� X
                ScaleX = door.transform.localScale.x; // �������� ������� ������� �� X
                door.transform.DOLocalMoveX(PosX - Mathf.Sign(ScaleX) * 1.133f, 2); // ����������� �������� ����� �����
                closeTween = door.transform.DOLocalMoveX(PosX, 2).SetDelay(3); // ������� ����� ����� 3 �������
            }
        }
    }
}