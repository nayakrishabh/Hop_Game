using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField] private Rigidbody ballrb;
    [SerializeField] private int platformCount = 20; // Easily adjustable for levels
    public static bool startflag = false;
    public Vector3 forcedirection;
    public float power;

    private PoolManager poolManager;
    private Queue<GameObject> platformPool;
    private List<GameObject> activePlatforms = new List<GameObject>();
    private float platformSpacing = 6.1f;
    private float lastPlatformZ = 0f;


    private IEnumerator InitPlatformSpawning() {
        // Wait a frame to ensure PoolManager's Start() is complete
        yield return null;

        poolManager = PoolManager.Instance;

        if (poolManager == null) {
            Debug.Log("PoolManager is not yet initialized.");
            yield break;
        }

        var dict = poolManager.getPoolDictionary();

        Debug.Log("PoolManager contains " + dict.Count + " pools.");

        if (!dict.ContainsKey("Panel")) {
            Debug.Log("PoolManager does not contain a 'Panel' pool!");
            yield break;
        }

        platformPool = dict["Panel"];
        int initialPlatformCount = platformPool.Count;
        lastPlatformZ = 0f;

        #region PlatformSpawner
        for (int i = 0; i < initialPlatformCount; i++) {
            float horpos = Random.Range(-2.6f, 2.6f);
            Vector3 pos = new Vector3(horpos, 0, lastPlatformZ + platformSpacing);

            GameObject platform = platformPool.Dequeue();
            platform.transform.position = pos;
            platform.SetActive(true);
            activePlatforms.Add(platform);

            lastPlatformZ += platformSpacing;
        }
        #endregion
    }

    private void Start() {
        StartCoroutine(InitPlatformSpawning());
    }



    void Update()
    {
        //Press 'R' to Restart the Game
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(0);
            startflag = false;
        }
        //Click 'Left Mouse Button' to start the Hop Of the Ball
        if (!startflag) {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                ballrb.AddForce(forcedirection * power, ForceMode.Force);
                startflag = true;
            }
        }
        // Handle platform recycling if game has started
        if (activePlatforms.Count > 0 && startflag) {
            float ballZ = ballrb.position.z;

            // ✅ Iterate backwards to safely remove items from list
            for (int i = activePlatforms.Count - 1; i >= 0; i--) {
                GameObject platform = activePlatforms[i];

                // Check if the platform is far behind the player
                if (platform.transform.position.z < ballZ - 10f) {
                    platform.SetActive(false);
                    platformPool.Enqueue(platform);
                    activePlatforms.RemoveAt(i);

                    // ✅ Spawn a new platform ahead
                    float horpos = Random.Range(-2.6f, 2.6f);
                    Vector3 newPos = new Vector3(horpos, 0, lastPlatformZ + platformSpacing);

                    if (platformPool.Count > 0) {
                        GameObject newPlatform = platformPool.Dequeue();
                        newPlatform.transform.position = newPos;
                        newPlatform.SetActive(true);
                        activePlatforms.Add(newPlatform);

                        lastPlatformZ += platformSpacing;
                    }
                }
            }
        }
        //if (activePlatforms.Count > 0 && startflag) {
        //    float ballZ = ballrb.position.z;

        //    for(int i = 0; i < activePlatforms.Count; i++) {
        //        GameObject platform = activePlatforms[i];
        //        if (platform.transform.position.z < ballZ - 10f) {
        //            // If the platform is behind the ball by more than 10 units, deactivate it
        //            platform.SetActive(false);
        //            platformPool.Enqueue(platform);
        //            activePlatforms.RemoveAt(i);
        //        }
        //    }
        //}
    }
}
