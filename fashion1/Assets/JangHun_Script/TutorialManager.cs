using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public List<TutorialStep> steps = new List<TutorialStep>();
    public TextMeshProUGUI instructionText;
    public GameObject instructionPanel;
    public GameObject overlayPanel;
    public int currentStepIndex = 0;
    private Button highlightedButton;
    private Coroutine highlightCoroutine;

    public bool tutorialCheck;
    public GameObject tutorial_Danger;
    void Start()
    {
        overlayPanel.SetActive(false);
        StartTutorial();

    }

    public void StartTutorial()
    {
        tutorialCheck = true;
        currentStepIndex = 0;
        ShowCurrentStep();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (instructionPanel.activeSelf && (steps[currentStepIndex].highlightButton == null || !steps[currentStepIndex].highlightButton.interactable))
            {
                OnButtonClick();
            }
        }
    }

    public void SShowManager()
    {
        StartCoroutine(ShowPopup());
    }
    IEnumerator ShowPopup()
    {
        Debug.Log("ShowPopup coroutine started");
        tutorial_Danger.SetActive(true);
        yield return new WaitForSeconds(1);
        tutorial_Danger.SetActive(false);
        RestartCurrentStep();
    }
    void ShowCurrentStep()
    {
        ResetAllButtons();
        if (currentStepIndex >= steps.Count)
        {
            EndTutorial();
            return;
        }

        TutorialStep step = steps[currentStepIndex];
        instructionText.text = step.instructionText;

        if (step.hidePanel)
        {
            StartCoroutine(HidePanelAfterDelay(0.3f));
        }
        else
        {
            instructionPanel.SetActive(true);
        }

        if (step.highlightButton != null)
        {
            HighlightButton(step.highlightButton);
            step.highlightButton.onClick.RemoveAllListeners();
            step.highlightButton.onClick.AddListener(() => OnButtonClicked(step.highlightButton));
        }
    }
    void SShowCurrentStep()
    {
        ResetAllButtons();
        if (currentStepIndex >= steps.Count)
        {
            EndTutorial();
            return;
        }

        TutorialStep step = steps[currentStepIndex];
        instructionText.text = step.instructionText;

        if (step.hidePanel)
        {
            StartCoroutine(HidePanelAfterDelay(0.3f));
        }
        else
        {
            instructionPanel.SetActive(true);
        }

        if (step.highlightButton != null)
        {
            HighlightButton(step.highlightButton);
            step.highlightButton.onClick.RemoveAllListeners();
            step.highlightButton.onClick.AddListener(() => OnButtonClicked(step.highlightButton));
        }
    }
    IEnumerator HidePanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        instructionPanel.SetActive(false);
    }

    void HighlightButton(Button button)
    {
        foreach (Button btn in FindObjectsOfType<Button>())
        {
            btn.interactable = false;
        }

        button.interactable = true;
        highlightedButton = button;
        if (highlightCoroutine != null)
        {
            StopCoroutine(highlightCoroutine);
        }
        highlightCoroutine = StartCoroutine(BlinkButton(button));
    }

    IEnumerator BlinkButton(Button button)
    {
        while (true)
        {
            button.GetComponent<Image>().color = Color.red;
            yield return new WaitForSeconds(0.5f);
            button.GetComponent<Image>().color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void OnButtonClicked(Button button)
    {
        if (currentStepIndex < steps.Count && steps[currentStepIndex].onStepComplete != null)
        {
            steps[currentStepIndex].onStepComplete.Invoke();
        }
        if (highlightCoroutine != null)
        {
            StopCoroutine(highlightCoroutine);
            button.GetComponent<Image>().color = Color.white;
        }
        button.onClick.RemoveAllListeners();
        OnButtonClick();
    }

    public void OnButtonClick()
    {
        if (tutorialCheck == true)
        {
            currentStepIndex++;
        }
        ShowCurrentStep();
    }

    public void RestartCurrentStep()
    {
        CallBackStep();
    }
    public void CallBackStep()
    {
        StartTutorial();
    }

    void EndTutorial()
    {
        instructionPanel.SetActive(false);
        overlayPanel.SetActive(false);
        Debug.Log("튜토리얼 완료!");
        ResetAllButtons();
    }

    public void ResetAllButtons()
    {
        foreach (var obj in FindObjectsOfType<Button>())
        {
            obj.interactable = true;
        }
    }

    public void SkipTutorial()
    {
        EndTutorial();
    }
}
