using System;
using System.Collections.Generic;
using System.Linq;

namespace SeaButtle2
{
    /// <summary>
    /// Game - собственно игра
    /// Самый верхний уровень, всё начинается с метода Route
    /// </summary>
    public class Game
    {
        public bool Over { get; set; }
        private List<Player> players;
        private Player currentPlayer;

        public Game(params Player[] players)
        {
            Over = false;
            this.players = players.ToList();

            // TODO по умолчанию первый игрок ходит первым
        }

        /// <summary>
        /// ParseAndExecuteCommand роутер входящих команд
        /// </summary>
        /// <param name="input"> Пользовательский ввод </param>
        /// <param name="playerId"></param>
        /// <returns> Результат выполнения команды </returns>
        public string ParseAndExecuteCommand(string input)
        {
            switch (input)
            {
                case "status":
                    return HandleStatusCommand();
                default:
                    // выстрел принимается в виде буква цифра, например a1
                    var letterPart = input[0];
                    var numberPart = input.Substring(1);

                    if (!char.IsLetter(letterPart))
                    {
                        throw new ArgumentException();
                    }
                    if (int.TryParse(numberPart, out var y))
                    {
                        var x = letterPart - 'a';
                        return HandleShootCommand(new Point(x, y));
                    }

                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// HandleShootCommand обработка команды выстрела
        /// </summary>
        /// <param name="point"> Точка - координаты выстрела</param>
        /// <param name="playerId"> Идентификатор игрока, который стреляет </param>
        /// <returns></returns>
        public string HandleShootCommand(Point point)
        {
            var result = currentPlayer.DoMove(point);
            if (currentPlayer.IsWin)
            {
                this.Over = true;
            }

            this.currentPlayer = currentPlayer.NextPlayer;
            return result.ToString();
        }

        /// <summary>
        /// HandleStatusCommand обработка команды статус - возвращает текущее поле игрока
        /// </summary>
        /// <returns></returns>
        public string HandleStatusCommand()
        {

            // TODO
            return "";
        }


    }
}
