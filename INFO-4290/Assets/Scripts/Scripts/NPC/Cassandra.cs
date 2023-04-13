using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Cassandra : BoundNPC
{

    [SerializeField] private StringValue currentScene;
    [SerializeField] private StringValue lastScene;


    [SerializeField] private GameObject winPanel;
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (dialogueSets.Length > 0 && dialogueSets != null)
            {
                dialogueSets[0].toggleDialogue();
            }
            if (dialogueSets[0].endOfDialogue)
            {
                StartCoroutine(LoadStart());
            }
        }
        else if (!playerInRange)
        {
            foreach (DialogueTrigger dialogue in dialogueSets)
            {
                dialogue.index = 0;
            }
        }
    }

    private IEnumerator LoadStart()
    {
        winPanel.SetActive(true);
        yield return new WaitForSeconds(5f);
        currentScene.element = "StartMenu";
        lastScene.element = "Home";
        FindObjectOfType<AudioManager>().stopAllThemes();
        FindObjectOfType<AudioManager>().Play(currentScene.element + " Theme");
        SceneManager.LoadSceneAsync(currentScene.element);
        FindObjectOfType<AudioManager>().findAudioSource("running").volume = 0.6f;
        winPanel.SetActive(false);
    }
}
