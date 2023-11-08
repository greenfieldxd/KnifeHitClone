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
    
    public static void SetOranges(int newOrangeScore)
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

    public static void SetKnifeType(KnifeType knifeType)
    {
        PlayerPrefs.SetString(AppConstans.KNIFE_TYPE, knifeType.ToString());
        Debug.Log("" + knifeType);
    }

    public static string GetKnifeType()
    {
        var knifeTypeString = PlayerPrefs.GetString(AppConstans.KNIFE_TYPE, "DEFAULT");

        return knifeTypeString;
    }

    public static bool IsKnifeOpened(KnifeType knifeType)
    {
        if (PlayerPrefs.GetInt(AppConstans.KNIFE_IS_OPENED + knifeType, 0) == 0) return false;
        else return true;
    }
    
    public static void SetOpenStatusForKnife(KnifeType knifeType)
    {
        PlayerPrefs.SetInt(AppConstans.KNIFE_IS_OPENED + knifeType, 1);
    }
}
