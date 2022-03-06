using System.Collections.Generic;
using System.Linq;

namespace SeaButtle2
{
 /// <summary>
    /// Player абстрактный класс игрока, все действующие лица должны наследоваться от него
    /// </summary>
    public class Player
    {
        private int id;
        private List<Player> enemies;
        private Field field;

        public string Name { get; set; }

        public Player NextPlayer { get; set; }

        public int ID
        {
            get { return id; }
        }

        public bool Lost { get; }

        public Player(int id, int[,] field)
        {
           
        }

        /// <summary>
        /// DoMove делает ход игрока(стреляем в противников)
        /// </summary>
        /// <param name="cmd"> Команда выстрела </param>
        /// <returns></returns>
        public ShootCommandResult DoMove(Point point)
        {
            var enemyFields = new List<string>();
            foreach (var enemy in enemies)
            {
                // противники принимают наш жестокий удар
                enemy.GetShot(point);
                enemyFields.Add(enemy.GetHiddenField());
            }

            return new ShootCommandResult(enemyFields);
        }

        /// <summary>
        /// GetShot принять вражеский выстрел
        /// </summary>
        /// <param name="point"></param>
        public ShootResult GetShot(Point point)
        {
            // здесь обработка выстрела по полю field игрока
            // если игрок проиграл, здесь должно выставляться состояние
        }

        /// <summary>
        /// GetHiddenField вернуть поле игрока, скрыв подробности от других игроков
        /// </summary>
        /// <returns></returns>
        public string GetHiddenField()
        {
            return field.ToString();
        }

        /// <summary>
        /// Игрок победил, если все его противники проиграли
        /// </summary>
        /// <returns> Поле игрока, закрыв точки в которые не стреляли </returns>
        public bool IsWin
        {
            get
            {
                if (enemies.All(enemy => enemy.Lost))
                {
                    return true;
                }

                return false;
            }

        }

        /// <summary>
        /// ToString вернуть поле игрока в виде "как есть" для игрока
        /// </summary>
        /// <returns> Поле игрока со всеми данными </returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
