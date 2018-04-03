using System.CodeDom.Compiler;
using System.Collections;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;

    public enum Type
    {
        LetterByLetter,
        WordByWord,
        LineByLine
    }

    [SerializeField] private Type _dialogueType;

    [SerializeField] private float _spacingDelay = 0;

    [SerializeField] private string _tagTrigger;


    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        if (_collider == null)
        {
            Debug.LogError("No Collider2D found on Dialogue Manager");
        }
        else if (_collider.isTrigger == false)
        {
            Debug.LogWarning("Collider2D on Dialogue Manager was not of type collider");
            _collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(_tagTrigger))
        {
            Time.timeScale = 0;

            StartCoroutine(DialogueCoroutineLineByLine(0));

            _collider.enabled = false;
        }
    }


    private IEnumerator DialogueCoroutineLetterByLetter(int letter, int line)
    {
        DialogueBox.TextBox.text += _dialogue.Lines[line].Line[letter];

        if (_dialogue.Lines[line].Line.Length == letter + 1)
        {
            //wait for delay
            yield return new WaitForSecondsRealtime(_dialogue.Lines[line].Delay);
            StartCoroutine(DialogueCoroutineLineByLine(line + 1));
        }
        else
        {
            yield return new WaitForSecondsRealtime(_spacingDelay);
            StartCoroutine(DialogueCoroutineLetterByLetter(letter + 1, line));
        }
    }

    private IEnumerator DialogueCoroutineWordByWord(int letter, int line)
    {
        var current = "";

        do
        {
            current += _dialogue.Lines[line].Line[letter];
            letter++;
        } while (!(_dialogue.Lines[line].Line[letter] == ' ' || _dialogue.Lines[line].Line.Length == letter + 1));

        DialogueBox.TextBox.text += current;

        if (_dialogue.Lines[line].Line.Length == letter + 1)
        {
            //wait for delay
            yield return new WaitForSecondsRealtime(_dialogue.Lines[line].Delay);
            StartCoroutine(DialogueCoroutineLineByLine(line + 1));
        }
        else
        {
            yield return new WaitForSecondsRealtime(_spacingDelay);
            StartCoroutine(DialogueCoroutineWordByWord(letter, line));
        }
    }

    private IEnumerator DialogueCoroutineLineByLine(int line)
    {
        //clear dialogue

        if (line < _dialogue.Lines.Count)
        {
            
            
            DialogueBox.TextBox.text = "";
            // add context string

            DialogueBox.TextBox.text += _dialogue.Lines[line].Context + "\n";

            switch (_dialogueType)
            {
                case Type.LetterByLetter:
                    StartCoroutine(DialogueCoroutineLetterByLetter(0, line));
                    break;
                case Type.WordByWord:
                    StartCoroutine(DialogueCoroutineWordByWord(0, line));
                    break;
                case Type.LineByLine:
                    DialogueBox.TextBox.text += _dialogue.Lines[line].Line;
                    //wait for delay
                    yield return new WaitForSecondsRealtime(_dialogue.Lines[line].Delay);
                    line += 1;
                    StartCoroutine(DialogueCoroutineLineByLine(line));
                    break;
            }
        }
        else
        {
            Time.timeScale = 1;
            
            DialogueBox.TextBox.text = "";
        }

        yield return null;
    }
}