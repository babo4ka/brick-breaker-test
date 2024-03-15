using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenBallScript : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;

    [SerializeField]
    private GameObject sniperBallManagingPanel;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenNewBall);
    }

    private void OpenNewBall()
    {
        bool opened = gameManager.GetComponent<BallManager>().OpenNewBall(BallType.SNIPER);

        if(opened)
        {
            sniperBallManagingPanel.SetActive(true);
        }
    }
}
