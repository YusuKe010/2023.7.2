using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class retry : MonoBehaviour
{
    [SerializeField] GameObject _playPanel;
    [SerializeField] GameObject _clearPanel;
    // Start is called before the first frame update
    public void Panel()
    {
        Debug.Log("‚¨‚µ‚½");
        _playPanel.SetActive(true);
        _clearPanel.SetActive(false);
    }

}
