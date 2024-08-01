// ����� ��� ����������� ����������� ������
public class TutorialFireFinger : MonoBehaviour
{
    private Image image; // ���������� ��� �����������
    
    void Start() // ��� ������
    {
        image = GetComponent<Image>(); // �������� ��������� �����������
        Tutorial.StateTutorialEvent += FingerFireOn; // ������������� �� ������� ��������� ��������� ��������
    }
    
    private void OnDestroy() // ��� ����������� �������
    {
        Tutorial.StateTutorialEvent -= FingerFireOn; // ������������ �� �������
    }
    
    public void FingerFireOn() // ����� ��� ����������� ������
    {
        //if (Tutorial.StateTutorial == 4) // ���� ������� ��������� �������� ����� 4
        //{
        //    GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(true); // ���������� �����������
        //    image.color= new Color32(255, 255, 255, 255); // ������������� ����� ���� ��� �����������
        //}
        
        //if (Tutorial.StateTutorial == 5) // ���� ������� ��������� �������� ����� 5
        //{
        //    GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(false); // ������������ �����������
        //}

    }
}