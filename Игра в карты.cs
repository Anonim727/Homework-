using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp15
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}

namespace CardGame
{ 
    public interface IPlayerAction
    {
        void MakeMove(Player player, GameState gameState);
    }  
    public class Card
    {
        public int Value { get; private set; } 

        public Card(int value)
        {
            if (value < 1 || value > 36)
                throw new ArgumentException("Value must be between 1 and 36");
            Value = value;
        }
    } 
    public abstract class Token
    {
        public abstract void Execute(Player currentPlayer, Player opponent, GameState gameState);
    }   
    public class SwapLastCardsToken : Token
    {
        public override void Execute(Player currentPlayer, Player opponent, GameState gameState)
        {
            var temp = currentPlayer.LastCards;
            currentPlayer.LastCards = opponent.LastCards;
            opponent.LastCards = temp;
            Console.WriteLine($"{currentPlayer.Name} использовал фишку обмена последними картами");
        }
    }   
    public class SwapAllCardsToken : Token
    {
        public override void Execute(Player currentPlayer, Player opponent, GameState gameState)
        {
            var temp = new List<Card>(currentPlayer.Cards);
            currentPlayer.Cards = opponent.Cards;
            opponent.Cards = temp;        
            currentPlayer.LastCards = new List<Card>(currentPlayer.Cards);
            opponent.LastCards = new List<Card>(opponent.Cards);

            Console.WriteLine($"{currentPlayer.Name} использовал фишку обмена всеми картами");
        }
    }   
    public class Player
    {
        public string Name { get; }
        public List<Card> Cards { get; set; } = new List<Card>();
        public List<Card> LastCards { get; set; } = new List<Card>();
        public int Chips { get; set; } = 2;
        public bool IsActive { get; set; } = true;

        public Player(string name)
        {
            Name = name;
        }

        public int GetScore()
        {
            int sum = 0;
            foreach (var card in Cards)
            {
                sum += card.Value;
            }
            return sum;
        }
    }   
    public class GameState
    {
        public Queue<Card> Deck { get; private set; }
        public Player Player1 { get; }
        public Player Player2 { get; }
        public Player CurrentPlayer { get; set; }
        public Player Opponent => CurrentPlayer == Player1 ? Player2 : Player1;
        public bool IsGameOver { get; set; } = false;

        public GameState(Player p1, Player p2, Queue<Card> deck)
        {
            Player1 = p1;
            Player2 = p2;
            Deck = deck;
        }
    }
    public class PlayerMove : IPlayerAction
    {
        public void MakeMove(Player player, GameState gameState)
        {
            Console.WriteLine($"{player.Name}, ваш ход. Ваша карта: {string.Join(", ", player.Cards)}");
            Console.WriteLine("Выберите действие: 1 - взять карту, 2 - использовать фишку 1, 3 - использовать фишку 2, 4 - закончить ход");
            int choice = int.Parse(Console.ReadLine());
           switch (choice)
            {
                case 1:
                    if (gameState.Deck.Count > 0)
                    {
                        var card = gameState.Deck.Dequeue();
                        player.Cards.Add(card);
                        player.LastCards = new List<Card> { card };
                        Console.WriteLine($"{player.Name} взял карту {card.Value}");
                    }
                    else
                    {
                        Console.WriteLine("Колода пуста");
                    }
                    break;
                case 2:
                    if (player.Chips > 0)
                    {
                        player.Chips--;
                        player.LastCards = new List<Card>(player.Cards);
                        gameState.CurrentPlayer = gameState.Opponent;
                        Console.WriteLine($"{player.Name} использовал фишку обмена последними картами");
                       
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Нет фишек этого типа");
                    }
                    break;
                case 3:
                    if (player.Chips > 0)
                    {
                        player.Chips--;
                        
                        var tempCards = new List<Card>(player.Cards);
                        player.Cards = new List<Card>(gameState.Opponent.Cards);
                        gameState.Opponent.Cards = tempCards;
                        
                        player.LastCards = new List<Card>(player.Cards);
                        gameState.Opponent.LastCards = new List<Card>(gameState.Opponent.Cards);
                        Console.WriteLine($"{player.Name} обменялся всеми картами");
                    }
                    else
                    {
                        Console.WriteLine("Нет фишек этого типа");
                    }
                    break;
                case 4:
                    player.IsActive = false;
                    Console.WriteLine($"{player.Name} закончил ход");
                    break;
                default:
                    Console.WriteLine("Некорректный ввод");
                    break;
            }
        }
    }

   
    public class Game
    {
        Player player1;
         Player player2;
         Queue<Card> deck;
         GameState gameState;
        public event Action<Player, Player> OnGameEnd;
        public Game()
        {           
            var cards = new List<Card>();
            for (int v = 1; v <= 36; v++)
            {
                cards.Add(new Card(v));
            }
            var rnd = new Random();
            for (int i = cards.Count - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                var temp = cards;
                cards = cards;
                cards = temp;
            }
            deck = new Queue<Card>(cards);           
            Console.WriteLine("Введите имя первого игрока:");
            var name1 = Console.ReadLine();
            Console.WriteLine("Введите имя второго игрока:");
            var name2 = Console.ReadLine();
            player1 = new Player(name1);
            player2 = new Player(name2);
            for (int i = 0; i < 5; i++)
            {
                player1.Cards.Add(deck.Dequeue());
                player2.Cards.Add(deck.Dequeue());
            }
            player1.Chips = 2;
            player2.Chips = 2;
            gameState = new GameState(player1, player2, deck)
            {
                CurrentPlayer = player1
            };
        }
        public void Start()
        {
            var move = new PlayerMove();

            while (!gameState.IsGameOver)
            {
                var current = gameState.CurrentPlayer;
                if (current.IsActive)
                {
                    move.MakeMove(current, gameState);
                    CheckGameOver();
                }
                else
                {                
                    gameState.CurrentPlayer = gameState.Opponent;
                }
                              if (current.IsActive)
                {
                    gameState.CurrentPlayer = gameState.Opponent;
                }
            }
            AnnounceWinner();
        }
        private void CheckGameOver()
        {
            if ((!player1.IsActive && !player2.IsActive) || (deck.Count == 0))
            {
                gameState.IsGameOver = true;
            }
        }
        private void AnnounceWinner()
        {
            int score1 = player1.GetScore();
            int score2 = player2.GetScore();

            int diff1 = Math.Abs(60 - score1);
            int diff2 = Math.Abs(60 - score2);

            Console.WriteLine($"{player1.Name} итоговая сумма: {score1}");
            Console.WriteLine($"{player2.Name} итоговая сумма: {score2}");

            if ((score1 <= 60 && score2 > 60) || (score1 <= 60 && diff1 <= diff2))
            {
                Console.WriteLine($"{player1.Name} побеждает!");
            }
            else if ((score2 <= 60 && score1 > 60) || (score2 <= 60 && diff2 < diff1))
            {
                Console.WriteLine($"{player2.Name} побеждает!");
            }
            else
            {
                Console.WriteLine("Ничья!");
            }
        }
    }
class Program
    {
        static void Main()
        {
            var game = new Game();
            game.Start();
        }
    }
}


