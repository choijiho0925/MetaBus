using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform player; // 카메라가 따라갈 타겟
    private float threshold = 6.0f; // 카메라가 따라가는 거리의 임계값
    private float yMovement = 12.0f; // 카메라의 y축 이동 거리
    private float yBaseLine; // 현재 카메라의 y축 기준선

    private void Start()
    {
        yBaseLine = Mathf.Floor(player.position.y / yMovement) * yMovement; // 카메라의 y축 기준선 초기화
        Vector3 pos = transform.position; // 카메라의 현재 위치
        transform.position = new Vector3(pos.x, yBaseLine, pos.z); // 카메라의 y축 위치를 기준선으로 설정
    }

    private void LateUpdate()
    {
        float plaeyrY = player.position.y; // 캐릭터의 y축 위치

        if (plaeyrY > yBaseLine + threshold)
        {
            yBaseLine += yMovement; // 캐릭터가 기준선보다 위로 이동하면 기준선을 올림
        }
        else if (plaeyrY < yBaseLine - threshold)
        {
            yBaseLine -= yMovement; // 캐릭터가 기준선보다 아래로 이동하면 기준선을 내림
        }

        Vector3 newPosition = new Vector3(transform.position.x, yBaseLine, transform.position.z); // 카메라의 새로운 위치
        transform.position = newPosition; // 카메라의 위치를 새로운 위치로 설정
    }
}
