using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHeightScale : MonoBehaviour
{
    // gameObject references to the projectile prefab and the instantiated clone
    public GameObject projectilePrefab, projectileInstance;
    // gameObject reference to platform the projectile rests on
    public GameObject platform;
    // gameObject reference to target area and physical target object to throw projectile at
    public GameObject targetField, targetObj;
    // floats to track original height and y-position of platform
    private float startHeight, startYposPlatform;
    // floats for player height and arm length
    private float height, armLength;
    // floats to track the original z-position of the target field and object
    public float startZposField, startZposObject;
    // float to scale platform down to have room for projectile to rest on top of it
    private float platformOffset, targetOffset;
    // float to scale the target position back by as the height scales
    private float multiplier;
    // gameObject reference to the scene's ProjectileManager
    public GameObject projectileManager;
    // Scene to track current active scene
    private Scene currScene;
    // int to get index of next scene to load from calibration
    private int nextSceneIndex;
    // bool to control fade
    private bool hasFaded;
    // Gameobject to control screenfades
    public GameObject screenFade;

    void Awake()
    {
        // Sets up starting values for calibration
        startHeight = platform.transform.localScale.y;
        startYposPlatform = platform.transform.position.y;
        startZposField = targetField.transform.TransformPoint(Vector3.zero).z;
        startZposObject = targetObj.transform.TransformPoint(Vector3.zero).z;
        platformOffset = GlobalControl.Instance.platformOffset;
        multiplier = GlobalControl.Instance.multiplier;
        height = GlobalControl.Instance.height;
        armLength = GlobalControl.Instance.armLength;
        targetOffset = GlobalControl.Instance.targetOffset;
        currScene = SceneManager.GetActiveScene();
        hasFaded = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        // For setting up the scene during and post-calibration
        if (GlobalControl.Instance.isRightHanded)
        {
            platform.transform.position = new Vector3(platform.transform.position.x + 1.056f, platform.transform.position.y, platform.transform.position.z);
        }
        AdjustPlatform();
        AdjustTarget();
        if (!currScene.name.Equals("Calibration"))
        {
            SpawnProjectile();
        }
        if (GlobalControl.Instance.progression.Equals(GlobalControl.ProgressionType.Choice))
        {
            GlobalControl.Instance.nextScene = GlobalControl.Scene.Classroom;
        }
        nextSceneIndex = currScene.buildIndex + 1;
    }

    // Update is called once per frame
    void Update()
    {
        height = GlobalControl.Instance.height;
        armLength = GlobalControl.Instance.armLength;

        if (GlobalControl.Instance.hasCalibrated && !hasFaded &&
            (OVRInput.GetUp(OVRInput.RawButton.X) || Input.GetKeyUp(KeyCode.KeypadEnter)))
        {
            hasFaded = true;
            LoadSceneHelper();
        }
    }

    // Script to adjust distance of target from player based on player height
    // NOTE: May not work properly with quick compromise made for Quest testing
    public void AdjustTarget()
    {
        //Debug.Log("Adjusting Target");
        targetField.transform.position = new Vector3(targetField.transform.position.x, targetField.transform.position.y, startZposField + ((height * multiplier) - targetOffset));
        targetObj.transform.position = new Vector3(targetObj.transform.position.x, targetObj.transform.position.y, startZposObject + ((height * multiplier) - targetOffset));
    }

    // Script to adjust platform height for projectile to rest on, matching where the player's hand will be
    public void AdjustPlatform()
    {
        //Debug.Log("Adjusting Platform");
        platform.transform.localScale = new Vector3(platform.transform.localScale.x, startHeight + armLength - platformOffset, platform.transform.localScale.z);
        platform.transform.position = new Vector3(platform.transform.position.x, startYposPlatform + (platform.transform.localScale.y / 2), platform.transform.position.z);
    }

    // Spawns the projectile to be thrown
    public void SpawnProjectile()
    {
        Debug.Log("DEBUG ----- Spawning " + projectilePrefab.name);
        // Spawns projectile based on provided prefab, updates corresponding arrays for projectiles, their positions, and their rotations
        projectileInstance = (GameObject)Instantiate(projectilePrefab, new Vector3(platform.transform.position.x, 
            platform.transform.position.y + platform.transform.localScale.y, 
            platform.transform.position.z), Quaternion.identity);
        GameObject[] newProjectiles = new GameObject[projectileManager.GetComponent<ProjectileManager>().projectiles.Length + 1];
        int i;
        for (i = 0; i < projectileManager.GetComponent<ProjectileManager>().projectiles.Length; i++)
        {
            newProjectiles[i] = projectileManager.GetComponent<ProjectileManager>().projectiles[i];
        }
        newProjectiles[i] = projectileInstance;
        projectileManager.GetComponent<ProjectileManager>().projectiles = newProjectiles;
        projectileManager.GetComponent<ProjectileManager>().AdditionalArrays();
    }

    // Function to fade
    IEnumerator Fade()
    {
        var fadeScript = screenFade.GetComponent<OVRScreenFade>();

        if (fadeScript)
            yield return new WaitForSeconds(fadeScript.fadeTime);
        LoadNextScene();
    }

    // Function that can be publicly called to run Fade()
    public void LoadSceneHelper()
    {
        screenFade.GetComponent<OVRScreenFade>().FadeOut();
        StartCoroutine(Fade());
    }

    // Function to load next scene in progression mode
    public void LoadNextScene()
    {
        // If player is in Choice progression mode, move to the selected nextScene
        if (GlobalControl.Instance.progression.Equals(GlobalControl.ProgressionType.Choice))
        {
            GlobalControl.Instance.NextScene();
        }
        // Otherwise, when in other modes, move linearly to the next scene
        Debug.Log("DEBUG ----- Loading next scene: " + SceneManager.GetSceneByBuildIndex(nextSceneIndex).name);
        SceneManager.LoadScene(nextSceneIndex);
    }
}
