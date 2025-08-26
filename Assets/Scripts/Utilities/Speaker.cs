using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Speaker : MonoBehaviour
{
    [Header("DialogueManager")]
    public DialogueManager dialogueManager;

    [Header("Who ?")]
    public bool isPlayerSpeaker = true;

    [Header("Delay Options")]
    [Tooltip("Délai (PlayLong)")]
    public float delayBetweenLines = 2f;
    [Tooltip("Délai (PlayChain)")]
    public float delayBetweenChain = 3f;

    [Header("Looping Options")]
    [Tooltip("Boucle automatique (PlayChain)")]
    public bool loopChain = false;
    [Tooltip("Auto-advance (PlayChain)")]
    public bool autoAdvance = false;

    [Header("Step-by-Step Options")]
    [Tooltip("Boucle à la fin de la liste (pas à pas)")]
    public bool loopStepByStep = false;

    [Header("Text")]
    [Tooltip("Phrases (PlayChain / Step-by-Step)")]
    public List<string> chainTexts = new List<string>();
    [TextArea(3, 10)]
    public string fullText;

    [Header("Speaker Sequence")]
    [Tooltip("true = player, false = butterfly")]
    public List<bool> speakerSequence = new List<bool>();
    public float hideAfter = 0f;
    private int _stepIndex;


    public void PlayLong()
    {
        if (dialogueManager == null) return;
        dialogueManager.SetSpeaker(isPlayerSpeaker);
        dialogueManager.PlayLongDialogue(fullText, delayBetweenLines);
    }

    public void PlayChain()
    {
        if (dialogueManager == null || chainTexts.Count == 0) return;
        dialogueManager.SetSpeaker(isPlayerSpeaker);
        dialogueManager.PlayChainesDialogue(
            chainTexts,
            delayBetweenChain,
            loopChain,
            autoAdvance
        );
    }

    public void PlayStepByStep()
    {
        if (dialogueManager == null || chainTexts.Count == 0) return;
        dialogueManager.SetSpeaker(isPlayerSpeaker);
        dialogueManager.StartStepByStep(chainTexts, loopStepByStep);
    }

    public void NextStep()
    {
        if (dialogueManager == null) return;
        dialogueManager.ShowNextStep();
    }

    public void PlayCustomChain()
    {
        if (dialogueManager == null
            || chainTexts.Count == 0
            || speakerSequence.Count != chainTexts.Count)
            return;

        StartCoroutine(ChainWithSequence());
    }

    private IEnumerator ChainWithSequence()
    {
        int previousIndex = -1;
        bool previousSpeaker = isPlayerSpeaker;

        do
        {
            for (int i = 0; i < chainTexts.Count; i++)
            {
                bool currentSpeaker = speakerSequence[i];
                if (i == 0 || currentSpeaker != previousSpeaker)
                    dialogueManager.SetSpeaker(currentSpeaker);

                dialogueManager.PlayOneLine(chainTexts[i], hideAfter);
                previousSpeaker = currentSpeaker;
                previousIndex = i;

                yield return new WaitForSeconds(delayBetweenChain);
            }
        }
        while (loopChain);
    }

    public void PlayStepByStepWithSequence()
    {
        if (dialogueManager == null
            || chainTexts.Count == 0
            || speakerSequence.Count != chainTexts.Count)
            return;

        _stepIndex = 0;
        dialogueManager.SetSpeaker(speakerSequence[0]);
        dialogueManager.StartStepByStep(chainTexts, loopStepByStep);
    }

    public void NextStepWithSequence()
    {
        _stepIndex++;

        if (_stepIndex >= chainTexts.Count)
        {
            if (loopStepByStep)
                _stepIndex = 0;
            else
                return;
        }

        bool currentSpeaker = speakerSequence[_stepIndex];
        dialogueManager.SetSpeaker(currentSpeaker);
        dialogueManager.ShowNextStep();
    }

}
