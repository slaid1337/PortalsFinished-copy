using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LvlItem : MonoBehaviour
{
    [SerializeField] private Button _lvlButton;
    [SerializeField] private int _sceneIndex;
    [SerializeField] private GameObject _lock;
    [SerializeField] private Text _text;

    private void Start()
    {
        _lvlButton.onClick.AddListener(LoadScene);
        _text.text = "Уровень " + _sceneIndex;

        if (_sceneIndex != 1)
        {
            if (PlayerPrefs.GetInt("CompleteLvl" + (_sceneIndex - 1), 0) == 1)
            {
                _lvlButton.interactable = true;
                _lock.SetActive(false);
            }
            else
            {
                _lvlButton.interactable = false;
                _lock.SetActive(true);
            }
        }
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneWait());
    }

    private IEnumerator LoadSceneWait()
    {
        LvlTransition.Instance.CloseLvl();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(_sceneIndex);
    }
}