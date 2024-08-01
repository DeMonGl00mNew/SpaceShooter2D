// ����� Scanner �������� �� ������ � �������� ���������� �� ����

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Scanner : MonoBehaviour
{
    static public bool isEmpty = true; // ���������� ��� ��������, ���� �� ������
    public Image MagnitImage; // ������ �� ����������� �������
    public Transform FirePlace; // ����� ��� �������� ����������
    public GameObject TriggeringGameObject; // ������, ������� ����������� ������

    // ��� ����� � �������
    private void OnTriggerStay2D(Collider2D other)
    {
        TriggeringGameObject = other.gameObject;
        
        // ���� ������ - ��������
        if (other.TryGetComponent<Meteorit>(out Meteorit meteorit))
        {
            // ���� �������� ��� �������� ��� �� �������, ������� �� ������
            if (meteorit.Zagrugen == true || meteorit.NaStancii == true)
                return;
                
            MagnitImage.color = new Color32(255, 255, 255, 255); // �������� ���� ����������� �������
            
            // ���� ��������� � ����������� �����, �������� ��� ������
            if (Tutorial.StateTutorial == 3)
                Tutorial.StateTutorial = 4;
        }
    }

    // ��� ������ �� ��������
    private void OnTriggerExit2D(Collider2D collision)
    {
        TriggeringGameObject = null;
        MagnitImage.color = new Color32(94, 94, 94, 255); // ���������� ���� ����������� ������� � ��������
    }

    // ����� ��� �������� ��������� �� ����
    public void ZagruzitNaBort()
    {
        if (TriggeringGameObject == null)
            return;

        // ���� ������ ������
        if (isEmpty == true)
        {
            // �������� �������� ��������� �������� � �������-��������
            if (TriggeringGameObject.TryGetComponent(out Meteorit meteorit))
            {
                // ���� �������� ��� �������� ��� �� �������, ������� �� ������
                if (meteorit.Zagrugen == true || meteorit.NaStancii == true)
                    return;
                    
                MagnitImage.color = new Color32(94, 94, 94, 255); // ���������� ���� ����������� ������� � ��������
                SingletoneResourses.Instance.SoundsManager.PlaySound(4); // ����������� ���� ��������
                meteorit.Zagrugen = true; 
                meteorit.StateDontShoot = true;
                isEmpty = false;

                // ������������ �������� ��������� � ������ ��� �������� �������� �������
                Rigidbody2D rb = meteorit.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
                meteorit.transform.parent = transform;

                // ������� ��������� ��� ����������� ��������� �� �����
                Destroy(TriggeringGameObject.GetComponent<ReturnToScene>());

                // ��������� �������� ��� ����������� ��������� �� ����� ��������
                StartCoroutine(replaceMeteorite(meteorit));
            }
        }
    }

    // �������� ��� ����������� ��������� �� ����� ��������
    IEnumerator replaceMeteorite(Meteorit meteorit)
    {
        // ��������� ������� ��� ��������
        if (meteorit == null || meteorit.NaStancii == true || !meteorit.Zagrugen)
            yield break;
            
        // ������� ����������� ���������
        meteorit.transform.DOLocalMove(FirePlace.localPosition, 2).SetSpeedBased().SetEase(Ease.Linear)
           .OnComplete(() => {
               // ���� ��������� � ����������� �����, �������� ��� ������
               if (Tutorial.StateTutorial == 4)
                   Tutorial.StateTutorial = 5;
           }); 
           
        yield return null;
    }
}