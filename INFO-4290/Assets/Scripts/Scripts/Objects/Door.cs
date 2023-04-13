using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DoorType
{
    key, enemy, button, breakable
}

public class Door : InteractableOnce
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public BoolValue open;
    public Inventory playerInventory;
    public ItemDatabase itemDB;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;
    public GameObject dialogueBox;
    public Text dialogueText;
    public string message;
    public bool needText;
    private bool closeText;
    Coroutine lastRoutine = null;

    private void OnEnable()
    {
        if (this.thisDoorType == DoorType.enemy)
        {
            triggered = false;
        }
        dialogueBox = GameObject.FindGameObjectWithTag("Player UI").transform.Find("Dialogue Box").gameObject;
        physicsCollider = this.transform.parent.GetComponent<BoxCollider2D>();
        dialogueText = dialogueBox.transform.Find("Dialogue Text").GetComponent<Text>();
        if (open.RunTimeValue && thisDoorType != DoorType.enemy)
        {
            startOpened();
        }
        else
        {
            Close();
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (playerInRange && thisDoorType == DoorType.key)
            {
                if(playerInventory.numberOfKeys>0)
                {
                    playerInventory.numberOfKeys--;
                    itemDB.GetItem("Small Key").numberHeld = playerInventory.numberOfKeys;
                    Open();
                }

            }

        }
    }

    public void Open()
    {
        doorSprite.enabled = false;
        open.RunTimeValue = true;
        physicsCollider.enabled = false;
        if (needText)
            lastRoutine = StartCoroutine(showText());
        if (thisDoorType == DoorType.button)
            triggerSwitchDoor();
        else if (thisDoorType == DoorType.key)
            trigger();
        else if (thisDoorType == DoorType.enemy)
            triggerEnemyDoor();
    }

    public void startOpened()
    {
        doorSprite.enabled = false;
        open.RunTimeValue = true;
        physicsCollider.enabled = false;
        triggered = true;
    }

    public void Close()
    {
        doorSprite.enabled = true;
        open.RunTimeValue = false;
        physicsCollider.enabled = true;
    }

    private IEnumerator showText()
    {
        dialogueBox.SetActive(true);
        dialogueText.text = message;
        yield return new WaitForSeconds(0.3f);
        closeText = true;
        yield return new WaitForSeconds(2f);
        dialogueBox.SetActive(false);
    }
}
