using System;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private RecruitmentController recruitmentController;

    private void Awake()
    {
        recruitmentController = GameObject.FindObjectOfType<RecruitmentController>().GetComponent<RecruitmentController>();
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
            }
        }
    }
}
