using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OldMaidGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static double cardHeightRatio = 6;
        public static double cardWidthRatio = 8;
        public static double cardHeight = 100;
        public static double handWidth = 0;
        public static double handHeight = 0;
        public static double margin = 10;
        static Random rnd = new Random();
        public List<Hand> hands = new List<Hand>();
        public OldMaid.OldMaidGame game;
        public StackPanel GetPlayer(string playerName)
        {
            return Players.Children.OfType<StackPanel>().Single(player => player.Name == playerName);
        }
        public Canvas GetPlayerHand(StackPanel player)
        {
            return player.Children.OfType<Canvas>().Single();
        }
        public Canvas GetPlayerHand(string playerName)
        {
            return GetPlayerHand(GetPlayer(playerName));
        }
        public void UpdateCards(Hand playerHand, OldMaid.OldMaidDeck cards)
        {
            playerHand.UpdateCards(cards, handWidth, cardHeight, 10);
        }
        public MainWindow()
        {
            InitializeComponent();
            List<OldMaid.Player> players = new List<OldMaid.Player>
            {
                new OldMaid.Player(0),
                new OldMaid.AIPlayer(1),
            };
            game = new OldMaid.OldMaidGame(players);
            game.Start();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.Visibility = Visibility.Hidden;
            Players.Visibility = Visibility.Visible;
        }

        public void Update()
        {
            UpdateCards(HumanHand, game.Players[0].hand);
            UpdateCards(AIHand, game.Players[1].hand);
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            cardHeight = (int)(e.NewSize.Height / cardHeightRatio);
            handWidth = e.NewSize.Width * 0.8;
            handHeight = e.NewSize.Height * 0.3;
            Update();
        }
    }
}
