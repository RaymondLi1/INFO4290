
using UnityEngine;
using UnityEngine.UI;

public class Prisoner : Sign
{
    private string charName;
    private Animator anim;
    [SerializeField] private BoolValue talkedTo;
    private bool giveDagger;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private Item dagger;
    [SerializeField] private Signal raiseItem;

    private Text nameText;

    // Start is called before the first frame update
    public override void Start()
    {
        charName = "Ogre's Prisoner";
        talkedTo.RunTimeValue = false;
        giveDagger = false;
        dialogueBox = GameObject.FindGameObjectWithTag("Player UI").transform.Find("Dialogue Box").gameObject;

        foreach (DialogueTrigger dialogue in dialogueSets)
        {
            dialogue.CharacterName = charName;
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        if (dialogueSets.Length > 0 && dialogueSets != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && playerInRange)
            {
                if (!talkedTo.RunTimeValue)
                {
                    stopOtherDialogues();
                    dialogueSets[0].toggleDialogue();
                    if (!playerInventory.CheckForItem(dagger) && dialogueSets[0].index == 2)
                    {
                        playerInventory.currentItem = dagger;
                        playerInventory.AddItem(dagger, 1);
                        raiseItem.Raise();
                        FindObjectOfType<AudioManager>().Play("winbrass");
                        giveDagger = true;
                        contextClue.Raise();
                    }
                    if (giveDagger && dialogueSets[0].index == 3)
                    {
                        raiseItem.Raise();
                        contextClue.Raise();
                    }
                }
                else
                {
                    stopOtherDialogues();
                    dialogueSets[1].toggleDialogue();
                }
            }

        }
        if (!playerInRange)
        {
            foreach (DialogueTrigger dialogue in dialogueSets)
            {
                dialogue.index = 0;
                choiceManager.closeChoiceBox();
            }
        }

        if (!playerInRange)
        {
            resetAllDialogIndexes();
            choiceManager.closeChoiceBox();
        }

        if (GameObject.FindGameObjectWithTag("Player UI").transform.Find("Dialogue Box").gameObject.activeInHierarchy == false)
        {
            if (dialogueSets[0].endOfDialogue)
            {
                talkedTo.RunTimeValue = true;
            }
            resetAllDialogIndexes();
            choiceManager.resetChoices();
            choiceManager.closeChoiceBox();
        }
    }
}
