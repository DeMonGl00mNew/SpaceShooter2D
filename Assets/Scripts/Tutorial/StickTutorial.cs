// ���� ������ �������� �� ��������� ���������� ����� � ������� � ����
// �� ������������� � ������������ �� ������� ���������� ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // ������ ���������� ��� ������ � ������
using UnityEngine.UI;

public class StickTutorial : MonoBehaviour
{
    public CanvasGroup AlphaFakeStick; // ������������ fake �����
    public GameObject SpaceShipTutorial; // ��������� �������
    private PlayerInputMap playerInputMap; // ����� ����� ������

    void Start()
    {
        playerInputMap = new PlayerInputMap(); // ������� ����� ����� �����
        playerInputMap.Player.Move.started += VisibleStick; // ����������� ����� VisibleStick �� ������� ������ ��������
        playerInputMap.Player.Move.canceled += InvisibleStick; // ����������� ����� InvisibleStick �� ������� ���������� ��������
        playerInputMap.Player.Enable(); // �������� ����� ����� ������

        Tutorial.StateTutorialEvent += UnsubscribeStick; // ����������� ����� UnsubscribeStick �� ������� ������ ��������
    }

    private void OnDestroy()
    {
        playerInputMap.Player.Move.started -= VisibleStick; // ���������� ����� VisibleStick �� ������� ������ ��������
        playerInputMap.Player.Move.canceled -= InvisibleStick; // ���������� ����� InvisibleStick �� ������� ���������� ��������
        Tutorial.StateTutorialEvent -= UnsubscribeStick; // ���������� ����� UnsubscribeStick �� ������� ������ ��������
    }

    private void InvisibleStick(InputAction.CallbackContext context)
    {
        AlphaFakeStick.alpha = 1; // ������������� ������������ fake �����
        SpaceShipTutorial.SetActive(true); // �������� ��������� �������
    }

    public void VisibleStick(InputAction.CallbackContext context)
    {
        AlphaFakeStick.alpha = 0; // ������������� ������������ fake �����
        SpaceShipTutorial.SetActive(false); // ��������� ��������� �������
    }

    private void UnsubscribeStick()
    {
        if (Tutorial.StateTutorial != 1)
        {
            AlphaFakeStick.alpha = 0; // ������������� ������������ fake �����
            playerInputMap.Player.Move.started -= VisibleStick; // ���������� ����� VisibleStick �� ������� ������ ��������
            playerInputMap.Player.Move.canceled -= InvisibleStick; // ���������� ����� InvisibleStick �� ������� ���������� ��������
        }
    }
}