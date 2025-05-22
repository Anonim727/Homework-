using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp17
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
interface IPlayer
{
    void TakeCard();
    void UseChip1();
    void UseChip2();
    void StopPlaying();
}

delegate void PlayerActionEventHandler();

class Card
{
    public string Suit { get; set; }
    public string Rank { get; set; }
    public int Value
    {
        get
        {
            if (int.TryParse(Rank, out int num))
                return num;
            else if (Rank == "A")
                return 1;
            else
                return 0;
        }
    }

    public Card(string suit, string rank)
    {
        Suit = suit;
        Rank = rank;
    }
    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}

class Chips
{
    public int Chip1 { get; set; }
    public int Chip2 { get; set; }
    public Chips()
    {
        Chip1 = 2;
        Chip2 = 2;
    }
}


class Player : IPlayer
{
    public string Name { get; set; }
    public List<Card> Hand { get; set; }
    public Chips Chips { get; set; }
    public bool IsPlaying { get; set; }
    public event PlayerActionEventHandler ActionTaken;

    public Player(string name)
    {
        Name = name;
        Hand = new List<Card>();
        Chips = new Chips();
        IsPlaying = true;
    }

    public void TakeCard(Card card)
    {
        if (IsPlaying)
            Hand.Add(card);
    }

    public void UseChip1()
    {
        ActionTaken?.Invoke();
    }

    public void UseChip2()
    {
        ActionTaken?.Invoke();
    }

    public void StopPlaying()
    {
        IsPlaying = false;
        Console.WriteLine($"{Name} has stopped playing.");
    }

    public int GetHandSum()
    {
        int sum = 0;
        foreach (var c in Hand)
            sum += c.Value;
        return sum;
    }

    public void ShowHand()
    {
        Console.WriteLine($"{Name}'s cards: {string.Join(", ", Hand)}");
        Console.WriteLine($"Sum of cards: {GetHandSum()}");
    }
}


class Game
{
     List<Card> deck;
     Player player1;
     Player player2;
     Player currentPlayer;
     Player otherPlayer;
     Random rand = new Random();

    public Game()
    {
        InitializeDeck();
        player1 = new Player("Player 1");
        player2 = new Player("Player 2");
        DealInitialCards(player1);
        DealInitialCards(player2);
        currentPlayer = player1;
        otherPlayer = player2;
        SubscribeActions(player1);
        SubscribeActions(player2);
    }

    
    private void InitializeDeck()
    {
        string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
        string[] ranks = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        deck = new List<Card>();
        foreach (string suit in suits)
        {
            foreach (var rank in ranks)
            {
                deck.Add(new Card(suit, rank));
            }
        }
        MixDeck();
    }

    void MixDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int j = rand.Next(i, deck.Count);
            var temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }
    }

    private void DealInitialCards(Player player)
    {
        for (int i = 0; i < 5; i++)
        {
            player.TakeCard(RemoveTopCard());
        }
    }

    private Card RemoveTopCard()
    {
        if (deck.Count == 0)
            InitializeDeck();
        Card c = deck[0];
        deck.RemoveAt(0);
        return c;
    }

     void SubscribeActions(Player player)
    {
        player.ActionTaken += () =>
        {
            if (!player.IsPlaying)
                return;

            Console.WriteLine($"\n{player.Name}'s turn. Choose action:");
            Console.WriteLine("1) TakeCard");
            Console.WriteLine("2) UseChip1");
            Console.WriteLine("3) UseChip2");
            Console.WriteLine("4) StopPlaying");
            string input = Console.ReadLine();
            switch (input)
            {
                case "TakeCard":
                    player.TakeCard(RemoveTopCard());
                    break;
                case "UseChip1":
                    if (player.Chips.Chip1 > 0)
                    {
                        player.Chips.Chip1--;
                        SwapHands();
                        Console.WriteLine($"{player.Name} used Chip1 and swapped hands.");
                    }
                    else
                        Console.WriteLine("Not enough Chip1.");
                    break;
                case "UseChip2":
                    if (player.Chips.Chip2 > 0 && otherPlayer.Hand.Count > 0)
                    {
                        player.Chips.Chip2--;
                        if (otherPlayer.Hand.Count > 0)
                        {
                            var topCard = otherPlayer.Hand[otherPlayer.Hand.Count - 1];
                            otherPlayer.Hand.RemoveAt(otherPlayer.Hand.Count - 1);
                            player.Hand.Add(topCard);
                            Console.WriteLine($"{player.Name} used Chip2 and took top card from {otherPlayer.Name}.");
                        }
                    }
                    else
                        Console.WriteLine("Cannot use Chip2.");
                    break;
                case "StopPlaying":
                    player.StopPlaying();
                    break;
                default:
                    break;
            }
            DisplayPlayerState(player);
            CheckWinCondition();
            SwitchPlayers();
        };
    }

    private void SwapHands()
    {
        var a = new List<Card>(player1.Hand);
        player1.Hand.Clear();
        player1.Hand.AddRange(player2.Hand);
        player2.Hand.Clear();
        player2.Hand.AddRange(a);
    }

    private void DisplayPlayerState(Player player)
    {
        Console.WriteLine($"\n{player.Name} cards:");
        player.ShowHand();
    }

    private void CheckWinCondition()
    {
        int sum1 = player1.GetHandSum();
        int sum2 = player2.GetHandSum();

        bool p1Over = sum1 > 60;
        bool p2Over = sum2 > 60;

        if (p1Over || p2Over)
        {
            if (p1Over && p2Over)
                Console.WriteLine("Both players exceeded 60. It's a draw!");
            else if (p1Over)
                Console.WriteLine($"{player2.Name} wins! {player1.Name} exceeded 60.");
            else
                Console.WriteLine($"{player1.Name} wins! {player2.Name} exceeded 60.");
            Environment.Exit(0);
        }
        else
        {
            if (sum1 > sum2)
                Console.WriteLine($"{player1.Name} is leading with {sum1} against {sum2}.");
            else if (sum2 > sum1)
                Console.WriteLine($"{player2.Name} is leading with {sum2} against {sum1}.");
            else
                Console.WriteLine($"It's a tie with {sum1}.");
        }
    }

    private void SwitchPlayers()
    {
        var a = currentPlayer;
        currentPlayer = otherPlayer;
        otherPlayer = a;
        if (currentPlayer.IsPlaying)
            RunPlayerTurn(currentPlayer);
    }

    public void Run()
    {
        RunPlayerTurn(currentPlayer);
    }

    private void RunPlayerTurn(Player player)
    {
        if (player.IsPlaying)
        {
            Console.WriteLine($"\n{player.Name}'s turn:");
            Console.WriteLine("Enter action command:");
            string command = Console.ReadLine();
            switch (command)
            {
                case "TakeCard":
                    player.TakeCard(RemoveTopCard());
                    break;
                case "UseChip1":
                    if (player.Chips.Chip1 > 0)
                    {
                        player.Chips.Chip1--;
                        SwapHands();
                        Console.WriteLine($"{player.Name} used Chip1 and swapped hands.");
                    }
                    else
                        Console.WriteLine("Not enough Chip1.");
                    break;
                case "UseChip2":
                    if (player.Chips.Chip2 > 0 && otherPlayer.Hand.Count > 0)
                    {
                        player.Chips.Chip2--;
                        var topCard = otherPlayer.Hand[otherPlayer.Hand.Count - 1];
                        otherPlayer.Hand.RemoveAt(otherPlayer.Hand.Count - 1);
                        player.Hand.Add(topCard);
                        Console.WriteLine($"{player.Name} used Chip2 and took top card from {otherPlayer.Name}.");
                    }
                    else
                        Console.WriteLine("Cannot use Chip2.");
                    break;
                case "StopPlaying":
                    player.StopPlaying();
                    break;
                default:
                    Console.WriteLine("Invalid command, turn skipped.");
                    break;
            }
            DisplayPlayerState(player);
            CheckWinCondition();
            SwitchPlayers();
        }
    }
}


class Program
{
    static void Main()
    {
        Game game = new Game();
        game.Run();
        Console.WriteLine("Game over.");
    }
}
