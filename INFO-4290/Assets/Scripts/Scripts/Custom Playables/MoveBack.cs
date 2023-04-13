using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MoveBack : MonoBehaviour
{
    [SerializeField] private PlayableAsset turnBack;
    [SerializeField] private PlayableAsset openingScene;
    [SerializeField] private BoolValue swordChest;
    [SerializeField] private BoolValue openingCutScene;
    [SerializeField] private BoolValue UIactive;
    private void Start()
    {
        if (!openingCutScene.RunTimeValue)
        {
            GetComponent<PlayableDirector>().playableAsset = openingScene;
            GetComponent<PlayableDirector>().Play();
            openingCutScene.RunTimeValue = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayableDirector playableDirector = GetComponent<PlayableDirector>();
        Debug.Log("Here");
        if (other.CompareTag("Player") && swordChest.RunTimeValue == false)
        {
            playableDirector.playableAsset = turnBack;
            playableDirector.Play();
        }
    }
}
