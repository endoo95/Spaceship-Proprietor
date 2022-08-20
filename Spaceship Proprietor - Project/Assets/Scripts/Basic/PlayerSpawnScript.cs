using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    void Awake()
    {
        if (_playerPrefab == null)
        {
            Destroy(transform);
        }

        var newPlayer = Instantiate(_playerPrefab);
        newPlayer.transform.parent = gameObject.transform;
    }
}
