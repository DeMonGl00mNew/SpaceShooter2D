// ����� ��� ������� "Toolbox"
public class Toolbox : MonoBehaviour
{
    Rigidbody2D rb; // ���������� ���������� ���� Rigidbody2D

    // ���������� ��� ������� ����������
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // �������� ��������� Rigidbody2D � �������
        rb.angularVelocity = Random.Range(-2f, 2f) * 100f; // ������ ��������� ������� ��������
    }

    // ���������� ��� ������������ � ������ �����������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���������, ���� �� � �������������� ������� ��������� Player
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            FindObjectOfType<SpaceStation>().Healing(100000); // �������� ����� Healing � ������� SpaceStation
            Destroy(gameObject); // ���������� ������� ������
        }
    }
}