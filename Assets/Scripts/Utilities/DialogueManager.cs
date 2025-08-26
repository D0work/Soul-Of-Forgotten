using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject playerBubble;        
    public GameObject butterflyBubble;
    public TextMeshProUGUI _playerText;
    public TextMeshProUGUI _butterflyText;

    private bool _isPlayerSpeaker;         

    private Coroutine _currentRoutine;
    private bool _chainIndexChanged = false;
    private int _chainIndex = 0;

    private List<string> _stepByStepDialogs;  
    private bool _stepByStepLoop;   
    private bool _stepByStepActive = false;


    private void Start()
    {
        _playerText.text = "";
        _butterflyText.text = "";

        playerBubble.SetActive(false);
        butterflyBubble.SetActive(false);
    }

    public void SwitchSpeaker()
    {
        _isPlayerSpeaker = !_isPlayerSpeaker;
        playerBubble.SetActive(_isPlayerSpeaker);
        butterflyBubble.SetActive(!_isPlayerSpeaker);
    }
    public void SetSpeaker(bool player)
    {
        if (player != _isPlayerSpeaker)
            SwitchSpeaker();
    }

    private void ShowUI()
    {
        if (_isPlayerSpeaker)
        {
            playerBubble.SetActive(true);
            _playerText.gameObject.SetActive(true);
        }
        else
        {
            butterflyBubble.SetActive(true);
            _butterflyText.gameObject.SetActive(true);
        }
    }

    private void HideUI()
    {
        playerBubble.SetActive(false);
        butterflyBubble.SetActive(false);
        _playerText.gameObject.SetActive(false);
        _butterflyText.gameObject.SetActive(false);
    }

    public void PlayLongDialogue(string fullText, float delayBetweenLines)
    {
        if (_currentRoutine != null)
            StopCoroutine(_currentRoutine);

        _currentRoutine = StartCoroutine(LongDialogueCoroutine(fullText, delayBetweenLines));
    }

    private IEnumerator LongDialogueCoroutine(string fullText, float delay)
    {
        ShowUI();

        var lines = fullText.Split('\n');
        foreach (var line in lines)
        {
            if (_isPlayerSpeaker)
            {
                _playerText.text = line;
            }
            else
            {
                _butterflyText.text = line;
            }
            yield return new WaitForSeconds(delay);
        }

        HideUI();
        _currentRoutine = null;
    }



    public void PlayChainesDialogue(List<string> dialogs,
                                    float delayBetweenDialogs,
                                    bool loop = true,
                                    bool autoAdvance = true)
    {
        if (_currentRoutine != null)
            StopCoroutine(_currentRoutine);

        _chainIndex = 0;
        _chainIndexChanged = false;
        _currentRoutine = StartCoroutine(
            ChainsDialogueCoroutine(dialogs, delayBetweenDialogs, loop, autoAdvance)
        );
    }

    private IEnumerator ChainsDialogueCoroutine(List<string> dialogs,
                                            float delay,
                                            bool loop,
                                            bool autoAdvance)
    {
        ShowUI();

        while (true)
        {
            if (_isPlayerSpeaker)
            {
                _playerText.text = dialogs[_chainIndex];
            }
            else
            {
                _butterflyText.text = dialogs[_chainIndex];
            }

            if (!autoAdvance)
            {
                yield return new WaitUntil(() => _chainIndexChanged);
                _chainIndexChanged = false;
            }
            else
            {
                yield return new WaitForSeconds(delay);
            }

            //string t = _isPlayerSpeaker ? _playerText.text : _butterflyText.text;
            IncrementChainIndex(dialogs.Count, loop);
            //Debug.Log($"_chainIndex: {_chainIndex} / {dialogs.Count} -> {t}");

            if (!loop && _chainIndex >= dialogs.Count)
            {
                //Debug.Log($"Stop after {delay} secondes");
                yield return new WaitForSeconds(delay);
                StopDialogue();
                yield break;
            }
        }
    }

    public void NextDialogue()
    {
        _chainIndexChanged = true;
        IncrementChainIndex(_chainIndex + 1, true);
    }

    private void IncrementChainIndex(int total, bool loop)
    {
        _chainIndex++;
        if (_chainIndex > total)
            _chainIndex = loop ? 0 : total - 1;
    }

    public void StopDialogue()
    {
        if (_currentRoutine != null)
            StopCoroutine(_currentRoutine);

        _currentRoutine = null;
        HideUI();
    }

    public void StartStepByStep(List<string> dialogs, bool loop = false)
    {
        if (dialogs == null || dialogs.Count == 0)
            return;

        StopDialogue();

        _stepByStepDialogs = dialogs;
        _stepByStepLoop = loop;
        _chainIndex = 0;
        _stepByStepActive = true;

        ShowUI();
        ShowNextStep();
    }

    public void ShowNextStep()
    {
        if (!_stepByStepActive || _stepByStepDialogs == null)
            return;

        if (_isPlayerSpeaker)
        {
            _playerText.text = _stepByStepDialogs[_chainIndex];
        }
        else
        {
            _butterflyText.text = _stepByStepDialogs[_chainIndex];
        }

        _chainIndex++;
        if (_chainIndex >= _stepByStepDialogs.Count)
        {
            if (_stepByStepLoop)
                _chainIndex = 0;
            else
                _chainIndex = _stepByStepDialogs.Count - 1;
        }
    }

    public void StopStepByStep()
    {
        _stepByStepActive = false;
        HideUI();
    }

    public void PlayOneLine(string line, float hideAfter = 0f)
    {
        if (_currentRoutine != null)
            StopCoroutine(_currentRoutine);
        _currentRoutine = StartCoroutine(OneLineCoroutine(line, hideAfter));
    }

    private IEnumerator OneLineCoroutine(string line, float hideAfter)
    {
        playerBubble.SetActive(_isPlayerSpeaker);
        butterflyBubble.SetActive(!_isPlayerSpeaker);

        ShowUI();

        if (_isPlayerSpeaker)
        {
            _playerText.text = line;
        }
        else
        {
            _butterflyText.text = line;
        }

        if (hideAfter > 0f)
            yield return new WaitForSeconds(hideAfter);

        HideUI();
        _currentRoutine = null;
    }
}
