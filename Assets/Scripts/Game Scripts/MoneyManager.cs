using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    private int currentMoney = 10000;

    private void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            // If not, set instance to this
            instance = this;
            // Make sure it doesn't get destroyed on scene load
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Set money value
    public void SetMoney(int amount)
    {
        currentMoney = amount;
    }

    // Add money
    public void AddMoney(int amount)
    {
        currentMoney += amount;
    }

    // Subtract money
    public void SubtractMoney(int amount)
    {
        currentMoney -= amount;
        if (currentMoney < 0) currentMoney = 0; // Prevent negative money
    }

    // Get current money
    public int GetMoney()
    {
        return currentMoney;
    }
}
