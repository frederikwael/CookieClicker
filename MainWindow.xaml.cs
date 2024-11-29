using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CookieClicker
{
    public partial class MainWindow : Window
    {
        // Game variables
        private int cookieCount = 0;
        private int cookiesPerClick = 1;
        private int passiveIncome = 0;
        private int level = 1;

        // Upgrade costs
        private int doubleClickCost = 50;
        private int passiveIncomeCost = 100;
        private int megaClickCost = 500;

        // Timer for passive income
        private readonly DispatcherTimer incomeTimer;

        // Passive income enabled flag
        private bool passiveIncomeEnabled = false;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize passive income timer but keep it stopped initially
            incomeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            incomeTimer.Tick += IncomeTimer_Tick;
        }

        // Passive income logic (called every second)
        private void IncomeTimer_Tick(object sender, EventArgs e)
        {
            cookieCount += passiveIncome;
            CheckLevelUp(); // Check for level-up condition
            UpdateUI();
        }

        // Update all UI elements
        private void UpdateUI()
        {
            CookieCountText.Text = $"Cookies: {cookieCount}";
            DoubleClickButton.Content = $"Double Click (Cost: {doubleClickCost})";
            PassiveIncomeButton.Content = $"Passive Income (Cost: {passiveIncomeCost})";
            MegaClickButton.Content = $"Mega Click (Cost: {megaClickCost})";
            LevelText.Text = $"Level: {level}"; // Update level display
        }

        // Button to increment cookies
        private void CookieButton_Click(object sender, RoutedEventArgs e)
        {
            cookieCount += cookiesPerClick;
            CheckLevelUp(); // Check for level-up condition
            UpdateUI();
        }

        // Check for level-up condition
        private void CheckLevelUp()
        {
            // Increase level for every 1000 cookies earned
            int newLevel = (cookieCount / 1000) + 1;
            if (newLevel > level)
            {
                level = newLevel;
                AddToEventLog($"Congratulations! You've reached Level {level}!");
            }
        }

        // Double click upgrade logic
        private void DoubleClickButton_Click(object sender, RoutedEventArgs e)
        {
            if (cookieCount >= doubleClickCost)
            {
                cookieCount -= doubleClickCost;
                cookiesPerClick *= 2;
                doubleClickCost *= 2; // Increase cost for the next purchase
                AddToEventLog("You bought Double Click!");
                UpdateUI();
            }
            else
            {
                AddToEventLog("Not enough cookies for Double Click.");
            }
        }

        // Passive income upgrade logic
        private void PassiveIncomeButton_Click(object sender, RoutedEventArgs e)
        {
            if (cookieCount >= passiveIncomeCost)
            {
                cookieCount -= passiveIncomeCost;
                passiveIncome += 1; // Increment passive income

                if (!passiveIncomeEnabled)
                {
                    // Start passive income only after the first purchase
                    incomeTimer.Start();
                    passiveIncomeEnabled = true;
                }

                passiveIncomeCost = (int)(passiveIncomeCost * 1.5); // Increase cost for the next purchase
                AddToEventLog("You bought Passive Income!");
                UpdateUI();
            }
            else
            {
                AddToEventLog("Not enough cookies for Passive Income.");
            }
        }

        // Mega click upgrade logic
        private void MegaClickButton_Click(object sender, RoutedEventArgs e)
        {
            if (cookieCount >= megaClickCost)
            {
                cookieCount -= megaClickCost;
                cookiesPerClick += 10; // Add 10 cookies per click
                megaClickCost *= 2; // Increase cost for the next purchase
                AddToEventLog("You bought Mega Click!");
                UpdateUI();
            }
            else
            {
                AddToEventLog("Not enough cookies for Mega Click.");
            }
        }

        // Log events to the EventLog control
        private void AddToEventLog(string message)
        {
            EventLog.Items.Add($"{DateTime.Now:T}: {message}");
            EventLog.ScrollIntoView(EventLog.Items[EventLog.Items.Count - 1]); // Auto-scroll to the latest log
        }
    }
}
