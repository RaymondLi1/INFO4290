using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool active;
    public BoolValue storedValue;
    public Sprite activeSprite;
    private SpriteRenderer mySprite;
    public Door thisDoor;

    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        active = storedValue.RunTimeValue;
        if (active)
            ActivateSwitchNoSound();
    }

    public void ActivateSwitch()
    {
        active = true;
        mySprite.sprite = activeSprite;
        storedValue.RunTimeValue = active;
        FindObjectOfType<AudioManager>().Play("click-button");
        thisDoor.Open();
    }

    public void ActivateSwitchNoSound()
    {
        active = true;
        mySprite.sprite = activeSprite;
        storedValue.RunTimeValue = active;
        thisDoor.Open();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger && !active)
        {
            ActivateSwitch();
        }
    }
}
