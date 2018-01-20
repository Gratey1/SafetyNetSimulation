using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCanvas : OurMonoBehaviour
{
    // *******************************************************************

    [SerializeField]
    private GameObject[] gameObjectsActiveWhilePlaying;

    [SerializeField]
    private GameObject[] gameObjectsActiveWhileNotPlaying;

    // *******************************************************************

    public override void OnEnable()
    {
        base.OnEnable();

        GameManager.PlaySelected += UpdatePlaySpecificGameObjects;
        GameManager.PauseSelected += UpdatePlaySpecificGameObjects;
        GameManager.ResetSelected += UpdatePlaySpecificGameObjects;

        UpdatePlaySpecificGameObjects();
    }

    public override void OnDisable()
    {
        base.OnDisable();

        GameManager.PlaySelected -= UpdatePlaySpecificGameObjects;
        GameManager.PauseSelected -= UpdatePlaySpecificGameObjects;
        GameManager.ResetSelected -= UpdatePlaySpecificGameObjects;
    }

    protected override bool UsesUpdate()
    {
        return false;
    }

    protected void UpdatePlaySpecificGameObjects()
    {
        SetGameObjectsActive(gameObjectsActiveWhilePlaying, GameManager.Instance.IsPlaying);
        SetGameObjectsActive(gameObjectsActiveWhileNotPlaying, !GameManager.Instance.IsPlaying);
    }

    protected void SetGameObjectsActive(GameObject[] _gameObjects, bool _isActive)
    {
        if (_gameObjects == null)
            return;

        for (int i = 0; i < _gameObjects.Length; i++)
        {
            GameObject _go = _gameObjects[i];
            if (_go != null && _go.activeSelf != _isActive)
            {
                _go.SetActive(_isActive);
            }
        }
    }

    // *******************************************************************
}
