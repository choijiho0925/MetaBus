using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    [SerializeField] Transform character;
    [SerializeField] float cameraSpeed = 5f;
    [SerializeField]Vector2 minBounds;
    [SerializeField]Vector2 maxBounds; 

    private void LateUpdate()
    {
        Vector3 characterPosition = character.position;
        characterPosition.z = transform.position.z;

        Vector3 tempPosition = Vector3.Lerp(transform.position, characterPosition, Time.deltaTime * cameraSpeed);

        tempPosition.x = Mathf.Clamp(tempPosition.x, minBounds.x, maxBounds.x);
        tempPosition.y = Mathf.Clamp(tempPosition.y, minBounds.y, maxBounds.y);

        transform.position = tempPosition;
    }
}
