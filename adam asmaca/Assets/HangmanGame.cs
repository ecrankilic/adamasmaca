using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HangmanGame : MonoBehaviour
{
    // UI Elemanları
    public TextMeshProUGUI hiddenWordText;
    public TextMeshProUGUI gameStateText;
    public TextMeshProUGUI remainingLivesText;
    public TextMeshProUGUI guessedLettersText;
    public Image hangmanImage;
    public TMP_InputField guessInput;
    public Button checkButton;

    // Oyun Değişkenleri
    private string hiddenWord = "kelebek";
    private string guessedLetters = "";
    private int remainingLives = 6;
    private bool isGameOver = false;

    // Adam asmaca resimleri (baş, gövde, vb.)
    public Sprite[] hangmanStages;

    void Start()
    {
        UpdateHiddenWordDisplay();
        UpdateLivesDisplay();
        gameStateText.text = "";
    }
    
    // Gizli kelimeyi gösteren alt çizgileri güncelleme
    private void UpdateHiddenWordDisplay()
    {
        string displayWord = "";
        foreach (char letter in hiddenWord)
        {
            if (guessedLetters.Contains(letter.ToString()))
                displayWord += letter + " ";
            else
                displayWord += "_ ";
        }
        hiddenWordText.text = displayWord;
    }

    private void UpdateLivesDisplay()
    {
        remainingLivesText.text = "Kalan Can: " + remainingLives;
    }
    
    private void UpdateHangmanImage()
    {
        if (remainingLives >= 0 && remainingLives < hangmanStages.Length)
        {
            hangmanImage.sprite = hangmanStages[remainingLives];
        }
    }

    public void OnCheckButtonClick()
    {
        if (isGameOver) return;

        string guessedLetter = guessInput.text.ToLower();
        guessInput.text = "";

        if (guessedLetter.Length != 1 || guessedLetters.Contains(guessedLetter))
        {
            gameStateText.text = "Geçersiz veya tekrarlanan tahmin!";
            return;
        }

        guessedLetters += guessedLetter;
        guessedLettersText.text = "Tahmin Edilen Harfler: " + guessedLetters;

        if (hiddenWord.Contains(guessedLetter))
        {
            UpdateHiddenWordDisplay();
            CheckWinCondition();
        }
        else
        {
            remainingLives--;
            UpdateLivesDisplay();
            UpdateHangmanImage();
            CheckLoseCondition();
        }
    }

    private void CheckWinCondition()
    {
        if (!hiddenWordText.text.Contains("_"))
        {
            isGameOver = true;
            gameStateText.text = "Kazandınız!";
        }
    }

    private void CheckLoseCondition()
    {
        if (remainingLives <= 0)
        {
            isGameOver = true;
            gameStateText.text = "Kaybettiniz!";
        }
    }
}

