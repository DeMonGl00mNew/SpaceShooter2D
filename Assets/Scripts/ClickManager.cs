// ����� ClickManager �������� �� ��������� ������ ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickManager : MonoBehaviour
{
    private PlayerInputMap playerInputMap; // ���������� ��� ���������� ������ ������
    Camera cam; // ���������� ��� �������� ������ �� ������
    Ray ray; // ��� ��� ����������� �������� ��� ��������
    LayerMask layerMask = 1 << 6; // ���� ��� ����������� ��������, � �������� ����� �����������������

    void Start()
    {
        cam = Camera.main; // �������� �������� ������
        playerInputMap = new PlayerInputMap(); // �������������� ���������� ������
        playerInputMap.Enable(); // �������� ����������
        playerInputMap.Player.Click.performed += InputClick; // ������������� �� ������� ����� ������
    }

    private void OnDestroy()
    {
        playerInputMap.Player.Click.performed -= InputClick; // ������������ �� ������� ����� ��� ����������� �������
    }

    private void InputClick(InputAction.CallbackContext context)
    {
        ray = cam.ScreenPointToRay(playerInputMap.Player.Position.ReadValue<Vector2>()); // ������� ��� �� ������ � ������� �����
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 15, layerMask); // ���������, ������ �� �� �� ������ �� ���� layerMask

        if (hit)
        {
            if (hit.collider.TryGetComponent(out TouchMeteorite touchMeteorite)) // ���� ������ �� �������� � ����������� TouchMeteorite
            {
                touchMeteorite.StateDontShootPropety = true; // ������������� ��������� ��������� ��� "�� ��������"
            }
        }
    }
}