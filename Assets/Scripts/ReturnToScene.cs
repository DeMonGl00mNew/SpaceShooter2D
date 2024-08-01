// ����� ��� ����������� ������� �� ����� ��� ������ �� �������
using System.Collections;
using UnityEngine;

public class ReturnToScene : MonoBehaviour
{
    private bool flagReplace = true; // ���� ��� �������� ����������� ������ �������
    private Rigidbody2D rb; // ������ �� ��������� Rigidbody2D � �������
    Coroutine rePlaceStop; // ������ �� �������� ��� ���������

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // �������� ��������� Rigidbody2D ��� ������
    }

    private void OnDestroy()
    {
        // ��� ����������� ������� ������������� ��������
        if (rePlaceStop != null && CouratineManager.Instance != null)
            CouratineManager.Instance.StopCoroutine(rePlaceStop);
    }

    // ��� ������ ������� �� ������� ��������
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out SceneColider sceneColider))
            if (flagReplace && gameObject.activeSelf && CouratineManager.Instance != null)
                rePlaceStop = CouratineManager.Instance.StartCoroutine(rePlace());
    }

    // �������� ��� ����������� ������� �� �����
    IEnumerator rePlace()
    {
        flagReplace = false; // ��������� ����������� ��������� ������

        // ���������� ������ �� ��������������� �������
        transform.position = transform.position * -1;
        yield return new WaitForSeconds(0.3f); // ���� ��������� �����

        // ����������� �������� �������, ���� �� �� �������� ������� � ��� �������� ������ 1
        if (!gameObject.CompareTag("Player"))
            if (rb.velocity.magnitude < 1)
                rb.velocity *= 3;

        flagReplace = true; // �������� ����������� ��������� ������
    }
}