using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem
{
    public static class GameScore
    {
        public static int currentScoreMultiplier = 1;
        public static int maxMultiplier = 2;

        public static Dictionary<string, int> scores = new Dictionary<string, int>
        {
            { ConstNames.MAIN_SCORE, 0 },
            { ConstNames.DISTANCE_COVERED, 0 },
        };

        public static void ResetScores()
        {
            List<string> keys = new List<string>();

            foreach (var score in scores)
            {
                var key = score.Key;
                keys.Add(key);
            }

            for (int i = 0; i < scores.Count; i++)
            {
                scores[keys[i]] = 0;
            }

            currentScoreMultiplier = 1;
        }
    }
}
