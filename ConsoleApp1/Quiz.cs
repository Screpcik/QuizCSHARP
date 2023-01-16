using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    public class Quiz
    {
        public List<Question> Questions { get; set; }
        public Player Player { get; set; }
        public Quiz()
        {
            LoadQuestionsFromFile("questions.txt");
        }

        private void LoadQuestionsFromFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var counter = 0;
            Questions = new List<Question>();

            var currentQuestion = new Question();

            foreach (var line in lines)
            {
                if (counter == 6)
                {
                    counter = 0;
                }
                if (counter == 0)
                {
                    currentQuestion.Title= line;
                }
                if (counter == 1)
                {
                    currentQuestion.AnswerA = line;
                }
                if (counter == 2)
                {
                    currentQuestion.AnswerB = line;
                }
                if (counter == 3)
                {
                    currentQuestion.AnswerC = line;
                }
                if (counter == 4)
                {
                    currentQuestion.AnswerD = line;
                }
                if (counter == 5)
                {
                    currentQuestion.RightAnswerLetter = line[0].ToString();
                    currentQuestion.Score = int.Parse(line[1].ToString());

                    var newQuestion = new Question
                    {
                        Title= currentQuestion.Title,
                        AnswerA= currentQuestion.AnswerA,
                        AnswerB= currentQuestion.AnswerB,
                        AnswerC= currentQuestion.AnswerC,
                        AnswerD = currentQuestion.AnswerD,
                        RightAnswerLetter= currentQuestion.RightAnswerLetter,
                        Score= currentQuestion.Score
                    };

                    Questions.Add(newQuestion);
                }               
                counter++;
            }
        }

        public void Start()
        {
            Player = new Player();

            Console.WriteLine("Witaj.\nJak masz na imię?");
            
            Player.Name = Console.ReadLine();

            Player.Score = 0;

            Player.CurrentQuestion = 1;

            for (var i = 0; i < Questions.Count; i++) 
            {
                var score = ShowQuestion(Player.CurrentQuestion);
                Player.Score += score;
                Player.CurrentQuestion++;
            }

            Console.WriteLine("Brawo!" + Player.Name + "\nUdało Ci się uzyskać " + Player.Score + " punktów. \nGratulacje");
        }

        public int ShowQuestion(int QuestionCounter)
        {
            var currentQuestionsToShow = Questions[QuestionCounter - 1];
            Console.WriteLine("Pytanie: " + currentQuestionsToShow.Title);
            Console.WriteLine("A:" + currentQuestionsToShow.AnswerA);
            Console.WriteLine("B:" + currentQuestionsToShow.AnswerB);
            Console.WriteLine("C:" + currentQuestionsToShow.AnswerC);
            Console.WriteLine("D:" + currentQuestionsToShow.AnswerD);

            var userResponse = Console.ReadLine();

            if(userResponse.ToLower() == currentQuestionsToShow.RightAnswerLetter.ToLower()) 
            {
                Console.WriteLine("Nieźle! Dobra odpowiedź.");
                return currentQuestionsToShow.Score;
            }

            Console.WriteLine("Zła odpowiedź :/");
            return 0;
        }
    }
}
