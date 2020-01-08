using UnityEngine;
using static Constants;

public class BoatController : MonoBehaviour
{

    public float shipSpeed = 20;
    float verticalInput;
    float movementFactor;
    float horizontalInput;
    float steerFactor;
    int numberOfDebris;
    Rigidbody rigidBody;
    PlayerDataController pDController;

    private void Awake()
    {
    }

    private void Start()
    {
        numberOfDebris = GameObject.FindGameObjectsWithTag("Wreck").Length;
        rigidBody = GetComponent<Rigidbody>();
        pDController = FindObjectOfType<PlayerDataController>().GetComponent<PlayerDataController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        if (verticalInput > 0)
        {
            rigidBody.velocity = transform.forward * verticalInput * shipSpeed;
        }
        if (horizontalInput > 0)
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * shipSpeed, Space.World);
        }
        else if (horizontalInput < 0)
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * shipSpeed, Space.World);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        switch (collision.gameObject.tag) {
            case "Wreck":
                Debug.Log("Hay: " + numberOfDebris);
                numberOfDebris--;
                Destroy(collision.gameObject);
                addRandomResources();
                break;
            default:
                break;
        }
    }

    private void addRandomResources() {
        int quantity = Random.Range(1,6);
        int resourceType = Random.Range(1, 11);
        switch (resourceType) {
            case 1: // water
            case 2: 
            case 3:
                pDController.PlayerData.CurrentWater += quantity * 2;
                break;
            case 4: // food
            case 5: 
            case 6: 
                pDController.PlayerData.CurrentFood += quantity * 2;
                break;
            case 7: // Medicine
                pDController.PlayerData.CurrentMedicine += quantity;
                break;
            case 8: // Gold
                pDController.PlayerData.CurrentGold += quantity * 10;
                break;
            default:
                // No encuentra nada, puede pasar.
                break;

        }

        Debug.Log("quedan: " + numberOfDebris);
        if (numberOfDebris == 0) {
            pDController.Save();
            SceneLoaderService sceneLoaderService = new SceneLoaderService();
            sceneLoaderService.LoadScene(SceneNames.MAIN_SCENE);
        }
    }
}
