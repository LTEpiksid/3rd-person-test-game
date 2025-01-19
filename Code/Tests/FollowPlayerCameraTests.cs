using NUnit.Framework;
using UnityEngine;
using System.Reflection;

public class FollowPlayerCameraTests
{
    private GameObject cameraGameObject;
    private FollowPlayerCamera followPlayerCamera;
    private GameObject playerGameObject;

    [SetUp]
    public void SetUp()
    {
        // Set up the game objects and components
        cameraGameObject = new GameObject();
        followPlayerCamera = cameraGameObject.AddComponent<FollowPlayerCamera>();
        playerGameObject = new GameObject();
        playerGameObject.tag = "Player";
        followPlayerCamera.player = playerGameObject.transform;

        followPlayerCamera.offset = new Vector3(0f, 2f, -5f);
        followPlayerCamera.rotationSpeed = 2f;
        followPlayerCamera.smoothSpeed = 0.5f; // Adjusted smoothSpeed

        playerGameObject.transform.position = new Vector3(0f, 0f, 0f); // Position player at origin
        cameraGameObject.transform.position = playerGameObject.transform.position + new Vector3(0f, 1f, -4f);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(cameraGameObject);
        Object.DestroyImmediate(playerGameObject);
    }

    [Test]
    public void FollowPlayer_LooksAtPlayer()
    {
        // Act
        followPlayerCamera.FollowPlayer();

        // Assert
        Vector3 forward = cameraGameObject.transform.forward;
        Vector3 toPlayer = (playerGameObject.transform.position - cameraGameObject.transform.position).normalized;

        float tolerance = 0.01f;
        Assert.That(Vector3.Dot(forward, toPlayer), Is.GreaterThan(1 - tolerance));
    }
}
