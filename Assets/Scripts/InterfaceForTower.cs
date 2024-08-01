// ����� ��� ���������� ����������� �����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfaceForTower : MonoBehaviour
{

    public TMP_Text CostsText; // ��������� ���� ��� ����������� ���������
    public GameObject CloseTower; // ������� ������ ��� �������� �����

    // ����� ���������� ��� ������ ����
    private void Start()
    {
        // ������������� �� ������� ���������� ��������� ������� �����
        ClickToTower.StateCountForAccessBuyEvent += UpdateCost;
        // ���������� ���������
        VisibleCost();
    }

    // ����� ���������� ��� ����������� �������
    private void OnDestroy()
    {
        // ������������ �� ������� ���������� ���������
        ClickToTower.StateCountForAccessBuyEvent -= UpdateCost;
    }

    // ����� ��� ���������� ���������
    private void UpdateCost()
    {
        if(CostsText.gameObject.activeSelf)
            CostsText.text = ClickToTower.StateCountForAccessBuy.ToString();
    }

    // ����� ��� ����������� ���������
    public void VisibleCost()
    {
        if (CostsText.gameObject.activeSelf)
            return;
        CostsText.gameObject.SetActive(true);
        CostsText.text = ClickToTower.StateCountForAccessBuy.ToString();
    }

    // ����� ��� ����������� ������ �������� �����
    public void VisibleClose()
    {
        if (CloseTower.activeSelf)
            return;
        CloseTower.gameObject.SetActive(true);
    }

    // ����� ��� �������� �����
    public void CloseTurret(GameObject turret)
    {
        if (turret.activeSelf)
        {
            turret.SetActive(false);
            CloseTower.SetActive(false);
            ClickToTower.StateCountForAccessBuy -= 2;
            VisibleCost();
        }
    }

    // ����� ��� ��������� ����� �� ������ ���������
    public void ClickToEmptyPlatform(GameObject currentTower)
    {
        if (Score.StateScore >= ClickToTower.StateCountForAccessBuy && !currentTower.activeSelf)
        {
            currentTower.SetActive(true);
            VisibleClose();
            CostsText.gameObject.SetActive(false);
            ClickToTower.StateCountForAccessBuy += 2;
        }
    }
}