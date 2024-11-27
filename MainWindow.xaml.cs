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
using System.Windows.Threading;

namespace CookieClicker
{
    public partial class MainWindow : Window
    {
        // Game variables
        private int cookieCount = 0;
        private int cookiesPerClick = 1;
        private int passiveIncome = 0;

        // Upgrade costs
        private int doubleClickCost = 50;
        private int passiveIncomeCost = 100;
        private int megaClickCost = 500;

        // Timer for passive income
        private readonly DispatcherTimer incomeTimer;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize passive income timer
            incomeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            incomeTimer.Tick += IncomeTimer_Tick;
            incomeTimer.Start();
        }

        private void CookieButton_Click(object sender, RoutedEventArgs e)
        {
            // Increase cookies based on cookies per click
            cookieCount += cookiesPerClick;
            UpdateUI();

        }
       
        private void DoubleClickButton_Click(object sender, RoutedEventArgs e)
        {
            if (cookieCount >= doubleClickCost)
            {
                cookieCount -= doubleClickCost;
                cookiesPerClick *= 2;
                doubleClickCost *= 2; // Increase cost
                DoubleClickButton.Content = $"Double Click (Cost: {doubleClickCost})";
                UpdateUI();
            }
        }

        private void PassiveIncomeButton_Click(object sender, RoutedEventArgs e)
        {
            if (cookieCount >= passiveIncomeCost)
            {
                cookieCount -= passiveIncomeCost;
                passiveIncome += 1;
                passiveIncomeCost = (int)(passiveIncomeCost * 1.5); // Increase cost
                PassiveIncomeButton.Content = $"Passive Income (Cost: {passiveIncomeCost})";
                UpdateUI();
            }
        }

        private void MegaClickButton_Click(object sender, RoutedEventArgs e)
        {
            if (cookieCount >= megaClickCost)
            {
                cookieCount -= megaClickCost;
                cookiesPerClick += 10;
                megaClickCost *= 2; // Increase cost
                MegaClickButton.Content = $"Mega Click (Cost: {megaClickCost})";
                UpdateUI();
            }
        }

        private void IncomeTimer_Tick(object sender, EventArgs e)
        {
            // Add passive income every second
            cookieCount += passiveIncome;
            UpdateUI();
        }

        private void UpdateUI()
        {
            // Update cookie count text
            CookieCountText.Text = $"Cookies: {cookieCount}";
        }
    }
}

