// �����, ���������� �� �������� �����
public class RotateEarth : MonoBehaviour
{
    // �������� ��������
    public float speedRotate = 10f;

    // ���������� ������ ����
    void Update()
    {
        // ������� ������ ������ ��� Z � ������������ ���������
        transform.Rotate(0, 0, 10 * Time.deltaTime * speedRotate);
    }
}