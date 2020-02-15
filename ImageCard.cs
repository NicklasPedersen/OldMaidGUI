using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;
using System.Diagnostics;

namespace OldMaidGUI
{
    public class ImageCard : System.Windows.Controls.Image
    {
        public OldMaid.OldMaidCard Card { get; }
        private bool isLifted;
        private double upAmount;
        public ImageCard(OldMaid.OldMaidCard card, double width, double height, double upAmount)
        {
            Card = card;
            Source = new BitmapImage(new Uri("C:/Users/nick8186/source/repos/OldMaidGUI/Cards/" + card.Value + ".png"));
            MouseEnter += OnMouseEnter;
            MouseLeave += OnMouseLeave;
            MouseDown += OnMouseClick;
            Stretch = Stretch.Fill;
            Width = width;
            Height = height;
            this.upAmount = -upAmount;
        }
        void OnMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Canvas.SetTop(this, upAmount);
        }
        // Puts the card the card back down unless it is targeted for pairing
        void OnMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!isLifted)
            {
                Canvas.SetTop(this, 0);
            }
        }
        // Lifts card for pairs on the players hand except if it's a "cat" card
        void OnMouseClick(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Card.Value == "cat") return; // If the card is "cat" it cannot be a pair and thusly must not be lifted

            if (Parent is Hand)
            {
                Hand playerHand = (Hand)Parent;
                if (playerHand.Name == "HumanHand")
                {
                    if (!isLifted)
                    {
                        if (playerHand.pair.Count < 2)
                        {
                            Canvas.SetTop(this, upAmount);
                            playerHand.pair.Add(this);
                            Debug.WriteLine(this + "Added");
                            isLifted = true;
                        }
                    }
                    else
                    {
                        if (playerHand.pair.Contains(this))
                        {
                            playerHand.pair.Remove(this);
                            Debug.WriteLine(this + "removed");
                            isLifted = false;
                        }
                    }
                }
            }
        }
        public override string ToString()
        {
            return Card.Value;
        }
    }
}
