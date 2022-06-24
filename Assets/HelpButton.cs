using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HelpButton : MonoBehaviour
{

    public void onHelpClick()
    {
        SceneManager.LoadScene("Help");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
