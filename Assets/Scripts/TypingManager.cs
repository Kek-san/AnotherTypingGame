using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TypingManager : MonoBehaviour
{
    [SerializeField] Enemy _target;

    //---Enemy reference---//
    private string _wordToComplete;
    private TextMeshProUGUI _enemyTextUI;
    //--------------------//

    private string _currentWord = "";

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

    private void Start() {
        _wordToComplete = _target.GetWord();
        _enemyTextUI = _target.GetTextUI();
    }

    private void OnCharacterTyped(char character) {

        if (string.IsNullOrEmpty(_wordToComplete)) return;

        if(character == _wordToComplete[0]) {

            _currentWord += character;
            _wordToComplete = _wordToComplete.Substring(1);
            UpdateVisual();

            if(_wordToComplete.Length == 0) {
                int damage = 1;
                _target.TakeDamage(damage);
            }
        }
    }

    private void UpdateVisual() {
        // Highlight what's left to type. Use rich text tags to color-code your text progress.
        int typedLength = _currentWord.Length;
        string typedPart = $"<color=#00FF00>{_currentWord.Substring(0, typedLength)}</color>";
        string remainingPart = $"<color=#FFFFFF>{_wordToComplete}</color>";

        _enemyTextUI.text = typedPart + remainingPart;
    }
}
