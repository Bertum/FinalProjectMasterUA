using UnityEngine;

public class DomeController : MonoBehaviour
{
    private void Start()
    {
        var seaTiles = GameObject.FindGameObjectsWithTag("SeaTile");
        var sandTiles = GameObject.FindGameObjectsWithTag("SandTile");
        var sandDecorations = GameObject.FindGameObjectsWithTag("SandDecorations");
        var borders = GameObject.FindGameObjectsWithTag("Border");
        var islands = GameObject.FindGameObjectsWithTag("Island");
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
        foreach (var tile in borders)
        {
            var renderers = tile.GetComponentsInChildren<MeshRenderer>();
            foreach (var render in renderers)
            {
                render.enabled = false;
            }
        }
        foreach (var tile in islands)
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
        EnableRender(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        DisableRender(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnableRender(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        DisableRender(collision.gameObject);
    }

    private void EnableRender(GameObject obj)
    {
        switch (obj.tag)
        {
            case "SeaTile":
            case "SandTile":
                obj.gameObject.GetComponent<MeshRenderer>().enabled = true;
                break;
            case "SandDecorations":
            case "Border":
            case "Island":
                var renderers = obj.GetComponentsInChildren<MeshRenderer>();
                foreach (var render in renderers)
                {
                    render.enabled = true;
                }
                break;
            default:
                break;
        }
    }

    private void DisableRender(GameObject obj)
    {
        switch (obj.tag)
        {
            case "SeaTile":
            case "SandTile":
                obj.gameObject.GetComponent<MeshRenderer>().enabled = false;
                break;
            case "SandDecorations":
            case "Border":
            case "Island":
                var renderers = obj.GetComponentsInChildren<MeshRenderer>();
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
