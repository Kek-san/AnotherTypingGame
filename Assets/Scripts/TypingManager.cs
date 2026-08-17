using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TypingManager : MonoBehaviour
{
    public static TypingManager Instance { get; private set; }

    [SerializeField] Enemy _target;

    private void OnEnable() {
        if(Keyboard.current != null) {
            Keyboard.current.onTextInput += OnCharacterTyped;
        }
    }

    private void OnDisable() {
        if (Keyboard.current != null) {
            Keyboard.current.onTextInput -= OnCharacterTyped;
        }
    }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        _target.OnEnemyDies += CheckTargetList;
    }

    private void CheckTargetList(Enemy enemy) {
        //Have a target list manager 
        //Checks if there are targets on the list
        //picks the first target on the list
        //if no target on the list
        //set _target to null

        _target = null;
    }

    private void OnCharacterTyped(char character) {
        if (_target == null) return;

        _target.RecieveKeystroke(character);
    }

}
