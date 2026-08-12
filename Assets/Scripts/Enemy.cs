using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textUI;
    [SerializeField] private int _hp = 1;

    private string _word;


    private void Start() {
        SetWord(_textUI.text.ToString());
    }

    private void SetWord(string word) {
        _word = word;
    }
    public string GetWord() {
        return _word;
    }

    public TextMeshProUGUI GetTextUI() {
        return _textUI;
    }

    public void TakeDamage(int damage) {
        _hp -= damage;
        if(_hp <= 0) {
            Destroy(gameObject);
        }
    }
}
