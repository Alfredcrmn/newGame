using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public FactionDatabase factionDB;

    public TextMeshProUGUI nameText;
    public SpriteRenderer artworkSprite;
    private int selectedOption = 0;

    void Start()
    {
        if (PlayerPrefs.HasKey("SelectedOption"))
        {
            Load(); // Load the saved selection
        }
        else
        {
            selectedOption = 0; // Default to first faction
        }
        UpdateFaction(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++;
        if (selectedOption >= factionDB.FactionCount)
        {
            selectedOption = 0;
        }
        UpdateFaction(selectedOption);
        Save();
    }

    public void BackOption()
    {
        selectedOption--;
        if (selectedOption < 0)
        {
            selectedOption = factionDB.FactionCount - 1;
        }
        UpdateFaction(selectedOption);
        Save();
    }

    private void UpdateFaction(int selectedOption)
    {
        Faction faction = factionDB.GetFaction(selectedOption);
        artworkSprite.sprite = faction.FactionSprite;
        nameText.text = faction.FactionName;

        // Normalize sprite size
        artworkSprite.transform.localScale = Vector3.one;  // Reset scale
        float width = artworkSprite.sprite.bounds.size.x;
        float height = artworkSprite.sprite.bounds.size.y;
    
        float targetSize = 2f;
        float scaleFactor = targetSize / Mathf.Max(width, height);
    
        artworkSprite.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("SelectedOption");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("SelectedOption", selectedOption);
        PlayerPrefs.Save(); // Ensure data is saved immediately
    }

    public void ChangeScene()
    {
        Save(); // Save before changing scene
        Debug.Log("Changing scene to GameplayScene..."); // Debug message
        SceneManager.LoadScene("GameplayScene"); // Load scene
    }
}
