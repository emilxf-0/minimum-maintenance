using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrechSpriteTest : MonoBehaviour
{

    public Transform startPosition;
    public Transform endPosition;
    public bool mirrorZ = true;
    private SpriteRenderer spritRenderer;

    void Start()
    {
        spritRenderer = GetComponent<SpriteRenderer>();

    }
    private void Update()
    {
        Strech(gameObject, startPosition.position, endPosition.position, mirrorZ);

    }

    public void Strech(GameObject _sprite, Vector3 _initialPosition, Vector3 _finalPosition, bool _mirrorZ)
    {
        Vector3 centerPos = (_initialPosition + _finalPosition) / 2f;
        _sprite.transform.position = centerPos;
        Vector3 direction = _finalPosition - _initialPosition;
        direction = Vector3.Normalize(direction);
        _sprite.transform.right = direction;
        if (_mirrorZ) _sprite.transform.right *= -1f;
        Vector3 scale = new Vector3(1, 1, 1);
        scale.x = Vector3.Distance(_initialPosition, _finalPosition);
        spritRenderer.size = new Vector2(scale.x, spritRenderer.size.y);
    }

}
