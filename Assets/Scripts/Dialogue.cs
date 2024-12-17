using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private string npcID; // Unique identifier for each NPC
    [SerializeField] private Image dialogueBox;
    [SerializeField] private Image optionsBox;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Button option_1;
    [SerializeField] private Button option_2;
    [SerializeField] private Button option_3;
    [SerializeField] private GameObject inventory;
    [SerializeField] private List<string> initialDialogueLines; // Initial dialogue lines to restart
    private List<string> currentDialogueLines; // Current dialogue lines
    [SerializeField] private List<string> optionLines;
    [SerializeField] private List<string> option1DialogueLines;
    [SerializeField] private List<string> option2DialogueLines;
    [SerializeField] private GameObject requiredItem; // The item the NPC wants
    [SerializeField] private List<string> yesItemDialogueLines; // Dialogue lines if the item is correct
    [SerializeField] private List<string> noItemDialogueLines; // Dialogue lines if the item is not in inventory
    [SerializeField] private SpriteRenderer npcSpriteRenderer;

    private int currentLineIndex = 0;
    private int optionRound = 0;
    private bool isDialogueActive = false;
    private bool isPlayerInRange = false; // Flag to indicate the player is in range

    void Start()
    {
        currentDialogueLines = new List<string>(initialDialogueLines); // Initialize with initial dialogue lines

        dialogueBox.gameObject.SetActive(false);
        optionsBox.gameObject.SetActive(false);

        // Set up button click listeners
        option_1.onClick.AddListener(() => OnOptionSelected(0));
        option_2.onClick.AddListener(() => OnOptionSelected(1));
        option_3.onClick.AddListener(() => OnOptionSelected(2));

        // Check and set NPC state from MainManager
        if (MainManager.Instance.IsItemGivenToNPC(npcID) && npcSpriteRenderer != null)
        {
            npcSpriteRenderer.color = Color.white;
        }
    }

    void Update()
    {
        if (isPlayerInRange && !isDialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
        else if (!isPlayerInRange && isDialogueActive)
        {
            EndDialogue();
        }

        // Check for player input to advance the dialogue
        if (isDialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            AdvanceDialogue();
        }

    }

    public void StartDialogue()
    {
        Debug.Log("Dialogue started");
        if (MainManager.Instance.IsItemGivenToNPC(npcID))
        {
            // Optionally display a message indicating the player has already given the item
            Debug.Log("You have already given the item.");
            return;
        }

        if (currentDialogueLines.Count > 0)
        {
            inventory.gameObject.SetActive(false);
            isDialogueActive = true;
            currentLineIndex = 0;
            optionRound = 0;
            dialogueBox.gameObject.SetActive(true);
            DisplayCurrentLine(currentDialogueLines);
        }
    }

    private void DisplayCurrentLine(List<string> dialogueLines)
    {
        if (currentLineIndex < dialogueLines.Count)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
        }
    }

    private void AdvanceDialogue()
    {
        if (currentLineIndex < currentDialogueLines.Count)
        {
            DisplayCurrentLine(currentDialogueLines);
            currentLineIndex++;
        }
        else if (currentLineIndex >= currentDialogueLines.Count && optionLines.Count > 0)
        {
            if (optionRound < 1)
            {
                DisplayOptions();
            }
            else
            {
                EndDialogue();
            }
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        inventory.gameObject.SetActive(true);
        isDialogueActive = false;
        dialogueBox.gameObject.SetActive(false);
        optionsBox.gameObject.SetActive(false);
        ResetDialogue();
        Debug.Log("Dialogue ended");
    }

    private void ResetDialogue()
    {
        currentDialogueLines = new List<string>(initialDialogueLines); // Reset to initial dialogue lines
        currentLineIndex = 0;
        optionRound = 0;
    }

    #region Options

    private void DisplayOptions()
    {
        optionRound++;

        dialogueBox.gameObject.SetActive(false); // Hide normal dialogue box
        optionsBox.gameObject.SetActive(true); // Show options box now instead

        // Set the text of each option button
        if (optionLines.Count > 0)
        {
            option_1.GetComponentInChildren<Text>().text = optionLines[0];
        }

        if (optionLines.Count > 1)
        {
            option_2.GetComponentInChildren<Text>().text = optionLines[1];
        }

        if (optionLines.Count > 2)
        {
            option_3.GetComponentInChildren<Text>().text = optionLines[2];
        }
        else if (optionLines.Count < 0)
        {
            EndDialogue();
        }
    }

    private void OnOptionSelected(int optionIndex)
    {
        if (isPlayerInRange)
        {
            optionsBox.gameObject.SetActive(false);
            dialogueBox.gameObject.SetActive(true);

            currentLineIndex = 0;

            if (optionIndex == 0 && option1DialogueLines.Count > 0)
            {
                StartNewDialogue(option1DialogueLines);
                Debug.Log("Option 1");
            }
            else if (optionIndex == 1 && option2DialogueLines.Count > 0)
            {
                StartNewDialogue(option2DialogueLines);
                Debug.Log("Option 2");
            }
            else if (optionIndex == 2)
            {
                if (HasRequiredItem())
                {
                    StartNewDialogue(yesItemDialogueLines);
                    RemoveItemFromInventory();
                    MainManager.Instance.SetItemGivenToNPC(npcID, true); // Update NPC state in MainManager
                    ItemCounter.Instance.itemCount += 1;

                    if (currentLineIndex > yesItemDialogueLines.Count)
                    {
                        ItemCounter.Instance.itemCount += 1;
                    }

                    if (npcSpriteRenderer != null)
                    {
                        npcSpriteRenderer.color = Color.white;
                    }

                    if (currentLineIndex > currentDialogueLines.Count)
                    {
                        EndDialogue();
                        dialogueBox.gameObject.SetActive(false);
                    }

                }
                else
                {
                    StartNewDialogue(noItemDialogueLines);
                }
            }
        }
    }

    private void StartNewDialogue(List<string> newDialogueLines)
    {
        currentDialogueLines = newDialogueLines;
        currentLineIndex = 0;
        isDialogueActive = true;
        DisplayCurrentLine(currentDialogueLines);
    }

    #endregion

    #region Items

    private bool HasRequiredItem()
    {
        Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] && inventory.slots[i].transform.childCount > 0)
            {
                Transform itemTransform = inventory.slots[i].transform.GetChild(0);
                if (itemTransform.gameObject.name == requiredItem.name + "(Clone)")
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void RemoveItemFromInventory()
    {
        Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] && inventory.slots[i].transform.childCount > 0)
            {
                Transform itemTransform = inventory.slots[i].transform.GetChild(0);
                if (itemTransform.gameObject.name == requiredItem.name + "(Clone)")
                {
                    Destroy(itemTransform.gameObject);
                    inventory.isFull[i] = false;
                    break;
                }
            }
        }
    }

    #endregion

    

    #region Collision Check

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true; // Set the flag to true when the player is in range
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    #endregion
}