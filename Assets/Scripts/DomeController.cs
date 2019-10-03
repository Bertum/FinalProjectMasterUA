using UnityEngine;

public class DomeController : MonoBehaviour
{
    private void Start()
    {
        var seaTiles = GameObject.FindGameObjectsWithTag("SeaTile");
        var sandTiles = GameObject.FindGameObjectsWithTag("SandTile");
        var sandDecorations = GameObject.FindGameObjectsWithTag("SandDecorations");
        foreach (var tile in seaTiles)
        {
            tile.GetComponent<MeshRenderer>().enabled = false;
        }
        foreach (var tile in sandTiles)
        {
            tile.GetComponent<MeshRenderer>().enabled = false;
        }
        foreach (var tile in sandDecorations)
        {
            var renderers = tile.GetComponentsInChildren<MeshRenderer>();
            foreach (var render in renderers)
            {
                render.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "SeaTile":
            case "SandTile":
                other.gameObject.GetComponent<MeshRenderer>().enabled = true;
                break;
            case "SandDecorations":
                var renderers = other.GetComponentsInChildren<MeshRenderer>();
                foreach (var render in renderers)
                {
                    render.enabled = true;
                }
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "SeaTile":
            case "SandTile":
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                break;
            case "SandDecorations":
                var renderers = other.GetComponentsInChildren<MeshRenderer>();
                foreach (var render in renderers)
                {
                    render.enabled = false;
                }
                break;
            default:
                break;
        }
    }
}
