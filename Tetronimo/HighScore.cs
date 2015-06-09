using System;
using System.Collections.Generic;
using System.Xml;

namespace Tetromino
{
    /// <summary>
    /// An entry in the high scores table consists of the player's name,
    /// their high score, and the number of lines that they cleared.
    /// </summary>
    public struct HighScoreEntry
    {
        public readonly string name;
        public readonly uint score;
        public readonly uint lines;

        public HighScoreEntry(string name, uint score, uint lines)
        {
            if (name == null)
                this.name = "";
            else
                this.name = name;

            this.score = score;
            this.lines = lines;
        }

        public static int Compare(HighScoreEntry e1, HighScoreEntry e2)
        {
            if (e1.score > e2.score)
                return -1;
            else if (e2.score > e1.score)
                return 1;
            else  // If two scores are equal, then the one with fewer lines is better
                return (e1.lines < e2.lines ? -1 : (e2.lines < e1.lines ? 1 : 0));
        }
    };

    public class HighScoresList
    {
        public static readonly int NumberOfScores = 10;
        private static string RootTag = "scores";
        private static string ScoreTag = "score";
        private static string EntryTag = "entry";
        private static string NameTag = "name";
        private static string LinesTag = "lines";

        List<HighScoreEntry> scores = new List<HighScoreEntry>();

        public HighScoresList()
        {
            string name;
            uint score, lines;

            try
            {
                var doc = new XmlDocument();
                doc.Load(Properties.Resources.HighScoresFilename);

                // Load each high score from file

                foreach (XmlElement element in doc.GetElementsByTagName(EntryTag))
                {
                    try
                    {
                        name = element.GetElementsByTagName(NameTag).Item(0).InnerText;
                        uint.TryParse(element.GetElementsByTagName(ScoreTag).Item(0).InnerText,
                            out score);
                        uint.TryParse(element.GetElementsByTagName(LinesTag).Item(0).InnerText,
                            out lines);

                        Insert(name, score, lines);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            catch(System.IO.FileNotFoundException)
            {
                // Load the default high scores if there was an error
                // loading the high score list from file
                LoadDefaultScores();
            }
            catch (System.Xml.XmlException)
            {
                LoadDefaultScores();
            }
        }

        /// <summary>
        /// Save the high score list to file.
        /// </summary>

        public void Save()
        {
            var doc = new XmlDocument();
            var scoresElement = doc.CreateElement("", RootTag, "");

            foreach (var score in scores)
            {
                var entryElement = doc.CreateElement("", EntryTag, "");
                var nameElement = doc.CreateElement("", NameTag, "");
                var scoreElement = doc.CreateElement("", ScoreTag, "");
                var linesElement = doc.CreateElement("", LinesTag, "");

                nameElement.InnerText = score.name;
                scoreElement.InnerText = score.score.ToString();
                linesElement.InnerText = score.lines.ToString();
                entryElement.AppendChild(nameElement);
                entryElement.AppendChild(scoreElement);
                entryElement.AppendChild(linesElement);
                scoresElement.AppendChild(entryElement);
            }

            doc.AppendChild(scoresElement);
            doc.Save(Properties.Resources.HighScoresFilename);
        }

        private void LoadDefaultScores()
        {
            Insert("Adam", 10000, 100);
            Insert("Betty", 9000, 90);
            Insert("Caren", 8000, 80);
            Insert("David", 7000, 70);
            Insert("Eric", 6000, 60);
            Insert("Frank", 5000, 50);
            Insert("Gemma", 4000, 40);
            Insert("Harry", 3000, 30);
            Insert("Iris", 2000, 20);
            Insert("Janet", 1000, 10);
        }

        // Add a new score to the list. Keep the top 10 scores and
        // discard the rest (if any)

        public void Insert(string name, uint score, uint lines)
        {
            scores.Add(new HighScoreEntry(name, score, lines));
            scores.Sort(HighScoreEntry.Compare);

            if (scores.Count > NumberOfScores)
                scores.RemoveRange(NumberOfScores, 1 + (scores.Count + NumberOfScores));
        }

        /// <summary>
        /// Is this new score a high score?
        /// </summary>
        /// <param name="score">The score to be compared with the top scores</param>
        /// <param name="lines">The number of lines to be compared with the top scores</param>
        /// <returns>True, if score is higher than the lowest high score. False, otherwise</returns>

        public bool IsHighScore(uint score, uint lines)
        {
            if (scores.Count >= NumberOfScores)
            {
                return HighScoreEntry.Compare(new HighScoreEntry(null, score, lines),
                    scores[NumberOfScores - 1]) == -1;
            }
            else
                return true;
        }

        public IEnumerable<HighScoreEntry> GetScores()
        {
            return scores;
        }
    }
}