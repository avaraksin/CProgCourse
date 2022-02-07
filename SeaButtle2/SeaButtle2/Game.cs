using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaButtle2
{
    
    public class Game
    {
        public bool isGameOn;
        Player hum;
        BilPlayer bil;

        public Game()
        {
            isGameOn = false;
        }

        public PointStatus GetPointStatus(Point point)
        {
            return hum.field.GetPointStatus(point);
        }

        public void StartGame()
        {
            isGameOn = true;
            hum = new Player();
            bil = new BilPlayer();
        }

        public void PlayGame()
        {
            bool overmove; // Переход хода
            Parser parser = new Parser();
            bil.StratOn = false;
            while (isGameOn)
            {
                overmove = false;
                while (!overmove) // Ход игрока
                {
                    string answer = hum.SetMove();
                    switch(answer.ToLower())
                    {
                        case "status":
                            GamePresenter.PrintAreaWithShips(hum.field.field);
                            break;

                        case "exit":
                            GamePresenter.PrintString("Ваша карта: ");
                            GamePresenter.PrintAreaWithShips(hum.field.field);
                            GamePresenter.PrintOverRow();
                            GamePresenter.PrintString("Карта компьютера: ");
                            GamePresenter.PrintAreaWithShips(bil.field.field);
                            GamePresenter.PrintOverRow();
                            GamePresenter.PrintString("Спасибо за игру!");
                            isGameOn = false;
                            overmove = true;
                            break;
                        
                        default:
                            PointStatus movestatus = bil.field.SetShoot(parser.GetPoint(answer));
                            switch (movestatus)
                            {
                                case PointStatus.ILLEGAL:
                                    GamePresenter.PrintString("Неправильный ход!");
                                    break;

                                case PointStatus.AWAY:
                                    GamePresenter.PrintString("Мимо!");
                                    overmove=true;
                                    break;

                                case PointStatus.DECKSUCK:
                                    GamePresenter.PrintString("Ранен!");
                                    break;

                                case PointStatus.SHIPSUCK:
                                    GamePresenter.PrintString("Потоплен!");
                                    break;

                            }

                            if (movestatus != PointStatus.ILLEGAL)
                            {
                                GamePresenter.PrintAreaWithoutShips(bil.field.field);
                            }
                            break;
                    }
                }

                if (isGameOn) //Ход компьютера
                {
                    overmove = false;
                    PointStatus movestatus;
                    Point shootPoint;
                    do
                    {
                        do
                        {
                            shootPoint = bil.SetMove(this);
                            movestatus = hum.field.SetShoot(shootPoint);
                        } while (movestatus == PointStatus.ILLEGAL);

                        GamePresenter.PrintString("Ход компьютера: " + parser.GetAddress(shootPoint));

                        switch (movestatus)
                        {
                            case PointStatus.AWAY:
                                GamePresenter.PrintString("Мимо!");
                                overmove = true;
                                break;

                            case PointStatus.DECKSUCK:
                                bil.newStrategy();
                                GamePresenter.PrintString("Ранен!");
                                break;

                            case PointStatus.SHIPSUCK:
                                bil.StratOn = false;
                                GamePresenter.PrintString("Потоплен!");
                                break;
                        }

                    } while (!overmove);

                }
            }
        }


    }
}
