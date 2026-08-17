using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public Action<Enemy> OnEnemyDies;

    [SerializeField] private TextMeshProUGUI _textUI;
    [SerializeField] private int _hp = 1;

    private string _word;


    private string _currentWord = "";
    private string _remainingWord = "";

    Coroutine _updateVisualRoutine;


    private void Awake() {
        SetWord(_textUI.text.ToString());
    }

    private void Start() {

    }

    public void RecieveKeystroke(char character) {

        if (string.IsNullOrEmpty(_word)) return;
        if (_updateVisualRoutine != null) return;

        if (character == _remainingWord[0]) {

            _currentWord += character;
            _remainingWord = _remainingWord.Substring(1);
            UpdateVisual();

            if (_remainingWord.Length == 0) {
                int damage = 1;
                TakeDamage(damage);
            }
        } else {
            _currentWord = "";
            _remainingWord = _word;
            float duration = 1f;
            _updateVisualRoutine = StartCoroutine(UpdatErrorVisualRoutine(duration));

        }
    }


    private void UpdateVisual() {
        // Highlight what's left to type. Use rich text tags to color-code your text progress.
        int typedLength = _currentWord.Length;
        string typedPart = $"<color=#00FF00>{_currentWord.Substring(0, typedLength)}</color>";
        string remainingPart = $"<color=#FFFFFF>{_remainingWord}</color>";

        _textUI.text = typedPart + remainingPart;
    }

    private IEnumerator UpdatErrorVisualRoutine(float duration) {
        float time = 0f;
        while (time < duration) {
            string remainingPart = $"<color=#FF0000>{_remainingWord}</color>";
            _textUI.text = remainingPart;
            time += Time.deltaTime;

            yield return null;
        }
        UpdateVisual();
        _updateVisualRoutine = null;


    }

    private void SetWord(string word) {
        _word = word;
        _remainingWord = word;
    }

    public void TakeDamage(int damage) {
        _hp -= damage;
        if(_hp <= 0) {
            OnEnemyDies?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
