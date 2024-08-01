// ����� ��� ���������� ����������
public class CouratineManager : MonoBehaviour
{
    // ����������� ���������� ��� ������� � ���������� ������
    static public CouratineManager Instance { get; private set; }

    // �����, ������������ ��� ������ �������
    void Awake()
    {
        // ���� ��������� ������ ��� �� ������
        if (Instance == null)
        {
            // ������� ��������� � �� ���������� ��� ��� ����� �����
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        // ���� ��� ���������� ��������� ������
        else if (Instance != this)
        {
            // ���������� ������� ������
            Destroy(gameObject);
        }

    }
}