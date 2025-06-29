using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Update_Collectible_Count : MonoBehaviour
{
    private TextMeshProUGUI collectibleText;
    public TextMeshProUGUI winText;
    public Button replayButton;

    private bool winDisplayed = false;

    void Start()
    {
        collectibleText = GetComponent<TextMeshProUGUI>();
        if (collectibleText == null)
        {
            Debug.LogError("UpdateCollectibleCount script requires a TextMeshProUGUI component on the same GameObject.");
            return;
        }

        if (winText != null)
        {
            winText.gameObject.SetActive(false); 
        }

        if (replayButton != null)
        {
            replayButton.gameObject.SetActive(false); 
        }
           
        if (replayButton != null)
        {
            replayButton.onClick.AddListener(RestartGame);
        }

        UpdateCollectibleDisplay();
        
    }

    void Update()
    {
        UpdateCollectibleDisplay();
    }
    private void UpdateCollectibleDisplay()
    {
        int totalCollectibles = 0;

        // Check and count objects of type Collectible
        Type collectibleType = Type.GetType("Collectible");
        if (collectibleType != null)
        {
            totalCollectibles += UnityEngine.Object.FindObjectsByType(collectibleType, FindObjectsSortMode.None).Length;
        }

        // Optionally, check and count objects of type Collectible2D as well if needed
        Type collectible2DType = Type.GetType("Collectible2D");
        if (collectible2DType != null)
        {
            totalCollectibles += UnityEngine.Object.FindObjectsByType(collectible2DType, FindObjectsSortMode.None).Length;
        }

        // Update the collectible count display
        collectibleText.text = $"Collectibles Remaining: {totalCollectibles}";

        if (totalCollectibles == 0 && !winDisplayed)
        {
            if (winText != null)
                winText.gameObject.SetActive(true);

            if (replayButton != null)
                replayButton.gameObject.SetActive(true);

            winDisplayed = true;
        }
    }
    public void RestartGame()
    {
        // Load lại scene hiện tại
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
