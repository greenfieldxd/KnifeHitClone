using UnityEngine;

public static class DataManager
{
    public static void SetBestScore(int newBestScore)
    {
        PlayerPrefs.SetInt(AppConstans.BEST_SCORE, newBestScore);
    }
    
    public static int GetBestScore()
    {
        return PlayerPrefs.GetInt(AppConstans.BEST_SCORE, 0);
    }
    
    public static void SetOrangeScore(int newOrangeScore)
    {
        PlayerPrefs.SetInt(AppConstans.ALL_ORANGES, newOrangeScore);
    }
    
    public static int GetAllOranges()
    {
        return PlayerPrefs.GetInt(AppConstans.ALL_ORANGES, 0);
    }
    public static void SetBestStage(int newBestStage)
    {
        PlayerPrefs.SetInt(AppConstans.BEST_STAGE, newBestStage);
    }
    
    public static int GetBestStage()
    {
        return PlayerPrefs.GetInt(AppConstans.BEST_STAGE, 0);
    }
}
