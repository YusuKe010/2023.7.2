using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] GameObject _ClearPanel;
    [SerializeField] GameObject _GamePanel;
    // Start is called before the first frame update
    public void NextGame(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    public void Panel()
    {
        _ClearPanel.SetActive(false);
        _GamePanel.SetActive(true);
    }

}
