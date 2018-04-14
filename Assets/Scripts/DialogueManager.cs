using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(AudioSource))]
public class DialogueManager : MonoBehaviour
{
    public enum Type
    {
        LetterByLetter,
        WordByWord,
        LineByLine
    }

    private AudioSource _audioSource;


    private Collider2D _collider;
    [SerializeField] private Dialogue _dialogue;

    [SerializeField] private Type _dialogueType;

    [SerializeField] private float _spacingDelay;

    [SerializeField] private string _tagTrigger;
    private bool _stop;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _audioSource = GetComponent<AudioSource>();
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
            _collider.enabled = false;
            Time.timeScale = 0;
            //DialogueBox.TextBox.gameObject.transform.parent.gameObject.SetActive(true);
            _stop = false;
            StartCoroutine(DialogueCoroutineLineByLine(0));
        }
    }


    private IEnumerator DialogueCoroutineLetterByLetter(int letter, int line)
    {
        DialogueBox.TextBox.text += _dialogue.Lines[line].Line[letter];

        if (_dialogue.Lines[line].Line.Length == letter + 1)
        {
            //wait for delay
            yield return new WaitForSecondsRealtime(_dialogue.Lines[line].Delay);
            _stop = false;
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
        } while (!(current[current.Length - 1] == ' ' || _dialogue.Lines[line].Line.Length >= letter));

        DialogueBox.TextBox.text += current;

        if (_dialogue.Lines[line].Line.Length == letter + 1)
        {
            //wait for delay
            yield return new WaitForSecondsRealtime(_dialogue.Lines[line].Delay);
            _stop = false;
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
        if (!_stop)
        {
            _stop = true;
            //clear dialogue

            if (line < _dialogue.Lines.Count)
            {
                DialogueBox.TextBox.text = "";
                // add context string
                if (_dialogue.Lines[line].Audio != null)
                    _audioSource.PlayOneShot(_dialogue.Lines[line].Audio);
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

                //DialogueBox.TextBox.gameObject.transform.parent.gameObject.SetActive(false);
            }
        }

        yield return null;
    }
}