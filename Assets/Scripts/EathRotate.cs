// ����� ��� �������� �������
public class EathRotate : MonoBehaviour
{
    public float speed = 1; // �������� ��������

    // ���������� ���������� ���� ��� �� ����
    void Update()
    {
        // �������� ������� ������ ����� ��� �� ���� x, y � z
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}