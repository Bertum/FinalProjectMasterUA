using UnityEngine;

public class RandomSkinSelector : MonoBehaviour
{
    private void Awake()
    {
        var skins = GetComponentsInChildren<SkinnedMeshRenderer>();
        var randomSkin = Random.Range(0, skins.Length - 1);
        for (int i = 0; i < skins.Length; i++)
        {
            if (randomSkin != i)
            {
                skins[i].enabled = false;
            }
        }
    }
}
