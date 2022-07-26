﻿using IntroSE.Kanban.Frontend.Model;
using IntroSE.Kanban.Frontend.ViewModel;
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
using System.Windows.Shapes;

namespace IntroSE.Kanban.Frontend.View
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private RegisterVM registerViewModel;
              
        public Register(BackendController backendController)
        {
            InitializeComponent();
            this.DataContext = new RegisterVM(backendController);
            this.registerViewModel = (RegisterVM)DataContext;                    
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
                registerViewModel.Register();           
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {            
            Login loginWindow = new Login(registerViewModel.Controller);
            loginWindow.Show();
            this.Close();
        }
    }
}
