using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseController : MonoBehaviour
{
    private RecruitmentController recruitmentController;
    private PlayerDataController pDataController;
    private SceneLoaderService sceneLoader;

    private void Awake()
    {
        sceneLoader = new SceneLoaderService();
        recruitmentController = GameObject.FindObjectOfType<RecruitmentController>().GetComponent<RecruitmentController>();
        pDataController = GameObject.FindObjectOfType<PlayerDataController>();        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 300))
            {
                if (hit.transform.gameObject.tag.Contains("Island"))
                {
                    string firstSplit = hit.transform.gameObject.name.Split(new[] { "Island" }, StringSplitOptions.None)[1];
                    string level = firstSplit.Split(new[] { "(Clone)" }, StringSplitOptions.None)[0];
                    recruitmentController.OpenShop(Convert.ToInt32(level));
                }
                if (hit.transform.gameObject.tag.Contains("BattleEvent")) {
                    pDataController.PlayerData.EventDifficulty = UnityEngine.Random.Range(1, 3);
                    //SaveData
                    pDataController.Save();
                    //Load new Scene with Parameters
                    sceneLoader.LoadScene(SceneNames.BATTLE_SCENE);
                }
                if (hit.transform.gameObject.tag.Contains("SinkEvent")) {
                    pDataController.PlayerData.EventDifficulty = UnityEngine.Random.Range(1, 3);
                    //SaveData
                    pDataController.Save();
                    //Load new Scene with Parameters
                    sceneLoader.LoadScene(SceneNames.BATTLE_SCENE);
                }
            }
        }
    }
}
