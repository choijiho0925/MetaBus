using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform player; // ī�޶� ���� Ÿ��
    private float threshold = 6.0f; // ī�޶� ���󰡴� �Ÿ��� �Ӱ谪
    private float yMovement = 12.0f; // ī�޶��� y�� �̵� �Ÿ�
    private float yBaseLine; // ���� ī�޶��� y�� ���ؼ�

    private void Start()
    {
        yBaseLine = Mathf.Floor(player.position.y / yMovement) * yMovement; // ī�޶��� y�� ���ؼ� �ʱ�ȭ
        Vector3 pos = transform.position; // ī�޶��� ���� ��ġ
        transform.position = new Vector3(pos.x, yBaseLine, pos.z); // ī�޶��� y�� ��ġ�� ���ؼ����� ����
    }

    private void LateUpdate()
    {
        float plaeyrY = player.position.y; // ĳ������ y�� ��ġ

        if (plaeyrY > yBaseLine + threshold)
        {
            yBaseLine += yMovement; // ĳ���Ͱ� ���ؼ����� ���� �̵��ϸ� ���ؼ��� �ø�
        }
        else if (plaeyrY < yBaseLine - threshold)
        {
            yBaseLine -= yMovement; // ĳ���Ͱ� ���ؼ����� �Ʒ��� �̵��ϸ� ���ؼ��� ����
        }

        Vector3 newPosition = new Vector3(transform.position.x, yBaseLine, transform.position.z); // ī�޶��� ���ο� ��ġ
        transform.position = newPosition; // ī�޶��� ��ġ�� ���ο� ��ġ�� ����
    }
}
