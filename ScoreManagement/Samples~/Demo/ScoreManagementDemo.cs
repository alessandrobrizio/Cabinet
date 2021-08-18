using UnityEngine;
using AlessandroBrizio.Cabinet.Core.Samples;

namespace AlessandroBrizio.Cabinet.ScoreManagement.Samples
{
    public class ScoreManagementDemo : DatabaseDemo<ScoresDatabaseTable>
    {
        [SerializeField] private string _playerName;
        [SerializeField] private int _score;
        [SerializeField] private int _seconds;

        public void AddScore()
        {
            table.AddScore(_playerName, _score, _seconds);
            _playerName = default;
            _score = default;
            _seconds = default;
        }

        public void RetrieveTopScores()
        {
            foreach (ScoreEntry scoreEntry in table.GetTopScores(3))
            {
                Debug.Log($"{scoreEntry.name} {scoreEntry.score} {scoreEntry.seconds} {scoreEntry.datetime}");
            }
        }

        public void RemoveAllScores()
        {
            table.RemoveAllScores();
        }
    }
}
