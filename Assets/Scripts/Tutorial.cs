using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialPanel;
    public Text tutorialText;
    [Multiline]
    public List<string> tutorialTexts;
    public int tutorialTextIndex;

    // Start is called before the first frame update
    void Start()
    {
        if (tutorialTexts.Count > 0)
        {
            tutorialPanel.SetActive(true);
        }
        tutorialText.text = tutorialTexts[tutorialTextIndex];
    }

    private void nextTutorialString()
    {
        tutorialTextIndex++;

        if (tutorialTextIndex >= tutorialTexts.Count)
        {
            tutorialPanel.SetActive(false);
            gameObject.SetActive(false);
            return;
        }

        if (tutorialTextIndex < tutorialTexts.Count)
        {
            tutorialText.text = tutorialTexts[tutorialTextIndex];
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            nextTutorialString();
        }

        if (Input.GetButtonDown("Skip"))
        {
            tutorialPanel.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
