using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    private int currentMoney = 10000;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void SetMoney(int amount)
    {
        currentMoney = amount;
    }
    public void AddMoney(int amount)
    {
        currentMoney += amount;
    }

    // Subtract money
    public void SubtractMoney(int amount)
    {
        currentMoney -= amount;
        if (currentMoney < 0) currentMoney = 0;
    }
    public int GetMoney()
    {
        return currentMoney;
    }
}
