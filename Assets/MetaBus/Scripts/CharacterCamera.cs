using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    [SerializeField] Transform character;
    [SerializeField] float cameraSpeed = 5f;
    Vector2 minBounds;
    Vector2 maxBounds;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - character.position;
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(character.position.x, character.position.y, -10);

        Vector3 characterPosition = character.position + offset;
        characterPosition.z = transform.position.z;

        characterPosition.x = Mathf.Clamp(characterPosition.x, -13f, 13f);
        characterPosition.y = Mathf.Clamp(characterPosition.y, -13f, 13f);

        transform.position = Vector3.Lerp(transform.position, characterPosition, Time.deltaTime * cameraSpeed);
    }
}
