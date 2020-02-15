using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace OldMaidGUI
{
    public class Hand : System.Windows.Controls.Canvas
    {
        public List<ImageCard> pair { get; } = new List<ImageCard>();
        public void UpdateCards(OldMaid.OldMaidDeck deck, double width, double height, double upAmount)
        {
            Children.Clear();
            Width = width;
            Height = height;
            ((StackPanel)Parent).Width = width;
            double halfCards = deck.Count + 1;
            double halfCardWidth = width / halfCards;
            for (int i = 0; i < deck.Count; i++)
            {
                ImageCard card = new ImageCard(deck[i], halfCardWidth * 2, height, upAmount);
                if (i != 0)
                {
                    SetLeft(card, i * halfCardWidth);
                }
                Children.Add(card);
            }
        }
    }
}
