/**
 * Manage the overall flow
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2024/01/18
 */

using UnityEngine;

public class SceneDirector_Script : MonoBehaviour
{
    private GameObject Main_Canvas, InGame_Canvas;

    // Specifies
    private void Awake()
    {
        gameObject.AddComponent<FileController_Script>();

        Main_Canvas = GameObject.Find("Main_Canvas");
        InGame_Canvas = GameObject.Find("InGame_Canvas");
    }

    // Main_Canvas set active and set Main_Canvas set sleep when game start
    private void Start()
    {
        MainOn();
    }

    // Main_Canvas set active
    public void MainOn()
    {
        Main_Canvas.SetActive(true);
        Main_Canvas.GetComponent<MainDirector_Script>().Init();

        InGame_Canvas.SetActive(false);
    }

    // InGame_Canvas set active
    public void InGameOn()
    {
        InGame_Canvas.SetActive(true);
        InGame_Canvas.GetComponent<InGameDirector_Script>().NewGambler();

        Main_Canvas.SetActive(false);
    }
}