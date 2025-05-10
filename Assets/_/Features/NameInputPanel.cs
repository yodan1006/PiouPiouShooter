using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameInputPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int maxNameLength = 10;

    private Action<string> onSubmitCallback;

    public void Initialize(int score, Action<string> onSubmit)
    {
        onSubmitCallback = onSubmit;
        scoreText.text = $"Score: {score}";
        nameInput.characterLimit = maxNameLength;
    }

    private void Start()
    {
        submitButton.onClick.AddListener(OnSubmitClicked);
    }

    private void OnEnable()
    {
        nameInput.text = PlayerPrefs.GetString("LastPlayerName", "");
        nameInput.Select();
    }

    private void OnSubmitClicked()
    {
        if (!string.IsNullOrEmpty(nameInput.text))
        {
            PlayerPrefs.SetString("LastPlayerName", nameInput.text);
            onSubmitCallback?.Invoke(nameInput.text);
            gameObject.SetActive(false);
        }
    }

}
