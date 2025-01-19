using NUnit.Framework;
using UnityEngine;

public class ProjectileControllerTests
{
    private GameObject projectileGameObject;
    private ProjectileController projectileController;

    [SetUp]
    public void SetUp()
    {
        // Create GameObject and add ProjectileController component
        projectileGameObject = new GameObject();
        projectileController = projectileGameObject.AddComponent<ProjectileController>();

        // Mimic the Start method
        projectileController.GetType().GetMethod("Start", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.Invoke(projectileController, null);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(projectileGameObject);
    }

    [Test]
    public void SetProperties_AssignsValuesCorrectly()
    {
        // Arrange
        float speed = 20f;
        int damage = 10;
        float range = 30f;

        // Act
        projectileController.SetProperties(speed, damage, range);

        // Assert
        Assert.AreEqual(speed, projectileController.GetType().GetField("speed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.GetValue(projectileController));
        Assert.AreEqual(damage, projectileController.GetType().GetField("damage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.GetValue(projectileController));
        Assert.AreEqual(range, projectileController.GetType().GetField("range", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.GetValue(projectileController));
    }

    [Test]
    public void MoveProjectile_UpdatesPosition()
    {
        // Arrange
        projectileController.SetProperties(10f, 10, 30f);
        Vector3 initialPosition = projectileGameObject.transform.position;

        // Act
        for (int i = 0; i < 10; i++) // Simulate 10 frames
        {
            projectileController.GetType().GetMethod("MoveProjectile", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.Invoke(projectileController, null);
        }

        // Assert
        Assert.AreNotEqual(initialPosition, projectileGameObject.transform.position);
    }
}
